using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IPerfilUsuariosNegocio
    {
        // C - CREATE (Crear)
        PerfilUsuarios Guardar(PerfilUsuarios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<PerfilUsuarios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        PerfilUsuarios ConsultarPorId(int id); // Método para consultar un PerfilUsuario por su ID, necesario para actualizar
        PerfilUsuarios Actualizar(PerfilUsuarios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}