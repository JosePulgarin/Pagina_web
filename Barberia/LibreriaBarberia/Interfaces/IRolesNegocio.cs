using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IRolesNegocio
    {
        // C - CREATE (Crear)
        Roles Guardar(Roles entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Roles> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Roles ConsultarPorId(int id); // Método para consultar un Rol por su ID, necesario para actualizar
        Roles Actualizar(Roles entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}