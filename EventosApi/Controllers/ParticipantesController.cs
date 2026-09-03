using EventosApi.BL.Interfaces;
using EventosApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EventosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController(IParticipanteService service) : ControllerBase
    {
        // GET: api/participantes
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ParticipanteDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var result = await service.GetParticipantesAsync();
            return Ok(result);
        }

        // GET api/participantes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ParticipanteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await service.GetParticipanteByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        // POST api/participantes
        [HttpPost]
        [ProducesResponseType(typeof(ParticipanteDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] ParticipanteDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.InsertParticipanteAsync(model);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/participantes/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ParticipanteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] ParticipanteDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await service.UpdateParticipanteAsync(id, model);
            return result != null ? Ok(result) : NotFound();
        }

        // DELETE api/participantes/5
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteParticipanteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
