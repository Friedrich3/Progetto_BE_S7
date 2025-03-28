using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progetto_BE_S7.DTOs.Evento;
using Progetto_BE_S7.Services;

namespace Progetto_BE_S7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoServices _eventoServices;
        public EventoController(EventoServices eventoServices)
        {
            _eventoServices = eventoServices;
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllEvents()
        {
            try
            {
                var result = await _eventoServices.GetallEvents();
                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                if (!result.Any())
                {
                    return NoContent();
                }
                return Ok(new { message = $" Risultati trovati: {result.Count()}", eventi = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("event")]
        public async Task<IActionResult> GetSingleEvent([FromQuery] int eventid)
        {
            try
            {
                var result = await _eventoServices.GetEvent(eventid);
                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                return Ok(new { message ="Evento trovato con sucesso", evento = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }

        [Authorize(Roles = "Amministratore")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvento([FromBody] CreateEventoRequestDto createEventoRequest )
        {
            try
            {
            var result = await _eventoServices.CreateNew(createEventoRequest);
            if (!result)
            {
                return BadRequest(new { message = "Ops, qualcosa e' andato storto" });
            }
            return Ok(new {message="Evento creato Correttamente!"});
            }
            catch
            {
                return StatusCode(500, "Errore interno del sistema");
            }


        }

        [Authorize(Roles = "Amministratore")]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] UpdateEventoRequestDto updateEventoRequest)
        {
            try
            {
                var result = await _eventoServices.Update(id, updateEventoRequest);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, qualcosa e' andato storto" });
                }
                return Ok(new { message = "Evento aggiornato Correttamente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                var result = await _eventoServices.Delete(id);
                if (!result)
                {
                    return BadRequest(new { message = "Ops, qualcosa e' andato storto" });
                }
                return Ok(new { message = "Evento eliminato Correttamente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }










    }
}
