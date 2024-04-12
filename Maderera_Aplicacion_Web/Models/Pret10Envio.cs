using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret10Envio
    {
        public Pret10Envio()
        {
            Pret11Recepcions = new HashSet<Pret11Recepcion>();
            Pret13EnvioDtls = new HashSet<Pret13EnvioDtl>();
            Pret17Archivos = new HashSet<Pret17Archivo>();
            Pret21EnvioEmpleados = new HashSet<Pret21EnvioEmpleado>();
        }

        public long IdEnvio { get; set; }
        public int IdLocation { get; set; }
        public int IdLocationTo { get; set; }
        public string NroEnvio { get; set; } = null!;
        public string TxtNro { get; set; } = null!;
        public string TxtSerie { get; set; } = null!;
        public long? IdExtraccion { get; set; }
        public int EnvioCant { get; set; }
        public DateTime FechaEnvio { get; set; }
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
        public string TipoEnvio { get; set; } = null!;
        public int IdTipoComp { get; set; }
        public string? NroPlaca { get; set; }
        public int? Post { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Pret07Extraccion? IdExtraccionNavigation { get; set; }
        public virtual Mstt08Location IdLocationNavigation { get; set; } = null!;
        public virtual Mstt08Location IdLocationToNavigation { get; set; } = null!;
        public virtual Sntt10TipoComprobante IdTipoCompNavigation { get; set; } = null!;
        public virtual Pert01Usuario? IdUsuarioModificadorNavigation { get; set; }
        public virtual Pert01Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Pret11Recepcion> Pret11Recepcions { get; set; }
        public virtual ICollection<Pret13EnvioDtl> Pret13EnvioDtls { get; set; }
        public virtual ICollection<Pret17Archivo> Pret17Archivos { get; set; }
        public virtual ICollection<Pret21EnvioEmpleado> Pret21EnvioEmpleados { get; set; }
    }
}
