using System;
using System.Collections.Generic;
using Maderera_Aplicacion_Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Maderera_Aplicacion_Web.Data
{
    public partial class EagleContext : DbContext
    {
        public EagleContext()
        {
        }

        public EagleContext(DbContextOptions<EagleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Clit01Paciente> Clit01Pacientes { get; set; } = null!;
        public virtual DbSet<Clit02Actividad> Clit02Actividads { get; set; } = null!;
        public virtual DbSet<Clit03Atencion> Clit03Atencions { get; set; } = null!;
        public virtual DbSet<Clit04AtencionDtl> Clit04AtencionDtls { get; set; } = null!;
        public virtual DbSet<Clit05AnteceAlergium> Clit05AnteceAlergia { get; set; } = null!;
        public virtual DbSet<Clit06TipoEspecialidad> Clit06TipoEspecialidads { get; set; } = null!;
        public virtual DbSet<Clit07EspecialidadMedica> Clit07EspecialidadMedicas { get; set; } = null!;
        public virtual DbSet<Clit08FuncionesVitale> Clit08FuncionesVitales { get; set; } = null!;
        public virtual DbSet<Clit09Anamnesi> Clit09Anamneses { get; set; } = null!;
        public virtual DbSet<Clit10ExamenFisico> Clit10ExamenFisicos { get; set; } = null!;
        public virtual DbSet<Clit11EstudioComplementario> Clit11EstudioComplementarios { get; set; } = null!;
        public virtual DbSet<Clit12ArchivoComplementario> Clit12ArchivoComplementarios { get; set; } = null!;
        public virtual DbSet<Clit13Laboratorio> Clit13Laboratorios { get; set; } = null!;
        public virtual DbSet<Clit14ImgLaboratorio> Clit14ImgLaboratorios { get; set; } = null!;
        public virtual DbSet<Clit15Diagnostico> Clit15Diagnosticos { get; set; } = null!;
        public virtual DbSet<Clit16Cie10> Clit16Cie10s { get; set; } = null!;
        public virtual DbSet<Clit17Tratamiento> Clit17Tratamientos { get; set; } = null!;
        public virtual DbSet<Clit18Evolucion> Clit18Evolucions { get; set; } = null!;
        public virtual DbSet<Clit19Citum> Clit19Cita { get; set; } = null!;
        public virtual DbSet<Csht01CajaDtl> Csht01CajaDtls { get; set; } = null!;
        public virtual DbSet<Fist01ControlNumeracion> Fist01ControlNumeracions { get; set; } = null!;
        public virtual DbSet<Fist02Nivel> Fist02Nivels { get; set; } = null!;
        public virtual DbSet<Fist03TipoNumeracion> Fist03TipoNumeracions { get; set; } = null!;
        public virtual DbSet<Fist04ParametroFiscal> Fist04ParametroFiscals { get; set; } = null!;
        public virtual DbSet<Fist05ConfiguracionFiscalCaja> Fist05ConfiguracionFiscalCajas { get; set; } = null!;
        public virtual DbSet<Grlt01Parametro> Grlt01Parametros { get; set; } = null!;
        public virtual DbSet<Grlt02Estado> Grlt02Estados { get; set; } = null!;
        public virtual DbSet<Grlt03Horario> Grlt03Horarios { get; set; } = null!;
        public virtual DbSet<Grlt04ConfiguracionCaja> Grlt04ConfiguracionCajas { get; set; } = null!;
        public virtual DbSet<Labt01Asistencium> Labt01Asistencia { get; set; } = null!;
        public virtual DbSet<Labt02AsistenciaAjustadum> Labt02AsistenciaAjustada { get; set; } = null!;
        public virtual DbSet<Labt03HorarioEmp> Labt03HorarioEmps { get; set; } = null!;
        public virtual DbSet<Labt04HorarioEmpDtl> Labt04HorarioEmpDtls { get; set; } = null!;
        public virtual DbSet<Labt05AsistenciaTempLast> Labt05AsistenciaTempLasts { get; set; } = null!;
        public virtual DbSet<Labt06Trabajo> Labt06Trabajos { get; set; } = null!;
        public virtual DbSet<Labt07EmpTrabajo> Labt07EmpTrabajos { get; set; } = null!;
        public virtual DbSet<Mstt01MedioPago> Mstt01MedioPagos { get; set; } = null!;
        public virtual DbSet<Mstt02Descuento> Mstt02Descuentos { get; set; } = null!;
        public virtual DbSet<Mstt03TipoOrden> Mstt03TipoOrdens { get; set; } = null!;
        public virtual DbSet<Mstt04CanalVtum> Mstt04CanalVta { get; set; } = null!;
        public virtual DbSet<Mstt05Razon> Mstt05Razons { get; set; } = null!;
        public virtual DbSet<Mstt06Impuesto> Mstt06Impuestos { get; set; } = null!;
        public virtual DbSet<Mstt07EstadoCivil> Mstt07EstadoCivils { get; set; } = null!;
        public virtual DbSet<Mstt08Location> Mstt08Locations { get; set; } = null!;
        public virtual DbSet<Mstt09TipoLocation> Mstt09TipoLocations { get; set; } = null!;
        public virtual DbSet<Mstt10Impresora> Mstt10Impresoras { get; set; } = null!;
        public virtual DbSet<Mstt11TipoImpresora> Mstt11TipoImpresoras { get; set; } = null!;
        public virtual DbSet<Mstt12Caja> Mstt12Cajas { get; set; } = null!;
        public virtual DbSet<Mstt13Turno> Mstt13Turnos { get; set; } = null!;
        public virtual DbSet<Mstt14Mesa> Mstt14Mesas { get; set; } = null!;
        public virtual DbSet<Mstt15EstadoMesa> Mstt15EstadoMesas { get; set; } = null!;
        public virtual DbSet<Mstt16TipoRazon> Mstt16TipoRazons { get; set; } = null!;
        public virtual DbSet<Pert010Condicion> Pert010Condicions { get; set; } = null!;
        public virtual DbSet<Pert01Usuario> Pert01Usuarios { get; set; } = null!;
        public virtual DbSet<Pert02Cliente> Pert02Clientes { get; set; } = null!;
        public virtual DbSet<Pert03Proveedor> Pert03Proveedors { get; set; } = null!;
        public virtual DbSet<Pert04Empleado> Pert04Empleados { get; set; } = null!;
        public virtual DbSet<Pert05CategoriaEmp> Pert05CategoriaEmps { get; set; } = null!;
        public virtual DbSet<Pert06ClaseEmp> Pert06ClaseEmps { get; set; } = null!;
        public virtual DbSet<Pert07AccessItem> Pert07AccessItems { get; set; } = null!;
        public virtual DbSet<Pert08SecurityAccess> Pert08SecurityAccesses { get; set; } = null!;
        public virtual DbSet<Pert09Inversionistum> Pert09Inversionista { get; set; } = null!;
        public virtual DbSet<Pert10Concepto> Pert10Conceptos { get; set; } = null!;
        public virtual DbSet<Pert11PagoPersonal> Pert11PagoPersonals { get; set; } = null!;
        public virtual DbSet<Pret01Predio> Pret01Predios { get; set; } = null!;
        public virtual DbSet<Pret02Campana> Pret02Campanas { get; set; } = null!;
        public virtual DbSet<Pret03TipoCampana> Pret03TipoCampanas { get; set; } = null!;
        public virtual DbSet<Pret04CampanaTipoArbol> Pret04CampanaTipoArbols { get; set; } = null!;
        public virtual DbSet<Pret05CtrlCalidad> Pret05CtrlCalidads { get; set; } = null!;
        public virtual DbSet<Pret06TipoArbol> Pret06TipoArbols { get; set; } = null!;
        public virtual DbSet<Pret07Extraccion> Pret07Extraccions { get; set; } = null!;
        public virtual DbSet<Pret08ExtraccionDtl> Pret08ExtraccionDtls { get; set; } = null!;
        public virtual DbSet<Pret09TipoPredio> Pret09TipoPredios { get; set; } = null!;
        public virtual DbSet<Pret10Envio> Pret10Envios { get; set; } = null!;
        public virtual DbSet<Pret11Recepcion> Pret11Recepcions { get; set; } = null!;
        public virtual DbSet<Pret12RecepcionDtl> Pret12RecepcionDtls { get; set; } = null!;
        public virtual DbSet<Pret13EnvioDtl> Pret13EnvioDtls { get; set; } = null!;
        public virtual DbSet<Pret14Produccion> Pret14Produccions { get; set; } = null!;
        public virtual DbSet<Pret15ProduccionDtl> Pret15ProduccionDtls { get; set; } = null!;
        public virtual DbSet<Pret16Merma> Pret16Mermas { get; set; } = null!;
        public virtual DbSet<Pret16TipoDireccion> Pret16TipoDireccions { get; set; } = null!;
        public virtual DbSet<Pret17Archivo> Pret17Archivos { get; set; } = null!;
        public virtual DbSet<Pret17MermaDtl> Pret17MermaDtls { get; set; } = null!;
        public virtual DbSet<Pret18TipoMotivo> Pret18TipoMotivos { get; set; } = null!;
        public virtual DbSet<Pret19ExtraccionEmpleado> Pret19ExtraccionEmpleados { get; set; } = null!;
        public virtual DbSet<Pret20ProduccionEmpleado> Pret20ProduccionEmpleados { get; set; } = null!;
        public virtual DbSet<Pret21EnvioEmpleado> Pret21EnvioEmpleados { get; set; } = null!;
        public virtual DbSet<Pret22RecepcionEmpleado> Pret22RecepcionEmpleados { get; set; } = null!;
        public virtual DbSet<Pret23VentaEmpleado> Pret23VentaEmpleados { get; set; } = null!;
        public virtual DbSet<Prot01Marca> Prot01Marcas { get; set; } = null!;
        public virtual DbSet<Prot02Modelo> Prot02Modelos { get; set; } = null!;
        public virtual DbSet<Prot03Familium> Prot03Familia { get; set; } = null!;
        public virtual DbSet<Prot04Subfamilium> Prot04Subfamilia { get; set; } = null!;
        public virtual DbSet<Prot05GrupoProd> Prot05GrupoProds { get; set; } = null!;
        public virtual DbSet<Prot06ClaseProd> Prot06ClaseProds { get; set; } = null!;
        public virtual DbSet<Prot07TipoProd> Prot07TipoProds { get; set; } = null!;
        public virtual DbSet<Prot08PrecioProd> Prot08PrecioProds { get; set; } = null!;
        public virtual DbSet<Prot09Producto> Prot09Productos { get; set; } = null!;
        public virtual DbSet<Prot10Recetum> Prot10Receta { get; set; } = null!;
        public virtual DbSet<Prot11RecetaDtl> Prot11RecetaDtls { get; set; } = null!;
        public virtual DbSet<Prot12RecetaGrupo> Prot12RecetaGrupos { get; set; } = null!;
        public virtual DbSet<Prot13Combo> Prot13Combos { get; set; } = null!;
        public virtual DbSet<Prot14ComboFixedDtl> Prot14ComboFixedDtls { get; set; } = null!;
        public virtual DbSet<Prot15ComboVariable> Prot15ComboVariables { get; set; } = null!;
        public virtual DbSet<Prot16ComboVariableDtl> Prot16ComboVariableDtls { get; set; } = null!;
        public virtual DbSet<Prot17ComboGrupo> Prot17ComboGrupos { get; set; } = null!;
        public virtual DbSet<Prot18Productocom> Prot18Productocoms { get; set; } = null!;
        public virtual DbSet<Prot19TipoProdCom> Prot19TipoProdComs { get; set; } = null!;
        public virtual DbSet<Prot20Marcacom> Prot20Marcacoms { get; set; } = null!;
        public virtual DbSet<Prot21Modelocom> Prot21Modelocoms { get; set; } = null!;
        public virtual DbSet<Rptt01Reporte> Rptt01Reportes { get; set; } = null!;
        public virtual DbSet<Rptt02CategoriaReporte> Rptt02CategoriaReportes { get; set; } = null!;
        public virtual DbSet<Sntt01TipoMedioPago> Sntt01TipoMedioPagos { get; set; } = null!;
        public virtual DbSet<Sntt02TipoDocIdentidad> Sntt02TipoDocIdentidads { get; set; } = null!;
        public virtual DbSet<Sntt03EntidadFinanciera> Sntt03EntidadFinancieras { get; set; } = null!;
        public virtual DbSet<Sntt04TipoMonedum> Sntt04TipoMoneda { get; set; } = null!;
        public virtual DbSet<Sntt05TipoExistencium> Sntt05TipoExistencia { get; set; } = null!;
        public virtual DbSet<Sntt06UnidadMedidum> Sntt06UnidadMedida { get; set; } = null!;
        public virtual DbSet<Sntt07TipoIntangible> Sntt07TipoIntangibles { get; set; } = null!;
        public virtual DbSet<Sntt08CodigoLibro> Sntt08CodigoLibros { get; set; } = null!;
        public virtual DbSet<Sntt09CtaContable> Sntt09CtaContables { get; set; } = null!;
        public virtual DbSet<Sntt10TipoComprobante> Sntt10TipoComprobantes { get; set; } = null!;
        public virtual DbSet<Sntt11Aduana> Sntt11Aduanas { get; set; } = null!;
        public virtual DbSet<Sntt12TipoOperacion> Sntt12TipoOperacions { get; set; } = null!;
        public virtual DbSet<Sntt13TipoActividad> Sntt13TipoActividads { get; set; } = null!;
        public virtual DbSet<Sntt14Nacionalidad> Sntt14Nacionalidads { get; set; } = null!;
        public virtual DbSet<Sntt15Vium> Sntt15Via { get; set; } = null!;
        public virtual DbSet<Sntt16Zona> Sntt16Zonas { get; set; } = null!;
        public virtual DbSet<Sntt17TipoTrabajador> Sntt17TipoTrabajadors { get; set; } = null!;
        public virtual DbSet<Sntt18SituacionEducativa> Sntt18SituacionEducativas { get; set; } = null!;
        public virtual DbSet<Sntt19Ocupacion> Sntt19Ocupacions { get; set; } = null!;
        public virtual DbSet<Sntt20RegimenPensionario> Sntt20RegimenPensionarios { get; set; } = null!;
        public virtual DbSet<Sntt21CondicionLaboral> Sntt21CondicionLaborals { get; set; } = null!;
        public virtual DbSet<Sntt22PeriodoRemuneracion> Sntt22PeriodoRemuneracions { get; set; } = null!;
        public virtual DbSet<Sntt23SaludEp> Sntt23SaludEps { get; set; } = null!;
        public virtual DbSet<Sntt24Situacion> Sntt24Situacions { get; set; } = null!;
        public virtual DbSet<Sntt25MotivoBaja> Sntt25MotivoBajas { get; set; } = null!;
        public virtual DbSet<Sntt26ModalidadFormativa> Sntt26ModalidadFormativas { get; set; } = null!;
        public virtual DbSet<Sntt27VinculoFamiliar> Sntt27VinculoFamiliars { get; set; } = null!;
        public virtual DbSet<Sntt28SuspencionLaboral> Sntt28SuspencionLaborals { get; set; } = null!;
        public virtual DbSet<Sntt29RegimenSalud> Sntt29RegimenSaluds { get; set; } = null!;
        public virtual DbSet<Sntt30RegimenLaboral> Sntt30RegimenLaborals { get; set; } = null!;
        public virtual DbSet<Sntt31Departamento> Sntt31Departamentos { get; set; } = null!;
        public virtual DbSet<Sntt32Provincium> Sntt32Provincia { get; set; } = null!;
        public virtual DbSet<Sntt33Distrito> Sntt33Distritos { get; set; } = null!;
        public virtual DbSet<Tnst01CompRecibido> Tnst01CompRecibidos { get; set; } = null!;
        public virtual DbSet<Tnst02CompRecibidoDtl> Tnst02CompRecibidoDtls { get; set; } = null!;
        public virtual DbSet<Tnst03CompRecibidoEstado> Tnst03CompRecibidoEstados { get; set; } = null!;
        public virtual DbSet<Tnst04CompEmitido> Tnst04CompEmitidos { get; set; } = null!;
        public virtual DbSet<Tnst05CompEmitidoDtl> Tnst05CompEmitidoDtls { get; set; } = null!;
        public virtual DbSet<Tnst06CompEmitidoEstado> Tnst06CompEmitidoEstados { get; set; } = null!;
        public virtual DbSet<Tnst07MedioPagoDtl> Tnst07MedioPagoDtls { get; set; } = null!;
        public virtual DbSet<Tnst08DescuentoDtl> Tnst08DescuentoDtls { get; set; } = null!;
        public virtual DbSet<Tott01TotalDiarioVtum> Tott01TotalDiarioVta { get; set; } = null!;
        public virtual DbSet<Tott02TotalDiarioProd> Tott02TotalDiarioProds { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost;database=Eagle;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Clit01Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PK1")
                    .IsClustered(false);

                entity.ToTable("CLIt01_paciente");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.CodPaciente)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cod_paciente");

                entity.Property(e => e.GrupoSanguineo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("grupo_sanguineo");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdOcupacion).HasColumnName("id_ocupacion");

                entity.Property(e => e.IdRegimenSalud).HasColumnName("id_regimen_salud");

                entity.Property(e => e.IdSaludEps).HasColumnName("id_salud_eps");

                entity.Property(e => e.NroHistClinica)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_hist_clinica");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("observaciones");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Clit01Pacientes)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("RefPERt02_cliente1541");

                entity.HasOne(d => d.IdOcupacionNavigation)
                    .WithMany(p => p.Clit01Pacientes)
                    .HasForeignKey(d => d.IdOcupacion)
                    .HasConstraintName("RefSNTt19_ocupacion1511");

                entity.HasOne(d => d.IdRegimenSaludNavigation)
                    .WithMany(p => p.Clit01Pacientes)
                    .HasForeignKey(d => d.IdRegimenSalud)
                    .HasConstraintName("RefSNTt29_regimen_salud1531");

                entity.HasOne(d => d.IdSaludEpsNavigation)
                    .WithMany(p => p.Clit01Pacientes)
                    .HasForeignKey(d => d.IdSaludEps)
                    .HasConstraintName("RefSNTt23_salud_eps1521");
            });

            modelBuilder.Entity<Clit02Actividad>(entity =>
            {
                entity.HasKey(e => e.IdActividad)
                    .HasName("PK_cli_t16_actividad")
                    .IsClustered(false);

                entity.ToTable("CLIt02_actividad");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.CodActividad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_actividad");

                entity.Property(e => e.IdActividadNext).HasColumnName("id_actividad_next");

                entity.Property(e => e.IdActividadPrev).HasColumnName("id_actividad_prev");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.NroOrden).HasColumnName("nro_orden");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObservacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_observacion");
            });

            modelBuilder.Entity<Clit03Atencion>(entity =>
            {
                entity.HasKey(e => e.IdAtencion)
                    .HasName("PK_cli_t17_atencion")
                    .IsClustered(false);

                entity.ToTable("CLIt03_atencion");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.CodAtencion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_atencion");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Clit03Atencions)
                    .HasForeignKey(d => d.IdActividad)
                    .HasConstraintName("FK_cli_t17_atencion_cli_t16_actividad");

                entity.HasOne(d => d.IdCitaNavigation)
                    .WithMany(p => p.Clit03Atencions)
                    .HasForeignKey(d => d.IdCita)
                    .HasConstraintName("RefCLIt19_cita1781");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Clit03Atencions)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado1791");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Clit03Atencions)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt01_paciente1611");
            });

            modelBuilder.Entity<Clit04AtencionDtl>(entity =>
            {
                entity.HasKey(e => e.IdAtencionDtl)
                    .HasName("PK_cli_t18_atencion_dtl")
                    .IsClustered(false);

                entity.ToTable("CLIt04_atencion_dtl");

                entity.Property(e => e.IdAtencionDtl).HasColumnName("id_atencion_dtl");

                entity.Property(e => e.CodAtencionDtl)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_atencion_dtl");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdActividad).HasColumnName("id_actividad");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObservacion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_observacion");

                entity.HasOne(d => d.IdActividadNavigation)
                    .WithMany(p => p.Clit04AtencionDtls)
                    .HasForeignKey(d => d.IdActividad)
                    .HasConstraintName("FK_cli_t18_atencion_dtl_cli_t16_actividad");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit04AtencionDtls)
                    .HasForeignKey(d => d.IdAtencion)
                    .HasConstraintName("FK_cli_t18_atencion_dtl_cli_t17_atencion");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Clit04AtencionDtls)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado1741");
            });

            modelBuilder.Entity<Clit05AnteceAlergium>(entity =>
            {
                entity.HasKey(e => e.IdAnteceAlergia)
                    .HasName("PK14")
                    .IsClustered(false);

                entity.ToTable("CLIt05_antece_alergia");

                entity.Property(e => e.IdAnteceAlergia).HasColumnName("id_antece_alergia");

                entity.Property(e => e.CodAnteceAlergia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_antece_alergia");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.TxtAlergias)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("txt_alergias");

                entity.Property(e => e.TxtAntecedentes)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasColumnName("txt_antecedentes");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Clit05AnteceAlergia)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("RefCLIt01_paciente1581");
            });

            modelBuilder.Entity<Clit06TipoEspecialidad>(entity =>
            {
                entity.HasKey(e => e.IdTipoEspecialidad)
                    .HasName("PK_rhu_t04_tipo_especialidad")
                    .IsClustered(false);

                entity.ToTable("CLIt06_tipo_especialidad");

                entity.Property(e => e.IdTipoEspecialidad).HasColumnName("id_tipo_especialidad");

                entity.Property(e => e.CodTipoEspecialidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_especialidad");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Clit07EspecialidadMedica>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidadMedica)
                    .HasName("PK10_1")
                    .IsClustered(false);

                entity.ToTable("CLIt07_especialidad_medica");

                entity.Property(e => e.IdEspecialidadMedica).HasColumnName("id_especialidad_medica");

                entity.Property(e => e.CodEspecialidadMedica)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_especialidad_medica");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoEspecialidad).HasColumnName("id_tipo_especialidad");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdTipoEspecialidadNavigation)
                    .WithMany(p => p.Clit07EspecialidadMedicas)
                    .HasForeignKey(d => d.IdTipoEspecialidad)
                    .HasConstraintName("FK_rhu_t02_especialidad_medica_rhu_t04_tipo_especialidad");
            });

            modelBuilder.Entity<Clit08FuncionesVitale>(entity =>
            {
                entity.HasKey(e => e.IdFuncionesVitales)
                    .HasName("PK13")
                    .IsClustered(false);

                entity.ToTable("CLIt08_funciones_vitales");

                entity.Property(e => e.IdFuncionesVitales).HasColumnName("id_funciones_vitales");

                entity.Property(e => e.Altura)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("altura");

                entity.Property(e => e.CodFuncionesVitales)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_funciones_vitales");

                entity.Property(e => e.Diastoles)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("diastoles");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Imc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imc");

                entity.Property(e => e.Peso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("peso");

                entity.Property(e => e.Pulsaciones)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pulsaciones");

                entity.Property(e => e.RitmoRespiratorio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ritmo_respiratorio");

                entity.Property(e => e.Sistoles)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sistoles");

                entity.Property(e => e.Temperatura)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("temperatura");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit08FuncionesVitales)
                    .HasForeignKey(d => d.IdAtencion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt03_atencion1631");
            });

            modelBuilder.Entity<Clit09Anamnesi>(entity =>
            {
                entity.HasKey(e => e.IdAnamnesis)
                    .HasName("PK16_1")
                    .IsClustered(false);

                entity.ToTable("CLIt09_anamnesis");

                entity.Property(e => e.IdAnamnesis).HasColumnName("id_anamnesis");

                entity.Property(e => e.CodAnamnesis)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_anamnesis");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit09Anamnesis)
                    .HasForeignKey(d => d.IdAtencion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt03_atencion1641");
            });

            modelBuilder.Entity<Clit10ExamenFisico>(entity =>
            {
                entity.HasKey(e => e.IdExamenFisico)
                    .HasName("PK16_1_1")
                    .IsClustered(false);

                entity.ToTable("CLIt10_examen_fisico");

                entity.Property(e => e.IdExamenFisico).HasColumnName("id_examen_fisico");

                entity.Property(e => e.CodExamenFisico)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_examen_fisico");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit10ExamenFisicos)
                    .HasForeignKey(d => d.IdAtencion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt03_atencion1651");
            });

            modelBuilder.Entity<Clit11EstudioComplementario>(entity =>
            {
                entity.HasKey(e => e.IdEstudioComplementario)
                    .HasName("PK16_1_1_1_1_1")
                    .IsClustered(false);

                entity.ToTable("CLIt11_estudio_complementario");

                entity.Property(e => e.IdEstudioComplementario).HasColumnName("id_estudio_complementario");

                entity.Property(e => e.CodEstudioComplementario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_estudio_complementario");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit11EstudioComplementarios)
                    .HasForeignKey(d => d.IdAtencion)
                    .HasConstraintName("RefCLIt03_atencion1671");
            });

            modelBuilder.Entity<Clit12ArchivoComplementario>(entity =>
            {
                entity.HasKey(e => e.IdArchivoComplelemtario)
                    .HasName("PK22")
                    .IsClustered(false);

                entity.ToTable("CLIt12_archivo_complementario");

                entity.Property(e => e.IdArchivoComplelemtario).HasColumnName("id_archivo_complelemtario");

                entity.Property(e => e.CodArchivoComplelemtario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_archivo_complelemtario");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdEstudioComplementario).HasColumnName("id_estudio_complementario");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtPath)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_path");

                entity.HasOne(d => d.IdEstudioComplementarioNavigation)
                    .WithMany(p => p.Clit12ArchivoComplementarios)
                    .HasForeignKey(d => d.IdEstudioComplementario)
                    .HasConstraintName("Refcli_t11_estudios_complementarios20");
            });

            modelBuilder.Entity<Clit13Laboratorio>(entity =>
            {
                entity.HasKey(e => e.IdLaboratorio)
                    .HasName("PK23")
                    .IsClustered(false);

                entity.ToTable("CLIt13_laboratorio");

                entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.Hemograma)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("hemograma");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Otros)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("otros");

                entity.Property(e => e.PanelMetabolico)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("panel_metabolico");

                entity.Property(e => e.PerfilHepatico)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("perfil_hepatico");

                entity.Property(e => e.PerfilLipidico)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("perfil_lipidico");

                entity.Property(e => e.PerfilRenal)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("perfil_renal");

                entity.Property(e => e.PerfilTriode)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("perfil_triode");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.Urinalisis)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("urinalisis");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit13Laboratorios)
                    .HasForeignKey(d => d.IdAtencion)
                    .HasConstraintName("RefCLIt03_atencion1691");
            });

            modelBuilder.Entity<Clit14ImgLaboratorio>(entity =>
            {
                entity.HasKey(e => e.IdImgLaboratorio)
                    .HasName("PK24")
                    .IsClustered(false);

                entity.ToTable("CLIt14_img_laboratorio");

                entity.Property(e => e.IdImgLaboratorio).HasColumnName("id_img_laboratorio");

                entity.Property(e => e.CodImgLaboratorio)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_img_laboratorio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtPath)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_path");

                entity.HasOne(d => d.IdLaboratorioNavigation)
                    .WithMany(p => p.Clit14ImgLaboratorios)
                    .HasForeignKey(d => d.IdLaboratorio)
                    .HasConstraintName("Refcli_t13_laboratorio22");
            });

            modelBuilder.Entity<Clit15Diagnostico>(entity =>
            {
                entity.HasKey(e => e.IdDiagnostico)
                    .HasName("PK16_1_1_1")
                    .IsClustered(false);

                entity.ToTable("CLIt15_diagnostico");

                entity.Property(e => e.IdDiagnostico).HasColumnName("id_diagnostico");

                entity.Property(e => e.Cod4Cie10)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod4_cie10");

                entity.Property(e => e.CodDiagnostico)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_diagnostico");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdCie10).HasColumnName("id_cie10");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtCie10)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("txt_cie10");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit15Diagnosticos)
                    .HasForeignKey(d => d.IdAtencion)
                    .HasConstraintName("RefCLIt03_atencion1721");

                entity.HasOne(d => d.IdCie10Navigation)
                    .WithMany(p => p.Clit15Diagnosticos)
                    .HasForeignKey(d => d.IdCie10)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt16_cie101711");
            });

            modelBuilder.Entity<Clit16Cie10>(entity =>
            {
                entity.HasKey(e => e.IdCie10)
                    .HasName("PK100")
                    .IsClustered(false);

                entity.ToTable("CLIt16_cie10");

                entity.Property(e => e.IdCie10).HasColumnName("id_cie10");

                entity.Property(e => e.Cod3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_3");

                entity.Property(e => e.Cod4)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_4");

                entity.Property(e => e.TxtCategoria)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("txt_categoria");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Clit17Tratamiento>(entity =>
            {
                entity.HasKey(e => e.IdTratamiento)
                    .HasName("PK16_1_1_1_1")
                    .IsClustered(false);

                entity.ToTable("CLIt17_tratamiento");

                entity.Property(e => e.IdTratamiento).HasColumnName("id_tratamiento");

                entity.Property(e => e.CodTratamiento)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tratamiento");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit17Tratamientos)
                    .HasForeignKey(d => d.IdAtencion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt03_atencion1701");
            });

            modelBuilder.Entity<Clit18Evolucion>(entity =>
            {
                entity.HasKey(e => e.IdEvolucion)
                    .HasName("PK16_1_1_2")
                    .IsClustered(false);

                entity.ToTable("CLIt18_evolucion");

                entity.Property(e => e.IdEvolucion).HasColumnName("id_evolucion");

                entity.Property(e => e.CodEvolucion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_evolucion");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdAtencion).HasColumnName("id_atencion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(5000)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdAtencionNavigation)
                    .WithMany(p => p.Clit18Evolucions)
                    .HasForeignKey(d => d.IdAtencion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt03_atencion1731");
            });

            modelBuilder.Entity<Clit19Citum>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("PK25")
                    .IsClustered(false);

                entity.ToTable("CLIt19_cita");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.CodCita)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_cita");

                entity.Property(e => e.Duracion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("duracion");

                entity.Property(e => e.FecCita)
                    .HasColumnType("date")
                    .HasColumnName("fec_cita");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.Hora)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("hora");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");

                entity.Property(e => e.SnAsistencia).HasColumnName("sn_asistencia");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObs)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_obs");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Clit19Cita)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado1761");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Clit19Cita)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCLIt01_paciente1771");
            });

            modelBuilder.Entity<Csht01CajaDtl>(entity =>
            {
                entity.HasKey(e => e.IdCajaDtl)
                    .HasName("PK106")
                    .IsClustered(false);

                entity.ToTable("CSHt01_caja_dtl");

                entity.Property(e => e.IdCajaDtl).HasColumnName("id_caja_dtl");

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora).HasColumnName("hora");

                entity.Property(e => e.IdCaja).HasColumnName("id_caja");

                entity.Property(e => e.IdEmpAutorizador).HasColumnName("id_emp_autorizador");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdTurno).HasColumnName("id_turno");

                entity.Property(e => e.SnClose).HasColumnName("sn_close");

                entity.Property(e => e.SnOpen).HasColumnName("sn_open");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Csht01CajaDtls)
                    .HasForeignKey(d => d.IdCaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt12_caja138");

                entity.HasOne(d => d.IdEmpAutorizadorNavigation)
                    .WithMany(p => p.Csht01CajaDtlIdEmpAutorizadorNavigations)
                    .HasForeignKey(d => d.IdEmpAutorizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado154");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Csht01CajaDtlIdEmpleadoNavigations)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado140");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.Csht01CajaDtls)
                    .HasForeignKey(d => d.IdTurno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt13_turno160");
            });

            modelBuilder.Entity<Fist01ControlNumeracion>(entity =>
            {
                entity.HasKey(e => e.IdControlNumeracion)
                    .HasName("PK93")
                    .IsClustered(false);

                entity.ToTable("FISt01_control_numeracion");

                entity.Property(e => e.IdControlNumeracion).HasColumnName("id_control_numeracion");

                entity.Property(e => e.Fecha01).HasColumnName("fecha01");

                entity.Property(e => e.Fecha02).HasColumnName("fecha02");

                entity.Property(e => e.FechaRegistro).HasColumnName("fecha_registro");

                entity.Property(e => e.IdCaja).HasColumnName("id_caja");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.IdTipoNumeracion).HasColumnName("id_tipo_numeracion");

                entity.Property(e => e.LockedBy).HasColumnName("locked_by");

                entity.Property(e => e.NroActual).HasColumnName("nro_actual");

                entity.Property(e => e.NroFinal).HasColumnName("nro_final");

                entity.Property(e => e.NroInicial).HasColumnName("nro_inicial");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtInfo01)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_info01");

                entity.Property(e => e.TxtInfo02)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_info02");

                entity.Property(e => e.TxtNroSerie)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_nro_serie");

                entity.Property(e => e.TxtSerieImpresora)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie_impresora");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Fist01ControlNumeracions)
                    .HasForeignKey(d => d.IdCaja)
                    .HasConstraintName("RefMSTt12_caja124");

                entity.HasOne(d => d.IdCanVtaNavigation)
                    .WithMany(p => p.Fist01ControlNumeracions)
                    .HasForeignKey(d => d.IdCanVta)
                    .HasConstraintName("RefMSTt04_canal_vta132");

                entity.HasOne(d => d.IdNivelNavigation)
                    .WithMany(p => p.Fist01ControlNumeracions)
                    .HasForeignKey(d => d.IdNivel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefFISt02_nivel127");

                entity.HasOne(d => d.IdTipoCompNavigation)
                    .WithMany(p => p.Fist01ControlNumeracions)
                    .HasForeignKey(d => d.IdTipoComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefSNTt10_tipo_comprobante126");

                entity.HasOne(d => d.IdTipoNumeracionNavigation)
                    .WithMany(p => p.Fist01ControlNumeracions)
                    .HasForeignKey(d => d.IdTipoNumeracion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefFISt03_tipo_numeracion128");
            });

            modelBuilder.Entity<Fist02Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNivel)
                    .HasName("PK97")
                    .IsClustered(false);

                entity.ToTable("FISt02_nivel");

                entity.Property(e => e.IdNivel).HasColumnName("id_nivel");

                entity.Property(e => e.CodNivel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_nivel");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Fist03TipoNumeracion>(entity =>
            {
                entity.HasKey(e => e.IdTipoNumeracion)
                    .HasName("PK98")
                    .IsClustered(false);

                entity.ToTable("FISt03_tipo_numeracion");

                entity.Property(e => e.IdTipoNumeracion).HasColumnName("id_tipo_numeracion");

                entity.Property(e => e.CodTipoNumeracion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_numeracion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Fist04ParametroFiscal>(entity =>
            {
                entity.HasKey(e => e.IdParametroFiscal)
                    .HasName("PK112")
                    .IsClustered(false);

                entity.ToTable("FISt04_parametro_fiscal");

                entity.Property(e => e.IdParametroFiscal).HasColumnName("id_parametro_fiscal");

                entity.Property(e => e.CodParametroFiscal)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_parametro_fiscal");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.ValorDefault)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("valor_default");
            });

            modelBuilder.Entity<Fist05ConfiguracionFiscalCaja>(entity =>
            {
                entity.HasKey(e => e.IdConfiguracionFiscalCaja)
                    .HasName("PK114_1")
                    .IsClustered(false);

                entity.ToTable("FISt05_configuracion_fiscal_caja");

                entity.Property(e => e.IdConfiguracionFiscalCaja).HasColumnName("id_configuracion_fiscal_caja");

                entity.Property(e => e.IdCaja).HasColumnName("id_caja");

                entity.Property(e => e.IdParametroFiscal).HasColumnName("id_parametro_fiscal");

                entity.Property(e => e.Valor)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("valor");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Fist05ConfiguracionFiscalCajas)
                    .HasForeignKey(d => d.IdCaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt12_caja148");

                entity.HasOne(d => d.IdParametroFiscalNavigation)
                    .WithMany(p => p.Fist05ConfiguracionFiscalCajas)
                    .HasForeignKey(d => d.IdParametroFiscal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefFISt04_parametro_fiscal147");
            });

            modelBuilder.Entity<Grlt01Parametro>(entity =>
            {
                entity.HasKey(e => e.IdParametro)
                    .HasName("PKGRLt01_cod_parametro")
                    .IsClustered(false);

                entity.ToTable("GRLt01_parametro");

                entity.Property(e => e.IdParametro).HasColumnName("id_parametro");

                entity.Property(e => e.CodParametro)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_parametro");

                entity.Property(e => e.DecValor)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("dec_valor");

                entity.Property(e => e.SnEdit).HasColumnName("sn_edit");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtObs)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_obs");

                entity.Property(e => e.TxtValor)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_valor");
            });

            modelBuilder.Entity<Grlt02Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PK21")
                    .IsClustered(false);

                entity.ToTable("GRLt02_estado");

                entity.Property(e => e.IdEstado)
                    .ValueGeneratedNever()
                    .HasColumnName("id_estado");

                entity.Property(e => e.CodEstado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Grlt03Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK120_1")
                    .IsClustered(false);

                entity.ToTable("GRLt03_horario");

                entity.Property(e => e.IdHorario).HasColumnName("id_horario");

                entity.Property(e => e.CodHorario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_horario");

                entity.Property(e => e.Hora)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("hora");
            });

            modelBuilder.Entity<Grlt04ConfiguracionCaja>(entity =>
            {
                entity.HasKey(e => e.IdConfig)
                    .HasName("PK99")
                    .IsClustered(false);

                entity.ToTable("GRLt04_configuracion_caja");

                entity.Property(e => e.IdConfig).HasColumnName("id_config");

                entity.Property(e => e.IdCaja).HasColumnName("id_caja");

                entity.Property(e => e.TxtPathlog)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_pathlog");

                entity.HasOne(d => d.IdCajaNavigation)
                    .WithMany(p => p.Grlt04ConfiguracionCajas)
                    .HasForeignKey(d => d.IdCaja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt12_caja129");
            });

            modelBuilder.Entity<Labt01Asistencium>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia)
                    .HasName("PK123")
                    .IsClustered(false);

                entity.ToTable("LABt01_asistencia");

                entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");

                entity.Property(e => e.BreakIn).HasColumnName("break_in");

                entity.Property(e => e.BreakInStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_in_status");

                entity.Property(e => e.BreakOut).HasColumnName("break_out");

                entity.Property(e => e.BreakOutStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_out_status");

                entity.Property(e => e.ClockIn).HasColumnName("clock_in");

                entity.Property(e => e.ClockInStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_in_status");

                entity.Property(e => e.ClockOut).HasColumnName("clock_out");

                entity.Property(e => e.ClockOutStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_out_status");

                entity.Property(e => e.FechaCronologica).HasColumnName("fecha_cronologica");

                entity.Property(e => e.FechaGmt).HasColumnName("fecha_gmt");

                entity.Property(e => e.FechaNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_negocio");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.IdEmpAutorizador).HasColumnName("id_emp_autorizador");

                entity.Property(e => e.IdEmpTrabajo).HasColumnName("id_emp_trabajo");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.SnBreakIn).HasColumnName("sn_break_in");

                entity.Property(e => e.SnClockIn).HasColumnName("sn_clock_in");

                entity.Property(e => e.TxtObsv)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_obsv");

                entity.Property(e => e.TxtPhotoPathClockIn)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("txt_photo_path_clock_in");

                entity.Property(e => e.TxtPhotoPathClockOut)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("txt_photo_path_clock_out");

                entity.HasOne(d => d.IdCanVtaNavigation)
                    .WithMany(p => p.Labt01Asistencia)
                    .HasForeignKey(d => d.IdCanVta)
                    .HasConstraintName("RefMSTt04_canal_vta152");

                entity.HasOne(d => d.IdEmpAutorizadorNavigation)
                    .WithMany(p => p.Labt01Asistencia)
                    .HasForeignKey(d => d.IdEmpAutorizador)
                    .HasConstraintName("RefPERt04_empleado153");

                entity.HasOne(d => d.IdEmpTrabajoNavigation)
                    .WithMany(p => p.Labt01Asistencia)
                    .HasForeignKey(d => d.IdEmpTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefLABt07_emp_trabajo172");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Labt01Asistencia)
                    .HasForeignKey(d => d.IdLocation)
                    .HasConstraintName("RefMSTt08_location151");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Labt01Asistencia)
                    .HasForeignKey(d => d.IdRazon)
                    .HasConstraintName("RefMSTt05_razon154");
            });

            modelBuilder.Entity<Labt02AsistenciaAjustadum>(entity =>
            {
                entity.HasKey(e => e.IdAsistenciaAjustada)
                    .HasName("PK123_1")
                    .IsClustered(false);

                entity.ToTable("LABt02_asistencia_ajustada");

                entity.Property(e => e.IdAsistenciaAjustada).HasColumnName("id_asistencia_ajustada");

                entity.Property(e => e.BreakIn).HasColumnName("break_in");

                entity.Property(e => e.BreakInAjust).HasColumnName("break_in_ajust");

                entity.Property(e => e.BreakInStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_in_status");

                entity.Property(e => e.BreakInStatusAjust)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_in_status_ajust");

                entity.Property(e => e.BreakOut).HasColumnName("break_out");

                entity.Property(e => e.BreakOutAjust).HasColumnName("break_out_ajust");

                entity.Property(e => e.BreakOutStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_out_status");

                entity.Property(e => e.BreakOutStatusAjust)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("break_out_status_ajust");

                entity.Property(e => e.ClockIn).HasColumnName("clock_in");

                entity.Property(e => e.ClockInAjust).HasColumnName("clock_in_ajust");

                entity.Property(e => e.ClockInStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_in_status");

                entity.Property(e => e.ClockInStatusAjust)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_in_status_ajust");

                entity.Property(e => e.ClockOut).HasColumnName("clock_out");

                entity.Property(e => e.ClockOutAjust).HasColumnName("clock_out_ajust");

                entity.Property(e => e.ClockOutStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_out_status");

                entity.Property(e => e.ClockOutStatusAjust)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clock_out_status_ajust");

                entity.Property(e => e.FechaCronologica).HasColumnName("fecha_cronologica");

                entity.Property(e => e.FechaCronologicaAjust).HasColumnName("fecha_cronologica_ajust");

                entity.Property(e => e.FechaNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_negocio");

                entity.Property(e => e.FechaNegocioAjust)
                    .HasColumnType("date")
                    .HasColumnName("fecha_negocio_ajust");

                entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");

                entity.Property(e => e.IdEmpAutorizador).HasColumnName("id_emp_autorizador");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.SnBreakIn).HasColumnName("sn_break_in");

                entity.Property(e => e.SnClockIn).HasColumnName("sn_clock_in");

                entity.Property(e => e.TxtObsv)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_obsv");

                entity.Property(e => e.TxtPhotoPathClockInAjust)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("txt_photo_path_clock_in_ajust");

                entity.Property(e => e.TxtPhotoPathClockOutAjust)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("txt_photo_path_clock_out_ajust");

                entity.HasOne(d => d.IdAsistenciaNavigation)
                    .WithMany(p => p.Labt02AsistenciaAjustada)
                    .HasForeignKey(d => d.IdAsistencia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefLABt01_asistencia160");

                entity.HasOne(d => d.IdEmpAutorizadorNavigation)
                    .WithMany(p => p.Labt02AsistenciaAjustada)
                    .HasForeignKey(d => d.IdEmpAutorizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado163");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Labt02AsistenciaAjustada)
                    .HasForeignKey(d => d.IdRazon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt05_razon161");
            });

            modelBuilder.Entity<Labt03HorarioEmp>(entity =>
            {
                entity.HasKey(e => e.IdHorarioEmp)
                    .HasName("PK123_2")
                    .IsClustered(false);

                entity.ToTable("LABt03_horario_emp");

                entity.Property(e => e.IdHorarioEmp).HasColumnName("id_horario_emp");

                entity.Property(e => e.FechaFinHorario)
                    .HasColumnType("date")
                    .HasColumnName("fecha_fin_horario");

                entity.Property(e => e.FechaInicioHorario)
                    .HasColumnType("date")
                    .HasColumnName("fecha_inicio_horario");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Labt03HorarioEmps)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado182");
            });

            modelBuilder.Entity<Labt04HorarioEmpDtl>(entity =>
            {
                entity.HasKey(e => e.IdHorarioEmpDtl)
                    .HasName("PK120")
                    .IsClustered(false);

                entity.ToTable("LABt04_horario_emp_dtl");

                entity.Property(e => e.IdHorarioEmpDtl).HasColumnName("id_horario_emp_dtl");

                entity.Property(e => e.FechaLabor)
                    .HasColumnType("date")
                    .HasColumnName("fecha_labor");

                entity.Property(e => e.HoraFin).HasColumnName("hora_fin");

                entity.Property(e => e.HoraFinBreak).HasColumnName("hora_fin_break");

                entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");

                entity.Property(e => e.HoraInicioBreak).HasColumnName("hora_inicio_break");

                entity.Property(e => e.IdHorarioEmp).HasColumnName("id_horario_emp");

                entity.Property(e => e.IdTurno).HasColumnName("id_turno");

                entity.Property(e => e.TiempoTolerancia).HasColumnName("tiempo_tolerancia");

                entity.HasOne(d => d.IdHorarioEmpNavigation)
                    .WithMany(p => p.Labt04HorarioEmpDtls)
                    .HasForeignKey(d => d.IdHorarioEmp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefLABt03_horario_emp160");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.Labt04HorarioEmpDtls)
                    .HasForeignKey(d => d.IdTurno)
                    .HasConstraintName("RefMSTt13_turno162");
            });

            modelBuilder.Entity<Labt05AsistenciaTempLast>(entity =>
            {
                entity.HasKey(e => e.IdAsistenciaTempLast)
                    .HasName("PK122")
                    .IsClustered(false);

                entity.ToTable("LABt05_asistencia_temp_last");

                entity.Property(e => e.IdAsistenciaTempLast)
                    .ValueGeneratedNever()
                    .HasColumnName("id_asistencia_temp_last");

                entity.Property(e => e.IdAsistencia).HasColumnName("id_asistencia");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            });

            modelBuilder.Entity<Labt06Trabajo>(entity =>
            {
                entity.HasKey(e => e.IdTrabajo)
                    .HasName("PK121")
                    .IsClustered(false);

                entity.ToTable("LABt06_trabajo");

                entity.Property(e => e.IdTrabajo).HasColumnName("id_trabajo");

                entity.Property(e => e.CodTrabajo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_trabajo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(750)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_nombre");
            });

            modelBuilder.Entity<Labt07EmpTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdEmpTrabajo)
                    .HasName("PK125")
                    .IsClustered(false);

                entity.ToTable("LABt07_emp_trabajo");

                entity.Property(e => e.IdEmpTrabajo).HasColumnName("id_emp_trabajo");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTrabajo).HasColumnName("id_trabajo");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Labt07EmpTrabajos)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado170");

                entity.HasOne(d => d.IdTrabajoNavigation)
                    .WithMany(p => p.Labt07EmpTrabajos)
                    .HasForeignKey(d => d.IdTrabajo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefLABt06_trabajo169");
            });

            modelBuilder.Entity<Mstt01MedioPago>(entity =>
            {
                entity.HasKey(e => e.IdMedioPago)
                    .HasName("PKTSOt01_id_forma_pago")
                    .IsClustered(false);

                entity.ToTable("MSTt01_medio_pago");

                entity.Property(e => e.IdMedioPago).HasColumnName("id_medio_pago");

                entity.Property(e => e.CodMedioPago)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_medio_pago");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoMedioPago).HasColumnName("id_tipo_medio_pago");

                entity.Property(e => e.ReqRef).HasColumnName("req_ref");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdTipoMedioPagoNavigation)
                    .WithMany(p => p.Mstt01MedioPagos)
                    .HasForeignKey(d => d.IdTipoMedioPago)
                    .HasConstraintName("RefSNTt01_tipo_medio_pago1381");
            });

            modelBuilder.Entity<Mstt02Descuento>(entity =>
            {
                entity.HasKey(e => e.IdDescuento)
                    .HasName("PK48")
                    .IsClustered(false);

                entity.ToTable("MSTt02_descuento");

                entity.Property(e => e.IdDescuento).HasColumnName("id_descuento");

                entity.Property(e => e.CodDescuento)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_descuento");

                entity.Property(e => e.DomHoraFin).HasColumnName("dom_hora_fin");

                entity.Property(e => e.DomHoraIni).HasColumnName("dom_hora_ini");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.JueHoraFin).HasColumnName("jue_hora_fin");

                entity.Property(e => e.JueHoraIni).HasColumnName("jue_hora_ini");

                entity.Property(e => e.LunHoraFin).HasColumnName("lun_hora_fin");

                entity.Property(e => e.LunHoraIni).HasColumnName("lun_hora_ini");

                entity.Property(e => e.MarHoraFin).HasColumnName("mar_hora_fin");

                entity.Property(e => e.MarHoraIni).HasColumnName("mar_hora_ini");

                entity.Property(e => e.MieHoraFin).HasColumnName("mie_hora_fin");

                entity.Property(e => e.MieHoraIni).HasColumnName("mie_hora_ini");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto");

                entity.Property(e => e.MontoMax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto_max");

                entity.Property(e => e.MontoMin)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto_min");

                entity.Property(e => e.P1FechaFin).HasColumnName("p1_fecha_fin");

                entity.Property(e => e.P1FechaIni).HasColumnName("p1_fecha_ini");

                entity.Property(e => e.P1HoraFin).HasColumnName("p1_hora_fin");

                entity.Property(e => e.P1HoraIni).HasColumnName("p1_hora_ini");

                entity.Property(e => e.P2FechaFin).HasColumnName("p2_fecha_fin");

                entity.Property(e => e.P2FechaIni).HasColumnName("p2_fecha_ini");

                entity.Property(e => e.P2HoraFin).HasColumnName("p2_hora_fin");

                entity.Property(e => e.P2HoraIni).HasColumnName("p2_hora_ini");

                entity.Property(e => e.P3FechaFin).HasColumnName("p3_fecha_fin");

                entity.Property(e => e.P3FechaIni).HasColumnName("p3_fecha_ini");

                entity.Property(e => e.P3HoraFin).HasColumnName("p3_hora_fin");

                entity.Property(e => e.P3HoraIni).HasColumnName("p3_hora_ini");

                entity.Property(e => e.P4FechaFin).HasColumnName("p4_fecha_fin");

                entity.Property(e => e.P4FechaIni).HasColumnName("p4_fecha_ini");

                entity.Property(e => e.P4HoraFin).HasColumnName("p4_hora_fin");

                entity.Property(e => e.P4HoraIni).HasColumnName("p4_hora_ini");

                entity.Property(e => e.P5FechaFin).HasColumnName("p5_fecha_fin");

                entity.Property(e => e.P5FechaIni).HasColumnName("p5_fecha_ini");

                entity.Property(e => e.P5HoraFin).HasColumnName("p5_hora_fin");

                entity.Property(e => e.P5HoraIni).HasColumnName("p5_hora_ini");

                entity.Property(e => e.P6FechaFin).HasColumnName("p6_fecha_fin");

                entity.Property(e => e.P6FechaIni).HasColumnName("p6_fecha_ini");

                entity.Property(e => e.P6HoraFin).HasColumnName("p6_hora_fin");

                entity.Property(e => e.P6HoraIni).HasColumnName("p6_hora_ini");

                entity.Property(e => e.P7FechaFin).HasColumnName("p7_fecha_fin");

                entity.Property(e => e.P7FechaIni).HasColumnName("p7_fecha_ini");

                entity.Property(e => e.P7HoraFin).HasColumnName("p7_hora_fin");

                entity.Property(e => e.P7HoraIni).HasColumnName("p7_hora_ini");

                entity.Property(e => e.Porcentaje)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("porcentaje");

                entity.Property(e => e.SabHoraFin).HasColumnName("sab_hora_fin");

                entity.Property(e => e.SabHoraIni).HasColumnName("sab_hora_ini");

                entity.Property(e => e.SnDescuenDia).HasColumnName("sn_descuen_dia");

                entity.Property(e => e.SnDescuenPeriodo).HasColumnName("sn_descuen_periodo");

                entity.Property(e => e.SnDomingo).HasColumnName("sn_domingo");

                entity.Property(e => e.SnDsctoMto).HasColumnName("sn_dscto_mto");

                entity.Property(e => e.SnDsctoMtoAbierto).HasColumnName("sn_dscto_mto_abierto");

                entity.Property(e => e.SnDsctoPorc).HasColumnName("sn_dscto_porc");

                entity.Property(e => e.SnDsctoPorcAbierto).HasColumnName("sn_dscto_porc_abierto");

                entity.Property(e => e.SnJueves).HasColumnName("sn_jueves");

                entity.Property(e => e.SnLunes).HasColumnName("sn_lunes");

                entity.Property(e => e.SnMartes).HasColumnName("sn_martes");

                entity.Property(e => e.SnMiercoles).HasColumnName("sn_miercoles");

                entity.Property(e => e.SnSabado).HasColumnName("sn_sabado");

                entity.Property(e => e.SnViernes).HasColumnName("sn_viernes");

                entity.Property(e => e.TipoDescuento)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("tipo_descuento")
                    .IsFixedLength();

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.VieHoraFin).HasColumnName("vie_hora_fin");

                entity.Property(e => e.VieHoraIni).HasColumnName("vie_hora_ini");
            });

            modelBuilder.Entity<Mstt03TipoOrden>(entity =>
            {
                entity.HasKey(e => e.IdTipoOrden)
                    .HasName("PKVTAt03_id_tipo_vta")
                    .IsClustered(false);

                entity.ToTable("MSTt03_tipo_orden");

                entity.Property(e => e.IdTipoOrden).HasColumnName("id_tipo_orden");

                entity.Property(e => e.CodTipoOrden)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_orden");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt04CanalVtum>(entity =>
            {
                entity.HasKey(e => e.IdCanVta)
                    .HasName("PK54")
                    .IsClustered(false);

                entity.ToTable("MSTt04_canal_vta");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.CodCanVta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_can_vta");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt05Razon>(entity =>
            {
                entity.HasKey(e => e.IdRazon)
                    .HasName("PK49")
                    .IsClustered(false);

                entity.ToTable("MSTt05_razon");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.CodRazon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_razon");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoRazon).HasColumnName("id_tipo_razon");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdTipoRazonNavigation)
                    .WithMany(p => p.Mstt05Razons)
                    .HasForeignKey(d => d.IdTipoRazon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt16_tipo_razon145");
            });

            modelBuilder.Entity<Mstt06Impuesto>(entity =>
            {
                entity.HasKey(e => e.IdImpuesto)
                    .HasName("PKt04_id_tipo_impto_1")
                    .IsClustered(false);

                entity.ToTable("MSTt06_impuesto");

                entity.Property(e => e.IdImpuesto).HasColumnName("id_impuesto");

                entity.Property(e => e.CodImpuesto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_impuesto");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.PorImpto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto01");

                entity.Property(e => e.PorImpto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto02");

                entity.Property(e => e.PorImpto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto03");

                entity.Property(e => e.PorImpto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto04");

                entity.Property(e => e.PorImpto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto05");

                entity.Property(e => e.PorImpto06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto06");

                entity.Property(e => e.PorImpto07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto07");

                entity.Property(e => e.PorImpto08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto08");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt07EstadoCivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadoCivil)
                    .HasName("PKCMRt12_id_est_civil")
                    .IsClustered(false);

                entity.ToTable("MSTt07_estado_civil");

                entity.Property(e => e.IdEstadoCivil).HasColumnName("id_estado_civil");

                entity.Property(e => e.CodEstadoCivil)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_estado_civil");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt08Location>(entity =>
            {
                entity.HasKey(e => e.IdLocation)
                    .HasName("PKCMRt10_cod_origen_entidad")
                    .IsClustered(false);

                entity.ToTable("MSTt08_location");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.CodLocation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_location");

                entity.Property(e => e.FechaNegocio).HasColumnName("fecha_negocio");

                entity.Property(e => e.Fono1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fono1");

                entity.Property(e => e.Fono2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fono2");

                entity.Property(e => e.IdDist).HasColumnName("id_dist");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoLocation).HasColumnName("id_tipo_location");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(9, 6)")
                    .HasColumnName("longitud");

                entity.Property(e => e.NroRuc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nro_ruc");

                entity.Property(e => e.SnAlmacen).HasColumnName("sn_almacen");

                entity.Property(e => e.SnLocationCurrent).HasColumnName("sn_location_current");

                entity.Property(e => e.TxtAbrev1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrev1");

                entity.Property(e => e.TxtAbrev2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrev2");

                entity.Property(e => e.TxtDatos1)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_datos1");

                entity.Property(e => e.TxtDatos2)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_datos2");

                entity.Property(e => e.TxtDatos3)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_datos3");

                entity.Property(e => e.TxtDatos4)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_datos4");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtDireccion1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion1");

                entity.Property(e => e.TxtDireccion2)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion2");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdDistNavigation)
                    .WithMany(p => p.Mstt08Locations)
                    .HasForeignKey(d => d.IdDist)
                    .HasConstraintName("RefSNTt33_distrito175");

                entity.HasOne(d => d.IdTipoLocationNavigation)
                    .WithMany(p => p.Mstt08Locations)
                    .HasForeignKey(d => d.IdTipoLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt09_tipo_location1011");
            });

            modelBuilder.Entity<Mstt09TipoLocation>(entity =>
            {
                entity.HasKey(e => e.IdTipoLocation)
                    .HasName("PKCMRt02_id_tipo_dir")
                    .IsClustered(false);

                entity.ToTable("MSTt09_tipo_location");

                entity.Property(e => e.IdTipoLocation).HasColumnName("id_tipo_location");

                entity.Property(e => e.CodTipoLocation)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_location");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt10Impresora>(entity =>
            {
                entity.HasKey(e => e.IdImpresora)
                    .HasName("PK94")
                    .IsClustered(false);

                entity.ToTable("MSTt10_impresora");

                entity.Property(e => e.IdImpresora).HasColumnName("id_impresora");

                entity.Property(e => e.CodImpresora)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_impresora");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoImpresora).HasColumnName("id_tipo_impresora");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtInfo01)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_info01");

                entity.Property(e => e.TxtIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_ip");

                entity.Property(e => e.TxtMarca)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_marca");

                entity.Property(e => e.TxtModelo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_modelo");

                entity.HasOne(d => d.IdTipoImpresoraNavigation)
                    .WithMany(p => p.Mstt10Impresoras)
                    .HasForeignKey(d => d.IdTipoImpresora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt11_tipo_impresora116");
            });

            modelBuilder.Entity<Mstt11TipoImpresora>(entity =>
            {
                entity.HasKey(e => e.IdTipoImpresora)
                    .HasName("PK94_1")
                    .IsClustered(false);

                entity.ToTable("MSTt11_tipo_impresora");

                entity.Property(e => e.IdTipoImpresora).HasColumnName("id_tipo_impresora");

                entity.Property(e => e.CodTipoImpresora)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_impresora");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtInfo01)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_info01");
            });

            modelBuilder.Entity<Mstt12Caja>(entity =>
            {
                entity.HasKey(e => e.IdCaja)
                    .HasName("PK96")
                    .IsClustered(false);

                entity.ToTable("MSTt12_caja");

                entity.Property(e => e.IdCaja).HasColumnName("id_caja");

                entity.Property(e => e.CodCaja)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_caja");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdImpresora).HasColumnName("id_impresora");

                entity.Property(e => e.IdImpresora02).HasColumnName("id_impresora02");

                entity.Property(e => e.IdImpresora03).HasColumnName("id_impresora03");

                entity.Property(e => e.IdImpresora04).HasColumnName("id_impresora04");

                entity.Property(e => e.IdImpresora05).HasColumnName("id_impresora05");

                entity.Property(e => e.IdImpresora06).HasColumnName("id_impresora06");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtInfo01)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_info01");

                entity.Property(e => e.TxtInfo02)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_info02");

                entity.Property(e => e.TxtIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_ip");

                entity.HasOne(d => d.IdImpresoraNavigation)
                    .WithMany(p => p.Mstt12CajaIdImpresoraNavigations)
                    .HasForeignKey(d => d.IdImpresora)
                    .HasConstraintName("RefMSTt10_impresora117");

                entity.HasOne(d => d.IdImpresora02Navigation)
                    .WithMany(p => p.Mstt12CajaIdImpresora02Navigations)
                    .HasForeignKey(d => d.IdImpresora02)
                    .HasConstraintName("RefMSTt10_impresora119");

                entity.HasOne(d => d.IdImpresora03Navigation)
                    .WithMany(p => p.Mstt12CajaIdImpresora03Navigations)
                    .HasForeignKey(d => d.IdImpresora03)
                    .HasConstraintName("RefMSTt10_impresora120");

                entity.HasOne(d => d.IdImpresora04Navigation)
                    .WithMany(p => p.Mstt12CajaIdImpresora04Navigations)
                    .HasForeignKey(d => d.IdImpresora04)
                    .HasConstraintName("RefMSTt10_impresora121");

                entity.HasOne(d => d.IdImpresora05Navigation)
                    .WithMany(p => p.Mstt12CajaIdImpresora05Navigations)
                    .HasForeignKey(d => d.IdImpresora05)
                    .HasConstraintName("RefMSTt10_impresora122");

                entity.HasOne(d => d.IdImpresora06Navigation)
                    .WithMany(p => p.Mstt12CajaIdImpresora06Navigations)
                    .HasForeignKey(d => d.IdImpresora06)
                    .HasConstraintName("RefMSTt10_impresora123");
            });

            modelBuilder.Entity<Mstt13Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno)
                    .HasName("PK103")
                    .IsClustered(false);

                entity.ToTable("MSTt13_turno");

                entity.Property(e => e.IdTurno).HasColumnName("id_turno");

                entity.Property(e => e.CodTurno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_turno");

                entity.Property(e => e.HoraFin).HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt14Mesa>(entity =>
            {
                entity.HasKey(e => e.IdMesa)
                    .HasName("PK104")
                    .IsClustered(false);

                entity.ToTable("MSTt14_mesa");

                entity.Property(e => e.IdMesa).HasColumnName("id_mesa");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.CodMesa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_mesa");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.IdEstadoMesa).HasColumnName("id_estado_mesa");

                entity.Property(e => e.TxtNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_num");

                entity.HasOne(d => d.IdCanVtaNavigation)
                    .WithMany(p => p.Mstt14Mesas)
                    .HasForeignKey(d => d.IdCanVta)
                    .HasConstraintName("RefMSTt04_canal_vta157");

                entity.HasOne(d => d.IdEstadoMesaNavigation)
                    .WithMany(p => p.Mstt14Mesas)
                    .HasForeignKey(d => d.IdEstadoMesa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt15_estado_mesa159");
            });

            modelBuilder.Entity<Mstt15EstadoMesa>(entity =>
            {
                entity.HasKey(e => e.IdEstadoMesa)
                    .HasName("PK105")
                    .IsClustered(false);

                entity.ToTable("MSTt15_estado_mesa");

                entity.Property(e => e.IdEstadoMesa).HasColumnName("id_estado_mesa");

                entity.Property(e => e.CodEstadoMesa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_estado_mesa");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtColorHex)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("txt_color_hex");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Mstt16TipoRazon>(entity =>
            {
                entity.HasKey(e => e.IdTipoRazon)
                    .HasName("PK111_1")
                    .IsClustered(false);

                entity.ToTable("MSTt16_tipo_razon");

                entity.Property(e => e.IdTipoRazon).HasColumnName("id_tipo_razon");

                entity.Property(e => e.CodTipoRazon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_razon");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Pert010Condicion>(entity =>
            {
                entity.HasKey(e => e.IdCondicion)
                    .HasName("PK__PERt010___C92374002DE1D2B8");

                entity.ToTable("PERt010_Condicion");

                entity.Property(e => e.IdCondicion).HasColumnName("id_condicion");

                entity.Property(e => e.Txtdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txtdesc");
            });

            modelBuilder.Entity<Pert01Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PKCMR16_cod_usuario")
                    .IsClustered(false);

                entity.ToTable("PERt01_usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.CodUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_usuario");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdPassword).HasColumnName("id_password");

                entity.Property(e => e.SnUpdRequered).HasColumnName("sn_upd_requered");

                entity.Property(e => e.TxtClave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_clave");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Pert01Usuarios)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt04_empleado1371");
            });

            modelBuilder.Entity<Pert02Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PKCMRt13_id_sujeto")
                    .IsClustered(false);

                entity.ToTable("PERt02_cliente");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Celular1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular1");

                entity.Property(e => e.Celular2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular2");

                entity.Property(e => e.Celular3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular3");

                entity.Property(e => e.CodCliente)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_cliente");

                entity.Property(e => e.CodTipoPer)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_per")
                    .IsFixedLength();

                entity.Property(e => e.FecNac).HasColumnName("fec_nac");

                entity.Property(e => e.IdDist).HasColumnName("id_dist");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdEstadoCivil).HasColumnName("id_estado_civil");

                entity.Property(e => e.IdNacionalidad).HasColumnName("id_nacionalidad");

                entity.Property(e => e.IdTipoDocIdentidad).HasColumnName("id_tipo_doc_identidad");

                entity.Property(e => e.IdVia).HasColumnName("id_via");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.Info01)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Info05)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info05");

                entity.Property(e => e.Info06)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info06");

                entity.Property(e => e.Info07)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info07");

                entity.Property(e => e.Info08)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info08");

                entity.Property(e => e.Info09)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info09");

                entity.Property(e => e.Info10)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info10");

                entity.Property(e => e.NomVia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_via");

                entity.Property(e => e.NomZona)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_zona");

                entity.Property(e => e.NroDoc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_doc");

                entity.Property(e => e.NroRuc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_ruc");

                entity.Property(e => e.NroVia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nro_via");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.TelefFijo1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo1");

                entity.Property(e => e.TelefFijo2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo2");

                entity.Property(e => e.TelefFijo3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo3");

                entity.Property(e => e.TxtApeMat)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_mat");

                entity.Property(e => e.TxtApePat)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_pat");

                entity.Property(e => e.TxtDireccion1)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion1");

                entity.Property(e => e.TxtDireccion2)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion2");

                entity.Property(e => e.TxtEmail1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email1");

                entity.Property(e => e.TxtEmail2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email2");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNomComercial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_nom_comercial");

                entity.Property(e => e.TxtPriNom)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("txt_pri_nom");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.TxtRznSocial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_rzn_social");

                entity.Property(e => e.TxtSegNom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_seg_nom");

                entity.Property(e => e.TxtWeb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_web");

                entity.Property(e => e.UrlImg)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("url_img");

                entity.HasOne(d => d.IdDistNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdDist)
                    .HasConstraintName("RefSNTt33_distrito1041");

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdEstadoCivil)
                    .HasConstraintName("FKCMRt13_id_est_civil");

                entity.HasOne(d => d.IdNacionalidadNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdNacionalidad)
                    .HasConstraintName("RefSNTt14_nacionalidad1231");

                entity.HasOne(d => d.IdTipoDocIdentidadNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdTipoDocIdentidad)
                    .HasConstraintName("FKCMRt13_cod_tipo_doc");

                entity.HasOne(d => d.IdViaNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdVia)
                    .HasConstraintName("RefSNTt15_via1021");

                entity.HasOne(d => d.IdZonaNavigation)
                    .WithMany(p => p.Pert02Clientes)
                    .HasForeignKey(d => d.IdZona)
                    .HasConstraintName("RefSNTt16_zona1031");
            });

            modelBuilder.Entity<Pert03Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PKCMRt13_id_sujeto_1")
                    .IsClustered(false);

                entity.ToTable("PERt03_proveedor");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.Celular1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular1");

                entity.Property(e => e.Celular2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular2");

                entity.Property(e => e.Celular3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular3");

                entity.Property(e => e.CodProveedor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_proveedor");

                entity.Property(e => e.CodTipoPer)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_per")
                    .IsFixedLength();

                entity.Property(e => e.FecNac).HasColumnName("fec_nac");

                entity.Property(e => e.IdDist).HasColumnName("id_dist");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdEstadoCivil).HasColumnName("id_estado_civil");

                entity.Property(e => e.IdNacionalidad).HasColumnName("id_nacionalidad");

                entity.Property(e => e.IdTipoDocIdentidad).HasColumnName("id_tipo_doc_identidad");

                entity.Property(e => e.IdVia).HasColumnName("id_via");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.Info01)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Info05)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info05");

                entity.Property(e => e.Info06)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info06");

                entity.Property(e => e.Info07)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info07");

                entity.Property(e => e.Info08)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info08");

                entity.Property(e => e.Info09)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info09");

                entity.Property(e => e.Info10)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info10");

                entity.Property(e => e.NomVia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_via");

                entity.Property(e => e.NomZona)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_zona");

                entity.Property(e => e.NroDoc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_doc");

                entity.Property(e => e.NroRuc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_ruc");

                entity.Property(e => e.NroVia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nro_via");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.TelefFijo1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo1");

                entity.Property(e => e.TelefFijo2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo2");

                entity.Property(e => e.TelefFijo3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo3");

                entity.Property(e => e.TxtApeMat)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_mat");

                entity.Property(e => e.TxtApePat)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_pat");

                entity.Property(e => e.TxtDireccion1)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion1");

                entity.Property(e => e.TxtDireccion2)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion2");

                entity.Property(e => e.TxtEmail1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email1");

                entity.Property(e => e.TxtEmail2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email2");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNomComercial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_nom_comercial");

                entity.Property(e => e.TxtPriNom)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("txt_pri_nom");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.TxtRznSocial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_rzn_social");

                entity.Property(e => e.TxtSegNom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_seg_nom");

                entity.Property(e => e.TxtWeb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_web");

                entity.Property(e => e.UrlImg)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("url_img");

                entity.HasOne(d => d.IdDistNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdDist)
                    .HasConstraintName("RefSNTt33_distrito1051");

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdEstadoCivil)
                    .HasConstraintName("RefMSTt07_estado_civil1061");

                entity.HasOne(d => d.IdNacionalidadNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdNacionalidad)
                    .HasConstraintName("RefSNTt14_nacionalidad1221");

                entity.HasOne(d => d.IdTipoDocIdentidadNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdTipoDocIdentidad)
                    .HasConstraintName("RefSNTt02_tipo_doc_identidad1091");

                entity.HasOne(d => d.IdViaNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdVia)
                    .HasConstraintName("RefSNTt15_via1071");

                entity.HasOne(d => d.IdZonaNavigation)
                    .WithMany(p => p.Pert03Proveedors)
                    .HasForeignKey(d => d.IdZona)
                    .HasConstraintName("RefSNTt16_zona1081");
            });

            modelBuilder.Entity<Pert04Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PKCMRt13_id_per")
                    .IsClustered(false);

                entity.ToTable("PERt04_empleado");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Celular1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular1");

                entity.Property(e => e.Celular2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular2");

                entity.Property(e => e.Celular3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("celular3");

                entity.Property(e => e.CodEmpleado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_empleado");

                entity.Property(e => e.CodTipoPer)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_per")
                    .IsFixedLength();

                entity.Property(e => e.FecNac).HasColumnName("fec_nac");

                entity.Property(e => e.FechaCese).HasColumnName("fecha_cese");

                entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");

                entity.Property(e => e.IdCategoriaEmp).HasColumnName("id_categoria_emp");

                entity.Property(e => e.IdClaseEmp).HasColumnName("id_clase_emp");

                entity.Property(e => e.IdCondicionLaboral).HasColumnName("id_condicion_laboral");

                entity.Property(e => e.IdDist).HasColumnName("id_dist");

                entity.Property(e => e.IdEntidadFinanciera).HasColumnName("id_entidad_financiera");

                entity.Property(e => e.IdEspecialidadMedica).HasColumnName("id_especialidad_medica");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdEstadoCivil).HasColumnName("id_estado_civil");

                entity.Property(e => e.IdModalidadFormativa).HasColumnName("id_modalidad_formativa");

                entity.Property(e => e.IdMotivoBaja).HasColumnName("id_motivo_baja");

                entity.Property(e => e.IdNacionalidad).HasColumnName("id_nacionalidad");

                entity.Property(e => e.IdOcupacion).HasColumnName("id_ocupacion");

                entity.Property(e => e.IdPeriodoRemuneracion).HasColumnName("id_periodo_remuneracion");

                entity.Property(e => e.IdRegimenLaboral).HasColumnName("id_regimen_laboral");

                entity.Property(e => e.IdRegimenPensionario).HasColumnName("id_regimen_pensionario");

                entity.Property(e => e.IdRegimenSalud).HasColumnName("id_regimen_salud");

                entity.Property(e => e.IdSaludEps).HasColumnName("id_salud_eps");

                entity.Property(e => e.IdSituacion).HasColumnName("id_situacion");

                entity.Property(e => e.IdSituacionEducativa).HasColumnName("id_situacion_educativa");

                entity.Property(e => e.IdSuspencionLaboral).HasColumnName("id_suspencion_laboral");

                entity.Property(e => e.IdTipoDocIdentidad).HasColumnName("id_tipo_doc_identidad");

                entity.Property(e => e.IdTipoTrabajador).HasColumnName("id_tipo_trabajador");

                entity.Property(e => e.IdVia).HasColumnName("id_via");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.Info01)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Info05)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info05");

                entity.Property(e => e.Info06)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info06");

                entity.Property(e => e.Info07)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info07");

                entity.Property(e => e.Info08)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info08");

                entity.Property(e => e.Info09)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info09");

                entity.Property(e => e.Info10)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("info10");

                entity.Property(e => e.NomVia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_via");

                entity.Property(e => e.NomZona)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nom_zona");

                entity.Property(e => e.NroCuenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_cuenta");

                entity.Property(e => e.NroDoc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_doc");

                entity.Property(e => e.NroHoraMes)
                    .HasColumnType("decimal(8, 4)")
                    .HasColumnName("nro_hora_mes");

                entity.Property(e => e.NroRuc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_ruc");

                entity.Property(e => e.NroVia)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nro_via");

                entity.Property(e => e.SalarioHora)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_hora");

                entity.Property(e => e.SalarioMensual)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_mensual");

                entity.Property(e => e.SalarioQuincenal)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_quincenal");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.TelefFijo1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo1");

                entity.Property(e => e.TelefFijo2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo2");

                entity.Property(e => e.TelefFijo3)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("telef_fijo3");

                entity.Property(e => e.TxtApeMat)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_mat");

                entity.Property(e => e.TxtApePat)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_pat");

                entity.Property(e => e.TxtDireccion1)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion1");

                entity.Property(e => e.TxtDireccion2)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion2");

                entity.Property(e => e.TxtEmail1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email1");

                entity.Property(e => e.TxtEmail2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email2");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNomComercial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_nom_comercial");

                entity.Property(e => e.TxtPriNom)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("txt_pri_nom");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.TxtRznSocial)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_rzn_social");

                entity.Property(e => e.TxtSegNom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_seg_nom");

                entity.Property(e => e.TxtWeb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_web");

                entity.Property(e => e.UrlImg)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("url_img");

                entity.HasOne(d => d.IdCategoriaEmpNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdCategoriaEmp)
                    .HasConstraintName("RefPERt05_categoria_emp142");

                entity.HasOne(d => d.IdClaseEmpNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdClaseEmp)
                    .HasConstraintName("RefPERt06_clase_emp177");

                entity.HasOne(d => d.IdCondicionLaboralNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdCondicionLaboral)
                    .HasConstraintName("RefSNTt21_condicion_laboral1281");

                entity.HasOne(d => d.IdDistNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdDist)
                    .HasConstraintName("RefSNTt33_distrito1161");

                entity.HasOne(d => d.IdEntidadFinancieraNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdEntidadFinanciera)
                    .HasConstraintName("RefSNTt03_entidad_financiera1201");

                entity.HasOne(d => d.IdEspecialidadMedicaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdEspecialidadMedica)
                    .HasConstraintName("RefCLIt07_especialidad_medica1601");

                entity.HasOne(d => d.IdEstadoCivilNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdEstadoCivil)
                    .HasConstraintName("RefMSTt07_estado_civil1171");

                entity.HasOne(d => d.IdModalidadFormativaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdModalidadFormativa)
                    .HasConstraintName("RefSNTt26_modalidad_formativa1331");

                entity.HasOne(d => d.IdMotivoBajaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdMotivoBaja)
                    .HasConstraintName("RefSNTt25_motivo_baja1321");

                entity.HasOne(d => d.IdNacionalidadNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdNacionalidad)
                    .HasConstraintName("RefSNTt14_nacionalidad1211");

                entity.HasOne(d => d.IdOcupacionNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdOcupacion)
                    .HasConstraintName("RefSNTt19_ocupacion1261");

                entity.HasOne(d => d.IdPeriodoRemuneracionNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdPeriodoRemuneracion)
                    .HasConstraintName("RefSNTt22_periodo_remuneracion1291");

                entity.HasOne(d => d.IdRegimenLaboralNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdRegimenLaboral)
                    .HasConstraintName("RefSNTt30_regimen_laboral1361");

                entity.HasOne(d => d.IdRegimenPensionarioNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdRegimenPensionario)
                    .HasConstraintName("RefSNTt20_regimen_pensionario1271");

                entity.HasOne(d => d.IdRegimenSaludNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdRegimenSalud)
                    .HasConstraintName("RefSNTt29_regimen_salud1351");

                entity.HasOne(d => d.IdSaludEpsNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdSaludEps)
                    .HasConstraintName("RefSNTt23_salud_eps1301");

                entity.HasOne(d => d.IdSituacionNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdSituacion)
                    .HasConstraintName("RefSNTt24_situacion1311");

                entity.HasOne(d => d.IdSituacionEducativaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdSituacionEducativa)
                    .HasConstraintName("RefSNTt18_situacion_educativa1251");

                entity.HasOne(d => d.IdSuspencionLaboralNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdSuspencionLaboral)
                    .HasConstraintName("RefSNTt28_suspencion_laboral1341");

                entity.HasOne(d => d.IdTipoDocIdentidadNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdTipoDocIdentidad)
                    .HasConstraintName("RefSNTt02_tipo_doc_identidad1151");

                entity.HasOne(d => d.IdTipoTrabajadorNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdTipoTrabajador)
                    .HasConstraintName("RefSNTt17_tipo_trabajador1241");

                entity.HasOne(d => d.IdViaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdVia)
                    .HasConstraintName("RefSNTt15_via1181");

                entity.HasOne(d => d.IdZonaNavigation)
                    .WithMany(p => p.Pert04Empleados)
                    .HasForeignKey(d => d.IdZona)
                    .HasConstraintName("RefSNTt16_zona1191");
            });

            modelBuilder.Entity<Pert05CategoriaEmp>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaEmp)
                    .HasName("PK110_1")
                    .IsClustered(false);

                entity.ToTable("PERt05_categoria_emp");

                entity.Property(e => e.IdCategoriaEmp).HasColumnName("id_categoria_emp");

                entity.Property(e => e.CodCategoriaEmp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_categoria_emp");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(750)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_nombre");
            });

            modelBuilder.Entity<Pert06ClaseEmp>(entity =>
            {
                entity.HasKey(e => e.IdClaseEmp)
                    .HasName("PK111")
                    .IsClustered(false);

                entity.ToTable("PERt06_clase_emp");

                entity.Property(e => e.IdClaseEmp).HasColumnName("id_clase_emp");

                entity.Property(e => e.CodClaseEmp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_clase_emp");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(750)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_nombre");
            });

            modelBuilder.Entity<Pert07AccessItem>(entity =>
            {
                entity.HasKey(e => e.IdAccessItem)
                    .HasName("PK127")
                    .IsClustered(false);

                entity.ToTable("PERt07_access_item");

                entity.Property(e => e.IdAccessItem)
                    .ValueGeneratedNever()
                    .HasColumnName("id_access_item");

                entity.Property(e => e.AppCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("app_code");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Pert08SecurityAccess>(entity =>
            {
                entity.HasKey(e => e.IdSecurityAccess)
                    .HasName("PK128")
                    .IsClustered(false);

                entity.ToTable("PERt08_security_access");

                entity.Property(e => e.IdSecurityAccess).HasColumnName("id_security_access");

                entity.Property(e => e.IdAccessItem).HasColumnName("id_access_item");

                entity.Property(e => e.IdClaseEmp).HasColumnName("id_clase_emp");

                entity.Property(e => e.SnAdd).HasColumnName("sn_add");

                entity.Property(e => e.SnDel).HasColumnName("sn_del");

                entity.Property(e => e.SnFull).HasColumnName("sn_full");

                entity.Property(e => e.SnNone).HasColumnName("sn_none");

                entity.Property(e => e.SnRead).HasColumnName("sn_read");

                entity.Property(e => e.SnUpd).HasColumnName("sn_upd");

                entity.HasOne(d => d.IdAccessItemNavigation)
                    .WithMany(p => p.Pert08SecurityAccesses)
                    .HasForeignKey(d => d.IdAccessItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt07_access_item173");

                entity.HasOne(d => d.IdClaseEmpNavigation)
                    .WithMany(p => p.Pert08SecurityAccesses)
                    .HasForeignKey(d => d.IdClaseEmp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt06_clase_emp174");
            });

            modelBuilder.Entity<Pert09Inversionistum>(entity =>
            {
                entity.HasKey(e => e.IdInversionista)
                    .HasName("PK__PERt09_i__5FD635CFDF174042");

                entity.ToTable("PERt09_inversionista");

                entity.Property(e => e.IdInversionista).HasColumnName("id_inversionista");

                entity.Property(e => e.Celular1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("celular1");

                entity.Property(e => e.Celular2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("celular2");

                entity.Property(e => e.CodInversionista)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_inversionista");

                entity.Property(e => e.FechNac)
                    .HasColumnType("date")
                    .HasColumnName("fech_nac");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");

                entity.Property(e => e.Info01)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroDoc)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nro_doc");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sexo")
                    .IsFixedLength();

                entity.Property(e => e.TelfFijo1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telf_fijo1");

                entity.Property(e => e.TelfFijo2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telf_fijo2");

                entity.Property(e => e.TxtApeMat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_mat");

                entity.Property(e => e.TxtApePat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_ape_pat");

                entity.Property(e => e.TxtDireccion1)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion1");

                entity.Property(e => e.TxtDireccion2)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_direccion2");

                entity.Property(e => e.TxtEmail1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email1");

                entity.Property(e => e.TxtEmail2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_email2");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtPrimNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_prim_nom");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.TxtRznScl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_rzn_scl");

                entity.Property(e => e.TxtSegunNom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_segun_nom");

                entity.HasOne(d => d.IdTipoDocumentoNavigation)
                    .WithMany(p => p.Pert09Inversionista)
                    .HasForeignKey(d => d.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt09_in__id_ti__12F3B011");
            });

            modelBuilder.Entity<Pert10Concepto>(entity =>
            {
                entity.HasKey(e => e.IdConcepto)
                    .HasName("PK__PERt10_c__4B70853EF94BCFFC");

                entity.ToTable("PERt10_concepto");

                entity.Property(e => e.IdConcepto).HasColumnName("id_concepto");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");
            });

            modelBuilder.Entity<Pert11PagoPersonal>(entity =>
            {
                entity.HasKey(e => e.IdPagoPersonal)
                    .HasName("PK__PERt11_p__7095985A00E96146");

                entity.ToTable("PERt11_pago_personal");

                entity.Property(e => e.IdPagoPersonal).HasColumnName("Id_pago_personal");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdAutorizador).HasColumnName("id_autorizador");

                entity.Property(e => e.IdCampaña).HasColumnName("id_campaña");

                entity.Property(e => e.IdConcepto).HasColumnName("id_concepto");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdPredio).HasColumnName("id_predio");

                entity.Property(e => e.Mes)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("mes");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAutorizadorNavigation)
                    .WithMany(p => p.Pert11PagoPersonals)
                    .HasForeignKey(d => d.IdAutorizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt11_pa__id_au__24DD5622");

                entity.HasOne(d => d.IdCampañaNavigation)
                    .WithMany(p => p.Pert11PagoPersonals)
                    .HasForeignKey(d => d.IdCampaña)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt11_pa__id_ca__26C59E94");

                entity.HasOne(d => d.IdConceptoNavigation)
                    .WithMany(p => p.Pert11PagoPersonals)
                    .HasForeignKey(d => d.IdConcepto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt11_pa__id_co__27B9C2CD");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Pert11PagoPersonals)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt11_pa__id_em__23E931E9");

                entity.HasOne(d => d.IdPredioNavigation)
                    .WithMany(p => p.Pert11PagoPersonals)
                    .HasForeignKey(d => d.IdPredio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PERt11_pa__id_pr__25D17A5B");
            });

            modelBuilder.Entity<Pret01Predio>(entity =>
            {
                entity.HasKey(e => e.IdPredio)
                    .HasName("PK__PREt01_p__A7C80C24E46ED92F");

                entity.ToTable("PREt01_predio");

                entity.HasIndex(e => e.NroSitio, "UQ__PREt01_p__4FA4A59CC2B346F4")
                    .IsUnique();

                entity.Property(e => e.IdPredio).HasColumnName("id_predio");

                entity.Property(e => e.Area)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("area");

                entity.Property(e => e.Coordenadas)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("coordenadas");

                entity.Property(e => e.FechaAdquisicion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_adquisicion");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("date")
                    .HasColumnName("fecha_compra");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdInversionista).HasColumnName("id_inversionista");

                entity.Property(e => e.IdTipoPredio).HasColumnName("id_tipo_predio");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.NroComprobante)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nro_comprobante");

                entity.Property(e => e.NroHectareas)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("nro_hectareas");

                entity.Property(e => e.NroSitio)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nro_sitio");

                entity.Property(e => e.PartidaRegistral)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Partida_Registral");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.Property(e => e.UnidadCatastral)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("unidad_catastral");

                entity.HasOne(d => d.IdDistritoNavigation)
                    .WithMany(p => p.Pret01Predios)
                    .HasForeignKey(d => d.IdDistrito)
                    .HasConstraintName("FK__PREt01_pr__id_di__1C7D1A4B");

                entity.HasOne(d => d.IdInversionistaNavigation)
                    .WithMany(p => p.Pret01Predios)
                    .HasForeignKey(d => d.IdInversionista)
                    .HasConstraintName("FK__PREt01_pr__id_in__1A94D1D9");

                entity.HasOne(d => d.IdTipoPredioNavigation)
                    .WithMany(p => p.Pret01Predios)
                    .HasForeignKey(d => d.IdTipoPredio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt01_pr__id_ti__1B88F612");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret01PredioIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt01_pr__id_us__18AC8967");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret01PredioIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt01_pr__id_us__19A0ADA0");
            });

            modelBuilder.Entity<Pret02Campana>(entity =>
            {
                entity.HasKey(e => e.IdCampana)
                    .HasName("PK__PREt02_C__CAA2C8F732080360");

                entity.ToTable("PREt02_Campana");

                entity.Property(e => e.IdCampana).HasColumnName("id_campana");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.CodigoCampana)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("codigo_campana");

                entity.Property(e => e.Coordenadas)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("coordenadas");

                entity.Property(e => e.FechaFina)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_fina");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdPredio).HasColumnName("id_predio");

                entity.Property(e => e.IdTipoCampana).HasColumnName("id_tipo_campana");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.NroArboles).HasColumnName("nro_arboles");

                entity.Property(e => e.NroHectarea).HasColumnName("nro_hectarea");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdDistritoNavigation)
                    .WithMany(p => p.Pret02Campanas)
                    .HasForeignKey(d => d.IdDistrito)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt02_Ca__id_di__2ACB39A2");

                entity.HasOne(d => d.IdPredioNavigation)
                    .WithMany(p => p.Pret02Campanas)
                    .HasForeignKey(d => d.IdPredio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt02_Ca__id_pr__28E2F130");

                entity.HasOne(d => d.IdTipoCampanaNavigation)
                    .WithMany(p => p.Pret02Campanas)
                    .HasForeignKey(d => d.IdTipoCampana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt02_Ca__id_ti__29D71569");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret02CampanaIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt02_Ca__id_us__26FAA8BE");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret02CampanaIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt02_Ca__id_us__27EECCF7");
            });

            modelBuilder.Entity<Pret03TipoCampana>(entity =>
            {
                entity.HasKey(e => e.IdTipoCampana)
                    .HasName("PK__PREt03_T__192A1BF741C6485F");

                entity.ToTable("PREt03_Tipo_Campana");

                entity.Property(e => e.IdTipoCampana).HasColumnName("id_tipo_campana");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Pret04CampanaTipoArbol>(entity =>
            {
                entity.HasKey(e => e.IdCampanaTipoarbol)
                    .HasName("PK__PREt04_C__2C6930E581A110AD");

                entity.ToTable("PREt04_Campana_TipoArbol");

                entity.Property(e => e.IdCampanaTipoarbol).HasColumnName("id_campana_tipoarbol");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Coordenadas)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("coordenadas");

                entity.Property(e => e.IdCampana).HasColumnName("id_campana");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoarbol).HasColumnName("id_tipoarbol");

                entity.Property(e => e.Latitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("longitud");

                entity.Property(e => e.NroArboles).HasColumnName("nro_arboles");

                entity.Property(e => e.NroHectareas).HasColumnName("nro_hectareas");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtTipoarbol)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Txt_Tipoarbol");

                entity.HasOne(d => d.IdCampanaNavigation)
                    .WithMany(p => p.Pret04CampanaTipoArbols)
                    .HasForeignKey(d => d.IdCampana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt04_Ca__id_ca__2F8FEEBF");

                entity.HasOne(d => d.IdTipoarbolNavigation)
                    .WithMany(p => p.Pret04CampanaTipoArbols)
                    .HasForeignKey(d => d.IdTipoarbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt04_Ca__id_ti__308412F8");
            });

            modelBuilder.Entity<Pret05CtrlCalidad>(entity =>
            {
                entity.HasKey(e => e.IdCtrlCalidad)
                    .HasName("PK__PREt05_C__ABDEDB3292614122");

                entity.ToTable("PREt05_Ctrl_Calidad");

                entity.Property(e => e.IdCtrlCalidad).HasColumnName("id_ctrl_calidad");

                entity.Property(e => e.Defecto)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("defecto");

                entity.Property(e => e.FechaRevision)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_revision");

                entity.Property(e => e.IdCampanaTipoarbol).HasColumnName("id_campana_tipoarbol");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoMo).HasColumnName("id_tipo_mo");

                entity.Property(e => e.Indicaciones)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("indicaciones");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.MaderaApta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("madera_apta");

                entity.Property(e => e.MaderaObservada)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("madera_observada");

                entity.Property(e => e.MaderaRechazada)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("madera_rechazada");

                entity.Property(e => e.MotivoRechazo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("motivo_rechazo");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdCampanaTipoarbolNavigation)
                    .WithMany(p => p.Pret05CtrlCalidads)
                    .HasForeignKey(d => d.IdCampanaTipoarbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt05_Ct__id_ca__3548C815");

                entity.HasOne(d => d.IdTipoMoNavigation)
                    .WithMany(p => p.Pret05CtrlCalidads)
                    .HasForeignKey(d => d.IdTipoMo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt05_Ct__id_ti__363CEC4E");
            });

            modelBuilder.Entity<Pret06TipoArbol>(entity =>
            {
                entity.HasKey(e => e.IdTipoarbol)
                    .HasName("PK__PREt06_T__5B19BD3B516B5904");

                entity.ToTable("PREt06_TipoArbol");

                entity.Property(e => e.IdTipoarbol).HasColumnName("id_tipoarbol");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.Txtdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txtdesc");
            });

            modelBuilder.Entity<Pret07Extraccion>(entity =>
            {
                entity.HasKey(e => e.IdExtraccion)
                    .HasName("PK__PREt07_E__53647F40E1047E45");

                entity.ToTable("PREt07_Extraccion");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.AltArbolProTotal).HasColumnName("alt_arbol_pro_total");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.DiamProTotal).HasColumnName("diam_pro_total");

                entity.Property(e => e.FechaExtraccion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_extraccion");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdCampana).HasColumnName("id_campana");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroArbolesTotal).HasColumnName("nro_arboles_total");

                entity.Property(e => e.NroExtraccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_extraccion");

                entity.Property(e => e.NroTrozosTotal).HasColumnName("nro_trozos_total");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNumero)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero");

                entity.Property(e => e.TxtSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdCampanaNavigation)
                    .WithMany(p => p.Pret07Extraccions)
                    .HasForeignKey(d => d.IdCampana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt07_Ex__id_ca__3B01A16B");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret07ExtraccionIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt07_Ex__id_us__391958F9");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret07ExtraccionIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt07_Ex__id_us__3A0D7D32");
            });

            modelBuilder.Entity<Pret08ExtraccionDtl>(entity =>
            {
                entity.HasKey(e => e.IdExtracciondtl)
                    .HasName("PK__PREt08_E__65C45592769CFE84");

                entity.ToTable("PREt08_ExtraccionDtl");

                entity.Property(e => e.IdExtracciondtl).HasColumnName("id_extracciondtl");

                entity.Property(e => e.AltArbolPro).HasColumnName("alt_arbol_pro");

                entity.Property(e => e.CodigoExtraccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.DiamPro).HasColumnName("diam_pro");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.IdTipoArbol).HasColumnName("id_tipoArbol");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroArboles).HasColumnName("nro_arboles");

                entity.Property(e => e.NroTrozos).HasColumnName("nro_trozos");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtTipoArbol)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_tipoArbol");

                entity.HasOne(d => d.IdExtraccionNavigation)
                    .WithMany(p => p.Pret08ExtraccionDtls)
                    .HasForeignKey(d => d.IdExtraccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt08_Ex__id_ex__41AE9EFA");

                entity.HasOne(d => d.IdTipoArbolNavigation)
                    .WithMany(p => p.Pret08ExtraccionDtls)
                    .HasForeignKey(d => d.IdTipoArbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt08_Ex__id_ti__42A2C333");
            });

            modelBuilder.Entity<Pret09TipoPredio>(entity =>
            {
                entity.HasKey(e => e.IdTipoPredio)
                    .HasName("PK__PREt09_T__8B9EFD1DC027CC3F");

                entity.ToTable("PREt09_Tipo_Predio");

                entity.Property(e => e.IdTipoPredio).HasColumnName("id_tipo_predio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Pret10Envio>(entity =>
            {
                entity.HasKey(e => e.IdEnvio)
                    .HasName("PK__PREt10_E__8C48C8CA08DAF5FB");

                entity.ToTable("PREt10_Envio");

                entity.Property(e => e.IdEnvio).HasColumnName("id_envio");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.EnvioCant).HasColumnName("envio_cant");

                entity.Property(e => e.FechaEnvio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_envio");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdLocationTo).HasColumnName("id_location_to");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.NroEnvio)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_envio");

                entity.Property(e => e.NroGuia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_guia");

                entity.Property(e => e.NroGuiaTransp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_guia_transp");

                entity.Property(e => e.NroPlaca)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nro_placa");

                entity.Property(e => e.TipoEnvio)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_envio");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNro)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("txt_nro");

                entity.Property(e => e.TxtSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdExtraccionNavigation)
                    .WithMany(p => p.Pret10Envios)
                    .HasForeignKey(d => d.IdExtraccion)
                    .HasConstraintName("FK__PREt10_En__id_ex__494FC0C2");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Pret10EnvioIdLocationNavigations)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt10_En__id_lo__47677850");

                entity.HasOne(d => d.IdLocationToNavigation)
                    .WithMany(p => p.Pret10EnvioIdLocationToNavigations)
                    .HasForeignKey(d => d.IdLocationTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt10_En__id_lo__485B9C89");

                entity.HasOne(d => d.IdTipoCompNavigation)
                    .WithMany(p => p.Pret10Envios)
                    .HasForeignKey(d => d.IdTipoComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PREt10_Envio_TipoComp");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret10EnvioIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt10_En__id_us__457F2FDE");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret10EnvioIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt10_En__id_us__46735417");
            });

            modelBuilder.Entity<Pret11Recepcion>(entity =>
            {
                entity.HasKey(e => e.IdRecepcion)
                    .HasName("PK__PREt11_R__BDEB0343C6E57872");

                entity.ToTable("PREt11_Recepcion");

                entity.Property(e => e.IdRecepcion).HasColumnName("id_recepcion");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.FechaRecepcion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_recepcion");

                entity.Property(e => e.IdEnvio).HasColumnName("id_envio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdLocationTo).HasColumnName("id_location_to");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.NroGuia)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nro_guia");

                entity.Property(e => e.NroGuiaTransp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_guia_transp");

                entity.Property(e => e.NroPalaca)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nro_palaca");

                entity.Property(e => e.NroRecepcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_recepcion");

                entity.Property(e => e.RecepcionCant).HasColumnName("recepcion_cant");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNro)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("txt_nro");

                entity.Property(e => e.TxtSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdEnvioNavigation)
                    .WithMany(p => p.Pret11Recepcions)
                    .HasForeignKey(d => d.IdEnvio)
                    .HasConstraintName("FK__PREt11_Re__id_en__4E1475DF");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Pret11RecepcionIdLocationNavigations)
                    .HasForeignKey(d => d.IdLocation)
                    .HasConstraintName("FK_PREt11_Recepcion_location");

                entity.HasOne(d => d.IdLocationToNavigation)
                    .WithMany(p => p.Pret11RecepcionIdLocationToNavigations)
                    .HasForeignKey(d => d.IdLocationTo)
                    .HasConstraintName("FK_PREt11_Recepcion_location_to");

                entity.HasOne(d => d.IdTipoCompNavigation)
                    .WithMany(p => p.Pret11Recepcions)
                    .HasForeignKey(d => d.IdTipoComp)
                    .HasConstraintName("FK_PREt11_Recepcion_tipo_comp");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret11RecepcionIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt11_Re__id_us__4C2C2D6D");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret11RecepcionIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt11_Re__id_us__4D2051A6");
            });

            modelBuilder.Entity<Pret12RecepcionDtl>(entity =>
            {
                entity.HasKey(e => e.IdRecepciondtl)
                    .HasName("PK__PREt12_R__2A69D8BF56BC6D69");

                entity.ToTable("PREt12_RecepcionDTL");

                entity.Property(e => e.IdRecepciondtl).HasColumnName("id_recepciondtl");

                entity.Property(e => e.AltArbolPro).HasColumnName("alt_arbol_pro");

                entity.Property(e => e.CodigoRecepcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.DiamPro).HasColumnName("diam_pro");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdRecepcion).HasColumnName("id_recepcion");

                entity.Property(e => e.IdTipoArbol).HasColumnName("id_tipoArbol");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroArboles).HasColumnName("nro_arboles");

                entity.Property(e => e.NroTrozos).HasColumnName("nro_trozos");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtTipoArbol)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_tipoArbol");

                entity.HasOne(d => d.IdRecepcionNavigation)
                    .WithMany(p => p.Pret12RecepcionDtls)
                    .HasForeignKey(d => d.IdRecepcion)
                    .HasConstraintName("FK__PREt12_Re__id_re__50F0E28A");

                entity.HasOne(d => d.IdTipoArbolNavigation)
                    .WithMany(p => p.Pret12RecepcionDtls)
                    .HasForeignKey(d => d.IdTipoArbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt12_Re__id_ti__51E506C3");
            });

            modelBuilder.Entity<Pret13EnvioDtl>(entity =>
            {
                entity.HasKey(e => e.IdEnviodtl)
                    .HasName("PK__PREt13_E__D29E9D7C8F4202A1");

                entity.ToTable("PREt13_EnvioDTL");

                entity.Property(e => e.IdEnviodtl).HasColumnName("id_enviodtl");

                entity.Property(e => e.AltArbolPro).HasColumnName("alt_arbol_pro");

                entity.Property(e => e.CodigoExtraccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Comentario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("comentario");

                entity.Property(e => e.DiamPro).HasColumnName("diam_pro");

                entity.Property(e => e.IdEnvio).HasColumnName("id_envio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoArbol).HasColumnName("id_tipoArbol");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroArboles).HasColumnName("nro_arboles");

                entity.Property(e => e.NroTrozos).HasColumnName("nro_trozos");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtTipoArbol)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_tipoArbol");

                entity.HasOne(d => d.IdEnvioNavigation)
                    .WithMany(p => p.Pret13EnvioDtls)
                    .HasForeignKey(d => d.IdEnvio)
                    .HasConstraintName("FK__PREt13_En__id_en__54C1736E");

                entity.HasOne(d => d.IdTipoArbolNavigation)
                    .WithMany(p => p.Pret13EnvioDtls)
                    .HasForeignKey(d => d.IdTipoArbol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt13_En__id_ti__55B597A7");
            });

            modelBuilder.Entity<Pret14Produccion>(entity =>
            {
                entity.HasKey(e => e.IdProduccion)
                    .HasName("PK__PREt14_P__9EBBA43342361041");

                entity.ToTable("PREt14_Produccion");

                entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");

                entity.Property(e => e.CantidadIns).HasColumnName("cantidadIns");

                entity.Property(e => e.ComentarioPro)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("comentario_pro");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_modificacion");

                entity.Property(e => e.FechaProduccion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_produccion");

                entity.Property(e => e.IdCampana).HasColumnName("id_campana");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.IdPredio).HasColumnName("id_predio");

                entity.Property(e => e.IdProductoIns).HasColumnName("id_productoIns");

                entity.Property(e => e.IdUmIns).HasColumnName("id_umIns");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.NroPro)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nro_pro");

                entity.Property(e => e.TipoPro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_pro");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtProIns)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_proIns");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdCampanaNavigation)
                    .WithMany(p => p.Pret14Produccions)
                    .HasForeignKey(d => d.IdCampana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt14_Pr__id_ca__76AC771E");

                entity.HasOne(d => d.IdExtraccionNavigation)
                    .WithMany(p => p.Pret14Produccions)
                    .HasForeignKey(d => d.IdExtraccion)
                    .HasConstraintName("FK__PREt14_Pr__id_ex__7988E3C9");

                entity.HasOne(d => d.IdPredioNavigation)
                    .WithMany(p => p.Pret14Produccions)
                    .HasForeignKey(d => d.IdPredio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt14_Pr__id_pr__75B852E5");

                entity.HasOne(d => d.IdProductoInsNavigation)
                    .WithMany(p => p.Pret14Produccions)
                    .HasForeignKey(d => d.IdProductoIns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt14_Pr__id_pr__74C42EAC");

                entity.HasOne(d => d.IdUmInsNavigation)
                    .WithMany(p => p.Pret14Produccions)
                    .HasForeignKey(d => d.IdUmIns)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt14_Pr__id_um__73D00A73");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Pret14ProduccionIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt14_Pr__id_us__77A09B57");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Pret14ProduccionIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("FK__PREt14_Pr__id_us__7894BF90");
            });

            modelBuilder.Entity<Pret15ProduccionDtl>(entity =>
            {
                entity.HasKey(e => e.IdProducciondtl)
                    .HasName("PK__PREt15_P__D0B83A26A020F197");

                entity.ToTable("PREt15_ProduccionDTL");

                entity.Property(e => e.IdProducciondtl).HasColumnName("id_producciondtl");

                entity.Property(e => e.CantidadPro).HasColumnName("cantidadPro");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");

                entity.Property(e => e.IdProductoPro).HasColumnName("id_productoPro");

                entity.Property(e => e.IdUmPro).HasColumnName("id_umPro");

                entity.Property(e => e.TotalProp).HasColumnName("total_prop");

                entity.Property(e => e.TxtComentario)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_comentario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtProPro)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("txt_proPro");

                entity.HasOne(d => d.IdProduccionNavigation)
                    .WithMany(p => p.Pret15ProduccionDtls)
                    .HasForeignKey(d => d.IdProduccion)
                    .HasConstraintName("FK__PREt15_Pr__id_pr__021E29CA");

                entity.HasOne(d => d.IdProductoProNavigation)
                    .WithMany(p => p.Pret15ProduccionDtls)
                    .HasForeignKey(d => d.IdProductoPro)
                    .HasConstraintName("FK__PREt15_Pr__id_pr__0035E158");

                entity.HasOne(d => d.IdUmProNavigation)
                    .WithMany(p => p.Pret15ProduccionDtls)
                    .HasForeignKey(d => d.IdUmPro)
                    .HasConstraintName("FK__PREt15_Pr__id_um__012A0591");
            });

            modelBuilder.Entity<Pret16Merma>(entity =>
            {
                entity.HasKey(e => e.IdMerma)
                    .HasName("PK__PREt16_M__5692DDF2C7591764");

                entity.ToTable("PREt16_Merma");

                entity.Property(e => e.IdMerma).HasColumnName("id_merma");

                entity.Property(e => e.FechaMerma)
                    .HasColumnType("date")
                    .HasColumnName("fecha_merma");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdExtraccionNavigation)
                    .WithMany(p => p.Pret16Mermas)
                    .HasForeignKey(d => d.IdExtraccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt16_Me__id_ex__05EEBAAE");

                entity.HasOne(d => d.IdProduccionNavigation)
                    .WithMany(p => p.Pret16Mermas)
                    .HasForeignKey(d => d.IdProduccion)
                    .HasConstraintName("FK__PREt16_Me__id_pr__04FA9675");
            });

            modelBuilder.Entity<Pret16TipoDireccion>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK__Pret16_t__70A6B7E7C8D936DE");

                entity.ToTable("Pret16_tipo_direccion");

                entity.Property(e => e.IdTipo).HasColumnName("Id_tipo");

                entity.Property(e => e.IdEstado).HasColumnName("id_Estado");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_Estado");

                entity.Property(e => e.TxtTipo)
                    .HasMaxLength(255)
                    .HasColumnName("txt_tipo");
            });

            modelBuilder.Entity<Pret17Archivo>(entity =>
            {
                entity.HasKey(e => e.IdArchivo)
                    .HasName("PK__Pret17_A__26B92111B3ECB213");

                entity.ToTable("Pret17_Archivos");

                entity.Property(e => e.FechaCargaArchivo).HasColumnType("datetime");

                entity.Property(e => e.IdEnvio).HasColumnName("Id_ENVIO");

                entity.Property(e => e.IdEstado).HasColumnName("id_Estado");

                entity.Property(e => e.IdPredio).HasColumnName("Id_PREDIO");

                entity.Property(e => e.IdRecepcion).HasColumnName("id_RECEPCION");

                entity.Property(e => e.IdTipoDir).HasColumnName("Id_tipo_dir");

                entity.Property(e => e.NombreArchivo).HasMaxLength(1000);

                entity.Property(e => e.RutaArchivo).HasMaxLength(1000);

                entity.Property(e => e.TipoArchivo).HasMaxLength(1000);

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_Estado");

                entity.HasOne(d => d.IdEnvioNavigation)
                    .WithMany(p => p.Pret17Archivos)
                    .HasForeignKey(d => d.IdEnvio)
                    .HasConstraintName("FK_Pret17_Archivos_EnvioTabla");

                entity.HasOne(d => d.IdPredioNavigation)
                    .WithMany(p => p.Pret17Archivos)
                    .HasForeignKey(d => d.IdPredio)
                    .HasConstraintName("FK__Pret17_Ar__Id_PR__2235F3A1");

                entity.HasOne(d => d.IdRecepcionNavigation)
                    .WithMany(p => p.Pret17Archivos)
                    .HasForeignKey(d => d.IdRecepcion)
                    .HasConstraintName("FK_Pret17_Archivos_RecepcionTabla");

                entity.HasOne(d => d.IdTipoDirNavigation)
                    .WithMany(p => p.Pret17Archivos)
                    .HasForeignKey(d => d.IdTipoDir)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pret17_Ar__Id_ti__2141CF68");
            });

            modelBuilder.Entity<Pret17MermaDtl>(entity =>
            {
                entity.HasKey(e => e.IdMermadtl)
                    .HasName("PK__PREt17_M__45503A9A37172721");

                entity.ToTable("PREt17_MermaDTL");

                entity.Property(e => e.IdMermadtl).HasColumnName("id_mermadtl");

                entity.Property(e => e.FechaMermadtl)
                    .HasColumnType("date")
                    .HasColumnName("fecha_mermadtl");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdMerma).HasColumnName("id_merma");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdMermaNavigation)
                    .WithMany(p => p.Pret17MermaDtls)
                    .HasForeignKey(d => d.IdMerma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt17_Me__id_me__08CB2759");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Pret17MermaDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt17_Me__id_pr__09BF4B92");
            });

            modelBuilder.Entity<Pret18TipoMotivo>(entity =>
            {
                entity.HasKey(e => e.IdTipoMo)
                    .HasName("PK__PREt18_t__BF3E46E742CAE3BF");

                entity.ToTable("PREt18_tipo_motivo");

                entity.Property(e => e.IdTipoMo).HasColumnName("id_tipo_mo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.Txtdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txtdesc");
            });

            modelBuilder.Entity<Pret19ExtraccionEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpext)
                    .HasName("PK__PREt19_E__9140066DEF8DC995");

                entity.ToTable("PREt19_Extraccion_Empleado");

                entity.Property(e => e.IdEmpext).HasColumnName("id_empext");

                entity.Property(e => e.IdEmpleadoEx).HasColumnName("id_empleado_ex");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdExtraccion).HasColumnName("id_extraccion");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.SalarioEmp)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_emp");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdEmpleadoExNavigation)
                    .WithMany(p => p.Pret19ExtraccionEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoEx)
                    .HasConstraintName("FK_empcortador");

                entity.HasOne(d => d.IdExtraccionNavigation)
                    .WithMany(p => p.Pret19ExtraccionEmpleados)
                    .HasForeignKey(d => d.IdExtraccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt19_Ex__id_ex__3ED2324F");
            });

            modelBuilder.Entity<Pret20ProduccionEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEmppro)
                    .HasName("PK__PREt20_P__938B85FBAFCB87FD");

                entity.ToTable("PREt20_Produccion_Empleado");

                entity.Property(e => e.IdEmppro).HasColumnName("id_emppro");

                entity.Property(e => e.IdEmpleadoPro).HasColumnName("id_empleado_pro");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProduccion).HasColumnName("id_produccion");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Salario)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdEmpleadoProNavigation)
                    .WithMany(p => p.Pret20ProduccionEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoPro)
                    .HasConstraintName("FK_empleado");

                entity.HasOne(d => d.IdProduccionNavigation)
                    .WithMany(p => p.Pret20ProduccionEmpleados)
                    .HasForeignKey(d => d.IdProduccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt20_Pr__id_pr__7D5974AD");
            });

            modelBuilder.Entity<Pret21EnvioEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEnvemp)
                    .HasName("PK__PREt21_E__7F313DCC2E5729FE");

                entity.ToTable("PREt21_Envio_Empleado");

                entity.Property(e => e.IdEnvemp).HasColumnName("id_envemp");

                entity.Property(e => e.IdEmpleadoEnv).HasColumnName("id_empleado_env");

                entity.Property(e => e.IdEnvio).HasColumnName("id_envio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.SalarioEmp)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_emp");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdEmpleadoEnvNavigation)
                    .WithMany(p => p.Pret21EnvioEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoEnv)
                    .HasConstraintName("FKenv_empleado");

                entity.HasOne(d => d.IdEnvioNavigation)
                    .WithMany(p => p.Pret21EnvioEmpleados)
                    .HasForeignKey(d => d.IdEnvio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt21_En__id_en__5986288B");
            });

            modelBuilder.Entity<Pret22RecepcionEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdRecemp)
                    .HasName("PK__PREt22_R__11DB8B5CA6B2413A");

                entity.ToTable("PREt22_Recepcion_Empleado");

                entity.Property(e => e.IdRecemp).HasColumnName("id_recemp");

                entity.Property(e => e.IdEmpleadoRec).HasColumnName("id_empleado_rec");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdRecepcion).HasColumnName("id_recepcion");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.SalarioEmp)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario_emp");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdEmpleadoRecNavigation)
                    .WithMany(p => p.Pret22RecepcionEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoRec)
                    .HasConstraintName("FK_recempleado");

                entity.HasOne(d => d.IdRecepcionNavigation)
                    .WithMany(p => p.Pret22RecepcionEmpleados)
                    .HasForeignKey(d => d.IdRecepcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt22_Re__id_re__5D56B96F");
            });

            modelBuilder.Entity<Pret23VentaEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpven)
                    .HasName("PK__PREt23_v__9D07F2885ADC1B64");

                entity.ToTable("PREt23_venta_Empleado");

                entity.Property(e => e.IdEmpven).HasColumnName("id_empven");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.IdEmpleadoVen).HasColumnName("id_empleado_ven");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Salario)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("salario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdCompEmitidoNavigation)
                    .WithMany(p => p.Pret23VentaEmpleados)
                    .HasForeignKey(d => d.IdCompEmitido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PREt23_ve__id_co__7128A7F2");

                entity.HasOne(d => d.IdEmpleadoVenNavigation)
                    .WithMany(p => p.Pret23VentaEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoVen)
                    .HasConstraintName("FK_empleado_envemp");
            });

            modelBuilder.Entity<Prot01Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PKALMt03_id_marca")
                    .IsClustered(false);

                entity.ToTable("PROt01_marca");

                entity.Property(e => e.IdMarca).HasColumnName("id_marca");

                entity.Property(e => e.CodMarca)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_marca");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot02Modelo>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PKALMt04_id_modelo")
                    .IsClustered(false);

                entity.ToTable("PROt02_modelo");

                entity.Property(e => e.IdModelo).HasColumnName("id_modelo");

                entity.Property(e => e.CodModelo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_modelo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdMarca).HasColumnName("id_marca");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Prot02Modelos)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt01_marca901");
            });

            modelBuilder.Entity<Prot03Familium>(entity =>
            {
                entity.HasKey(e => e.IdFamilia)
                    .HasName("PKALMt05_cod_cate")
                    .IsClustered(false);

                entity.ToTable("PROt03_familia");

                entity.Property(e => e.IdFamilia).HasColumnName("id_familia");

                entity.Property(e => e.CodFamilia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_familia");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot04Subfamilium>(entity =>
            {
                entity.HasKey(e => e.IdSubfamilia)
                    .HasName("PKALMt06_cod_clase")
                    .IsClustered(false);

                entity.ToTable("PROt04_subfamilia");

                entity.Property(e => e.IdSubfamilia).HasColumnName("id_subfamilia");

                entity.Property(e => e.CodSubfamilia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_subfamilia");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdFamilia).HasColumnName("id_familia");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.Prot04Subfamilia)
                    .HasForeignKey(d => d.IdFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt03_familia911");
            });

            modelBuilder.Entity<Prot05GrupoProd>(entity =>
            {
                entity.HasKey(e => e.IdGrupoProd)
                    .HasName("PKALMt05_cod_cate_1")
                    .IsClustered(false);

                entity.ToTable("PROt05_grupo_prod");

                entity.Property(e => e.IdGrupoProd).HasColumnName("id_grupo_prod");

                entity.Property(e => e.CodGrupoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_grupo_prod");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot06ClaseProd>(entity =>
            {
                entity.HasKey(e => e.IdClaseProd)
                    .HasName("PKALMt06_cod_clase_1")
                    .IsClustered(false);

                entity.ToTable("PROt06_clase_prod");

                entity.Property(e => e.IdClaseProd).HasColumnName("id_clase_prod");

                entity.Property(e => e.CodClaseProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_clase_prod");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdFamilia).HasColumnName("id_familia");

                entity.Property(e => e.IdGrupoProd).HasColumnName("id_grupo_prod");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdGrupoProdNavigation)
                    .WithMany(p => p.Prot06ClaseProds)
                    .HasForeignKey(d => d.IdGrupoProd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt05_grupo_prod961");
            });

            modelBuilder.Entity<Prot07TipoProd>(entity =>
            {
                entity.HasKey(e => e.IdTipoProd)
                    .HasName("PK07_cod_tipo_prod")
                    .IsClustered(false);

                entity.ToTable("PROt07_tipo_prod");

                entity.Property(e => e.IdTipoProd).HasColumnName("id_tipo_prod");

                entity.Property(e => e.CodTipoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_prod");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot08PrecioProd>(entity =>
            {
                entity.HasKey(e => e.IdPrecioProd)
                    .HasName("PK16")
                    .IsClustered(false);

                entity.ToTable("PROt08_precio_prod");

                entity.Property(e => e.IdPrecioProd).HasColumnName("id_precio_prod");

                entity.Property(e => e.FecEfectivoDesde).HasColumnName("fec_efectivo_desde");

                entity.Property(e => e.FecEfectivoHasta).HasColumnName("fec_efectivo_hasta");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.MtoCosto1)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_1");

                entity.Property(e => e.MtoCosto10)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_10");

                entity.Property(e => e.MtoCosto2)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_2");

                entity.Property(e => e.MtoCosto3)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_3");

                entity.Property(e => e.MtoCosto4)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_4");

                entity.Property(e => e.MtoCosto5)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_5");

                entity.Property(e => e.MtoCosto6)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_6");

                entity.Property(e => e.MtoCosto7)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_7");

                entity.Property(e => e.MtoCosto8)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_8");

                entity.Property(e => e.MtoCosto9)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_costo_9");

                entity.Property(e => e.MtoPuConIgv1)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_1");

                entity.Property(e => e.MtoPuConIgv10)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_10");

                entity.Property(e => e.MtoPuConIgv2)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_2");

                entity.Property(e => e.MtoPuConIgv3)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_3");

                entity.Property(e => e.MtoPuConIgv4)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_4");

                entity.Property(e => e.MtoPuConIgv5)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_5");

                entity.Property(e => e.MtoPuConIgv6)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_6");

                entity.Property(e => e.MtoPuConIgv7)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_7");

                entity.Property(e => e.MtoPuConIgv8)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_8");

                entity.Property(e => e.MtoPuConIgv9)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_con_igv_9");

                entity.Property(e => e.MtoPuSinIgv1)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_1");

                entity.Property(e => e.MtoPuSinIgv10)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_10");

                entity.Property(e => e.MtoPuSinIgv2)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_2");

                entity.Property(e => e.MtoPuSinIgv3)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_3");

                entity.Property(e => e.MtoPuSinIgv4)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_4");

                entity.Property(e => e.MtoPuSinIgv5)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_5");

                entity.Property(e => e.MtoPuSinIgv6)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_6");

                entity.Property(e => e.MtoPuSinIgv7)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_7");

                entity.Property(e => e.MtoPuSinIgv8)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_8");

                entity.Property(e => e.MtoPuSinIgv9)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pu_sin_igv_9");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObsv)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_obsv");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Prot08PrecioProds)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("RefPROt09_producto1461");
            });

            modelBuilder.Entity<Prot09Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PKALMt08_prod")
                    .IsClustered(false);

                entity.ToTable("PROt09_producto");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.AlturaProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("altura_prod");

                entity.Property(e => e.AnchoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ancho_prod");

                entity.Property(e => e.CodBarra)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_barra");

                entity.Property(e => e.CodProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_producto");

                entity.Property(e => e.CodProducto2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_producto2");

                entity.Property(e => e.CostoProd)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("costo_prod");

                entity.Property(e => e.DiametroProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("diametro_prod");

                entity.Property(e => e.IdClaseProd).HasColumnName("id_clase_prod");

                entity.Property(e => e.IdCombo).HasColumnName("id_combo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdImpuesto).HasColumnName("id_impuesto");

                entity.Property(e => e.IdModelo).HasColumnName("id_modelo");

                entity.Property(e => e.IdReceta).HasColumnName("id_receta");

                entity.Property(e => e.IdSubfamilia).HasColumnName("id_subfamilia");

                entity.Property(e => e.IdTipoExistencia).HasColumnName("id_tipo_existencia");

                entity.Property(e => e.IdTipoMoneda).HasColumnName("id_tipo_moneda");

                entity.Property(e => e.IdTipoProd).HasColumnName("id_tipo_prod");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.LargoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("largo_prod");

                entity.Property(e => e.MtoPvmaConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvma_con_igv");

                entity.Property(e => e.MtoPvmaSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvma_sin_igv");

                entity.Property(e => e.MtoPvmiConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvmi_con_igv");

                entity.Property(e => e.MtoPvmiSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvmi_sin_igv");

                entity.Property(e => e.MtoPvpuConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_igv");

                entity.Property(e => e.MtoPvpuSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_igv");

                entity.Property(e => e.PesoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("peso_prod");

                entity.Property(e => e.PorImpto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SnCombo).HasColumnName("sn_combo");

                entity.Property(e => e.SnCompra).HasColumnName("sn_compra");

                entity.Property(e => e.SnExento).HasColumnName("sn_exento");

                entity.Property(e => e.SnInafecto).HasColumnName("sn_inafecto");

                entity.Property(e => e.SnIncluyeImpto).HasColumnName("sn_incluye_impto");

                entity.Property(e => e.SnReceta).HasColumnName("sn_receta");

                entity.Property(e => e.SnVenta).HasColumnName("sn_venta");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.UrlImgProd)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("url_img_prod");

                entity.HasOne(d => d.IdClaseProdNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdClaseProd)
                    .HasConstraintName("RefPROt06_clase_prod971");

                entity.HasOne(d => d.IdComboNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdCombo)
                    .HasConstraintName("RefPROt13_combo145");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("RefMSTt06_impuesto1851");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdModelo)
                    .HasConstraintName("FKALMt08_id_modelo");

                entity.HasOne(d => d.IdRecetaNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdReceta)
                    .HasConstraintName("RefPROt10_receta991");

                entity.HasOne(d => d.IdSubfamiliaNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdSubfamilia)
                    .HasConstraintName("RefPROt04_subfamilia921");

                entity.HasOne(d => d.IdTipoExistenciaNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdTipoExistencia)
                    .HasConstraintName("RefALMt14_tipo_existencia88");

                entity.HasOne(d => d.IdTipoMonedaNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdTipoMoneda)
                    .HasConstraintName("FKVTAt04_id_mon");

                entity.HasOne(d => d.IdTipoProdNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdTipoProd)
                    .HasConstraintName("FKALMt08_cod_tipo_prod");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Prot09Productos)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FKALMt08_id_um");
            });

            modelBuilder.Entity<Prot10Recetum>(entity =>
            {
                entity.HasKey(e => e.IdReceta)
                    .HasName("PK92")
                    .IsClustered(false);

                entity.ToTable("PROt10_receta");

                entity.Property(e => e.IdReceta).HasColumnName("id_receta");

                entity.Property(e => e.CodReceta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_receta");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdRecetaGrupo).HasColumnName("id_receta_grupo");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdRecetaGrupoNavigation)
                    .WithMany(p => p.Prot10Receta)
                    .HasForeignKey(d => d.IdRecetaGrupo)
                    .HasConstraintName("RefPROt12_receta_grupo144");
            });

            modelBuilder.Entity<Prot11RecetaDtl>(entity =>
            {
                entity.HasKey(e => e.IdRecetaDtl)
                    .HasName("PK51")
                    .IsClustered(false);

                entity.ToTable("PROt11_receta_dtl");

                entity.Property(e => e.IdRecetaDtl).HasColumnName("id_receta_dtl");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdReceta).HasColumnName("id_receta");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("peso");

                entity.Property(e => e.TxtProducto)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_producto");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Prot11RecetaDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("RefALMt08_producto114");

                entity.HasOne(d => d.IdRecetaNavigation)
                    .WithMany(p => p.Prot11RecetaDtls)
                    .HasForeignKey(d => d.IdReceta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt10_receta1001");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Prot11RecetaDtls)
                    .HasForeignKey(d => d.IdUm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefALMt01_unidad_medida116");
            });

            modelBuilder.Entity<Prot12RecetaGrupo>(entity =>
            {
                entity.HasKey(e => e.IdRecetaGrupo)
                    .HasName("PK99_1_1")
                    .IsClustered(false);

                entity.ToTable("PROt12_receta_grupo");

                entity.Property(e => e.IdRecetaGrupo).HasColumnName("id_receta_grupo");

                entity.Property(e => e.CodRecetaGrupo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_receta_grupo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot13Combo>(entity =>
            {
                entity.HasKey(e => e.IdCombo)
                    .HasName("PK97_1")
                    .IsClustered(false);

                entity.ToTable("PROt13_combo");

                entity.Property(e => e.IdCombo).HasColumnName("id_combo");

                entity.Property(e => e.CodCombo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_combo");

                entity.Property(e => e.IdComboGrupo).HasColumnName("id_combo_grupo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdImpuesto).HasColumnName("id_impuesto");

                entity.Property(e => e.MtoPvpuConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_tax");

                entity.Property(e => e.MtoPvpuSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_tax");

                entity.Property(e => e.SnIncluyeImpto).HasColumnName("sn_incluye_impto");

                entity.Property(e => e.SnPrecioAcumulado).HasColumnName("sn_precio_acumulado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdComboGrupoNavigation)
                    .WithMany(p => p.Prot13Combos)
                    .HasForeignKey(d => d.IdComboGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt17_combo_grupo166");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.Prot13Combos)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("RefMSTt06_impuesto143");
            });

            modelBuilder.Entity<Prot14ComboFixedDtl>(entity =>
            {
                entity.HasKey(e => e.IdComboFixedDtl)
                    .HasName("PK98_1")
                    .IsClustered(false);

                entity.ToTable("PROt14_combo_fixed_dtl");

                entity.Property(e => e.IdComboFixedDtl).HasColumnName("id_combo_fixed_dtl");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.CodComboFixedDtl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_combo_fixed_dtl");

                entity.Property(e => e.IdCombo).HasColumnName("id_combo");

                entity.Property(e => e.IdComboVariable).HasColumnName("id_combo_variable");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.MtoPvpuConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_tax");

                entity.Property(e => e.MtoPvpuSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_tax");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdComboNavigation)
                    .WithMany(p => p.Prot14ComboFixedDtls)
                    .HasForeignKey(d => d.IdCombo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt13_combo165");

                entity.HasOne(d => d.IdComboVariableNavigation)
                    .WithMany(p => p.Prot14ComboFixedDtls)
                    .HasForeignKey(d => d.IdComboVariable)
                    .HasConstraintName("RefPROt15_combo_variable168");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Prot14ComboFixedDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("RefPROt09_producto164");
            });

            modelBuilder.Entity<Prot15ComboVariable>(entity =>
            {
                entity.HasKey(e => e.IdComboVariable)
                    .HasName("PK113")
                    .IsClustered(false);

                entity.ToTable("PROt15_combo_variable");

                entity.Property(e => e.IdComboVariable).HasColumnName("id_combo_variable");

                entity.Property(e => e.CodComboVariable)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_combo_variable");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdImpuesto).HasColumnName("id_impuesto");

                entity.Property(e => e.MtoPvpuConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_tax");

                entity.Property(e => e.MtoPvpuSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_tax");

                entity.Property(e => e.SnIncluyeImpto).HasColumnName("sn_incluye_impto");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdImpuestoNavigation)
                    .WithMany(p => p.Prot15ComboVariables)
                    .HasForeignKey(d => d.IdImpuesto)
                    .HasConstraintName("RefMSTt06_impuesto142");
            });

            modelBuilder.Entity<Prot16ComboVariableDtl>(entity =>
            {
                entity.HasKey(e => e.IdComboVariableDtl)
                    .HasName("PK114")
                    .IsClustered(false);

                entity.ToTable("PROt16_combo_variable_dtl");

                entity.Property(e => e.IdComboVariableDtl).HasColumnName("id_combo_variable_dtl");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.CodComboVariableDtl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_combo_variable_dtl");

                entity.Property(e => e.IdComboVariable).HasColumnName("id_combo_variable");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.MtoPvpuConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_tax");

                entity.Property(e => e.MtoPvpuSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_tax");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdComboVariableNavigation)
                    .WithMany(p => p.Prot16ComboVariableDtls)
                    .HasForeignKey(d => d.IdComboVariable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt15_combo_variable167");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Prot16ComboVariableDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPROt09_producto169");
            });

            modelBuilder.Entity<Prot17ComboGrupo>(entity =>
            {
                entity.HasKey(e => e.IdComboGrupo)
                    .HasName("PK99_1")
                    .IsClustered(false);

                entity.ToTable("PROt17_combo_grupo");

                entity.Property(e => e.IdComboGrupo).HasColumnName("id_combo_grupo");

                entity.Property(e => e.CodComboGrupo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_combo_grupo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot18Productocom>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PKALMt18_prod")
                    .IsClustered(false);

                entity.ToTable("PROt18_productocom");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.AlturaProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("altura_prod");

                entity.Property(e => e.AnchoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ancho_prod");

                entity.Property(e => e.CodBarra)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cod_barra");

                entity.Property(e => e.CodProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_producto");

                entity.Property(e => e.CodProducto2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_producto2");

                entity.Property(e => e.CostoProd)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("costo_prod");

                entity.Property(e => e.DiametroProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("diametro_prod");

                entity.Property(e => e.IdClaseProd).HasColumnName("id_clase_prod");

                entity.Property(e => e.IdCombo).HasColumnName("id_combo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdFamilia).HasColumnName("id_familia");

                entity.Property(e => e.IdImpuesto).HasColumnName("id_impuesto");

                entity.Property(e => e.IdModelo).HasColumnName("id_modelo");

                entity.Property(e => e.IdReceta).HasColumnName("id_receta");

                entity.Property(e => e.IdSubfamilia).HasColumnName("id_subfamilia");

                entity.Property(e => e.IdTipoExistencia).HasColumnName("id_tipo_existencia");

                entity.Property(e => e.IdTipoMoneda).HasColumnName("id_tipo_moneda");

                entity.Property(e => e.IdTipoProd).HasColumnName("id_tipo_prod");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.LargoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("largo_prod");

                entity.Property(e => e.MtoPvmaConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvma_con_igv");

                entity.Property(e => e.MtoPvmaSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvma_sin_igv");

                entity.Property(e => e.MtoPvmiConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvmi_con_igv");

                entity.Property(e => e.MtoPvmiSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvmi_sin_igv");

                entity.Property(e => e.MtoPvpuConIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_con_igv");

                entity.Property(e => e.MtoPvpuSinIgv)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_pvpu_sin_igv");

                entity.Property(e => e.PesoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("peso_prod");

                entity.Property(e => e.PorImpto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_impto");

                entity.Property(e => e.SnCombo).HasColumnName("sn_combo");

                entity.Property(e => e.SnCompra).HasColumnName("sn_compra");

                entity.Property(e => e.SnExento).HasColumnName("sn_exento");

                entity.Property(e => e.SnInafecto).HasColumnName("sn_inafecto");

                entity.Property(e => e.SnIncluyeImpto).HasColumnName("sn_incluye_impto");

                entity.Property(e => e.SnReceta).HasColumnName("sn_receta");

                entity.Property(e => e.SnVenta).HasColumnName("sn_venta");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtReferencia)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_referencia");

                entity.Property(e => e.UrlImgProd)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("url_img_prod");

                entity.HasOne(d => d.IdModeloNavigation)
                    .WithMany(p => p.Prot18Productocoms)
                    .HasForeignKey(d => d.IdModelo)
                    .HasConstraintName("FK_PROt18_productocom_PROt21_modelocom");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Prot18Productocoms)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FK_PROt18_productocom_SNTt06_unidad_medida");
            });

            modelBuilder.Entity<Prot19TipoProdCom>(entity =>
            {
                entity.HasKey(e => e.IdTipoProd)
                    .HasName("PK19_cod_tipo_prod")
                    .IsClustered(false);

                entity.ToTable("PROt19_tipo_prod_com");

                entity.Property(e => e.IdTipoProd).HasColumnName("id_tipo_prod");

                entity.Property(e => e.CodTipoProd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_prod");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Prot20Marcacom>(entity =>
            {
                entity.HasKey(e => e.IdMarca)
                    .HasName("PKALMt20_id_marca")
                    .IsClustered(false);

                entity.ToTable("PROt20_marcacom");

                entity.Property(e => e.IdMarca).HasColumnName("id_marca");

                entity.Property(e => e.CodMarca)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_marca");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdTipoProd).HasColumnName("id_tipo_prod");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdTipoProdNavigation)
                    .WithMany(p => p.Prot20Marcacoms)
                    .HasForeignKey(d => d.IdTipoProd)
                    .HasConstraintName("FK_PROt20_marcacom_PROt19_tipo_prod_com");
            });

            modelBuilder.Entity<Prot21Modelocom>(entity =>
            {
                entity.HasKey(e => e.IdModelo)
                    .HasName("PKALMt21_id_modelo")
                    .IsClustered(false);

                entity.ToTable("PROt21_modelocom");

                entity.Property(e => e.IdModelo).HasColumnName("id_modelo");

                entity.Property(e => e.CodModelo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_modelo");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdMarca).HasColumnName("id_marca");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Prot21Modelocoms)
                    .HasForeignKey(d => d.IdMarca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROt21_modelocom_PROt20_marcacom");
            });

            modelBuilder.Entity<Rptt01Reporte>(entity =>
            {
                entity.HasKey(e => e.IdReporte)
                    .HasName("PK108")
                    .IsClustered(false);

                entity.ToTable("RPTt01_reporte");

                entity.Property(e => e.IdReporte).HasColumnName("id_reporte");

                entity.Property(e => e.CodReporte)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_reporte");

                entity.Property(e => e.IdCategoriaReporte).HasColumnName("id_categoria_reporte");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.SnClaseEmpleado).HasColumnName("sn_clase_empleado");

                entity.Property(e => e.SnDateRange).HasColumnName("sn_date_range");

                entity.Property(e => e.SnEmpleado).HasColumnName("sn_empleado");

                entity.Property(e => e.SnProductoPorFamilia).HasColumnName("sn_producto_por_familia");

                entity.Property(e => e.SnProductoPorNombre).HasColumnName("sn_producto_por_nombre");

                entity.Property(e => e.SnProductoPorSubfamilia).HasColumnName("sn_producto_por_subfamilia");

                entity.Property(e => e.SnRvcRange).HasColumnName("sn_rvc_range");

                entity.Property(e => e.SnTurno).HasColumnName("sn_turno");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtPath)
                    .HasMaxLength(260)
                    .IsUnicode(false)
                    .HasColumnName("txt_path");

                entity.HasOne(d => d.IdCategoriaReporteNavigation)
                    .WithMany(p => p.Rptt01Reportes)
                    .HasForeignKey(d => d.IdCategoriaReporte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefRPTt02_categoria_reporte141");
            });

            modelBuilder.Entity<Rptt02CategoriaReporte>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaReporte)
                    .HasName("PK109")
                    .IsClustered(false);

                entity.ToTable("RPTt02_categoria_reporte");

                entity.Property(e => e.IdCategoriaReporte).HasColumnName("id_categoria_reporte");

                entity.Property(e => e.CodCategoriaReporte)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_categoria_reporte");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt01TipoMedioPago>(entity =>
            {
                entity.HasKey(e => e.IdTipoMedioPago)
                    .HasName("PK64")
                    .IsClustered(false);

                entity.ToTable("SNTt01_tipo_medio_pago");

                entity.Property(e => e.IdTipoMedioPago).HasColumnName("id_tipo_medio_pago");

                entity.Property(e => e.CodTipoMedioPago)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_medio_pago");

                entity.Property(e => e.CodTipoMedioPagoPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_medio_pago_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt02TipoDocIdentidad>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocIdentidad)
                    .HasName("PKCMRt01_cod_tipo_doc")
                    .IsClustered(false);

                entity.ToTable("SNTt02_tipo_doc_identidad");

                entity.Property(e => e.IdTipoDocIdentidad).HasColumnName("id_tipo_doc_identidad");

                entity.Property(e => e.CodTipoDocIdentidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_doc_identidad");

                entity.Property(e => e.CodTipoDocIdentidadPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_doc_identidad_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt03EntidadFinanciera>(entity =>
            {
                entity.HasKey(e => e.IdEntidadFinanciera)
                    .HasName("PK73_1")
                    .IsClustered(false);

                entity.ToTable("SNTt03_entidad_financiera");

                entity.Property(e => e.IdEntidadFinanciera).HasColumnName("id_entidad_financiera");

                entity.Property(e => e.CodEntidadFinanciera)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_entidad_financiera");

                entity.Property(e => e.CodEntidadFinancieraPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_entidad_financiera_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt04TipoMonedum>(entity =>
            {
                entity.HasKey(e => e.IdTipoMoneda)
                    .HasName("PKVTAt04_id_mon")
                    .IsClustered(false);

                entity.ToTable("SNTt04_tipo_moneda");

                entity.Property(e => e.IdTipoMoneda).HasColumnName("id_tipo_moneda");

                entity.Property(e => e.CodTipoMoneda)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_moneda");

                entity.Property(e => e.CodTipoMonedaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_moneda_ple");

                entity.Property(e => e.DecCambio)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("dec_cambio");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtPais)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_pais");
            });

            modelBuilder.Entity<Sntt05TipoExistencium>(entity =>
            {
                entity.HasKey(e => e.IdTipoExistencia)
                    .HasName("PK53")
                    .IsClustered(false);

                entity.ToTable("SNTt05_tipo_existencia");

                entity.Property(e => e.IdTipoExistencia).HasColumnName("id_tipo_existencia");

                entity.Property(e => e.CodTipoExistencia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_existencia");

                entity.Property(e => e.CodTipoExistenciaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_existencia_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt06UnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.IdUm)
                    .HasName("PKALMt01_id_um")
                    .IsClustered(false);

                entity.ToTable("SNTt06_unidad_medida");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.CodUm)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_um");

                entity.Property(e => e.CodUmPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_um_ple");

                entity.Property(e => e.DecFactor)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("dec_factor");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUmBase).HasColumnName("id_um_base");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtOperacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("txt_operacion")
                    .IsFixedLength();

                entity.Property(e => e.TxtUnidBase)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_unid_base");
            });

            modelBuilder.Entity<Sntt07TipoIntangible>(entity =>
            {
                entity.HasKey(e => e.IdTipoIntangible)
                    .HasName("PK73")
                    .IsClustered(false);

                entity.ToTable("SNTt07_tipo_intangible");

                entity.Property(e => e.IdTipoIntangible).HasColumnName("id_tipo_intangible");

                entity.Property(e => e.CodTipoIntangible)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_intangible");

                entity.Property(e => e.CodTipoIntangiblePle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_intangible_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt08CodigoLibro>(entity =>
            {
                entity.HasKey(e => e.IdLibro)
                    .HasName("PK74")
                    .IsClustered(false);

                entity.ToTable("SNTt08_codigo_libro");

                entity.Property(e => e.IdLibro).HasColumnName("id_libro");

                entity.Property(e => e.CodLibro)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_libro");

                entity.Property(e => e.CodLibroPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_libro_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt09CtaContable>(entity =>
            {
                entity.HasKey(e => e.IdCtaContable)
                    .HasName("PK75_1")
                    .IsClustered(false);

                entity.ToTable("SNTt09_cta_contable");

                entity.Property(e => e.IdCtaContable).HasColumnName("id_cta_contable");

                entity.Property(e => e.CodCtaContable)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_cta_contable");

                entity.Property(e => e.CodCtaContablePle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_cta_contable_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt10TipoComprobante>(entity =>
            {
                entity.HasKey(e => e.IdTipoComp)
                    .HasName("PKVTAt01_cod_tipo_comp")
                    .IsClustered(false);

                entity.ToTable("SNTt10_tipo_comprobante");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.CodLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_location");

                entity.Property(e => e.CodTipoComp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_comp");

                entity.Property(e => e.CodTipoCompPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_comp_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.NroContador).HasColumnName("nro_contador");

                entity.Property(e => e.NroFinal).HasColumnName("nro_final");

                entity.Property(e => e.SnEmitoComp).HasColumnName("sn_emito_comp");

                entity.Property(e => e.SnReciboComp).HasColumnName("sn_recibo_comp");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt11Aduana>(entity =>
            {
                entity.HasKey(e => e.IdAduana)
                    .HasName("PK75")
                    .IsClustered(false);

                entity.ToTable("SNTt11_aduana");

                entity.Property(e => e.IdAduana).HasColumnName("id_aduana");

                entity.Property(e => e.CodAduana)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_aduana");

                entity.Property(e => e.CodAduanaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_aduana_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt12TipoOperacion>(entity =>
            {
                entity.HasKey(e => e.IdTipoOperacion)
                    .HasName("PK55")
                    .IsClustered(false);

                entity.ToTable("SNTt12_tipo_operacion");

                entity.Property(e => e.IdTipoOperacion).HasColumnName("id_tipo_operacion");

                entity.Property(e => e.CodTipoOperacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_operacion");

                entity.Property(e => e.CodTipoOperacionPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_operacion_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt13TipoActividad>(entity =>
            {
                entity.HasKey(e => e.IdTipoOperacion)
                    .HasName("PK56")
                    .IsClustered(false);

                entity.ToTable("SNTt13_tipo_actividad");

                entity.Property(e => e.IdTipoOperacion).HasColumnName("id_tipo_operacion");

                entity.Property(e => e.CodTipoActividad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_actividad");

                entity.Property(e => e.CodTipoActividadPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_actividad_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt14Nacionalidad>(entity =>
            {
                entity.HasKey(e => e.IdNacionalidad)
                    .HasName("PK57")
                    .IsClustered(false);

                entity.ToTable("SNTt14_nacionalidad");

                entity.Property(e => e.IdNacionalidad).HasColumnName("id_nacionalidad");

                entity.Property(e => e.CodNacionalidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_nacionalidad");

                entity.Property(e => e.CodNacionalidadPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_nacionalidad_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt15Vium>(entity =>
            {
                entity.HasKey(e => e.IdVia)
                    .HasName("PKCMRt04_cod_tipo_via")
                    .IsClustered(false);

                entity.ToTable("SNTt15_via");

                entity.Property(e => e.IdVia).HasColumnName("id_via");

                entity.Property(e => e.CodVia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_via");

                entity.Property(e => e.CodViaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_via_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt16Zona>(entity =>
            {
                entity.HasKey(e => e.IdZona)
                    .HasName("PKCMRt03_cod_tipo_zona")
                    .IsClustered(false);

                entity.ToTable("SNTt16_zona");

                entity.Property(e => e.IdZona).HasColumnName("id_zona");

                entity.Property(e => e.CodZona)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_zona");

                entity.Property(e => e.CodZonaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_zona_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt17TipoTrabajador>(entity =>
            {
                entity.HasKey(e => e.IdTipoTrabajador)
                    .HasName("PK58")
                    .IsClustered(false);

                entity.ToTable("SNTt17_tipo_trabajador");

                entity.Property(e => e.IdTipoTrabajador).HasColumnName("id_tipo_trabajador");

                entity.Property(e => e.CodTipoTrabajador)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_trabajador");

                entity.Property(e => e.CodTipoTrabajadorPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_tipo_trabajador_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt18SituacionEducativa>(entity =>
            {
                entity.HasKey(e => e.IdSituacionEducativa)
                    .HasName("PK59")
                    .IsClustered(false);

                entity.ToTable("SNTt18_situacion_educativa");

                entity.Property(e => e.IdSituacionEducativa).HasColumnName("id_situacion_educativa");

                entity.Property(e => e.CodSituacionEducativa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_situacion_educativa");

                entity.Property(e => e.CodSituacionEducativaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_situacion_educativa_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt19Ocupacion>(entity =>
            {
                entity.HasKey(e => e.IdOcupacion)
                    .HasName("PK60")
                    .IsClustered(false);

                entity.ToTable("SNTt19_ocupacion");

                entity.Property(e => e.IdOcupacion).HasColumnName("id_ocupacion");

                entity.Property(e => e.CodOcupacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_ocupacion");

                entity.Property(e => e.CodOcupacionPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_ocupacion_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt20RegimenPensionario>(entity =>
            {
                entity.HasKey(e => e.IdRegimenPensionario)
                    .HasName("PK61")
                    .IsClustered(false);

                entity.ToTable("SNTt20_regimen_pensionario");

                entity.Property(e => e.IdRegimenPensionario).HasColumnName("id_regimen_pensionario");

                entity.Property(e => e.CodRegimenPensionario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_pensionario");

                entity.Property(e => e.CodRegimenPensionarioPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_pensionario_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt21CondicionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdCondicionLaboral)
                    .HasName("PK62")
                    .IsClustered(false);

                entity.ToTable("SNTt21_condicion_laboral");

                entity.Property(e => e.IdCondicionLaboral).HasColumnName("id_condicion_laboral");

                entity.Property(e => e.CodCondicionLaboral)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_condicion_laboral");

                entity.Property(e => e.CodCondicionLaboralPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_condicion_laboral_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt22PeriodoRemuneracion>(entity =>
            {
                entity.HasKey(e => e.IdPeriodoRemuneracion)
                    .HasName("PK63")
                    .IsClustered(false);

                entity.ToTable("SNTt22_periodo_remuneracion");

                entity.Property(e => e.IdPeriodoRemuneracion).HasColumnName("id_periodo_remuneracion");

                entity.Property(e => e.CodPeriodoRemuneracion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_periodo_remuneracion");

                entity.Property(e => e.CodPeriodoRemuneracionPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_periodo_remuneracion_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt23SaludEp>(entity =>
            {
                entity.HasKey(e => e.IdSaludEps)
                    .HasName("PK65")
                    .IsClustered(false);

                entity.ToTable("SNTt23_salud_eps");

                entity.Property(e => e.IdSaludEps).HasColumnName("id_salud_eps");

                entity.Property(e => e.CodSaludEps)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_salud_eps");

                entity.Property(e => e.CodSaludEpsPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_salud_eps_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt24Situacion>(entity =>
            {
                entity.HasKey(e => e.IdSituacion)
                    .HasName("PK66")
                    .IsClustered(false);

                entity.ToTable("SNTt24_situacion");

                entity.Property(e => e.IdSituacion).HasColumnName("id_situacion");

                entity.Property(e => e.CodSituacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_situacion");

                entity.Property(e => e.CodSituacionPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_situacion_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt25MotivoBaja>(entity =>
            {
                entity.HasKey(e => e.IdMotivoBaja)
                    .HasName("PK67")
                    .IsClustered(false);

                entity.ToTable("SNTt25_motivo_baja");

                entity.Property(e => e.IdMotivoBaja).HasColumnName("id_motivo_baja");

                entity.Property(e => e.CodMotivoBaja)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_motivo_baja");

                entity.Property(e => e.CodMotivoBajaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_motivo_baja_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt26ModalidadFormativa>(entity =>
            {
                entity.HasKey(e => e.IdModalidadFormativa)
                    .HasName("PK68")
                    .IsClustered(false);

                entity.ToTable("SNTt26_modalidad_formativa");

                entity.Property(e => e.IdModalidadFormativa).HasColumnName("id_modalidad_formativa");

                entity.Property(e => e.CodModalidadFormativa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_modalidad_formativa");

                entity.Property(e => e.CodModalidadFormativaPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_modalidad_formativa_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt27VinculoFamiliar>(entity =>
            {
                entity.HasKey(e => e.IdVinculoFamiliar)
                    .HasName("PK69")
                    .IsClustered(false);

                entity.ToTable("SNTt27_vinculo_familiar");

                entity.Property(e => e.IdVinculoFamiliar).HasColumnName("id_vinculo_familiar");

                entity.Property(e => e.CodVinculoFamiliar)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_vinculo_familiar");

                entity.Property(e => e.CodVinculoFamiliarPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_vinculo_familiar_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt28SuspencionLaboral>(entity =>
            {
                entity.HasKey(e => e.IdSuspencionLaboral)
                    .HasName("PK70")
                    .IsClustered(false);

                entity.ToTable("SNTt28_suspencion_laboral");

                entity.Property(e => e.IdSuspencionLaboral).HasColumnName("id_suspencion_laboral");

                entity.Property(e => e.CodSuspencionLaboral)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_suspencion_laboral");

                entity.Property(e => e.CodSuspencionLaboralPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_suspencion_laboral_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt29RegimenSalud>(entity =>
            {
                entity.HasKey(e => e.IdRegimenSalud)
                    .HasName("PK71")
                    .IsClustered(false);

                entity.ToTable("SNTt29_regimen_salud");

                entity.Property(e => e.IdRegimenSalud).HasColumnName("id_regimen_salud");

                entity.Property(e => e.CodRegimenSalud)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_salud");

                entity.Property(e => e.CodRegimenSaludPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_salud_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt30RegimenLaboral>(entity =>
            {
                entity.HasKey(e => e.IdRegimenLaboral)
                    .HasName("PK72")
                    .IsClustered(false);

                entity.ToTable("SNTt30_regimen_laboral");

                entity.Property(e => e.IdRegimenLaboral).HasColumnName("id_regimen_laboral");

                entity.Property(e => e.CodRegimenLaboral)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_laboral");

                entity.Property(e => e.CodRegimenLaboralPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_regimen_laboral_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtAbrv)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("txt_abrv");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt31Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDpto)
                    .HasName("PKCMRt06_cod_dpto")
                    .IsClustered(false);

                entity.ToTable("SNTt31_departamento");

                entity.Property(e => e.IdDpto).HasColumnName("id_dpto");

                entity.Property(e => e.CodDpto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_dpto");

                entity.Property(e => e.CodDptoPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_dpto_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");
            });

            modelBuilder.Entity<Sntt32Provincium>(entity =>
            {
                entity.HasKey(e => e.IdProv)
                    .HasName("PKCMRt07_cod_prov")
                    .IsClustered(false);

                entity.ToTable("SNTt32_provincia");

                entity.Property(e => e.IdProv).HasColumnName("id_prov");

                entity.Property(e => e.CodProv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_prov");

                entity.Property(e => e.CodProvPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_prov_ple");

                entity.Property(e => e.IdDpto).HasColumnName("id_dpto");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdDptoNavigation)
                    .WithMany(p => p.Sntt32Provincia)
                    .HasForeignKey(d => d.IdDpto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefSNTt31_departamento931");
            });

            modelBuilder.Entity<Sntt33Distrito>(entity =>
            {
                entity.HasKey(e => e.IdDist)
                    .HasName("PKCMRt08_cod_dist")
                    .IsClustered(false);

                entity.ToTable("SNTt33_distrito");

                entity.Property(e => e.IdDist).HasColumnName("id_dist");

                entity.Property(e => e.CodDist)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_dist");

                entity.Property(e => e.CodDistPle)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cod_dist_ple");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProv).HasColumnName("id_prov");

                entity.Property(e => e.TxtDesc)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("txt_desc");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.HasOne(d => d.IdProvNavigation)
                    .WithMany(p => p.Sntt33Distritos)
                    .HasForeignKey(d => d.IdProv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefSNTt32_provincia951");
            });

            modelBuilder.Entity<Tnst01CompRecibido>(entity =>
            {
                entity.HasKey(e => e.IdCompRecibido)
                    .HasName("PKCTBt10_id_comp_recibido")
                    .IsClustered(false);

                entity.ToTable("TNSt01_comp_recibido");

                entity.Property(e => e.IdCompRecibido).HasColumnName("id_comp_recibido");

                entity.Property(e => e.FecCanc).HasColumnName("fec_canc");

                entity.Property(e => e.FecEmi).HasColumnName("fec_emi");

                entity.Property(e => e.FecRegRecibido).HasColumnName("fec_reg_recibido");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.FecVcto).HasColumnName("fec_vcto");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.IdTipoMoneda).HasColumnName("id_tipo_moneda");

                entity.Property(e => e.IdTipoOrden).HasColumnName("id_tipo_orden");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Info05)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info05");

                entity.Property(e => e.Info06)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info06");

                entity.Property(e => e.Info07)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info07");

                entity.Property(e => e.Info08)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info08");

                entity.Property(e => e.Info09)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info09");

                entity.Property(e => e.Info10)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info10");

                entity.Property(e => e.InfoDate01)
                    .HasColumnType("date")
                    .HasColumnName("info_date01");

                entity.Property(e => e.InfoDate02)
                    .HasColumnType("date")
                    .HasColumnName("info_date02");

                entity.Property(e => e.InfoDate03)
                    .HasColumnType("date")
                    .HasColumnName("info_date03");

                entity.Property(e => e.InfoDate04)
                    .HasColumnType("date")
                    .HasColumnName("info_date04");

                entity.Property(e => e.InfoDate05)
                    .HasColumnType("date")
                    .HasColumnName("info_date05");

                entity.Property(e => e.InfoMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto01");

                entity.Property(e => e.InfoMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto02");

                entity.Property(e => e.InfoMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto03");

                entity.Property(e => e.InfoMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto04");

                entity.Property(e => e.InfoMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto05");

                entity.Property(e => e.MtoCmsTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_cms_tot");

                entity.Property(e => e.MtoDsctoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_tot");

                entity.Property(e => e.MtoExonerado)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_exonerado");

                entity.Property(e => e.MtoFleteTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_flete_tot");

                entity.Property(e => e.MtoImptoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_impto_tot");

                entity.Property(e => e.MtoNeto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_neto");

                entity.Property(e => e.MtoNoAfecto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_no_afecto");

                entity.Property(e => e.MtoServicio)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_servicio");

                entity.Property(e => e.MtoSubTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_sub_tot");

                entity.Property(e => e.MtoTcVta)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_tc_vta");

                entity.Property(e => e.MtoTotComp)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_tot_comp");

                entity.Property(e => e.NroCompRecibido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nro_comp_recibido");

                entity.Property(e => e.Post).HasColumnName("post");

                entity.Property(e => e.PostDate).HasColumnName("post_date");

                entity.Property(e => e.RefFecha)
                    .HasColumnType("date")
                    .HasColumnName("ref_fecha");

                entity.Property(e => e.RefIdCompRecibido).HasColumnName("ref_id_comp_recibido");

                entity.Property(e => e.RefNumero)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("ref_numero");

                entity.Property(e => e.RefSerie)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ref_serie");

                entity.Property(e => e.RefTipoComprobante)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ref_tipo_comprobante");

                entity.Property(e => e.SnCancelada).HasColumnName("sn_cancelada");

                entity.Property(e => e.SnCredito).HasColumnName("sn_credito");

                entity.Property(e => e.TaxMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto01");

                entity.Property(e => e.TaxMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto02");

                entity.Property(e => e.TaxMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto03");

                entity.Property(e => e.TaxMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto04");

                entity.Property(e => e.TaxMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto05");

                entity.Property(e => e.TaxMto06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto06");

                entity.Property(e => e.TaxMto07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto07");

                entity.Property(e => e.TaxMto08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto08");

                entity.Property(e => e.TaxPor01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por01");

                entity.Property(e => e.TaxPor02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por02");

                entity.Property(e => e.TaxPor03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por03");

                entity.Property(e => e.TaxPor04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por04");

                entity.Property(e => e.TaxPor05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por05");

                entity.Property(e => e.TaxPor06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por06");

                entity.Property(e => e.TaxPor07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por07");

                entity.Property(e => e.TaxPor08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por08");

                entity.Property(e => e.TipoCompra)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tipo_compra");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNumero)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.Property(e => e.TxtSerie)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__TNSt01_co__id_cl__2C538F61");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefMSTt08_location163");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt03_proveedor1401");

                entity.HasOne(d => d.IdTipoCompNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdTipoComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCTBt10_cod_tipo_comp");

                entity.HasOne(d => d.IdTipoMonedaNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdTipoMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCTBt10_id_mon");

                entity.HasOne(d => d.IdTipoOrdenNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdTipoOrden)
                    .HasConstraintName("FKCTBt10_id_tipo_vta");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tnst01CompRecibidos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt01_usuario1441");
            });

            modelBuilder.Entity<Tnst02CompRecibidoDtl>(entity =>
            {
                entity.HasKey(e => e.IdCompRecibidoDtl)
                    .HasName("PKtCTB14_nro_corr_1")
                    .IsClustered(false);

                entity.ToTable("TNSt02_comp_recibido_dtl");

                entity.Property(e => e.IdCompRecibidoDtl).HasColumnName("id_comp_recibido_dtl");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdCompRecibido).HasColumnName("id_comp_recibido");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.MtoDsctoConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_con_tax");

                entity.Property(e => e.MtoDsctoSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_sin_tax");

                entity.Property(e => e.MtoVtaConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_vta_con_tax");

                entity.Property(e => e.MtoVtaSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_vta_sin_tax");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("peso");

                entity.Property(e => e.PorDscto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_dscto");

                entity.Property(e => e.PunitConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("punit_con_tax");

                entity.Property(e => e.PunitSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("punit_sin_tax");

                entity.Property(e => e.TaxMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto01");

                entity.Property(e => e.TaxMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto02");

                entity.Property(e => e.TaxMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto03");

                entity.Property(e => e.TaxMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto04");

                entity.Property(e => e.TaxMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto05");

                entity.Property(e => e.TaxMto06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto06");

                entity.Property(e => e.TaxMto07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto07");

                entity.Property(e => e.TaxMto08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto08");

                entity.Property(e => e.TaxMtoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto_tot");

                entity.Property(e => e.TaxPor01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por01");

                entity.Property(e => e.TaxPor02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por02");

                entity.Property(e => e.TaxPor03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por03");

                entity.Property(e => e.TaxPor04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por04");

                entity.Property(e => e.TaxPor05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por05");

                entity.Property(e => e.TaxPor06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por06");

                entity.Property(e => e.TaxPor07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por07");

                entity.Property(e => e.TaxPor08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por08");

                entity.Property(e => e.TaxPorTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por_tot");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.Property(e => e.TxtProducto)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("txt_producto");

                entity.HasOne(d => d.IdCompRecibidoNavigation)
                    .WithMany(p => p.Tnst02CompRecibidoDtls)
                    .HasForeignKey(d => d.IdCompRecibido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefTNSt01_comp_recibido166");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Tnst02CompRecibidoDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TNSt02_comp_recibido_dtl_PROt18_productocom");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Tnst02CompRecibidoDtls)
                    .HasForeignKey(d => d.IdRazon)
                    .HasConstraintName("RefMSTt05_razon168");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Tnst02CompRecibidoDtls)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("RefSNTt06_unidad_medida167");
            });

            modelBuilder.Entity<Tnst03CompRecibidoEstado>(entity =>
            {
                entity.HasKey(e => e.IdCompRecibidoEstado)
                    .HasName("PKCTBt12_nro_corr")
                    .IsClustered(false);

                entity.ToTable("TNSt03_comp_recibido_estado");

                entity.Property(e => e.IdCompRecibidoEstado).HasColumnName("id_comp_recibido_estado");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdCompRecibido).HasColumnName("id_comp_recibido");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.HasOne(d => d.IdCompRecibidoNavigation)
                    .WithMany(p => p.Tnst03CompRecibidoEstados)
                    .HasForeignKey(d => d.IdCompRecibido)
                    .HasConstraintName("FKCTBt12_id_comp_recibido");
            });

            modelBuilder.Entity<Tnst04CompEmitido>(entity =>
            {
                entity.HasKey(e => e.IdCompEmitido)
                    .HasName("PKCTBt13_id_comp_em")
                    .IsClustered(false);

                entity.ToTable("TNSt04_comp_emitido");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.CodCaja)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cod_caja");

                entity.Property(e => e.FecCanc).HasColumnName("fec_canc");

                entity.Property(e => e.FecEmi).HasColumnName("fec_emi");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegEmitido).HasColumnName("fec_reg_emitido");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.FecVcto).HasColumnName("fec_vcto");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdCampana).HasColumnName("id_campana");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdLocationTo).HasColumnName("id_location_to");

                entity.Property(e => e.IdMesa).HasColumnName("id_mesa");

                entity.Property(e => e.IdPredio).HasColumnName("id_predio");

                entity.Property(e => e.IdTipoComp).HasColumnName("id_tipo_comp");

                entity.Property(e => e.IdTipoMoneda).HasColumnName("id_tipo_moneda");

                entity.Property(e => e.IdTipoOrden).HasColumnName("id_tipo_orden");

                entity.Property(e => e.IdTurno).HasColumnName("id_turno");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdUsuarioModificador).HasColumnName("id_usuario_modificador");

                entity.Property(e => e.Info01)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info01");

                entity.Property(e => e.Info02)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info02");

                entity.Property(e => e.Info03)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info03");

                entity.Property(e => e.Info04)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info04");

                entity.Property(e => e.Info05)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info05");

                entity.Property(e => e.Info06)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info06");

                entity.Property(e => e.Info07)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info07");

                entity.Property(e => e.Info08)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info08");

                entity.Property(e => e.Info09)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info09");

                entity.Property(e => e.Info10)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("info10");

                entity.Property(e => e.InfoDate01)
                    .HasColumnType("date")
                    .HasColumnName("info_date01");

                entity.Property(e => e.InfoDate02)
                    .HasColumnType("date")
                    .HasColumnName("info_date02");

                entity.Property(e => e.InfoDate03)
                    .HasColumnType("date")
                    .HasColumnName("info_date03");

                entity.Property(e => e.InfoDate04)
                    .HasColumnType("date")
                    .HasColumnName("info_date04");

                entity.Property(e => e.InfoDate05)
                    .HasColumnType("date")
                    .HasColumnName("info_date05");

                entity.Property(e => e.InfoMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto01");

                entity.Property(e => e.InfoMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto02");

                entity.Property(e => e.InfoMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto03");

                entity.Property(e => e.InfoMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto04");

                entity.Property(e => e.InfoMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("info_mto05");

                entity.Property(e => e.MtoDsctoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_tot");

                entity.Property(e => e.MtoExonerado)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_exonerado");

                entity.Property(e => e.MtoImptoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_impto_tot");

                entity.Property(e => e.MtoNeto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_neto");

                entity.Property(e => e.MtoNoAfecto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_no_afecto");

                entity.Property(e => e.MtoServicio)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_servicio");

                entity.Property(e => e.MtoSubTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_sub_tot");

                entity.Property(e => e.MtoTcVta)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_tc_vta");

                entity.Property(e => e.MtoTotComp)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_tot_comp");

                entity.Property(e => e.NroCheque).HasColumnName("nro_cheque");

                entity.Property(e => e.NroCompEmitido)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nro_comp_emitido");

                entity.Property(e => e.NumComensales).HasColumnName("num_comensales");

                entity.Property(e => e.Post).HasColumnName("post");

                entity.Property(e => e.PostDate).HasColumnName("post_date");

                entity.Property(e => e.RefFecha)
                    .HasColumnType("date")
                    .HasColumnName("ref_fecha");

                entity.Property(e => e.RefIdCompEmitido).HasColumnName("ref_id_comp_emitido");

                entity.Property(e => e.RefNumero)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ref_numero");

                entity.Property(e => e.RefSerie)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ref_serie");

                entity.Property(e => e.RefTipoComprobante)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ref_tipo_comprobante");

                entity.Property(e => e.SnChkAbierto).HasColumnName("sn_chk_abierto");

                entity.Property(e => e.SnChkEnviado).HasColumnName("sn_chk_enviado");

                entity.Property(e => e.TaxMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto01");

                entity.Property(e => e.TaxMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto02");

                entity.Property(e => e.TaxMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto03");

                entity.Property(e => e.TaxMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto04");

                entity.Property(e => e.TaxMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto05");

                entity.Property(e => e.TaxMto06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto06");

                entity.Property(e => e.TaxMto07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto07");

                entity.Property(e => e.TaxMto08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto08");

                entity.Property(e => e.TaxPor01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por01");

                entity.Property(e => e.TaxPor02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por02");

                entity.Property(e => e.TaxPor03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por03");

                entity.Property(e => e.TaxPor04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por04");

                entity.Property(e => e.TaxPor05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por05");

                entity.Property(e => e.TaxPor06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por06");

                entity.Property(e => e.TaxPor07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por07");

                entity.Property(e => e.TaxPor08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por08");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtNumero)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero");

                entity.Property(e => e.TxtNumeroFe)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_numero_fe");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.Property(e => e.TxtSerie)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie");

                entity.Property(e => e.TxtSerieFe)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("txt_serie_fe");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.Property(e => e.TxtUsuarioModificador)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario_modificador");

                entity.HasOne(d => d.IdCampanaNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdCampana)
                    .HasConstraintName("FK_TNSt04_comp_emitido_id_campana");

                entity.HasOne(d => d.IdCanVtaNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdCanVta)
                    .HasConstraintName("RefVTAt05_canal_vta89");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt02_cliente1881");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Tnst04CompEmitidoIdLocationNavigations)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCMRt10_origen_entidad107");

                entity.HasOne(d => d.IdLocationToNavigation)
                    .WithMany(p => p.Tnst04CompEmitidoIdLocationToNavigations)
                    .HasForeignKey(d => d.IdLocationTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TNSt04_co__id_lo__4AD81681");

                entity.HasOne(d => d.IdMesaNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdMesa)
                    .HasConstraintName("RefMSTt14_mesa158");

                entity.HasOne(d => d.IdPredioNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdPredio)
                    .HasConstraintName("FK_TNSt04_comp_emitido_id_predio");

                entity.HasOne(d => d.IdTipoCompNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdTipoComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCTBt13_cod_tipo_comp");

                entity.HasOne(d => d.IdTipoMonedaNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdTipoMoneda)
                    .HasConstraintName("FKCTBt13_id_mon");

                entity.HasOne(d => d.IdTipoOrdenNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdTipoOrden)
                    .HasConstraintName("FKCTBt13_id_tipo_vta");

                entity.HasOne(d => d.IdTurnoNavigation)
                    .WithMany(p => p.Tnst04CompEmitidos)
                    .HasForeignKey(d => d.IdTurno)
                    .HasConstraintName("RefMSTt13_turno161_1");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Tnst04CompEmitidoIdUsuarioNavigations)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefPERt01_usuario1451");

                entity.HasOne(d => d.IdUsuarioModificadorNavigation)
                    .WithMany(p => p.Tnst04CompEmitidoIdUsuarioModificadorNavigations)
                    .HasForeignKey(d => d.IdUsuarioModificador)
                    .HasConstraintName("id_usuario_modificador");
            });

            modelBuilder.Entity<Tnst05CompEmitidoDtl>(entity =>
            {
                entity.HasKey(e => e.IdCompEmitidoDtl)
                    .HasName("PKtCTB14_nro_corr")
                    .IsClustered(false);

                entity.ToTable("TNSt05_comp_emitido_dtl");

                entity.Property(e => e.IdCompEmitidoDtl).HasColumnName("id_comp_emitido_dtl");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cantidad");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.MtoDsctoConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_con_tax");

                entity.Property(e => e.MtoDsctoSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_dscto_sin_tax");

                entity.Property(e => e.MtoVtaConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_vta_con_tax");

                entity.Property(e => e.MtoVtaSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_vta_sin_tax");

                entity.Property(e => e.Peso)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("peso");

                entity.Property(e => e.PorDscto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("por_dscto");

                entity.Property(e => e.PunitConTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("punit_con_tax");

                entity.Property(e => e.PunitSinTax)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("punit_sin_tax");

                entity.Property(e => e.TaxMto01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto01");

                entity.Property(e => e.TaxMto02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto02");

                entity.Property(e => e.TaxMto03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto03");

                entity.Property(e => e.TaxMto04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto04");

                entity.Property(e => e.TaxMto05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto05");

                entity.Property(e => e.TaxMto06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto06");

                entity.Property(e => e.TaxMto07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto07");

                entity.Property(e => e.TaxMto08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto08");

                entity.Property(e => e.TaxMtoTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_mto_tot");

                entity.Property(e => e.TaxPor01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por01");

                entity.Property(e => e.TaxPor02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por02");

                entity.Property(e => e.TaxPor03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por03");

                entity.Property(e => e.TaxPor04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por04");

                entity.Property(e => e.TaxPor05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por05");

                entity.Property(e => e.TaxPor06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por06");

                entity.Property(e => e.TaxPor07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por07");

                entity.Property(e => e.TaxPor08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por08");

                entity.Property(e => e.TaxPorTot)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("tax_por_tot");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.Property(e => e.TxtProducto)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("txt_producto");

                entity.HasOne(d => d.IdCompEmitidoNavigation)
                    .WithMany(p => p.Tnst05CompEmitidoDtls)
                    .HasForeignKey(d => d.IdCompEmitido)
                    .HasConstraintName("FKCTBt14_id_comp_emitido");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Tnst05CompEmitidoDtls)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCTBt14_id_prod");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Tnst05CompEmitidoDtls)
                    .HasForeignKey(d => d.IdRazon)
                    .HasConstraintName("RefCTBt03_Razon83");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Tnst05CompEmitidoDtls)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FKCTBt14id_um_comp");
            });

            modelBuilder.Entity<Tnst06CompEmitidoEstado>(entity =>
            {
                entity.HasKey(e => e.IdCompEmitidoEstado)
                    .HasName("PKCTBt12_nro_corr_1")
                    .IsClustered(false);

                entity.ToTable("TNSt06_comp_emitido_estado");

                entity.Property(e => e.IdCompEmitidoEstado).HasColumnName("id_comp_emitido_estado");

                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");

                entity.HasOne(d => d.IdCompEmitidoNavigation)
                    .WithMany(p => p.Tnst06CompEmitidoEstados)
                    .HasForeignKey(d => d.IdCompEmitido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefTNSt04_comp_emitido1391");
            });

            modelBuilder.Entity<Tnst07MedioPagoDtl>(entity =>
            {
                entity.HasKey(e => e.IdMedioPagoDtl)
                    .HasName("PK50")
                    .IsClustered(false);

                entity.ToTable("TNSt07_medio_pago_dtl");

                entity.Property(e => e.IdMedioPagoDtl).HasColumnName("id_medio_pago_dtl");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdMedioPago).HasColumnName("id_medio_pago");

                entity.Property(e => e.Impuesto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("impuesto");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto");

                entity.Property(e => e.MtoTipoCambio)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_tipo_cambio");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.Property(e => e.TxtRef)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("txt_ref");

                entity.HasOne(d => d.IdCompEmitidoNavigation)
                    .WithMany(p => p.Tnst07MedioPagoDtls)
                    .HasForeignKey(d => d.IdCompEmitido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCTBt13_comp_emitido81");

                entity.HasOne(d => d.IdMedioPagoNavigation)
                    .WithMany(p => p.Tnst07MedioPagoDtls)
                    .HasForeignKey(d => d.IdMedioPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefTSOt01_medio_pago82");
            });

            modelBuilder.Entity<Tnst08DescuentoDtl>(entity =>
            {
                entity.HasKey(e => e.IdDescuentoDtl)
                    .HasName("PK51_desct")
                    .IsClustered(false);

                entity.ToTable("TNSt08_descuento_dtl");

                entity.Property(e => e.IdDescuentoDtl).HasColumnName("id_descuento_dtl");

                entity.Property(e => e.IdCompEmitido).HasColumnName("id_comp_emitido");

                entity.Property(e => e.IdDescuento).HasColumnName("id_descuento");

                entity.Property(e => e.IdEmpAutorizador).HasColumnName("id_emp_autorizador");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdRazon).HasColumnName("id_razon");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("monto");

                entity.Property(e => e.Porcentaje)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("porcentaje");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtObserv)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("txt_observ");

                entity.HasOne(d => d.IdCompEmitidoNavigation)
                    .WithMany(p => p.Tnst08DescuentoDtls)
                    .HasForeignKey(d => d.IdCompEmitido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCTBt13_comp_emitido84");

                entity.HasOne(d => d.IdDescuentoNavigation)
                    .WithMany(p => p.Tnst08DescuentoDtls)
                    .HasForeignKey(d => d.IdDescuento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCTBt02_Descuento85");

                entity.HasOne(d => d.IdEmpAutorizadorNavigation)
                    .WithMany(p => p.Tnst08DescuentoDtls)
                    .HasForeignKey(d => d.IdEmpAutorizador)
                    .HasConstraintName("RefPERt04_empleado146");

                entity.HasOne(d => d.IdRazonNavigation)
                    .WithMany(p => p.Tnst08DescuentoDtls)
                    .HasForeignKey(d => d.IdRazon)
                    .HasConstraintName("RefCTBt03_Razon86");
            });

            modelBuilder.Entity<Tott01TotalDiarioVtum>(entity =>
            {
                entity.HasKey(e => e.IdTotalDiarioVta)
                    .HasName("PK47")
                    .IsClustered(false);

                entity.ToTable("TOTt01_total_diario_vta");

                entity.Property(e => e.IdTotalDiarioVta).HasColumnName("id_total_diario_vta");

                entity.Property(e => e.CantBoletas).HasColumnName("cant_boletas");

                entity.Property(e => e.CantComprobantes).HasColumnName("cant_comprobantes");

                entity.Property(e => e.CantFacturas).HasColumnName("cant_facturas");

                entity.Property(e => e.CantNotCred).HasColumnName("cant_not_cred");

                entity.Property(e => e.CantOrdenes).HasColumnName("cant_ordenes");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.MtoBoletas)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_boletas");

                entity.Property(e => e.MtoComprobantes)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_comprobantes");

                entity.Property(e => e.MtoFacturas)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_facturas");

                entity.Property(e => e.MtoFormaPago01)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago01");

                entity.Property(e => e.MtoFormaPago02)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago02");

                entity.Property(e => e.MtoFormaPago03)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago03");

                entity.Property(e => e.MtoFormaPago04)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago04");

                entity.Property(e => e.MtoFormaPago05)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago05");

                entity.Property(e => e.MtoFormaPago06)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago06");

                entity.Property(e => e.MtoFormaPago07)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago07");

                entity.Property(e => e.MtoFormaPago08)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago08");

                entity.Property(e => e.MtoFormaPago09)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago09");

                entity.Property(e => e.MtoFormaPago10)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_forma_pago10");

                entity.Property(e => e.MtoNotCred)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_not_cred");

                entity.Property(e => e.MtoOrdenes)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("mto_ordenes");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");
            });

            modelBuilder.Entity<Tott02TotalDiarioProd>(entity =>
            {
                entity.HasKey(e => e.IdTotalDiarioProd)
                    .HasName("PK110")
                    .IsClustered(false);

                entity.ToTable("TOTt02_total_diario_prod");

                entity.Property(e => e.IdTotalDiarioProd).HasColumnName("id_total_diario_prod");

                entity.Property(e => e.CantCompra)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cant_compra");

                entity.Property(e => e.CantTransOut)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cant_trans_out");

                entity.Property(e => e.CantTransfIn)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cant_transf_in");

                entity.Property(e => e.CantVta)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("cant_vta");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("costo");

                entity.Property(e => e.FecNegocio)
                    .HasColumnType("date")
                    .HasColumnName("fec_negocio");

                entity.Property(e => e.FecRegistro).HasColumnName("fec_registro");

                entity.Property(e => e.IdCanVta).HasColumnName("id_can_vta");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.IdLocation).HasColumnName("id_location");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdTipoOrden).HasColumnName("id_tipo_orden");

                entity.Property(e => e.IdUm).HasColumnName("id_um");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Post).HasColumnName("post");

                entity.Property(e => e.PostDate).HasColumnName("post_date");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("precio");

                entity.Property(e => e.PrecioAvg)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("precio_avg");

                entity.Property(e => e.PrecioLast)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("precio_last");

                entity.Property(e => e.SohFin)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("soh_fin");

                entity.Property(e => e.SohIni)
                    .HasColumnType("decimal(18, 8)")
                    .HasColumnName("soh_ini");

                entity.Property(e => e.TxtEstado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("txt_estado");

                entity.Property(e => e.TxtUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("txt_usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
