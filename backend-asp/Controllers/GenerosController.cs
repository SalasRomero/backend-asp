using backend_asp.Entidades;
using backend_asp.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_asp.Controllers
{
    [Route("api/generos")] //Aqui definimos el nombre de la ruta. Tambien se puede usar [controller]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IRepositorio _repositorio;
        private readonly ILogger _logger;
        public GenerosController(IRepositorio repositorio, ILogger<GenerosController> logger) {
            this._repositorio = repositorio;
            this._logger = logger;
        }

        [HttpGet] // api/generos //Definimos el metodo que se usa
        [HttpGet("listado")] // api/generos/listado
        [HttpGet("/listadogeneros")] // /listadogeneros
        public ActionResult<List<Genero>> Get() {

            _logger.LogInformation("Vamos a mostrar los generos");
            return this._repositorio.ObtenerTodosLosGeneros();
        }

        [HttpGet("guid")]
        public ActionResult<Guid> GetGui()
        {
            return this._repositorio.ObtenerGuid();
        }



        //[HttpGet("ejemplo")]
        //[HttpGet("{Id:int}/{nombre=David}")] // /api/generos/1/David
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Genero>> Get(int Id,[FromHeader]string nombre)
        {

            _logger.LogDebug($"Obteniendo un género por el id {Id}");

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var genero = await this._repositorio.ObtenerPorId(Id);

            if (genero == null) {
                _logger.LogWarning($"No pudimos encontrar el género del id {Id}");
                return NotFound();
            }

            return genero;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {

            this._repositorio.CrearGenero(genero);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
        { 

            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete() { 
            return NoContent();
        }
    }
}
