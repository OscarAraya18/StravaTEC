    using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;


namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly CarreraRepo _repository;
        public CarreraController(CarreraRepo repo)
        {
            _repository = repo;
        }


        [HttpGet]
        [Route("api/carrera/user/carrerasDisponibles")]
        public IActionResult getCarrerasNoInscritas([FromQuery] string usuario)
        {
            var resultado = _repository.verCarrerasDisponibles(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }

            return Ok(resultado);
        }


        [HttpGet]
        [Route("api/carrera/admin/miscarreras")]
        public IActionResult getCarrera([FromQuery] string usuario)
        {
            var resultado = _repository.verMisCarreras(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/carrera/admin/verCarrera")]
        public IActionResult GetCarreraAdministradaPorNombre([FromQuery] string usuario, [FromQuery] string nombreCarrera)
        {
            var resultado = _repository.verCarreraAdministradaPorNombre(usuario, nombreCarrera);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/carrera/user/buscarCarrera")]
        public IActionResult GetCarreraPorNombre([FromQuery] string usuario, [FromQuery] string nombreCarrera)
        {
            var resultado = _repository.buscarCarreraPorNombre(usuario, nombreCarrera);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/carrera/user/carrerasConPosiciones")]
        public IActionResult GetCarrerasConPosiciones([FromQuery] string usuario)
        {
            var resultado = _repository.carrerasConPosiciones(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpPost]
        [Route("api/carrera/admin/new")]
        public IActionResult nuevaCarrera([FromBody] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(carrera);
                _repository.SaveChanges();
                return Ok("Carrera creada correctamente");
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("api/carrera/admin/edit")]
        public IActionResult actualizarCarrera([FromBody] Carrera carrera, [FromQuery] string usuarioAdmin)
        {
            if (carrera.Admindeportista != usuarioAdmin)
            {
                return BadRequest();
            }

            _repository.Update(carrera);
            _repository.SaveChanges();
            return Ok("Carrera actualizada correctamente");
        }

        [HttpDelete]
        [Route("api/carrera/admin/delete")]
        public IActionResult eliminarCarrera([FromQuery] string nombreCarrera, [FromQuery] string usuario)
        {
            _repository.Delete(nombreCarrera, usuario);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");
        }

    }
}
