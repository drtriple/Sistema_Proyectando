//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace backend_proyectando.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleControl
    {
        public long idDetalleControl { get; set; }
        public long id_control { get; set; }
        public long id_fase_proceso { get; set; }
        public int horas { get; set; }
        public string comentarios { get; set; }
    
        public virtual Control Control { get; set; }
        public virtual FaseProceso FaseProceso { get; set; }
    }
}
