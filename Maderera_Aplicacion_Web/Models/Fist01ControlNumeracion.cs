using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Fist01ControlNumeracion
    {
        public long IdControlNumeracion { get; set; }
        public int? IdCaja { get; set; }
        public int? IdCanVta { get; set; }
        public int IdNivel { get; set; }
        public int IdTipoNumeracion { get; set; }
        public int IdTipoComp { get; set; }
        public string? TxtNroSerie { get; set; }
        public long? NroInicial { get; set; }
        public long? NroFinal { get; set; }
        public long? NroActual { get; set; }
        public string? TxtSerieImpresora { get; set; }
        public string? TxtInfo01 { get; set; }
        public string? TxtInfo02 { get; set; }
        public DateTime? Fecha01 { get; set; }
        public DateTime? Fecha02 { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? LockedBy { get; set; }
        public int IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Mstt12Caja? IdCajaNavigation { get; set; }
        public virtual Mstt04CanalVtum? IdCanVtaNavigation { get; set; }
        public virtual Fist02Nivel IdNivelNavigation { get; set; } = null!;
        public virtual Sntt10TipoComprobante IdTipoCompNavigation { get; set; } = null!;
        public virtual Fist03TipoNumeracion IdTipoNumeracionNavigation { get; set; } = null!;
    }
}
