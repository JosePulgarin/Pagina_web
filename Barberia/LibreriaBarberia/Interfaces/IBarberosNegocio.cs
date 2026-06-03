using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IBarberosNegocio
    {
        // C - CREATE (Crear)
        Barberos Guardar(Barberos entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Barberos> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Barberos ConsultarPorId(int id); // Método para consultar un Barbero por su ID, necesario para actualizar
        Barberos Actualizar(Barberos entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}