using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface ICategoriasProductosNegocio
    {
        // C - CREATE (Crear)
        CategoriasProductos Guardar(CategoriasProductos entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<CategoriasProductos> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        CategoriasProductos ConsultarPorId(int id); // Método para consultar una categoria de productos por su ID, necesario para actualizar
        CategoriasProductos Actualizar(CategoriasProductos entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}