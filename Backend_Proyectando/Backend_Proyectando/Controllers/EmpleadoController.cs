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
    [RoutePrefix("api/empleado")]
    public class EmpleadoController : ApiController
    {
        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarEmpleado(clsOpeEmpleado datos)
        {
            var resultado = datos.AgregarEmpleado();
            return Ok(resultado);
        }

        [HttpPost]
        [Route("actualizar")]
        public IHttpActionResult ActualizarEmpleado(clsOpeEmpleado datos)
        {
            var resultado = datos.ActualizarEmpleado();
            return Ok(resultado);
        }

        [HttpGet]
        [Route("consultar/{documento}")]
        public IHttpActionResult ConsultarEmpleado(string documento)
        {
            clsOpeEmpleado ope = new clsOpeEmpleado();
            var empleado = ope.ConsultarEmpleado(documento);
            return Ok(empleado);
        }

        [HttpGet]
        [Route("listar")]
        public IHttpActionResult ListarEmpleados()
        {
            clsOpeEmpleado ope = new clsOpeEmpleado();
            var empleados = ope.ListarEmpleados();
            return Ok(empleados);
        }
    }
}
