using backend_proyectando.Class;
using backend_proyectando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace backend_proyectando.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/proyecto")]
    public class ProyectoController : ApiController
    {
        [HttpPost]
        [Route("crear")]
        public IHttpActionResult CrearProyecto([FromBody] Proyecto p)
        {
            clsOpeProyecto ope = new clsOpeProyecto { tbProyecto = p };
            var result = ope.CrearProyecto();
            return Ok(result);
        }

        [HttpPost]
        [Route("asignarfase")]
        public IHttpActionResult AsignarFaseProceso([FromBody] dynamic data)
        {
            clsOpeProyecto ope = new clsOpeProyecto();
            long idFaseProceso = data.idFaseProceso;
            long idProyecto = data.idProyecto;
            DateTime fechaInicio = data.fechaInicio;
            var result = ope.AsignarFaseProceso(idFaseProceso, idProyecto, fechaInicio);
            return Ok(result);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            clsOpeProyecto ope = new clsOpeProyecto();
            return Ok(ope.ListarProyectos());
        }
    }
}
