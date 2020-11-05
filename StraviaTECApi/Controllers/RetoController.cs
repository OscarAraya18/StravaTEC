using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class RetoController : ControllerBase
    {
        private readonly RetoRepo _repository;

        public RetoController(RetoRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/reto/misretos")]
        public IActionResult getReto([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosAdministrados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/reto/estado")]
        public IActionResult getEstadoRetos([FromQuery] string usuario)
        {
            var resultado = _repository.verEstadoRetos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpPost]
        [Route("api/reto/new")]
        public IActionResult nuevoReto([FromBody] Reto reto)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(reto);
                _repository.SaveChanges();
                return Ok("Reto creado correctamente");
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("api/reto/edit")]
        public IActionResult actualizarReto([FromBody] Reto reto, [FromQuery] string usuarioAdmin)
        {
            if (reto.Admindeportista != usuarioAdmin)
            {
                return BadRequest();
            }

            _repository.Update(reto);
            _repository.SaveChanges();
            return Ok("Reto actualizado correctamente");
        }

        [HttpDelete]
        [Route("api/reto/delete")]
        public IActionResult eliminarCarrera([FromQuery] string nombre)
        {
            _repository.Delete(nombre);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");   
        }
    }
}
