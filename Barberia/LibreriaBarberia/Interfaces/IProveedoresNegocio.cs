using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IProveedoresNegocio
    {
        // C - CREATE (Crear)
        Proveedores Guardar(Proveedores entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Proveedores> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Proveedores ConsultarPorId(int id); // Método para consultar un Proveedor por su ID, necesario para actualizar
        Proveedores Actualizar(Proveedores entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}