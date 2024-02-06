using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Clit01Paciente
    {
        public Clit01Paciente()
        {
            Clit03Atencions = new HashSet<Clit03Atencion>();
            Clit05AnteceAlergia = new HashSet<Clit05AnteceAlergium>();
            Clit19Cita = new HashSet<Clit19Citum>();
        }

        public long IdPaciente { get; set; }
        public string? CodPaciente { get; set; }
        public string? NroHistClinica { get; set; }
        public string? GrupoSanguineo { get; set; }
        public string? Observaciones { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdOcupacion { get; set; }
        public int? IdSaludEps { get; set; }
        public int? IdRegimenSalud { get; set; }
        public long? IdCliente { get; set; }

        public virtual Pert02Cliente? IdClienteNavigation { get; set; }
        public virtual Sntt19Ocupacion? IdOcupacionNavigation { get; set; }
        public virtual Sntt29RegimenSalud? IdRegimenSaludNavigation { get; set; }
        public virtual Sntt23SaludEp? IdSaludEpsNavigation { get; set; }
        public virtual ICollection<Clit03Atencion> Clit03Atencions { get; set; }
        public virtual ICollection<Clit05AnteceAlergium> Clit05AnteceAlergia { get; set; }
        public virtual ICollection<Clit19Citum> Clit19Cita { get; set; }
    }
}
