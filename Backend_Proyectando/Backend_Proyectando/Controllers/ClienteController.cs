using backend_proyectando.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace backend_proyectando.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult AgregarCliente(clsOpeCliente datos)
        {
            var resultado = datos.AgregarCliente();
            return Ok(resultado);
        }

        // PUT: api/cliente/actualizar
        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult Actualizar([FromBody] Cliente cliente)
        {
            clsOpeCliente ope = new clsOpeCliente();
            string result = ope.ActualizarCliente(cliente);
            if (result == "Cliente actualizado correctamente.")
                return Ok(result);
            else
                return BadRequest(result);
        }

        // PUT: api/cliente/cambiarestado
        [HttpPut]
        [Route("cambiarestado")]
        public IHttpActionResult CambiarEstado([FromBody] dynamic data)
        {
            string documento = data.documento;
            bool estado = data.activo;

            clsOpeCliente ope = new clsOpeCliente();
            string result = ope.CambiarEstadoCliente(documento, estado);
            if (result.Contains("actualizado"))
                return Ok(result);
            else
                return BadRequest(result);
        }

        // GET: api/cliente/listar
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var db = new proyectandoEntities();
                var clientes = db.Cliente
                    .Select(c => new {
                        c.documento,
                        c.nombre,
                        c.apellido,
                        c.email,
                        c.activo,
                        Telefonos = c.Telefono.Select(t => new {
                            t.id_telefono,
                            t.numero,
                            t.id_tipo_telefono
                        }),
                        Direcciones = c.Direccion.Select(d => new {
                            d.id_direccion,
                            d.descripcion,
                            d.id_ciudad
                        })
                    }).ToList();

                return Ok(clientes);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}