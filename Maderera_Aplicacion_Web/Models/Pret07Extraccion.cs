using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret07Extraccion
    {
        public Pret07Extraccion()
        {
            Pret08ExtraccionDtls = new HashSet<Pret08ExtraccionDtl>();
            Pret10Envios = new HashSet<Pret10Envio>();
            Pret14Produccions = new HashSet<Pret14Produccion>();
            Pret19ExtraccionEmpleados = new HashSet<Pret19ExtraccionEmpleado>();
        }

        public long IdExtraccion { get; set; }
        public long IdCampana { get; set; }
        public DateTime FechaExtraccion { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public string NroExtraccion { get; set; } = null!;
        public string TxtNumero { get; set; } = null!;
        public string TxtSerie { get; set; } = null!;
        public int NroArbolesTotal { get; set; }
        public int NroTrozosTotal { get; set; }
        public double DiamProTotal { get; set; }
        public double AltArbolProTotal { get; set; }
        public int IdEstado { get; set; }
        public string? Comentario { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public virtual Pret02Campana IdCampanaNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pret08ExtraccionDtl> Pret08ExtraccionDtls { get; set; }
        public virtual ICollection<Pret10Envio> Pret10Envios { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14Produccions { get; set; }
        public virtual ICollection<Pret19ExtraccionEmpleado> Pret19ExtraccionEmpleados { get; set; }
    }
}
