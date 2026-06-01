using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IPromocionesServiciosNegocio
    {
        // C - CREATE (Crear)
        PromocionesServicios Guardar(PromocionesServicios entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<PromocionesServicios> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        PromocionesServicios ConsultarPorId(int id); // Método para consultar un Promoción de servicios por su ID, necesario para actualizar
        PromocionesServicios Actualizar(PromocionesServicios entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}