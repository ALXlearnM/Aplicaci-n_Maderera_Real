using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret02Campana
    {
        public Pret02Campana()
        {
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
            Pret04CampanaTipoArbols = new HashSet<Pret04CampanaTipoArbol>();
            Pret07Extraccions = new HashSet<Pret07Extraccion>();
            Pret14Produccions = new HashSet<Pret14Produccion>();
            Pret16Mermas = new HashSet<Pret16Merma>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public long IdCampana { get; set; }
        public long IdPredio { get; set; }
        public long IdTipoCampana { get; set; }
        public string? CodigoCampana { get; set; }
        public double Area { get; set; }
        public int NroArboles { get; set; }
        public string Coordenadas { get; set; } = null!;
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFina { get; set; }
        public int IdDistrito { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal NroHectarea { get; set; }

        public virtual Sntt33Distrito IdDistritoNavigation { get; set; } = null!;
        public virtual Pret01Predio IdPredioNavigation { get; set; } = null!;
        public virtual Pret03TipoCampana IdTipoCampanaNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
        public virtual ICollection<Pret04CampanaTipoArbol> Pret04CampanaTipoArbols { get; set; }
        public virtual ICollection<Pret07Extraccion> Pret07Extraccions { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14Produccions { get; set; }
        public virtual ICollection<Pret16Merma> Pret16Mermas { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}
