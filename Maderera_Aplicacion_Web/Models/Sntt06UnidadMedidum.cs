using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Sntt06UnidadMedidum
    {
        public Sntt06UnidadMedidum()
        {
            Pret14Produccions = new HashSet<Pret14Produccion>();
            Pret15ProduccionDtls = new HashSet<Pret15ProduccionDtl>();
            Pret17MermaDtls = new HashSet<Pret17MermaDtl>();
            Prot09Productos = new HashSet<Prot09Producto>();
            Prot11RecetaDtls = new HashSet<Prot11RecetaDtl>();
            Prot18Productocoms = new HashSet<Prot18Productocom>();
            Tnst02CompRecibidoDtls = new HashSet<Tnst02CompRecibidoDtl>();
            Tnst05CompEmitidoDtls = new HashSet<Tnst05CompEmitidoDtl>();
        }

        public int IdUm { get; set; }
        public string? CodUm { get; set; }
        public string? CodUmPle { get; set; }
        public string? TxtAbrv { get; set; }
        public string TxtDesc { get; set; } = null!;
        public string? TxtUnidBase { get; set; }
        public int? IdUmBase { get; set; }
        public string? TxtOperacion { get; set; }
        public decimal? DecFactor { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Pret14Produccion> Pret14Produccions { get; set; }
        public virtual ICollection<Pret15ProduccionDtl> Pret15ProduccionDtls { get; set; }
        public virtual ICollection<Pret17MermaDtl> Pret17MermaDtls { get; set; }
        public virtual ICollection<Prot09Producto> Prot09Productos { get; set; }
        public virtual ICollection<Prot11RecetaDtl> Prot11RecetaDtls { get; set; }
        public virtual ICollection<Prot18Productocom> Prot18Productocoms { get; set; }
        public virtual ICollection<Tnst02CompRecibidoDtl> Tnst02CompRecibidoDtls { get; set; }
        public virtual ICollection<Tnst05CompEmitidoDtl> Tnst05CompEmitidoDtls { get; set; }
    }
}
