using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
