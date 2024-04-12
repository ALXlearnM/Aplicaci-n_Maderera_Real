using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert09Inversionistum
    {
        public Pert09Inversionistum()
        {
            Pret01Predios = new HashSet<Pret01Predio>();
        }

        public long IdInversionista { get; set; }
        public string? CodInversionista { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NroDoc { get; set; } = null!;
        public string? Sexo { get; set; }
        public string? TxtApePat { get; set; }
        public string? TxtApeMat { get; set; }
        public string? TxtPrimNom { get; set; }
        public string? TxtSegunNom { get; set; }
        public DateTime? FechNac { get; set; }
        public string? TxtRznScl { get; set; }
        public string? TxtDireccion1 { get; set; }
        public string? TxtDireccion2 { get; set; }
        public string? TxtReferencia { get; set; }
        public string? TxtEmail1 { get; set; }
        public string? TxtEmail2 { get; set; }
        public string? TelfFijo1 { get; set; }
        public string? TelfFijo2 { get; set; }
        public string? Celular1 { get; set; }
        public string? Celular2 { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        public virtual Sntt02TipoDocIdentidad IdTipoDocumentoNavigation { get; set; } = null!;
        public virtual ICollection<Pret01Predio> Pret01Predios { get; set; }
    }
}
