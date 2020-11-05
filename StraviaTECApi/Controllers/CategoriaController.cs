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
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaRepo _repository;

        public CategoriaController(CategoriaRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/categoria")]
        public IActionResult getCategoria()
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
