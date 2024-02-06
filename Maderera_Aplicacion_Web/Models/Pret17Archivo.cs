using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret17Archivo
    {
        public long IdArchivo { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string RutaArchivo { get; set; } = null!;
        public string TipoArchivo { get; set; } = null!;
        public int TamanoArchivo { get; set; }
        public DateTime FechaCargaArchivo { get; set; }
        public long IdTipoDir { get; set; }
        public long? IdPredio { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;
        public long? IdEnvio { get; set; }
        public long? IdRecepcion { get; set; }

        public virtual Pret10Envio? IdEnvioNavigation { get; set; }
        public virtual Pret01Predio? IdPredioNavigation { get; set; }
        public virtual Pret11Recepcion? IdRecepcionNavigation { get; set; }
        public virtual Pret16TipoDireccion IdTipoDirNavigation { get; set; } = null!;
    }
}
