using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit14ImgLaboratorio
    {
        public long IdImgLaboratorio { get; set; }
        public string? CodImgLaboratorio { get; set; }
        public long? IdLaboratorio { get; set; }
        public string? TxtPath { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual Clit13Laboratorio? IdLaboratorioNavigation { get; set; }
    }
}
