    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class PatrocinadorController : ControllerBase
    {
        private readonly PatrocinadorRepo _repository;

        //Se inyecta el repositorio correspondiente
        public PatrocinadorController(PatrocinadorRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para obtener todos los patrocinadores de la base de datos
        /// </summary>
        /// <returns>Un ok con el resultado</returns>
        [HttpGet]
        [Route("api/patrocinadores")]
        public IActionResult GetPatrocinadores()
        {
            var resultado = _repository.obtenerTodos();

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

    }
}
