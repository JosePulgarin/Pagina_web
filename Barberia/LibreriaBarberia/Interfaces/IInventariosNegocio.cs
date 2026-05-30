using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IInventariosNegocio
    {
        // C - CREATE (Crear)
        Inventarios Guardar(Inventarios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Inventarios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Inventarios ConsultarPorId(int id); // Método para consultar un Inventario por su ID, necesario para actualizar
        Inventarios Actualizar(Inventarios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}