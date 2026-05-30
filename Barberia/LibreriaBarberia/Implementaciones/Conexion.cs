using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class Conexion : DbContext, IConexion // Implementación de la interfaz IConexion y hereda de DbContext para manejar la conexión a la base de datos
    {
        public string? string_conexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.string_conexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        // ================================================================
        //  APAGAR BORRADOS EN CASCADA PARA EVITAR EL ERROR DE CICLOS
        // ================================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public DbSet<Sedes>? Sedes { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<PerfilUsuarios> PerfilUsuarios { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<PromocionesEspeciales> PromocionesEspeciales { get; set; }
        public DbSet<Barberos> Barberos { get; set; }
        public DbSet<Recepcionistas> Recepcionistas { get; set; }
        public DbSet<Membresias> Membresias { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<HorariosLaborales> HorariosLaborales { get; set; }
        public DbSet<GastosOperativos> GastosOperativos { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<PromocionesServicios> PromocionesServicios { get; set; }
        public DbSet<Agendas> Agendas { get; set; }
        public DbSet<CategoriasProductos> CategoriasProductos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<ReseñasClientes> ReseñasClientes { get; set; }
        public DbSet<ReservasServicios> ReservasServicios { get; set; }
        public DbSet<Facturas> Facturas { get; set; }
        public DbSet<Comisiones> Comisiones { get; set; }
        public DbSet<Historicos> Historicos { get; set; }
        public DbSet<Portafolios> Portafolios { get; set; }

        public override int SaveChanges()
        {
            // 1. Atrapamos todas las entidades que están a punto de cambiar
            // Ignoramos la tabla "Historicos" para no hacer un bucle infinito
            var cambios = ChangeTracker.Entries()
                .Where(e => e.Entity.GetType().Name != "Historicos" &&
                           (e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted))
                .ToList();

            // 2. Por cada cambio detectado, creamos un registro automático
            foreach (var entry in cambios)
            {
                string accionDetectada = "";

                if (entry.State == EntityState.Added) accionDetectada = "CREACIÓN";
                else if (entry.State == EntityState.Modified) accionDetectada = "ACTUALIZACIÓN";
                else if (entry.State == EntityState.Deleted) accionDetectada = "ELIMINACIÓN";

                // Obtenemos el nombre exacto de la tabla/clase que se modificó (ej: "Sedes", "Agendas")
                string nombreTabla = entry.Entity.GetType().Name;

                // Creamos el registro fantasma
                Historicos registroAutomatico = new Historicos();
                registroAutomatico.Accion = accionDetectada;
                registroAutomatico.Entidad = "Acción automática en la tabla: " + nombreTabla;
                registroAutomatico.Usuario = "Admin_Swagger"; // Temporal hasta tener el Login
                registroAutomatico.Fecha = DateTime.Now;

                // Añadimos el histórico a la fila de guardado (sin llamar a SaveChanges otra vez)
                this.Historicos!.Add(registroAutomatico);
            }

            // 3. Dejamos que Entity Framework continúe y guarde todo de un solo golpe
            return base.SaveChanges();



        }
    }
}
