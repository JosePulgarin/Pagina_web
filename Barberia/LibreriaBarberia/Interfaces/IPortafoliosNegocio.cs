using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IPortafoliosNegocio
    {
        // C - CREATE (Crear)
        Portafolios Guardar(Portafolios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Portafolios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Portafolios ConsultarPorId(int id); // Método para consultar un Portafolio por su ID, necesario para actualizar
        Portafolios Actualizar(Portafolios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}