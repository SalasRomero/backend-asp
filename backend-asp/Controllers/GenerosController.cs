using backend_asp.Entidades;
using backend_asp.Filtros;
using backend_asp.Repositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController : ControllerBase
    {
        //private readonly IRepositorio _repositorio;
        private readonly ILogger _logger;
        public GenerosController(ILogger<GenerosController> logger) {
            //this._repositorio = repositorio;
            this._logger = logger;
        }

        [HttpGet] // api/generos //Definimos el metodo que se usa
        //[HttpGet("listado")] // api/generos/listado
        //[HttpGet("/listadogeneros")] // /listadogeneros
        //Este filtro no funciona cuando se usa la cabecera autorization
        //[ResponseCache(Duration=60)] // se agrega el filtro
        // [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // con esto decimos que nos deben de pasar un token para entrar
        //[ServiceFilter(typeof(MiFiltroDeAccion))] //Permite inicializar el filtro
        public ActionResult<List<Genero>> Get() 
        {

            //_logger.LogInformation("Vamos a mostrar los generos");
            //return this._repositorio.ObtenerTodosLosGeneros();
            return new List<Genero> { new Genero { Id=1,Nombre="Comedia"} };
        }

        //[HttpGet("guid")]
        //public ActionResult<Guid> GetGui()
        //{
        //    return this._repositorio.ObtenerGuid();
        //}



        //[HttpGet("ejemplo")]
        //[HttpGet("{Id:int}/{nombre=David}")] // /api/generos/1/David
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Genero>> Get(int Id)
        {

            //_logger.LogDebug($"Obteniendo un género por el id {Id}");

            //if (!ModelState.IsValid) {
            //    return BadRequest(ModelState);
            //}

            //var genero = await this._repositorio.ObtenerPorId(Id);

            //if (genero == null) {
            //    //throw new ApplicationException($"El género de ID {Id} no fue encontrado");
            //    _logger.LogWarning($"No pudimos encontrar el género del id {Id}");
            //    return NotFound();
            //}

            //return genero;

            throw new NotImplementedException();

        }

        [HttpPost]
        public ActionResult Post([FromBody] Genero genero)
        {

            throw new NotImplementedException();

        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
        {

            throw new NotImplementedException();

        }

        [HttpDelete]
        public ActionResult Delete() {
            throw new NotImplementedException();

        }
    }
}
