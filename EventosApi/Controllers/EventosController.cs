using EventosApi.BL.Interfaces;
using EventosApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController(IEventoService service) : ControllerBase
    {
        // GET: api/eventos
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventoDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetEventosAsync();
            return Ok(result);
        }

        // GET api/eventos/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetEventoByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        // POST api/eventos
        [HttpPost]
        [ProducesResponseType(typeof(EventoDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] EventoDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.InsertEventoAsync(model);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/eventos/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EventoDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] EventoDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.UpdateEventoAsync(id, model);
            return result != null ? Ok(result) : NotFound();
        }

        // DELETE api/eventos/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteEventoAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
