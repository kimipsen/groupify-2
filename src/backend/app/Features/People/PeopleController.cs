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
        public async Task<IActionResult> GetPeople([FromBody] GetPeopleQuery query, CancellationToken cancellationToken)
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
            if (command.Name == Name.Unspecified)
                return BadRequest(new { Message = "Name is required" });

            await dispatcher.Send(command, cancellationToken);
            return NoContent();
        }

        // PUT: api/people/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePerson(PersonId id, [FromBody] Name name)
        {
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
