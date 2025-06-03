using backend_proyectando.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;


namespace backend_proyectando.Class
{
	public class clsOpeLogin
	{
		private proyectandoEntities obd = new proyectandoEntities();
		public Usuario tbUsuario { get; set; }

        public object IniciarSesion()
        {
            if (tbUsuario == null)
                return "Debe enviar los datos del usuario.";

            if (string.IsNullOrWhiteSpace(tbUsuario.username) || string.IsNullOrWhiteSpace(tbUsuario.password))
                return "El nombre de usuario y la contraseña son obligatorios.";

            var usuario = obd.Usuario.FirstOrDefault(u =>
                u.username == tbUsuario.username &&
                u.password == tbUsuario.password &&
                u.activo == true);

            if (usuario == null)
                return "Usuario o contraseña incorrectos o usuario inactivo.";

            return new
            {
                mensaje = "OK",
                documentoEmp = usuario.documentoEmp
            };
        }

        public string ActivarUsuario(string username)
        {
            var user = obd.Usuario.FirstOrDefault(u => u.username == username);
            if (user == null)
                return "Usuario no encontrado.";

            user.activo = true;
            obd.SaveChanges();
            return "Usuario activado correctamente.";
        }

    }
}