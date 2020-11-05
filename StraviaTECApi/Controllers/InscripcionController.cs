using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFConsole.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

        [HttpPost]
        [Route("api/inscripcion/new")]
        public IActionResult nuevaInscripcion([FromBody] Inscripcion inscripcion, [FromQuery] string nombreCarrera)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(inscripcion, nombreCarrera);
                _repository.SaveChanges();
                return Ok("Inscripcion creada correctamente");
            }

            return BadRequest(ModelState);

        }

        [Route("api/inscripcion/accept")]
        public IActionResult nuevaInscripcion([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                _repository.aceptarInscripcion(id);
                _repository.SaveChanges();
                return Ok("Inscripcion aceptada correctamente");
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
            return Ok("Inscripcion actualizado correctamente");
        }

    }
}
