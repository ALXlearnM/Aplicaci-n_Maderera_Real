using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Mstt02Descuento
    {
        public Mstt02Descuento()
        {
            Tnst08DescuentoDtls = new HashSet<Tnst08DescuentoDtl>();
        }

        public int IdDescuento { get; set; }
        public string? CodDescuento { get; set; }
        public string? TxtDesc { get; set; }
        public string? TipoDescuento { get; set; }
        public decimal? Porcentaje { get; set; }
        public decimal? Monto { get; set; }
        public decimal? MontoMin { get; set; }
        public decimal? MontoMax { get; set; }
        public int SnDsctoPorc { get; set; }
        public int SnDsctoPorcAbierto { get; set; }
        public int SnDsctoMto { get; set; }
        public int SnDsctoMtoAbierto { get; set; }
        public int? SnDescuenPeriodo { get; set; }
        public int? SnDescuenDia { get; set; }
        public DateTime? P1FechaIni { get; set; }
        public DateTime? P1FechaFin { get; set; }
        public TimeSpan? P1HoraIni { get; set; }
        public TimeSpan? P1HoraFin { get; set; }
        public DateTime? P2FechaIni { get; set; }
        public DateTime? P2FechaFin { get; set; }
        public TimeSpan? P2HoraIni { get; set; }
        public TimeSpan? P2HoraFin { get; set; }
        public DateTime? P3FechaIni { get; set; }
        public DateTime? P3FechaFin { get; set; }
        public TimeSpan? P3HoraIni { get; set; }
        public TimeSpan? P3HoraFin { get; set; }
        public DateTime? P4FechaIni { get; set; }
        public DateTime? P4FechaFin { get; set; }
        public TimeSpan? P4HoraIni { get; set; }
        public TimeSpan? P4HoraFin { get; set; }
        public DateTime? P5FechaIni { get; set; }
        public DateTime? P5FechaFin { get; set; }
        public TimeSpan? P5HoraIni { get; set; }
        public TimeSpan? P5HoraFin { get; set; }
        public DateTime? P6FechaIni { get; set; }
        public DateTime? P6FechaFin { get; set; }
        public TimeSpan? P6HoraIni { get; set; }
        public TimeSpan? P6HoraFin { get; set; }
        public DateTime? P7FechaIni { get; set; }
        public DateTime? P7FechaFin { get; set; }
        public TimeSpan? P7HoraIni { get; set; }
        public TimeSpan? P7HoraFin { get; set; }
        public int? SnDomingo { get; set; }
        public int? SnLunes { get; set; }
        public int? SnMartes { get; set; }
        public int? SnMiercoles { get; set; }
        public int? SnJueves { get; set; }
        public int? SnViernes { get; set; }
        public int? SnSabado { get; set; }
        public TimeSpan? DomHoraIni { get; set; }
        public TimeSpan? DomHoraFin { get; set; }
        public TimeSpan? LunHoraIni { get; set; }
        public TimeSpan? LunHoraFin { get; set; }
        public TimeSpan? MarHoraIni { get; set; }
        public TimeSpan? MarHoraFin { get; set; }
        public TimeSpan? MieHoraIni { get; set; }
        public TimeSpan? MieHoraFin { get; set; }
        public TimeSpan? JueHoraIni { get; set; }
        public TimeSpan? JueHoraFin { get; set; }
        public TimeSpan? VieHoraIni { get; set; }
        public TimeSpan? VieHoraFin { get; set; }
        public TimeSpan? SabHoraIni { get; set; }
        public TimeSpan? SabHoraFin { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }

        public virtual ICollection<Tnst08DescuentoDtl> Tnst08DescuentoDtls { get; set; }
    }
}
