using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret11Recepcion
    {
        public Pret11Recepcion()
        {
            Pret12RecepcionDtls = new HashSet<Pret12RecepcionDtl>();
            Pret17Archivos = new HashSet<Pret17Archivo>();
            Pret22RecepcionEmpleados = new HashSet<Pret22RecepcionEmpleado>();
        }

        public long IdRecepcion { get; set; }
        public long? IdEnvio { get; set; }
        public int RecepcionCant { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long IdUsuario { get; set; }
        public long? IdUsuarioModificador { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string? TxtUsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? NroGuia { get; set; }
        public string? NroGuiaTransp { get; set; }
        public string? Comentario { get; set; }
        public int? IdTipoComp { get; set; }
        public string? NroPalaca { get; set; }
        public string? NroRecepcion { get; set; }
        public string? TxtNro { get; set; }
        public string? TxtSerie { get; set; }
        public int? IdLocation { get; set; }
        public int? IdLocationTo { get; set; }
        public int? Post { get; set; }
        public DateTime? PostDate { get; set; }

        public virtual Pret10Envio? IdEnvioNavigation { get; set; }
        public virtual Mstt08Location? IdLocationNavigation { get; set; }
        public virtual Mstt08Location? IdLocationToNavigation { get; set; }
        public virtual Sntt10TipoComprobante? IdTipoCompNavigation { get; set; }
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pret12RecepcionDtl> Pret12RecepcionDtls { get; set; }
        public virtual ICollection<Pret17Archivo> Pret17Archivos { get; set; }
        public virtual ICollection<Pret22RecepcionEmpleado> Pret22RecepcionEmpleados { get; set; }
    }
}
