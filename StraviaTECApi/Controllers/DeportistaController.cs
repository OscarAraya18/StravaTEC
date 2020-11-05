using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using StraviaTECApi.Models;
using System.Collections.Generic;

namespace StraviaTECApi.Controllers
{
    [ApiController]
    public class DeportistaController : ControllerBase
    {
        private readonly DeportistaRepo _repository;

        public DeportistaController(DeportistaRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/deportista")]
        public IActionResult getDeportista([FromQuery] string usuario)
        {
            var resultado = _repository.obtenerPorUsuario(usuario);

            if(resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/deportista/amigos/actividades")]
        public IActionResult getActividadesAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.verActividadesAmigos(usuario);
            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/deportista/carreras")]
        public IActionResult getCarrerasInscritas([FromQuery] string usuario)
        {
            var resultado = _repository.verCarrerasInscritas(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpPost]
        [Route("api/deportista/new")]
        public IActionResult nuevoDeportista([FromBody] Deportista deportista)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(deportista);
                _repository.SaveChanges();
                return Ok("Deportista creado correctamente");
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/deportista/amigo/new")]
        public IActionResult seguirDeportista([FromBody] string amigo, [FromQuery] string usuario)
        {
            _repository.seguirDeportista(usuario, amigo);
            _repository.SaveChanges();
            return Ok("Amigo agregado correctamente");
        }



        [HttpPut]
        [Route("api/deportista/edit")]
        public IActionResult actualizarDeportista([FromBody] Deportista deportista, [FromQuery] string usuario)
        {
            if (deportista.Usuario != usuario)
            {
                return BadRequest();
            }
            
            _repository.Update(deportista);
            _repository.SaveChanges();
            return Ok("Deportista actualizado correctamente");
        }

        [HttpDelete]
        [Route("api/deportista/delete")]
        public IActionResult eliminarDeportista([FromQuery] string usuario)
        {
            _repository.Delete(usuario);
            _repository.SaveChanges();
            return Ok("Deportista eliminado correctamente");
        }   
    }
}
