using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IMetodosPagoNegocio
    {
        // C - CREATE (Crear)
        MetodosPago Guardar(MetodosPago entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<MetodosPago> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        MetodosPago ConsultarPorId(int id); // Método para consultar un MetodoPago por su ID, necesario para actualizar
        MetodosPago Actualizar(MetodosPago entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}

