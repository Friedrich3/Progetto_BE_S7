using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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



    }
}
