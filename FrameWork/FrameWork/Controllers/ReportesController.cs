
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FrameWork.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportesController : ApiController
    {
        ReportController report = new ReportController();

        [HttpGet]
        [Route("api/admin/verParticipantes")]
        public IHttpActionResult GetParticipantes([FromUri] string nombreCarrera, [FromUri] string admin)
        {
            var resultado = report.Reporte_participantes(nombreCarrera, admin);

            if (resultado == null)
            {
                return BadRequest("No se ha encontrado nada");
            }

            return Ok(resultado);
        }

        [HttpGet]
        [Route("api/admin/verPosiciones")]
        public IHttpActionResult GetPosiciones([FromUri] string nombreCarrera, [FromUri] string admin)
        {
            var resultado = report.Reporte_posiciones(nombreCarrera, admin);

            if (resultado == null)
            {
                return BadRequest("No se ha encontrado nada");
            }

            return Ok(resultado);
        }


    }

    
}
