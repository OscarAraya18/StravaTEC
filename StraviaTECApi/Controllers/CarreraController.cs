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


        [HttpPost]
        [Route("api/carrera/admin/new")]
        public IActionResult nuevaCarrera([FromBody] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(carrera);
                _repository.SaveChanges();
                return Ok("Carrera creado correctamente");
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
            return Ok("Carrera actualizado correctamente");
        }

        [HttpDelete]
        [Route("api/carrera/admin/delete")]
        public IActionResult eliminarCarrera([FromQuery] string nombre)
        {
            _repository.Delete(nombre);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");
        }

    }
}
