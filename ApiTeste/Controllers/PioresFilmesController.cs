using ApiTeste.Models;
using ApiTeste.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PioresFilmesController : ControllerBase
    {
        private readonly Context _context; 
        private readonly IPioresFilmesService _pioresFilmesService;

        public PioresFilmesController(IPioresFilmesService pioresFilmesService ,Context context)
        {
            _pioresFilmesService = pioresFilmesService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMinMaxInterval()
        {
           return Ok(_pioresFilmesService.GetMinMaxInterval());       
        }
       
    }
}