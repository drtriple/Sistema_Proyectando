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
    [RoutePrefix("api/user")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] Usuario usuario)
        {
            clsOpeLogin ope = new clsOpeLogin();
            ope.tbUsuario = usuario;

            var result = ope.IniciarSesion();

            if (result is string)
                return BadRequest(result.ToString());

            return Ok(result);
        }

        [HttpPost]
        [Route("activar/{username}")]
        public IHttpActionResult ActivarUsuario(string username)
        {
            clsOpeLogin ope = new clsOpeLogin();
            var resultado = ope.ActivarUsuario(username);
            return Ok(resultado);
        }
    }
}
