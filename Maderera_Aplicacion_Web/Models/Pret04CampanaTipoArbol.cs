using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret04CampanaTipoArbol
    {
        public Pret04CampanaTipoArbol()
        {
            Pret05CtrlCalidads = new HashSet<Pret05CtrlCalidad>();
        }

        public long IdCampanaTipoarbol { get; set; }
        public long IdCampana { get; set; }
        public long IdTipoarbol { get; set; }
        public string TxtTipoarbol { get; set; } = null!;
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public int NroArboles { get; set; }
        public double Area { get; set; }
        public string Coordenadas { get; set; } = null!;
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public int NroHectareas { get; set; }

        public virtual Pret02Campana IdCampanaNavigation { get; set; } = null!;
        public virtual Pret06TipoArbol IdTipoarbolNavigation { get; set; } = null!;
        public virtual ICollection<Pret05CtrlCalidad> Pret05CtrlCalidads { get; set; }
    }
}
