using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IClientesNegocio
    {
        // C - CREATE (Crear)
        Clientes Guardar(Clientes entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Clientes> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Clientes ConsultarPorId(int id); // Método para consultar un Cliente por su ID, necesario para actualizar
        Clientes Actualizar(Clientes entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}
