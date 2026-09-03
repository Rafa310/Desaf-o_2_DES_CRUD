using EventosApi.BL.Interfaces;
using EventosApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizadoresController(IOrganizadorService service) : ControllerBase
    {
        // GET: api/organizadores
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrganizadorDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetOrganizadoresAsync();
            return Ok(result);
        }

        // GET api/organizadores/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizadorDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetOrganizadorByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        // POST api/organizadores
        [HttpPost]
        [ProducesResponseType(typeof(OrganizadorDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] OrganizadorDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.InsertOrganizadorAsync(model);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/organizadores/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrganizadorDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] OrganizadorDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.UpdateOrganizadorAsync(id, model);
            return result != null ? Ok(result) : NotFound();
        }

        // DELETE api/organizadores/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteOrganizadorAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
