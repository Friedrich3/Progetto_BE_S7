using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Progetto_BE_S7.DTOs.Biglietto;
using Progetto_BE_S7.Services;

namespace Progetto_BE_S7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BigliettoController : ControllerBase
    {
        private readonly BigliettoServices _bigliettoServices;
        public BigliettoController(BigliettoServices bigliettoServices)
        {
            _bigliettoServices = bigliettoServices;
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("allTickets")]
        public async Task<IActionResult> AllTickets()
        {
            try
            {
                var result = await _bigliettoServices.GetAllTickets();
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

        //[Authorize(Roles = "Amministratore")]
        //[HttpGet("TicketsBy")]
        //public async Task<IActionResult> TicketsBy([FromQuery]int eventId)
        //{



        //}

        [Authorize]
        [HttpPost("buyTicket")]
        public async Task<IActionResult> BuyTicket([FromBody] AcquistoBigliettoDto acquistoBiglietto)
        {
            var user = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value;
            var result = await _bigliettoServices.BuyNew(acquistoBiglietto, user);
            if (!result)
            {
                return BadRequest(new { message = "Ops, qualcosa e' andato storto" });
            };
            return Ok(new { message = "Biglietto Acquistato Correttamente" });
        }

        [Authorize]
        [HttpGet("myTickets")]
        public async Task<IActionResult> UserTickets()
        {
            try
            {
                var user = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email).Value;
                var result = await _bigliettoServices.UserTickets(user);
                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                if (!result.Any())
                {
                    return NoContent();
                }
                return Ok(new { message = $" Risultati trovati: {result.Count()}", tickets = result });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }


    }

}

