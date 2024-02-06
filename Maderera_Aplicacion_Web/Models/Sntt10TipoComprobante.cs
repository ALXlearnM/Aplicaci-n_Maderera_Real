using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt10TipoComprobante
    {
        public Sntt10TipoComprobante()
        {
            Fist01ControlNumeracions = new HashSet<Fist01ControlNumeracion>();
            Pret10Envios = new HashSet<Pret10Envio>();
            Pret11Recepcions = new HashSet<Pret11Recepcion>();
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdTipoComp { get; set; }
        public string? CodTipoComp { get; set; }
        public string? CodTipoCompPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdLocation { get; set; }
        public string? CodLocation { get; set; }
        public long? NroFinal { get; set; }
        public long? NroContador { get; set; }
        public int SnEmitoComp { get; set; }
        public int SnReciboComp { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Fist01ControlNumeracion> Fist01ControlNumeracions { get; set; }
        public virtual ICollection<Pret10Envio> Pret10Envios { get; set; }
        public virtual ICollection<Pret11Recepcion> Pret11Recepcions { get; set; }
        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}
