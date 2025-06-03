using backend_proyectando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_proyectando.Class
{
    public class clsOpeControl
    {
        private proyectandoEntities obd = new proyectandoEntities();

        public class ControlInput
        {
            public long idProyectoFase { get; set; }
            public string documentoEmp { get; set; }
            public DateTime fecha { get; set; }
            public double tiempo_empleado { get; set; }
            public string descripcion { get; set; }
            public string estado { get; set; }
        }

        public (bool success, string message) RegistrarControl(ControlInput input)
        {
            if (string.IsNullOrWhiteSpace(input.documentoEmp) || string.IsNullOrWhiteSpace(input.descripcion) || string.IsNullOrWhiteSpace(input.estado))
                return (false, "Campos obligatorios vacíos.");

            if (input.tiempo_empleado <= 0)
                return (false, "Tiempo debe ser mayor que cero.");

            try
            {
                Control nuevo = new Control
                {
                    documentoEmpleado = input.documentoEmp,
                    fecha = input.fecha,
                    observaciones = input.descripcion
                };

                obd.Control.Add(nuevo);
                obd.SaveChanges();

                DetalleControl detalle = new DetalleControl
                {
                    id_control = nuevo.id_control,
                    id_fase_proceso = (int)input.idProyectoFase, // reutilizamos como faseProceso
                    horas = (int)Math.Round(input.tiempo_empleado),
                    comentarios = input.estado
                };

                obd.DetalleControl.Add(detalle);
                obd.SaveChanges();

                return (true, "Control registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return (false, "Error al registrar control: " + ex.Message);
            }
        }
    }
}