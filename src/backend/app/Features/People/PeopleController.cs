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
        public IActionResult GetPeople()
        {
            return Ok(new { Message = "List of people" });
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
        public IActionResult CreatePerson([FromBody] string name)
        {
            return CreatedAtAction(nameof(GetPerson), new { id = Guid.NewGuid() }, new { Name = name });
        }

        // PUT: api/people/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody] string name)
        {
            return Ok(new { Message = $"Updated person with ID {id} to name {name}" });
        }

        // DELETE: api/people/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePerson(PersonId id)
        {
            return NoContent();
        }
    }
}
