using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit13Laboratorio
    {
        public Clit13Laboratorio()
        {
            Clit14ImgLaboratorios = new HashSet<Clit14ImgLaboratorio>();
        }

        public long IdLaboratorio { get; set; }
        public DateTime? FecNegocio { get; set; }
        public DateTime? FecRegistro { get; set; }
        public string? Hemograma { get; set; }
        public string? Urinalisis { get; set; }
        public string? PerfilRenal { get; set; }
        public string? PerfilLipidico { get; set; }
        public string? PerfilHepatico { get; set; }
        public string? PerfilTriode { get; set; }
        public string? PanelMetabolico { get; set; }
        public string? Otros { get; set; }
        public int? IdUsuario { get; set; }
        public string? TxtUsuario { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long? IdAtencion { get; set; }

        public virtual Clit03Atencion? IdAtencionNavigation { get; set; }
        public virtual ICollection<Clit14ImgLaboratorio> Clit14ImgLaboratorios { get; set; }
    }
}
