using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly GrupoRepo _repository;

        // se inyecta el repositorio correspondiente
        public GrupoController(GrupoRepo repo)
        {
            _repository = repo;
        }

        /// <summary>
        /// Petición para acceder a todos los grupos en los cuales un deportista
        /// aún no está asociado
        /// </summary>
        /// <param name="usuario">El usuario que hace la consulta</param>
        /// <returns>Un ok con el resultado obtenido en caso de exito</returns>
        [HttpGet]
        [Route("api/grupos")]
        public IActionResult GetGruposNoAsociados([FromQuery] string usuario)
        {
            var resultado = _repository.verTodosNoAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a las carreras que un grupo puede ver
        /// </summary>
        /// <param name="idGrupo">id del grupo a consultar</param>
        /// <param name="usuario">el usuario que hace la consulta</param>
        /// <returns>Un ok con el resultado en caso de exito</returns>
        [HttpGet]
        [Route("api/grupo/carreras")]
        public IActionResult GetCarreras([FromQuery] int idGrupo, [FromQuery] string usuario)
        {
            var resultado = _repository.accederCarreras(idGrupo, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a los retos que un grupo puede ver
        /// </summary>
        /// <param name="idGrupo">el id del grupo a consultar</param>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/grupo/retos")]
        public IActionResult GetRetos([FromQuery] int idGrupo, [FromQuery] string usuario)
        {
            var resultado = _repository.accederRetos(idGrupo, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a los grupos administrados por un deportista
        /// </summary>
        /// <param name="usuario">el usuario que hace la petición</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/grupo/admin/misgrupos")]
        public IActionResult GetGrupos([FromQuery] string usuario)
        {
            var resultado = _repository.verMisGruposAdministrados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a los grupos asociados por un deportista
        /// </summary>
        /// <param name="usuario">el usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de exito</returns>
        [HttpGet]
        [Route("api/grupo/user/grupos")]
        public IActionResult GetGruposAsociados([FromQuery] string usuario)
        {
            var resultado = _repository.verMisGruposAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

       /// <summary>
       /// Petición para acceder a todos los grupos de la base de datos
       /// </summary>
       /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/grupo/todos")]
        public IActionResult GetTodosLosGrupos()
        {
            var resultado = _repository.verTodosLosGrupos();

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para acceder a todos los grupos en los cuales un deportista 
        /// aún no se ha inscrito
        /// </summary>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/user/grupos/noInscritos")]
        public IActionResult GetGruposNoInscritos([FromQuery] string usuario)
        {
            var resultado = _repository.verTodosLosGruposNoInscritos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para buscar un grupo de acuerdo a su nombre
        /// </summary>
        /// <param name="nombreGrupo">El nombre del grupo a buscar</param>
        /// <param name="usuario">El usuario que realiza la consulta</param>
        /// <returns>Un ok con el resultado en caso de éxito</returns>
        [HttpGet]
        [Route("api/user/grupo")]
        public IActionResult BuscarGruposPorNombre([FromQuery] string nombreGrupo, [FromQuery] string usuario)
        {
            var resultado = _repository.buscarPorNombre(nombreGrupo, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Petición para crear un nuevo grupo
        /// </summary>
        /// <param name="grupo">El grupo a crear con su respectiva info</param>
        /// <returns>Un ok en caso de exito</returns>
        [HttpPost]
        [Route("api/grupo/new")]
        public IActionResult NuevoGrupo([FromBody] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(grupo);
                _repository.SaveChanges();
                return Ok("Grupo creado correctamente");
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Petición para agregar un deportista a un grupo
        /// </summary>
        /// <param name="grupo">El objeto grupo</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPost]
        [Route("api/grupo/new/deportista")]
        public IActionResult AgregarAGrupo([FromBody] GrupoDeportista grupo)
        {
            _repository.agregarAgrupo(grupo);
            _repository.SaveChanges();
            return Ok("Agregado correctamente");
        }

        /// <summary>
        /// Petición para editar la información de un grupo
        /// </summary>
        /// <param name="grupo">El objeto grupo con la información actualizada</param>
        /// <param name="usuarioAdmin">El usuario que hace la consulta</param>
        /// <returns>Un ok en caso de éxito</returns>
        [HttpPut]
        [Route("api/grupo/edit")]
        public IActionResult actualizarGrupo([FromBody] Grupo grupo, [FromQuery] string usuarioAdmin)
        {
            if (grupo.Admindeportista != usuarioAdmin)
            {
                return BadRequest();
            }

            _repository.Update(grupo);
            _repository.SaveChanges();
            return Ok("Grupo actualizado correctamente");
        }

        /// <summary>
        /// Petición para eliminar un grupo
        /// </summary>
        /// <param name="idGrupo">el id del grupo a eliminar</param>
        /// <param name="usuario">el usuario que realiza la petición</param>
        /// <returns>Un ok en caso de tener éxito</returns>
        [HttpDelete]
        [Route("api/grupo/delete")]
        public IActionResult eliminarGrupo([FromQuery] int idGrupo, [FromQuery] string usuario)
        {
            _repository.Delete(idGrupo, usuario);
            _repository.SaveChanges();
            return Ok("Grupo eliminado correctamente");
        }
    }
}
