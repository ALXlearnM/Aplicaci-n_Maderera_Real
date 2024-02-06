using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit02Actividad
    {
        public Clit02Actividad()
        {
            Clit03Atencions = new HashSet<Clit03Atencion>();
            Clit04AtencionDtls = new HashSet<Clit04AtencionDtl>();
        }

        public long IdActividad { get; set; }
        public string? CodActividad { get; set; }
        public string? TxtDesc { get; set; }
        public string? TxtObservacion { get; set; }
        public int? NroOrden { get; set; }
        public int? IdActividadPrev { get; set; }
        public int? IdActividadNext { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Clit03Atencion> Clit03Atencions { get; set; }
        public virtual ICollection<Clit04AtencionDtl> Clit04AtencionDtls { get; set; }
    }
}
