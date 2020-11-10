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

        public PatrocinadorController(PatrocinadorRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/patrocinadores")]
        public IActionResult getPatrocinadores()
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
