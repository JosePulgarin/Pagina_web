using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IMembresiasNegocio
    {
        // C - CREATE (Crear)
        Membresias Guardar(Membresias entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Membresias> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Membresias ConsultarPorId(int id); // Método para consultar un Membresia por su ID, necesario para actualizar
        Membresias Actualizar(Membresias entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}