using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IRecepcionistasNegocio
    {
        // C - CREATE (Crear)
        Recepcionistas Guardar(Recepcionistas entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Recepcionistas> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Recepcionistas ConsultarPorId(int id); // Método para consultar un Recepcionista por su ID, necesario para actualizar
        Recepcionistas Actualizar(Recepcionistas entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}