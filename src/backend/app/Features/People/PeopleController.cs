using app.Domain;
using app.Domain.Types;
using app.Setup.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace app.Features.People
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController(IDispatcher dispatcher) : ControllerBase
    {
        // GET: api/people
        [HttpGet]
        public async Task<IActionResult> GetPeople([FromQuery] GetPeopleQuery query, CancellationToken cancellationToken)
        {
            var people = await dispatcher.Query<GetPeopleQuery, IEnumerable<Person>>(query, cancellationToken);
            return Ok(people);
        }
        
        // GET: api/people/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(PersonId id, CancellationToken cancellationToken)
        {
            var person = await dispatcher.Query<GetPersonQuery, Person?>(new GetPersonQuery { Id = id }, cancellationToken);

            if (person == null)
                return NotFound(new { Message = $"Person with ID {id} not found" });
            
            return Ok(person);
        }

        // POST: api/people
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody]CreatePersonCommand command, CancellationToken cancellationToken)
        {
            if (command.Name.Equals(Name.Unspecified))
                return BadRequest(new { Message = "Name is required" });

            var id = await dispatcher.Send<CreatePersonCommand, PersonId>(command, cancellationToken);
            return Ok(new { Message = $"Created person with ID {id}", Id = id });
        }

        // PUT: api/people/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(PersonId id, [FromBody] Name name, CancellationToken cancellationToken)
        {
            if (name.Equals(Name.Unspecified))
                return BadRequest(new { Message = "Name is required" });

            if (id.Equals(PersonId.Empty))
                return BadRequest(new { Message = "ID is required" });

            await dispatcher.Send(new UpdatePersonCommand { Id = id, Name = name }, cancellationToken);
            return Ok(new { Message = $"Updated person with ID {id} to name {name}" });
        }

        // DELETE: api/people/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(PersonId id, CancellationToken cancellationToken)
        {
            await dispatcher.Send(new DeletePersonCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}
