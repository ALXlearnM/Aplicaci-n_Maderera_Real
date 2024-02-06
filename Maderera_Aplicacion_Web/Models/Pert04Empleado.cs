using System;
using System.Collections.Generic;

namespace Maderera_Aplicacion_Web.Models
{
    public partial class Pert04Empleado
    {
        public Pert04Empleado()
        {
            Clit03Atencions = new HashSet<Clit03Atencion>();
            Clit04AtencionDtls = new HashSet<Clit04AtencionDtl>();
            Clit19Cita = new HashSet<Clit19Citum>();
            Csht01CajaDtlIdEmpAutorizadorNavigations = new HashSet<Csht01CajaDtl>();
            Csht01CajaDtlIdEmpleadoNavigations = new HashSet<Csht01CajaDtl>();
            Labt01Asistencia = new HashSet<Labt01Asistencium>();
            Labt02AsistenciaAjustada = new HashSet<Labt02AsistenciaAjustadum>();
            Labt03HorarioEmps = new HashSet<Labt03HorarioEmp>();
            Labt07EmpTrabajos = new HashSet<Labt07EmpTrabajo>();
            Pert01Usuarios = new HashSet<Pert01Usuario>();
            Pert11PagoPersonals = new HashSet<Pert11PagoPersonal>();
            Pret19ExtraccionEmpleados = new HashSet<Pret19ExtraccionEmpleado>();
            Pret20ProduccionEmpleados = new HashSet<Pret20ProduccionEmpleado>();
            Pret21EnvioEmpleados = new HashSet<Pret21EnvioEmpleado>();
            Pret22RecepcionEmpleados = new HashSet<Pret22RecepcionEmpleado>();
            Pret23VentaEmpleados = new HashSet<Pret23VentaEmpleado>();
            Tnst08DescuentoDtls = new HashSet<Tnst08DescuentoDtl>();
        }

        public long IdEmpleado { get; set; }
        public string? CodEmpleado { get; set; }
        public string CodTipoPer { get; set; } = null!;
        public int? IdTipoDocIdentidad { get; set; }
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
        public int? IdEstadoCivil { get; set; }
        public int? IdVia { get; set; }
        public string? NomVia { get; set; }
        public string? NroVia { get; set; }
        public int? IdZona { get; set; }
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
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }
        public decimal? SalarioMensual { get; set; }
        public decimal? SalarioQuincenal { get; set; }
        public decimal? SalarioHora { get; set; }
        public decimal? NroHoraMes { get; set; }
        public string? NroCuenta { get; set; }
        public int? IdEstado { get; set; }
        public string? TxtEstado { get; set; }
        public int? IdDist { get; set; }
        public int? IdNacionalidad { get; set; }
        public int? IdEntidadFinanciera { get; set; }
        public int? IdTipoTrabajador { get; set; }
        public int? IdSituacionEducativa { get; set; }
        public int? IdOcupacion { get; set; }
        public int? IdRegimenPensionario { get; set; }
        public int? IdCondicionLaboral { get; set; }
        public int? IdPeriodoRemuneracion { get; set; }
        public int? IdSaludEps { get; set; }
        public int? IdSituacion { get; set; }
        public int? IdMotivoBaja { get; set; }
        public int? IdModalidadFormativa { get; set; }
        public int? IdSuspencionLaboral { get; set; }
        public int? IdRegimenSalud { get; set; }
        public int? IdRegimenLaboral { get; set; }
        public int? IdEspecialidadMedica { get; set; }
        public int? IdCategoriaEmp { get; set; }
        public int? IdClaseEmp { get; set; }

        public virtual Pert05CategoriaEmp? IdCategoriaEmpNavigation { get; set; }
        public virtual Pert06ClaseEmp? IdClaseEmpNavigation { get; set; }
        public virtual Sntt21CondicionLaboral? IdCondicionLaboralNavigation { get; set; }
        public virtual Sntt33Distrito? IdDistNavigation { get; set; }
        public virtual Sntt03EntidadFinanciera? IdEntidadFinancieraNavigation { get; set; }
        public virtual Clit07EspecialidadMedica? IdEspecialidadMedicaNavigation { get; set; }
        public virtual Mstt07EstadoCivil? IdEstadoCivilNavigation { get; set; }
        public virtual Sntt26ModalidadFormativa? IdModalidadFormativaNavigation { get; set; }
        public virtual Sntt25MotivoBaja? IdMotivoBajaNavigation { get; set; }
        public virtual Sntt14Nacionalidad? IdNacionalidadNavigation { get; set; }
        public virtual Sntt19Ocupacion? IdOcupacionNavigation { get; set; }
        public virtual Sntt22PeriodoRemuneracion? IdPeriodoRemuneracionNavigation { get; set; }
        public virtual Sntt30RegimenLaboral? IdRegimenLaboralNavigation { get; set; }
        public virtual Sntt20RegimenPensionario? IdRegimenPensionarioNavigation { get; set; }
        public virtual Sntt29RegimenSalud? IdRegimenSaludNavigation { get; set; }
        public virtual Sntt23SaludEp? IdSaludEpsNavigation { get; set; }
        public virtual Sntt18SituacionEducativa? IdSituacionEducativaNavigation { get; set; }
        public virtual Sntt24Situacion? IdSituacionNavigation { get; set; }
        public virtual Sntt28SuspencionLaboral? IdSuspencionLaboralNavigation { get; set; }
        public virtual Sntt02TipoDocIdentidad? IdTipoDocIdentidadNavigation { get; set; }
        public virtual Sntt17TipoTrabajador? IdTipoTrabajadorNavigation { get; set; }
        public virtual Sntt15Vium? IdViaNavigation { get; set; }
        public virtual Sntt16Zona? IdZonaNavigation { get; set; }
        public virtual ICollection<Clit03Atencion> Clit03Atencions { get; set; }
        public virtual ICollection<Clit04AtencionDtl> Clit04AtencionDtls { get; set; }
        public virtual ICollection<Clit19Citum> Clit19Cita { get; set; }
        public virtual ICollection<Csht01CajaDtl> Csht01CajaDtlIdEmpAutorizadorNavigations { get; set; }
        public virtual ICollection<Csht01CajaDtl> Csht01CajaDtlIdEmpleadoNavigations { get; set; }
        public virtual ICollection<Labt01Asistencium> Labt01Asistencia { get; set; }
        public virtual ICollection<Labt02AsistenciaAjustadum> Labt02AsistenciaAjustada { get; set; }
        public virtual ICollection<Labt03HorarioEmp> Labt03HorarioEmps { get; set; }
        public virtual ICollection<Labt07EmpTrabajo> Labt07EmpTrabajos { get; set; }
        public virtual ICollection<Pert01Usuario> Pert01Usuarios { get; set; }
        public virtual ICollection<Pert11PagoPersonal> Pert11PagoPersonals { get; set; }
        public virtual ICollection<Pret19ExtraccionEmpleado> Pret19ExtraccionEmpleados { get; set; }
        public virtual ICollection<Pret20ProduccionEmpleado> Pret20ProduccionEmpleados { get; set; }
        public virtual ICollection<Pret21EnvioEmpleado> Pret21EnvioEmpleados { get; set; }
        public virtual ICollection<Pret22RecepcionEmpleado> Pret22RecepcionEmpleados { get; set; }
        public virtual ICollection<Pret23VentaEmpleado> Pret23VentaEmpleados { get; set; }
        public virtual ICollection<Tnst08DescuentoDtl> Tnst08DescuentoDtls { get; set; }
    }
}
