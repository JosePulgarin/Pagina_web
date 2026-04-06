using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class Conexion : DbContext, IConexion // Implementación de la interfaz IConexion y hereda de DbContext para manejar la conexión a la base de datos
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Sedes>? Sedes { get; set; }
        /*public DbSet<PerfilUsuarios> PerfilUsuarios { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<PromocionesEspeciales> PromocionesEspeciales { get; set; }
        public DbSet<Barberos> Barberos { get; set; }
        public DbSet<Recepcionistas> Recepcionistas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<HorariosLaborales> HorariosLaborales { get; set; }
        public DbSet<GastosOperativos> GastosOperativos { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<PromocionesServicios> PromocionesServicios { get; set; }
        public DbSet<Agendas> Agendas { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<ReseñasClientes> ReseñasClientes { get; set; }
        public DbSet<ReservasServicios> ReservasServicios { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<Comisiones> Comisiones { get; set; }*/
    }
}