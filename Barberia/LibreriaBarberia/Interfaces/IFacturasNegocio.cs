using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IFacturasNegocio
    {
        // C - CREATE (Crear)
        Facturas Guardar(Facturas entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<Facturas> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        Facturas ConsultarPorId(int id); // Método para consultar una Factura por su ID, necesario para actualizar
        Facturas Actualizar(Facturas entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}