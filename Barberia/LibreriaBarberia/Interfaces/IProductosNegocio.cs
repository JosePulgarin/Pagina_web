using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IProductosNegocio
    {
        // C - CREATE (Crear)
        Productos Guardar(Productos entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Productos> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Productos ConsultarPorId(int id); // Método para consultar un producto por su ID, necesario para actualizar
        Productos Actualizar(Productos entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}