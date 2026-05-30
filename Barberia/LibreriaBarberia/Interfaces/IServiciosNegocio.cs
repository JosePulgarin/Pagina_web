using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IServiciosNegocio
    {
        // C - CREATE (Crear)
        Servicios Guardar(Servicios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Servicios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Servicios ConsultarPorId(int id); // Método para consultar un Servicio por su ID, necesario para actualizar
        Servicios Actualizar(Servicios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}