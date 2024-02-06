using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pret06TipoArbol
    {
        public Pret06TipoArbol()
        {
            Pret04CampanaTipoArbols = new HashSet<Pret04CampanaTipoArbol>();
            Pret08ExtraccionDtls = new HashSet<Pret08ExtraccionDtl>();
            Pret12RecepcionDtls = new HashSet<Pret12RecepcionDtl>();
            Pret13EnvioDtls = new HashSet<Pret13EnvioDtl>();
        }

        public long IdTipoarbol { get; set; }
        public string? Txtdesc { get; set; }
        public int IdEstado { get; set; }
        public string TxtEstado { get; set; } = null!;

        public virtual ICollection<Pret04CampanaTipoArbol> Pret04CampanaTipoArbols { get; set; }
        public virtual ICollection<Pret08ExtraccionDtl> Pret08ExtraccionDtls { get; set; }
        public virtual ICollection<Pret12RecepcionDtl> Pret12RecepcionDtls { get; set; }
        public virtual ICollection<Pret13EnvioDtl> Pret13EnvioDtls { get; set; }
    }
}
