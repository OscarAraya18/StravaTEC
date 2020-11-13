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

        public DeportistaController(DeportistaRepo repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("api/user/getDeportista")]
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
        [Route("api/user/buscar/nombre")]
        public IActionResult getDeportistaPorNombre([FromQuery] string nombre)
        {
            var resultado = _repository.obtenerPorNombre(nombre);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/user/amigos/actividades")]
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
        [Route("api/user/noAmigos")]
        public IActionResult getDeportistasNoAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.mostrarTodosDeportistasNoAmigos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/user/amigos")]
        public IActionResult getDeportistasAmigos([FromQuery] string usuario)
        {
            var resultado = _repository.verAmigosAsociados(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }


        [HttpGet]
        [Route("api/user/carreras")]
        public IActionResult getCarrerasInscritas([FromQuery] string usuario)
        {
            var resultado = _repository.verCarrerasInscritas(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/user/retos")]
        public IActionResult getRetosIncompletos([FromQuery] string usuario)
        {
            var resultado = _repository.verRetosIncompletos(usuario);

            if (resultado == null)
            {
                return BadRequest();
            }
            return Ok(resultado);
        }

        [HttpPost]
        [Route("api/user/new")]
        public IActionResult nuevoDeportista([FromBody] DeportistaParser deportista)
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
        [Route("api/user/login")]
        public IActionResult verificarLogin([FromBody] Login login)
        {
            var resultado = _repository.verificarLogin(login);

            if (resultado == false)
                return BadRequest("Usuario o contraseña incorrectos");
            
            return Ok("Inicio de sesión exitoso");
        }

        [HttpPost]
        [Route("api/user/amigo/new")]
        public IActionResult seguirDeportista([FromQuery] string amigo, [FromQuery] string usuario)
        {
            _repository.seguirDeportista(usuario, amigo);
            _repository.SaveChanges();
            return Ok("Amigo agregado correctamente");
        }


        [HttpPost]
        [Route("api/user/registrar/actividad")]
        public IActionResult registrarActividades([FromBody] Actividad actividad)
        {
            var resultado = _repository.registrarActividades(actividad);

            _repository.SaveChanges();

            if (resultado)
                return Ok("Actividad registrada correctamente");
            return BadRequest("Ha ocurrido un error");
        }

        [HttpPut]
        [Route("api/user/edit")]
        public IActionResult actualizarDeportista([FromBody] DeportistaParser deportista, [FromQuery] string usuario)
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
