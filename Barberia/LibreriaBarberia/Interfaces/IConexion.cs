using LibreriaBarberia.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace LibreriaBarberia.Interfaces
{
    public interface IConexion
    {
        string? string_conexion { get; set; }
        DbSet<Sedes>? Sedes { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<PerfilUsuarios> PerfilUsuarios { get; set; }
        DbSet<Servicios> Servicios { get; set; }
        DbSet<MetodosPago> MetodosPago { get; set; }
        DbSet<Proveedores> Proveedores { get; set; }
        DbSet<PromocionesEspeciales> PromocionesEspeciales { get; set; }
        DbSet<Barberos> Barberos { get; set; }
        DbSet<Recepcionistas> Recepcionistas { get; set; }
        DbSet<Membresias> Membresias { get; set; }
        DbSet<Clientes> Clientes { get; set; }
        DbSet<HorariosLaborales> HorariosLaborales { get; set; }
        DbSet<GastosOperativos> GastosOperativos { get; set; }
        DbSet<Inventarios> Inventarios { get; set; }
        DbSet<PromocionesServicios> PromocionesServicios { get; set; }
        DbSet<Agendas> Agendas { get; set; }
        DbSet<CategoriasProductos> CategoriasProductos { get; set; }
        DbSet<Productos> Productos { get; set; }
        DbSet<Reservas> Reservas { get; set; }
        DbSet<ReseñasClientes> ReseñasClientes { get; set; }
        DbSet<ReservasServicios> ReservasServicios { get; set; }
        DbSet<Facturas> Facturas { get; set; }
        DbSet<Comisiones> Comisiones { get; set; }
        DbSet<Historicos> Historicos { get; set; }
        DbSet<Portafolios> Portafolios { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}