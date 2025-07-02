using app.Domain;
using app.Domain.Types;
using app.Setup.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace app.Features.Organizations;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController(IDispatcher dispatcher) : ControllerBase
{
    // GET: api/organizations
    [HttpGet]
    public async Task<IActionResult> GetOrganizations([FromQuery] GetOrganizationsQuery query, CancellationToken cancellationToken)
    {
        var organizations = await dispatcher.Query<GetOrganizationsQuery, IEnumerable<Organization>>(query, cancellationToken);
        return Ok(organizations);
    }
    
    // GET: api/organizations/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganization(OrganizationId id, CancellationToken cancellationToken)
    {
        var organization = await dispatcher.Query<GetOrganizationQuery, Organization?>(new GetOrganizationQuery { Id = id }, cancellationToken);

        if (organization == null)
            return NotFound(new { Message = $"Organization with ID {id} not found" });
            
        return Ok(organization);
    }

    // POST: api/organizations
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody]CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        if (command.Name.Equals(Name.Unspecified))
            return BadRequest(new { Message = "Name is required" });

        var id = await dispatcher.Send<CreateOrganizationCommand, OrganizationId>(command, cancellationToken);
        return Ok(new { Message = $"Created organization with ID {id}", Id = id });
    }

    // PUT: api/organizations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(OrganizationId id, [FromBody] Name name, CancellationToken cancellationToken)
    {
        if (name.Equals(Name.Unspecified))
            return BadRequest(new { Message = "Name is required" });

        if (id.Equals(OrganizationId.Empty))
            return BadRequest(new { Message = "ID is required" });

        await dispatcher.Send(new UpdateOrganizationCommand { Id = id, Name = name }, cancellationToken);
        return Ok(new { Message = $"Updated organization with ID {id} to name {name}" });
    }

    // DELETE: api/organizations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(OrganizationId id, CancellationToken cancellationToken)
    {
        if (id.Equals(OrganizationId.Empty))
            return BadRequest(new { Message = "ID is required" });

        await dispatcher.Send(new DeleteOrganizationCommand { Id = id }, cancellationToken);
        return Ok(new { Message = $"Deleted organization with ID {id}" });
    }
}
