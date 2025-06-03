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
    [RoutePrefix("api/faseproceso")]
    public class FaseProcesoController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public IHttpActionResult Listar()
        {
            using (var db = new proyectandoEntities())
            {
                var lista = db.FaseProceso
                    .Where(fp => fp.activo == true)
                    .Select(fp => new
                    {
                        fp.id_fase_proceso,
                        Fase = fp.Fase.nombre,
                        Proceso = fp.Proceso.nombre
                    })
                    .ToList();

                return Ok(lista);
            }
        }
    }
}
