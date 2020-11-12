using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class InscripcionController : ControllerBase
    {

        private readonly InscripcionRepo _repository;

        public InscripcionController(InscripcionRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/inscripcion/enespera")]
        public IActionResult verEnEspera([FromQuery] string usuario)
        {
            var resultado = _repository.verInscripcionesEspera(usuario);

            if(resultado == null)
            {
                return BadRequest();
            }

            return Ok(resultado);

        }

        [HttpGet]
        [Route("api/inscripcion/carrera/enespera")]
        public IActionResult verCarreraEnEspera([FromQuery] string nombreCarrera, [FromQuery] string usuario)
        {
            var resultado = _repository.verInscripcionesEsperaCarrera(nombreCarrera, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }

            return Ok(resultado);

        }

        [HttpPost]
        [Route("api/inscripcion/new")]
        public IActionResult nuevaInscripcion([FromBody] Inscripcion inscripcion, [FromQuery] string nombreCarrera,
            [FromQuery] string adminCarrera)
        {
            if (ModelState.IsValid)
            {
                var resultado =_repository.Create(inscripcion, nombreCarrera, adminCarrera);
                if (resultado)
                {
                    if(_repository.SaveChanges())
                        return Ok("Inscripcion creada correctamente");
                    return BadRequest("Ya hay una inscripcion para esa carrera en espera");
                }
                BadRequest("Revisar la categoría del deportista");
            }

            return BadRequest(ModelState);

        }

        [HttpPost]
        [Route("api/inscripcion/accept")]
        public IActionResult nuevaInscripcion([FromBody] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                _repository.aceptarInscripcion(inscripcion);
                if (_repository.SaveChanges())
                    return Ok("Inscripcion aceptada correctamente");
                return BadRequest("Ha ocurrido un error");
            }

            return BadRequest(ModelState);

        }

        [HttpPut]
        [Route("api/inscripcion/edit")]
        public IActionResult actualizarInscripcion([FromBody] Inscripcion inscripcion, [FromQuery] string usuario)
        {
            if (inscripcion.Usuariodeportista != usuario)
            {
                return BadRequest();
            }

            _repository.Update(inscripcion);
            _repository.SaveChanges();
            return Ok("Inscripcion actualizada correctamente");
        }

        [HttpDelete]
        [Route("api/inscripcion/delete")]
        public IActionResult actualizarInscripcion([FromBody] Inscripcion inscripcion)
        {
            _repository.Delete(inscripcion);
            _repository.SaveChanges();
            return Ok("Inscripción eliminada correctamente");
        }

    }
}
