    using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;


namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly CarreraRepo _repository;

        // se inyecta el repositorio correspondiente
        public CarreraController(CarreraRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para pedir todas las carreras a las que se puede
        /// inscribir un deportista
        /// </summary>
        /// <param name="usuario">El usuario que hace la petición</param>
        /// <returns>Un Ok con la lista de carreras en caso de exito</returns>
        [HttpGet]
        [Route("api/carrera/user/carrerasDisponibles")]
        public IActionResult GetCarrerasDisponibles([FromQuery] string usuario)
        {
            var resultado = _repository.verCarrerasDisponibles(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }

            return Ok(resultado);
        }

        /// <summary>
        /// Petición para ver las carreras que administra un deportista
        /// </summary>
        /// <param name="usuario">el usuario que consulta</param>
        /// <returns>Un ok con las carreras administradas en caso de exito</returns>
        [HttpGet]
        [Route("api/carrera/admin/miscarreras")]
        public IActionResult GetCarrera([FromQuery] string usuario)
        {
            var resultado = _repository.verMisCarreras(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder alguna carrera administrada mediante el nombre
        /// </summary>
        /// <param name="usuario">el usuario que hace la consulta</param>
        /// <param name="nombreCarrera">la carrera a buscar</param>
        /// <returns>Un ok con la carrera encontrada en caso de exito</returns>
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

        /// <summary>
        /// Petición para buscar una carrera por coincidencia en el nombre 
        /// </summary>
        /// <param name="usuario">usuario que realiza la consulta</param>
        /// <param name="nombreCarrera">el nombre de carrera a consultar</param>
        /// <returns>Un ok con el resultado de la búsqueda en caso de éxito</returns>
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

        /// <summary>
        /// Petición para acceder a las carreras inscritas por un deportista con sus
        /// respectivas posiciones en caso de que esté terminada
        /// </summary>
        /// <param name="usuario">usuario que realiza la consulta</param>
        /// <returns></returns>
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

        /// <summary>
        /// Petición para crear una nueva carrera
        /// </summary>
        /// <param name="carrera">el objeto carrera con toda la información respectiva</param>
        /// <returns>Un ok en caso de exito</returns>
        [HttpPost]
        [Route("api/carrera/admin/new")]
        public IActionResult NuevaCarrera([FromBody] Carrera carrera)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(carrera);
                _repository.SaveChanges();
                return Ok("Carrera creada correctamente");
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Petición para actualizar información de una carrera
        /// </summary>
        /// <param name="carrera">El objeto carreraa con la información respectiva</param>
        /// <param name="usuarioAdmin">El usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPut]
        [Route("api/carrera/admin/edit")]
        public IActionResult ActualizarCarrera([FromBody] Carrera carrera, [FromQuery] string usuarioAdmin)
        {
            if (carrera.Admindeportista != usuarioAdmin)
            {
                return BadRequest();
            }

            _repository.Update(carrera);
            _repository.SaveChanges();
            return Ok("Carrera actualizada correctamente");
        }

        /// <summary>
        /// Petición para eliminar una carrera
        /// </summary>
        /// <param name="nombreCarrera">El nombre de la carrera que quiere eliminar</param>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpDelete]
        [Route("api/carrera/admin/delete")]
        public IActionResult EliminarCarrera([FromQuery] string nombreCarrera, [FromQuery] string usuario)
        {
            _repository.Delete(nombreCarrera, usuario);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");
        }

    }
}
