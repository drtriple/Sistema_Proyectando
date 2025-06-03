using System;
using System.Collections.Generic;
using System.Linq;
using backend_proyectando.Models;

namespace proyectando.Class
{
    public class clsOpeDireccion
    {
        private proyectandoEntities obd = new proyectandoEntities();

        public string ActualizarDirecciones(string documentoCliente, List<Direccion> nuevasDirecciones)
        {
            try
            {
                foreach (var nueva in nuevasDirecciones)
                {
                    var dir = obd.Direccion.FirstOrDefault(d => d.id_direccion == nueva.id_direccion && d.documentoCliente == documentoCliente);
                    if (dir != null)
                    {
                        dir.id_ciudad = nueva.id_ciudad;
                        dir.descripcion = nueva.descripcion;
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error al actualizar direcciones: " + ex.Message;
            }
        }
    }
}
