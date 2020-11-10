using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private readonly GrupoRepo _repository;

        public GrupoController(GrupoRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/grupos")]
        public IActionResult getGruposNoAsociados([FromQuery] string usuario)
        {
            var resultado = _repository.verTodosNoAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/grupo/carreras")]
        public IActionResult getCarreras([FromQuery] string nombreGrupo, [FromQuery] string usuario)
        {
            var resultado = _repository.accederCarreras(nombreGrupo, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/grupo/retos")]
        public IActionResult getRetos([FromQuery] string nombreGrupo, [FromQuery] string usuario)
        {
            var resultado = _repository.accederRetos(nombreGrupo, usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }


        [HttpGet]
        [Route("api/grupo/admin/misgrupos")]
        public IActionResult getGrupos([FromQuery] string usuario)
        {
            var resultado = _repository.verMisGruposAdministrados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/grupo/user/grupos")]
        public IActionResult getGruposAsociados([FromQuery] string usuario)
        {
            var resultado = _repository.verMisGruposAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/grupo/todos")]
        public IActionResult getTodosLosGrupos([FromQuery] string usuario)
        {
            var resultado = _repository.verTodosLosGrupos();

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/user/grupos/noInscritos")]
        public IActionResult getGruposNoInscritos([FromQuery] string usuario)
        {
            var resultado = _repository.verTodosLosGruposNoInscritos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }


        [HttpPost]
        [Route("api/grupo/new")]
        public IActionResult nuevoGrupo([FromBody] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(grupo);
                _repository.SaveChanges();
                return Ok("Grupo creado correctamente");
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/grupo/new/deportista")]
        public IActionResult nuevoGrupo([FromBody] Grupo grupo, [FromQuery] string usuario)
        {
            _repository.agregarAgrupo(usuario, grupo);
            _repository.SaveChanges();
            return Ok("Agregado correctamente");
        }

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

        [HttpDelete]
        [Route("api/grupo/delete")]
        public IActionResult eliminarGrupo([FromQuery] string nombreGrupo, [FromQuery] string usuario)
        {
            _repository.Delete(nombreGrupo, usuario);
            _repository.SaveChanges();
            return Ok("Grupo eliminado correctamente");
        }
    }
}
