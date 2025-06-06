﻿using backend_proyectando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

public class clsOpeEmpleado
{
    private proyectandoEntities obd = new proyectandoEntities();
    public Empleado tbEmpleado { get; set; }
    public Usuario tbUsuario { get; set; }

    public class EmpleadoDTO
    {
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public int idtipoTelefono { get; set; }
        public string tipoTelefonoNombre { get; set; }

        public int idcargo { get; set; }
        public string cargoNombre { get; set; }
        public bool esUsuario { get; set; }
    }


    public string AgregarEmpleado()
    {
        if (string.IsNullOrWhiteSpace(tbEmpleado.documento) ||
            string.IsNullOrWhiteSpace(tbEmpleado.nombre) ||
            string.IsNullOrWhiteSpace(tbEmpleado.apellido) ||
            string.IsNullOrWhiteSpace(tbEmpleado.email))
        {
            return "Campos obligatorios del empleado están vacíos.";
        }

        if (obd.Empleado.Any(e => e.documento == tbEmpleado.documento))
            return "El empleado ya existe.";

        obd.Empleado.Add(tbEmpleado);
        obd.SaveChanges();

        if (tbEmpleado.esUsuario)
        {
            if (string.IsNullOrWhiteSpace(tbUsuario.username) || string.IsNullOrWhiteSpace(tbUsuario.password))
                return "Datos de usuario incompletos.";

            tbUsuario.documentoEmp = tbEmpleado.documento;
            tbUsuario.activo = false;
            obd.Usuario.Add(tbUsuario);
            obd.SaveChanges();
        }

        return "Empleado agregado correctamente.";
    }

    public string ActualizarEmpleado()
    {
        var emp = obd.Empleado.Find(tbEmpleado.documento);
        if (emp == null) return "Empleado no encontrado.";

        emp.nombre = tbEmpleado.nombre;
        emp.apellido = tbEmpleado.apellido;
        emp.email = tbEmpleado.email;
        emp.telefono = tbEmpleado.telefono;
        emp.id_cargo = tbEmpleado.id_cargo;
        emp.id_tipo_telefono = tbEmpleado.id_tipo_telefono;
        emp.esUsuario = tbEmpleado.esUsuario;

        if (tbEmpleado.esUsuario)
        {
            var usuario = obd.Usuario.FirstOrDefault(u => u.documentoEmp == tbEmpleado.documento);
            if (usuario == null)
            {
                if (string.IsNullOrWhiteSpace(tbUsuario.username) || string.IsNullOrWhiteSpace(tbUsuario.password))
                    return "Datos de usuario requeridos para nuevo usuario.";

                tbUsuario.documentoEmp = tbEmpleado.documento;
                tbUsuario.activo = false;
                obd.Usuario.Add(tbUsuario);
            }
            else
            {
                usuario.username = tbUsuario.username;
                usuario.password = tbUsuario.password;
                // No activar aún
            }
        }

        obd.SaveChanges();
        return "Empleado actualizado.";
    }

    public EmpleadoDTO ConsultarEmpleado(string documento)
    {
        var emp = obd.Empleado
             .Include("Cargo")
             .Include("TipoTel")
             .FirstOrDefault(e => e.documento == documento);

        if (emp == null) return null;

        return new EmpleadoDTO
        {
            documento = emp.documento,
            nombre = emp.nombre,
            apellido = emp.apellido,
            email = emp.email,
            telefono = emp.telefono,
            idtipoTelefono = (int)(emp.TipoTel?.id_tipo_telefono),
            tipoTelefonoNombre = emp.TipoTel?.descripcion,
            idcargo = (int)(emp.Cargo?.id_cargo),
            cargoNombre = emp.Cargo?.nombre,
            esUsuario = emp.esUsuario
        };
    }

    public List<Empleado> ListarEmpleados()
    {
        return obd.Empleado.ToList();
    }
}
