using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class RetoController : ControllerBase
    {
        private readonly RetoRepo _repository;

        // Se inyecta el repositorio correspondiente
        public RetoController(RetoRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para acceder a los retos administrados por un deportista
        /// </summary>
        /// <param name="usuario">el usuario que realiza la petición</param>
        /// <returns>Un ok con el resultado</returns>
        [HttpGet]
        [Route("api/reto/admin/misretos")]
        public IActionResult GetReto([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosAdministrados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a un reto por su nombre
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <param name="nombreReto">El nombre del reto a acceder</param>
        /// <returns>Un ok con el resultado</returns>
        [HttpGet]
        [Route("api/reto/admin/verReto")]
        public IActionResult GetRetoPorNombre([FromQuery] string usuario, [FromQuery] string nombreReto)
        {
            var resultado = _repository.verRetoPorNombre(usuario, nombreReto);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder todos los retos disponibles para un deportista 
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/reto/user/retosDisponibles")]
        public IActionResult GetRetosDisponibles([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosDisponibles(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para ver es estado de los retos de un deportista específico
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado</returns>
        [HttpGet]
        [Route("api/user/reto/estado")]
        public IActionResult GetEstadoRetos([FromQuery] string usuario)
        {
            var resultado = _repository.verEstadoRetos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a todos los retos incompletos de un deportista específico
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado</returns>
        [HttpGet]
        [Route("api/reto/user/incompletos")]
        public IActionResult GetRetos([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosIncompletos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para crear un nuevo reto
        /// </summary>
        /// <param name="reto">El objeto reto con la información correspondiente</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/reto/new")]
        public IActionResult NuevoReto([FromBody] Reto reto)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(reto);
                _repository.SaveChanges();
                return Ok("Reto creado correctamente");
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Petición para inscribirse a un reto
        /// </summary>
        /// <param name="adminReto">El administrador del reto</param>
        /// <param name="nombreReto">El nombre del reto</param>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/reto/user/inscribirse")]
        public IActionResult inscribirReto([FromQuery] string adminReto, [FromQuery] string nombreReto, [FromQuery] string usuario)
        {
            _repository.inscribirReto(adminReto, nombreReto, usuario);
            _repository.SaveChanges();

            return Ok("Inscripción realizada correctamente");
        }

        /// <summary>
        /// Petición para actualizar la información de un reto
        /// </summary>
        /// <param name="reto">El reto con la información actualizada</param>
        /// <param name="usuarioAdmin">el usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de tener éxito</returns>
        [HttpPut]
        [Route("api/reto/edit")]
        public IActionResult ActualizarReto([FromBody] Reto reto, [FromQuery] string usuarioAdmin)
        {
            if (reto.Admindeportista != usuarioAdmin)
            {
                return BadRequest();
            }

            _repository.Update(reto);
            _repository.SaveChanges();
            return Ok("Reto actualizado correctamente");
        }

        /// <summary>
        /// Petición para eliminar un reto
        /// </summary>
        /// <param name="nombreReto">el nombre del reto a eliminar</param>
        /// <param name="usuario">el usuario que realiza la petición</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpDelete]
        [Route("api/reto/delete")]
        public IActionResult EliminarReto([FromQuery] string nombreReto, [FromQuery] string usuario)
        {
            _repository.Delete(nombreReto, usuario);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");   
        }
    }
}
