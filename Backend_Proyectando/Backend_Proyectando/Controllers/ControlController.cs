// ControlController.cs
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using backend_proyectando.Class;
using backend_proyectando.Models;

namespace backend_proyectando.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/control")]
    public class ControlController : ApiController
    {
        [HttpPost]
        [Route("registrar")]
        public IHttpActionResult RegistrarControl([FromBody] clsOpeControl.ControlInput input)
        {
            if (input == null)
                return BadRequest("Datos inválidos.");

            var ope = new clsOpeControl();
            var result = ope.RegistrarControl(input);
            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }
    }
}