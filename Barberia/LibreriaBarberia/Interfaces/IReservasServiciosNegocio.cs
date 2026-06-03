using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IReservasServiciosNegocio
    {
        // C - CREATE (Crear)
        ReservasServicios Guardar(ReservasServicios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<ReservasServicios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        ReservasServicios ConsultarPorId(int id); // Método para consultar un reserva de servicios por su ID, necesario para actualizar
        ReservasServicios Actualizar(ReservasServicios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}
