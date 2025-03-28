using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
