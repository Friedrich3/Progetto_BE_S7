using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Progetto_BE_S7.DTOs.Artista;
using Progetto_BE_S7.Services;

namespace Progetto_BE_S7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistaController : ControllerBase
    {
        private readonly ArtistaServices _artistaServices;
        public ArtistaController(ArtistaServices artistaServices)
        {
            _artistaServices = artistaServices;
        }

        [Authorize(Roles ="Amministratore")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ArtistaCreateRequestDto artistaCreateRequest)
        {
            try
            {
                var result = await _artistaServices.Create(artistaCreateRequest);
                if (!result)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                return Ok(new { message ="Artista aggiunto correttamente!"});
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Errore interno del sistema");
            }
        }
        [Authorize(Roles = "Amministratore")]
        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArtistaUpdateRequestDto artistaUpdateRequest)
        {
            try
            {
                var result = await _artistaServices.Update(id, artistaUpdateRequest);
                if (!result)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                return Ok(new { message = "Artista aggiornato correttamente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }

        [Authorize(Roles = "Amministratore")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _artistaServices.Delete(id);
                if (!result)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                return Ok(new { message = "Artista eliminato correttamente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }
        }

        [Authorize(Roles = "Amministratore")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _artistaServices.GetAllArtists();
                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                if (!result.Any())
                {
                    return NoContent();
                }

                return Ok(new { message = $" Risultati trovati: {result.Count()}" , artisti = result});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }

        }
        [Authorize(Roles = "Amministratore")]
        [HttpGet("artist")]
        public async Task<IActionResult> GetAll([FromQuery] int artistid)
        {
            try
            {
                var result = await _artistaServices.GetArtist(artistid);
                if (result == null)
                {
                    return BadRequest(new { message = "Ops qualcosa e' andato storto" });
                }
                return Ok(new { message = "Artista trovato con successo", artisti = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Errore interno del sistema");
            }

        }












    }
}
