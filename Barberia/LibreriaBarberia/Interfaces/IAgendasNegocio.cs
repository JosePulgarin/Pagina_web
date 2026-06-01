using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IAgendasNegocio
    {
        // C - CREATE (Crear)
        Agendas Guardar(Agendas entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Agendas> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Agendas ConsultarPorId(int id); // Método para consultar un Agenda por su ID, necesario para actualizar
        Agendas Actualizar(Agendas entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}