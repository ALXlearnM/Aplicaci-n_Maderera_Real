using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt08Location
    {
        public Mstt08Location()
        {
            Labt01Asistencia = new HashSet<Labt01Asistencium>();
            Pret10EnvioIdLocationNavigations = new HashSet<Pret10Envio>();
            Pret10EnvioIdLocationToNavigations = new HashSet<Pret10Envio>();
            Pret11RecepcionIdLocationNavigations = new HashSet<Pret11Recepcion>();
            Pret11RecepcionIdLocationToNavigations = new HashSet<Pret11Recepcion>();
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidoIdLocationNavigations = new HashSet<Tnst04CompEmitido>();
            Tnst04CompEmitidoIdLocationToNavigations = new HashSet<Tnst04CompEmitido>();
        }

        public int IdLocation { get; set; }
        public string? CodLocation { get; set; }
        public string TxtDesc { get; set; } = null!;
        public DateTime? FechaNegocio { get; set; }
        public string? TxtDireccion1 { get; set; }
        public string? TxtDireccion2 { get; set; }
        public string? TxtAbrev1 { get; set; }
        public string? TxtAbrev2 { get; set; }
        public string? NroRuc { get; set; }
        public string? Fono1 { get; set; }
        public string? Fono2 { get; set; }
        public string? TxtDatos1 { get; set; }
        public string? TxtDatos2 { get; set; }
        public string? TxtDatos3 { get; set; }
        public string? TxtDatos4 { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public int? SnAlmacen { get; set; }
        public int? SnLocationCurrent { get; set; }
        public int IdTipoLocation { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdDist { get; set; }

        public virtual Sntt33Distrito? IdDistNavigation { get; set; }
        public virtual Mstt09TipoLocation IdTipoLocationNavigation { get; set; } = null!;
        public virtual ICollection<Labt01Asistencium> Labt01Asistencia { get; set; }
        public virtual ICollection<Pret10Envio> Pret10EnvioIdLocationNavigations { get; set; }
        public virtual ICollection<Pret10Envio> Pret10EnvioIdLocationToNavigations { get; set; }
        public virtual ICollection<Pret11Recepcion> Pret11RecepcionIdLocationNavigations { get; set; }
        public virtual ICollection<Pret11Recepcion> Pret11RecepcionIdLocationToNavigations { get; set; }
        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidoIdLocationNavigations { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidoIdLocationToNavigations { get; set; }
    }
}
