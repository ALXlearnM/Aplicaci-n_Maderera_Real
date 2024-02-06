using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret01Predio
    {
        public Pret01Predio()
        {
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
            Pret02Campanas = new HashSet<Pret02Campana>();
            Pret14Produccions = new HashSet<Pret14Produccion>();
            Pret17Archivos = new HashSet<Pret17Archivo>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public long IdPredio { get; set; }
        public string NroSitio { get; set; } = null!;
        public decimal Area { get; set; }
        public string UnidadCatastral { get; set; } = null!;
        public decimal NroHectareas { get; set; }
        public string Coordenadas { get; set; } = null!;
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public DateTime? FechaCompra { get; set; }
        public string? NroComprobante { get; set; }
        public long IdTipoPredio { get; set; }
        public int? IdDistrito { get; set; }
        public long? IdInversionista { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? PartidaRegistral { get; set; }

        public virtual Sntt33Distrito? IdDistritoNavigation { get; set; }
        public virtual Pert09Inversionistum? IdInversionistaNavigation { get; set; }
        public virtual Pret09TipoPredio IdTipoPredioNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
        public virtual ICollection<Pret02Campana> Pret02Campanas { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14Produccions { get; set; }
        public virtual ICollection<Pret17Archivo> Pret17Archivos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}
