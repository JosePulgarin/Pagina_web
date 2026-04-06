using LibreriaBarberia.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }
        DbSet<Sedes>? Sedes { get; set; }
       /* DbSet<PerfilUsuarios> PerfilUsuarios { get; set; }
        DbSet<Servicios> Servicios { get; set; }
        DbSet<MetodosPago> MetodosPago { get; set; }
        DbSet<Proveedores> Proveedores { get; set; }
        DbSet<PromocionesEspeciales> PromocionesEspeciales { get; set; }
        DbSet<Barberos> Barberos { get; set; }
        DbSet<Recepcionistas> Recepcionistas { get; set; }
        DbSet<Clientes> Clientes { get; set; }
        DbSet<HorariosLaborales> HorariosLaborales { get; set; }
        DbSet<GastosOperativos> GastosOperativos { get; set; }
        DbSet<Inventarios> Inventarios { get; set; }
        DbSet<PromocionesServicios> PromocionesServicios { get; set; }
        DbSet<Agendas> Agendas { get; set; }
        DbSet<Productos> Productos { get; set; }
        DbSet<Reservas> Reservas { get; set; }
        DbSet<ReseñasClientes> ReseñasClientes { get; set; }
        DbSet<ReservasServicios> ReservasServicios { get; set; }
        DbSet<Facturas> Facturas { get; set; }
        DbSet<Comisiones> Comisiones { get; set; }*/
    }
}