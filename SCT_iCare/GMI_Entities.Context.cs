﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCT_iCare
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GMIEntities : DbContext
    {
        public GMIEntities()
            : base("name=GMIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Aptitud> Aptitud { get; set; }
        public virtual DbSet<Archivos> Archivos { get; set; }
        public virtual DbSet<AudioGA> AudioGA { get; set; }
        public virtual DbSet<Biometricos> Biometricos { get; set; }
        public virtual DbSet<CallCenter> CallCenter { get; set; }
        public virtual DbSet<Canal> Canal { get; set; }
        public virtual DbSet<Canales> Canales { get; set; }
        public virtual DbSet<Captura> Captura { get; set; }
        public virtual DbSet<CapturaIncidencia> CapturaIncidencia { get; set; }
        public virtual DbSet<Cardiologo> Cardiologo { get; set; }
        public virtual DbSet<CarruselMedico> CarruselMedico { get; set; }
        public virtual DbSet<CartaNoAccidentesESP> CartaNoAccidentesESP { get; set; }
        public virtual DbSet<CERTIFICADO> CERTIFICADO { get; set; }
        public virtual DbSet<Cita> Cita { get; set; }
        public virtual DbSet<CitaDS> CitaDS { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<CondensadoPaciente> CondensadoPaciente { get; set; }
        public virtual DbSet<ConteoDoctores2> ConteoDoctores2 { get; set; }
        public virtual DbSet<DeclaracionSaludESP> DeclaracionSaludESP { get; set; }
        public virtual DbSet<Dictamen> Dictamen { get; set; }
        public virtual DbSet<DictamenESP> DictamenESP { get; set; }
        public virtual DbSet<DictamenProblema> DictamenProblema { get; set; }
        public virtual DbSet<Doctores> Doctores { get; set; }
        public virtual DbSet<Doctores2> Doctores2 { get; set; }
        public virtual DbSet<DoctorModulo> DoctorModulo { get; set; }
        public virtual DbSet<DoctorModuloDS> DoctorModuloDS { get; set; }
        public virtual DbSet<DocumentosESP> DocumentosESP { get; set; }
        public virtual DbSet<EKGRandom> EKGRandom { get; set; }
        public virtual DbSet<EPI_A_Heredofamiliares> EPI_A_Heredofamiliares { get; set; }
        public virtual DbSet<EPI_A_NoPatologicos> EPI_A_NoPatologicos { get; set; }
        public virtual DbSet<EPI_A_Patologicos> EPI_A_Patologicos { get; set; }
        public virtual DbSet<EPI_AparatosSistemas> EPI_AparatosSistemas { get; set; }
        public virtual DbSet<EPI_Audiologia> EPI_Audiologia { get; set; }
        public virtual DbSet<EPI_Cardiologia> EPI_Cardiologia { get; set; }
        public virtual DbSet<EPI_DictamenAptitud> EPI_DictamenAptitud { get; set; }
        public virtual DbSet<EPI_ESP> EPI_ESP { get; set; }
        public virtual DbSet<EPI_ExploracionFisica> EPI_ExploracionFisica { get; set; }
        public virtual DbSet<EPI_GA> EPI_GA { get; set; }
        public virtual DbSet<EPI_Laboratorio> EPI_Laboratorio { get; set; }
        public virtual DbSet<EPI_Odontologia> EPI_Odontologia { get; set; }
        public virtual DbSet<Epi_Oftalmologia> Epi_Oftalmologia { get; set; }
        public virtual DbSet<EPI_SignosVitales> EPI_SignosVitales { get; set; }
        public virtual DbSet<ExpedienteRevaloracion> ExpedienteRevaloracion { get; set; }
        public virtual DbSet<Expedientes> Expedientes { get; set; }
        public virtual DbSet<Farmacia> Farmacia { get; set; }
        public virtual DbSet<FirmaESP> FirmaESP { get; set; }
        public virtual DbSet<FotoPacienteESP> FotoPacienteESP { get; set; }
        public virtual DbSet<Fotos> Fotos { get; set; }
        public virtual DbSet<HemoglobinaGlucosiladaESP> HemoglobinaGlucosiladaESP { get; set; }
        public virtual DbSet<HuellasESP> HuellasESP { get; set; }
        public virtual DbSet<HuellasRandom> HuellasRandom { get; set; }
        public virtual DbSet<IncidenciaDictamen> IncidenciaDictamen { get; set; }
        public virtual DbSet<IncidenciaPaciente> IncidenciaPaciente { get; set; }
        public virtual DbSet<log_InicioGestor> log_InicioGestor { get; set; }
        public virtual DbSet<log_Movimientos> log_Movimientos { get; set; }
        public virtual DbSet<MedicinaInterna> MedicinaInterna { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<modulo> modulo { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<MovimientosAudio> MovimientosAudio { get; set; }
        public virtual DbSet<Nutriologo> Nutriologo { get; set; }
        public virtual DbSet<Oftalmologo> Oftalmologo { get; set; }
        public virtual DbSet<operaciones> operaciones { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<PacienteDS> PacienteDS { get; set; }
        public virtual DbSet<PacienteESP> PacienteESP { get; set; }
        public virtual DbSet<PacienteGA> PacienteGA { get; set; }
        public virtual DbSet<PagosGestores> PagosGestores { get; set; }
        public virtual DbSet<Recepcionista> Recepcionista { get; set; }
        public virtual DbSet<RecepcionistaDS> RecepcionistaDS { get; set; }
        public virtual DbSet<ReferenciasSB> ReferenciasSB { get; set; }
        public virtual DbSet<Referido> Referido { get; set; }
        public virtual DbSet<rol_operacion> rol_operacion { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolMenu> RolMenu { get; set; }
        public virtual DbSet<SignosVitalesDS> SignosVitalesDS { get; set; }
        public virtual DbSet<SucursalDS> SucursalDS { get; set; }
        public virtual DbSet<Sucursales> Sucursales { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<TicketSeguro> TicketSeguro { get; set; }
        public virtual DbSet<Tipificaciones> Tipificaciones { get; set; }
        public virtual DbSet<UrgentesCount> UrgentesCount { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    
        public virtual ObjectResult<buscarGestor_Result> buscarGestor(string consulta, Nullable<int> cantidad)
        {
            var consultaParameter = consulta != null ?
                new ObjectParameter("consulta", consulta) :
                new ObjectParameter("consulta", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<buscarGestor_Result>("buscarGestor", consultaParameter, cantidadParameter);
        }
    }
}
