using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt04CanalVtum
    {
        public Mstt04CanalVtum()
        {
            Fist01ControlNumeracions = new HashSet<Fist01ControlNumeracion>();
            Labt01Asistencia = new HashSet<Labt01Asistencium>();
            Mstt14Mesas = new HashSet<Mstt14Mesa>();
            Tnst04CompEmitidos = new HashSet<Tnst04CompEmitido>();
        }

        public int IdCanVta { get; set; }
        public string? CodCanVta { get; set; }
        public string? TxtAbrv { get; set; }
        public string? TxtDesc { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Fist01ControlNumeracion> Fist01ControlNumeracions { get; set; }
        public virtual ICollection<Labt01Asistencium> Labt01Asistencia { get; set; }
        public virtual ICollection<Mstt14Mesa> Mstt14Mesas { get; set; }
        public virtual ICollection<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; }
    }
}
