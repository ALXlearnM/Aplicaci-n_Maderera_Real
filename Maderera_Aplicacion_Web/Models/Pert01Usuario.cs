using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert01Usuario
    {
        public Pert01Usuario()
        {
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
            Pret01PredioIdUsuarioModificadorNavigations = new HashSet<Pret01Predio>();
            Pret01PredioIdUsuarioNavigations = new HashSet<Pret01Predio>();
            Pret02CampanaIdUsuarioModificadorNavigations = new HashSet<Pret02Campana>();
            Pret02CampanaIdUsuarioNavigations = new HashSet<Pret02Campana>();
            Pret07ExtraccionIdUsuarioModificadorNavigations = new HashSet<Pret07Extraccion>();
            Pret07ExtraccionIdUsuarioNavigations = new HashSet<Pret07Extraccion>();
            Pret10EnvioIdUsuarioModificadorNavigations = new HashSet<Pret10Envio>();
            Pret10EnvioIdUsuarioNavigations = new HashSet<Pret10Envio>();
            Pret11RecepcionIdUsuarioModificadorNavigations = new HashSet<Pret11Recepcion>();
            Pret11RecepcionIdUsuarioNavigations = new HashSet<Pret11Recepcion>();
            Pret14ProduccionIdUsuarioModificadorNavigations = new HashSet<Pret14Produccion>();
            Pret14ProduccionIdUsuarioNavigations = new HashSet<Pret14Produccion>();
            Tnst01CompRecibidos = new HashSet<Tnst01CompRecibido>();
            Tnst04CompEmitidoIdUsuarioModificadorNavigations = new HashSet<Tnst04CompEmitido>();
            Tnst04CompEmitidoIdUsuarioNavigations = new HashSet<Tnst04CompEmitido>();
        }

        public long IdUsuario { get; set; }
        public string? CodUsuario { get; set; }
        public long? IdPassword { get; set; }
        public string TxtUsuario { get; set; } = null!;
        public string TxtClave { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public int SnUpdRequered { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public long IdEmpleado { get; set; }
        public virtual Pert04Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
        public virtual ICollection<Pret01Predio> Pret01PredioIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret01Predio> Pret01PredioIdUsuarioNavigations { get; set; }
        public virtual ICollection<Pret02Campana> Pret02CampanaIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret02Campana> Pret02CampanaIdUsuarioNavigations { get; set; }
        public virtual ICollection<Pret07Extraccion> Pret07ExtraccionIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret07Extraccion> Pret07ExtraccionIdUsuarioNavigations { get; set; }
        public virtual ICollection<Pret10Envio> Pret10EnvioIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret10Envio> Pret10EnvioIdUsuarioNavigations { get; set; }
        public virtual ICollection<Pret11Recepcion> Pret11RecepcionIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret11Recepcion> Pret11RecepcionIdUsuarioNavigations { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14ProduccionIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Pret14Produccion> Pret14ProduccionIdUsuarioNavigations { get; set; }
        public virtual ICollection<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidoIdUsuarioModificadorNavigations { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidoIdUsuarioNavigations { get; set; }
        [NotMapped]
        public Boolean MantenerActivo { get; set; }
    }
}
