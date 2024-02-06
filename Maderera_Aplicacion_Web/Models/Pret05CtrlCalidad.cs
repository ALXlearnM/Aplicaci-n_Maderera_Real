using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret05CtrlCalidad
    {
        public long IdCtrlCalidad { get; set; }
        public long IdCampanaTipoarbol { get; set; }
        public string Defecto { get; set; } = null!;
        public long IdTipoMo { get; set; }
        public string MotivoRechazo { get; set; } = null!;
        public string Indicaciones { get; set; } = null!;
        public string? MaderaApta { get; set; }
        public string? MaderaObservada { get; set; }
        public string? MaderaRechazada { get; set; }
        public DateTime FechaRevision { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual Pret04CampanaTipoArbol IdCampanaTipoarbolNavigation { get; set; } = null!;
        public virtual Pret18TipoMotivo IdTipoMoNavigation { get; set; } = null!;
    }
}
