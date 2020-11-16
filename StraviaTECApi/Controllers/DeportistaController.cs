using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;
using StraviaTECApi.Parsers;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class DeportistaController : ControllerBase
    {
        private readonly DeportistaRepo _repository;

        // se inyecta el repositorio respectivo
        public DeportistaController(DeportistaRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para acceder a un deportista por su nombre de usuario
        /// </summary>
        /// <param name="usuario">el usuario a buscar</param>
        /// <returns>Un ok con el usuario en caso de exito</returns>
        [HttpGet]
        [Route("api/user/getDeportista")]
        public IActionResult GetDeportista([FromQuery] string usuario)
        {
            var resultado = _repository.obtenerPorUsuario(usuario);

            if(resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para buscar un deportista por coincidencias en el nombre
        /// </summary>
        /// <param name="nombre">el nombre a buscar</param>
        /// <param name="usuario">el usuario que hace la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/user/buscarPorNombre")]
        public IActionResult GetDeportistaPorNombre([FromQuery] string nombre, [FromQuery] string usuario)
        {
            var resultado = _repository.obtenerPorNombre(nombre, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para ver las actividades de los amigos de un deportista específico
        /// </summary>
        /// <param name="usuario">El usuario que realiza la petición</param>
        /// <returns>Un ok con la lista de actividades en caso de exito</returns>
        [HttpGet]
        [Route("api/user/amigos/actividades")]
        public IActionResult GetActividadesAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.verActividadesAmigos(usuario);
            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a todos los deportistas NO amigos de un usuario específico
        /// </summary>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de exito</returns>
        [HttpGet]
        [Route("api/user/noAmigos")]
        public IActionResult GetDeportistasNoAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.mostrarTodosDeportistasNoAmigos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a los deportistas que son amigos de un deportista en específico
        /// </summary>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de exito</returns>
        [HttpGet]
        [Route("api/user/amigos")]
        public IActionResult GetDeportistasAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.verAmigosAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a las carreras inscritas por un deportista específico
        /// </summary>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el usuario en específico</returns>
        [HttpGet]
        [Route("api/user/carreras")]
        public IActionResult GetCarrerasInscritas([FromQuery] string usuario)
        {
            var resultado = _repository.verCarrerasInscritas(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a todos los retos incompletos de un deportista
        /// </summary>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/user/retos")]
        public IActionResult GetRetosIncompletos([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosIncompletos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para crear un nuevo deportista
        /// </summary>
        /// <param name="deportista">El objeto deportista con la información respectiva</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/new")]
        public IActionResult NuevoDeportista([FromBody] DeportistaParser deportista)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(deportista);
                _repository.SaveChanges();
                return Ok("Deportista creado correctamente");
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Petición para verificar el login de un usuario
        /// </summary>
        /// <param name="login">El objeto login con su información respectiva</param>
        /// <returns>Un ok en caso de exito, un BadRequest en caso contrario</returns>
        [HttpPost]
        [Route("api/user/login")]
        public IActionResult VerificarLogin([FromBody] Login login)
        {
            var resultado = _repository.verificarLogin(login);

            if (resultado == false)
                return BadRequest("Usuario o contraseña incorrectos");
            
            return Ok("Inicio de sesión exitoso");
        }

        /// <summary>
        /// Petición para seguir un deportista
        /// </summary>
        /// <param name="amigo">el usuario del amigo a agregar</param>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un Ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/user/amigo/new")]
        public IActionResult SeguirDeportista([FromQuery] string amigo, [FromQuery] string usuario)
        {
            _repository.seguirDeportista(usuario, amigo);
            _repository.SaveChanges();
            return Ok("Amigo agregado correctamente");
        }

        /// <summary>
        /// Petición para registrar una actividad
        /// </summary>
        /// <param name="actividad">EL objeto actividad con la información respectiva</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/user/registrar/actividad")]
        public IActionResult RegistrarActividades([FromBody] Actividad actividad)
        {
            var resultado = _repository.registrarActividades(actividad);

            _repository.SaveChanges();

            if (resultado)
                return Ok("Actividad registrada correctamente");
            return BadRequest("Ha ocurrido un error");
        }

        /// <summary>
        /// Petición para actualizar la información de un deportista
        /// </summary>
        /// <param name="deportista">El objeto deportista con la información actualizada</param>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok en caso de exito</returns>
        [HttpPut]
        [Route("api/user/edit")]
        public IActionResult ActualizarDeportista([FromBody] DeportistaParser deportista, [FromQuery] string usuario)
        {
            if (deportista.Usuario != usuario)
            {
                return BadRequest();
            }
            
            _repository.Update(deportista);
            _repository.SaveChanges();
            return Ok("Deportista actualizado correctamente");
        }

        /// <summary>
        /// Petición para eliminar un deportista específico
        /// </summary>
        /// <param name="usuario">El usuario del deportista a eliminar</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/deportista/delete")]
        public IActionResult EliminarDeportista([FromQuery] string usuario)
        {
            _repository.Delete(usuario);
            _repository.SaveChanges();
            return Ok("Deportista eliminado correctamente");
        }   
    }
}
