using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaRepo _repository;

        //se inyecta el repositorio correspondiente
        public CategoriaController(CategoriaRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para acceder a todas las categorías de la base de datos
        /// </summary>
        /// <returns>Un ok con la lista de las categorías</returns>
        [HttpGet]
        [Route("api/categoria")]
        public IActionResult GetCategorias()
        {
            var resultado = _repository.obtenerTodas();

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }
    }
}
