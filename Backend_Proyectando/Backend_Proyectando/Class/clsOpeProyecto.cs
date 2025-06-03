using backend_proyectando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_proyectando.Class
{
	public class clsOpeProyecto
	{
        private proyectandoEntities obd = new proyectandoEntities();
        public Proyecto tbProyecto { get; set; }

        public string CrearProyecto()
        {
            if (tbProyecto == null || string.IsNullOrWhiteSpace(tbProyecto.nombre) || string.IsNullOrWhiteSpace(tbProyecto.documentoCliente))
                return "Faltan datos requeridos";

            obd.Proyecto.Add(tbProyecto);
            obd.SaveChanges();
            return "OK";
        }

        public string AsignarFaseProceso(long idFaseProceso, long idProyecto, DateTime fechaInicio)
        {
            var pf = new ProyectoFase
            {
                id_faseProceso = idFaseProceso,
                id_proyecto = idProyecto,
                fecha_inicio = fechaInicio
            };
            obd.ProyectoFase.Add(pf);
            obd.SaveChanges();
            return "OK";
        }

        public object ListarProyectos()
        {
            return obd.Proyecto.Select(p => new {
                p.id_proyecto,
                p.nombre,
                p.descripcion,
                p.fecha_inicio,
                p.fecha_fin_estim,
                Cliente = p.Cliente.nombre,
                Estado = p.Estado.nombre
            }).ToList();
        }
    }
}