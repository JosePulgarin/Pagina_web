using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IComisionesNegocio
    {
        // C - CREATE (Crear)
        Comisiones Guardar(Comisiones entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Comisiones> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Comisiones ConsultarPorId(int id); // Método para consultar una comisión por su ID, necesario para actualizar
        Comisiones Actualizar(Comisiones entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}