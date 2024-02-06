using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert02Cliente
    {
        public Pert02Cliente()
        {
            Clit01Pacientes = new HashSet<Clit01Paciente>();
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public long IdCliente { get; set; }
        public string? CodCliente { get; set; }
        public string CodTipoPer { get; set; } = null!;
        public string? NroDoc { get; set; }
        public string? NroRuc { get; set; }
        public string? Sexo { get; set; }
        public string? TxtApePat { get; set; }
        public string? TxtApeMat { get; set; }
        public string? TxtPriNom { get; set; }
        public string? TxtSegNom { get; set; }
        public DateTime? FecNac { get; set; }
        public string? TxtRznSocial { get; set; }
        public string? TxtNomComercial { get; set; }
        public string? NomVia { get; set; }
        public string? NroVia { get; set; }
        public string? NomZona { get; set; }
        public string? TxtDireccion1 { get; set; }
        public string? TxtDireccion2 { get; set; }
        public string? TxtReferencia { get; set; }
        public string? TxtEmail1 { get; set; }
        public string? TxtEmail2 { get; set; }
        public string? TxtWeb { get; set; }
        public string? TelefFijo1 { get; set; }
        public string? TelefFijo2 { get; set; }
        public string? TelefFijo3 { get; set; }
        public string? Celular1 { get; set; }
        public string? Celular2 { get; set; }
        public string? Celular3 { get; set; }
        public string? UrlImg { get; set; }
        public string? Info01 { get; set; }
        public string? Info02 { get; set; }
        public string? Info03 { get; set; }
        public string? Info04 { get; set; }
        public string? Info05 { get; set; }
        public string? Info06 { get; set; }
        public string? Info07 { get; set; }
        public string? Info08 { get; set; }
        public string? Info09 { get; set; }
        public string? Info10 { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdVia { get; set; }
        public int? IdZona { get; set; }
        public int? IdDist { get; set; }
        public int? IdTipoDocIdentidad { get; set; }
        public int? IdNacionalidad { get; set; }

        public virtual Sntt33Distrito? IdDistNavigation { get; set; }
        public virtual Mstt07EstadoCivil? IdEstadoCivilNavigation { get; set; }
        public virtual Sntt14Nacionalidad? IdNacionalidadNavigation { get; set; }
        public virtual Sntt02TipoDocIdentidad? IdTipoDocIdentidadNavigation { get; set; }
        public virtual Sntt15Vium? IdViaNavigation { get; set; }
        public virtual Sntt16Zona? IdZonaNavigation { get; set; }
        public virtual ICollection<Clit01Paciente> Clit01Pacientes { get; set; }
        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}
