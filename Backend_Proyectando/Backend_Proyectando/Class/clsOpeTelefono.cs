using System;
using System.Collections.Generic;
using System.Linq;
using backend_proyectando.Models;

namespace proyectando.Class
{
    public class clsOpeTelefono
    {
        private proyectandoEntities obd = new proyectandoEntities();

        public string ActualizarTelefonos(string documentoCliente, List<Telefono> nuevosTelefonos)
        {
            try
            {
                foreach (var nuevo in nuevosTelefonos)
                {
                    var tel = obd.Telefono.FirstOrDefault(t => t.id_telefono == nuevo.id_telefono && t.documentoCliente == documentoCliente);
                    if (tel != null)
                    {
                        tel.numero = nuevo.numero;
                        tel.id_tipo_telefono = nuevo.id_tipo_telefono;
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                return "Error al actualizar teléfonos: " + ex.Message;
            }
        }
    }
}
