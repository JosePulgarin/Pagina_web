using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IReseñasClientesNegocio
    {
        // C - CREATE (Crear)
        ReseñasClientes Guardar(ReseñasClientes entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<ReseñasClientes> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        ReseñasClientes ConsultarPorId(int id); // Método para consultar una Reseña de cliente por su ID, necesario para actualizar
        ReseñasClientes Actualizar(ReseñasClientes entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}