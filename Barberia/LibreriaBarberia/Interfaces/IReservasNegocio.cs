using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IReservasNegocio
    {
        // C - CREATE (Crear)
        Reservas Guardar(Reservas entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Reservas> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Reservas ConsultarPorId(int id); // Método para consultar un Sede por su ID, necesario para actualizar
        Reservas Actualizar(Reservas entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}