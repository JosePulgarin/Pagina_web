using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IHorariosLaboralesNegocio
    {
        // C - CREATE (Crear)
        HorariosLaborales Guardar(HorariosLaborales entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<HorariosLaborales> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        HorariosLaborales ConsultarPorId(int id); // Método para consultar un HorarioLaboral por su ID, necesario para actualizar
        HorariosLaborales Actualizar(HorariosLaborales entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}