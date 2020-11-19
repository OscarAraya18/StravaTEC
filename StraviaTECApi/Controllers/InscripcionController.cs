using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly InscripcionRepo _repository;

        // se inyecta el repositorio correspondiente
        public InscripcionController(InscripcionRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para acceder a las inscripciones en espera que tiene un deportista
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/inscripcion/enespera")]
        public IActionResult VerEnEspera([FromQuery] string usuario)
        {
            var resultado = _repository.verInscripcionesEspera(usuario);

            if(resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para ver las peticiones en espera que tiene
        /// una carrera en específico
        /// </summary>
        /// <param name="nombreCarrera">el nombre de la carrera a consultar</param>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/inscripcion/carrera/enespera")]
        public IActionResult VerCarreraEnEspera([FromQuery] string nombreCarrera, [FromQuery] string usuario)
        {
            var resultado = _repository.verInscripcionesEsperaCarrera(nombreCarrera, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para crear una nueva inscripción
        /// </summary>
        /// <param name="inscripcion">El objeto inscripción con la información correspondiente</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/inscripcion/new")]
        public IActionResult NuevaInscripcion([FromBody] InscripcionParser inscripcion)
        {
            if (ModelState.IsValid)
            {
                var resultado =_repository.Create(inscripcion);
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

        /// <summary>
        /// Petición para aceptar una inscripción
        /// </summary>
        /// <param name="inscripcion">La inscripción a aceptar</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/inscripcion/accept")]
        public IActionResult AceptarInscripcion([FromBody] Inscripcion inscripcion)
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

        /// <summary>
        /// Petición para actualizar una inscripción
        /// </summary>
        /// <param name="inscripcion">La inscripción actualizada</param>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPut]
        [Route("api/inscripcion/edit")]
        public IActionResult ActualizarInscripcion([FromBody] InscripcionParser inscripcion, [FromQuery] string usuario)
        {
            if (inscripcion.Usuariodeportista != usuario)
            {
                return BadRequest();
            }

            _repository.Update(inscripcion);
            _repository.SaveChanges();
            return Ok("Inscripcion actualizada correctamente");
        }

        /// <summary>
        /// Petición para eliminar/denegar una inscripción
        /// </summary>
        /// <param name="adminCarrera">administrador de la carrera a inscribir</param>
        /// <param name="nombreCarrera">el nombre de la carrera a inscribir</param>
        /// <param name="usuario">el usuario que realizó la inscripción</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpDelete]
        [Route("api/inscripcion/delete")]
        public IActionResult DenegarInscripcion([FromQuery] string adminCarrera, [FromQuery] string nombreCarrera, 
            [FromQuery] string usuario)
        {
            _repository.Delete(nombreCarrera, adminCarrera, usuario);
            _repository.SaveChanges();
            return Ok("Inscripción eliminada correctamente");
        }
    }
}
