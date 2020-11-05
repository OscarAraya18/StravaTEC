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
        [Route("api/grupo")]
        public IActionResult getGrupos()
        {
            var resultado = _repository.verTodos();

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/grupo/carrers")]
        public IActionResult getCarreras([FromBody] string nombreGrupo)
        {
            var resultado = _repository.accederCarreras(nombreGrupo);

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
        public IActionResult eliminarGrupo([FromQuery] string nombre)
        {
            _repository.Delete(nombre);
            _repository.SaveChanges();
            return Ok("Carrera eliminado correctamente");
        }
    }
}
