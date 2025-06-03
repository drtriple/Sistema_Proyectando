using backend_proyectando.Models;
using proyectando.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

public class clsOpeCliente
{
    private proyectandoEntities obd = new proyectandoEntities();

    public Cliente tbCliente { get; set; }
    public List<Telefono> telefonos { get; set; }
    public List<Direccion> direcciones { get; set; }

    // Método para agregar cliente
    public string AgregarCliente()
    {
        string validacion = ValidarDatos();
        if (validacion != "OK")
            return validacion;

        if (obd.Cliente.Any(c => c.documento == tbCliente.documento))
            return "El cliente ya existe.";

        tbCliente.activo = true;
        obd.Cliente.Add(tbCliente);

        foreach (var tel in telefonos)
        {
            tel.documentoCliente = tbCliente.documento;
            obd.Telefono.Add(tel);
        }

        foreach (var dir in direcciones)
        {
            dir.documentoCliente = tbCliente.documento;
            obd.Direccion.Add(dir);
        }

        obd.SaveChanges();
        return "Cliente y datos asociados agregados correctamente.";
    }

    // Método para actualizar cliente
    public string ActualizarCliente(Cliente clienteActualizado)
    {
        try
        {
            var cliente = obd.Cliente.FirstOrDefault(c => c.documento == clienteActualizado.documento);
            if (cliente == null) return "Cliente no encontrado.";

            // Solo actualizar el email
            if (!string.IsNullOrWhiteSpace(clienteActualizado.email))
            {
                if (!Regex.IsMatch(clienteActualizado.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return "Formato de email inválido.";
                cliente.email = clienteActualizado.email;
            }

            // Actualizar teléfonos
            var opTel = new clsOpeTelefono();
            var telResult = opTel.ActualizarTelefonos(clienteActualizado.documento, clienteActualizado.Telefono.ToList());
            if (telResult != "OK") return telResult;

            // Actualizar direcciones
            var opDir = new clsOpeDireccion();
            var dirResult = opDir.ActualizarDirecciones(clienteActualizado.documento, clienteActualizado.Direccion.ToList());
            if (dirResult != "OK") return dirResult;

            obd.SaveChanges();
            return "Cliente actualizado correctamente.";
        }
        catch (Exception ex)
        {
            return "Error al actualizar cliente: " + ex.Message;
        }
    }

    // Cambiar estado de un cliente
    public string CambiarEstadoCliente(string documento, bool nuevoEstado)
    {
        try
        {
            var cliente = obd.Cliente.FirstOrDefault(c => c.documento == documento);
            if (cliente == null) return "Cliente no encontrado.";

            cliente.activo = nuevoEstado;
            obd.SaveChanges();

            return "Estado del cliente actualizado correctamente.";
        }
        catch (Exception ex)
        {
            return "Error al cambiar estado: " + ex.Message;
        }
    }

    // Listar todos los clientes
    public List<Cliente> ListarClientes()
    {
        return obd.Cliente.ToList();
    }

    // Validación centralizada
    // Validar para cuando se crea un registro
    private string ValidarDatos()
    {
        if (tbCliente == null)
            return "Datos del cliente no enviados.";

        if (string.IsNullOrWhiteSpace(tbCliente.documento) ||
            string.IsNullOrWhiteSpace(tbCliente.nombre) ||
            string.IsNullOrWhiteSpace(tbCliente.apellido) ||
            string.IsNullOrWhiteSpace(tbCliente.email))
        {
            return "Todos los campos de cliente son obligatorios.";
        }

        // Validar formato de email
        try
        {
            MailAddress m = new MailAddress(tbCliente.email);
        }
        catch
        {
            return "El formato del correo electrónico no es válido.";
        }

        if (telefonos == null || telefonos.Count == 0)
            return "Debe registrar al menos un teléfono.";

        foreach (var tel in telefonos)
        {
            if (string.IsNullOrWhiteSpace(tel.numero) || tel.id_tipo_telefono <= 0)
                return "Cada teléfono debe tener número y tipo válidos.";
        }

        if (direcciones == null || direcciones.Count == 0)
            return "Debe registrar al menos una dirección.";

        foreach (var dir in direcciones)
        {
            if (string.IsNullOrWhiteSpace(dir.descripcion) || dir.id_ciudad <= 0)
                return "Cada dirección debe tener ciudad y descripción válidas.";
        }

        return "OK";
    }

    // Validar cuando se actualiza un registro
    private string ValidarClienteBasico()
    {
        if (string.IsNullOrWhiteSpace(tbCliente.documento)) return "El documento es obligatorio.";
        if (string.IsNullOrWhiteSpace(tbCliente.nombre)) return "El nombre es obligatorio.";
        if (string.IsNullOrWhiteSpace(tbCliente.apellido)) return "El apellido es obligatorio.";
        if (string.IsNullOrWhiteSpace(tbCliente.email)) return "El email es obligatorio.";

        if (!Regex.IsMatch(tbCliente.email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return "Formato de email inválido.";

        return "OK";
    }
}
