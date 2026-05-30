using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IPromocionesEspecialesNegocio
    {
        // C - CREATE (Crear)
        PromocionesEspeciales Guardar(PromocionesEspeciales entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<PromocionesEspeciales> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        PromocionesEspeciales ConsultarPorId(int id); // Método para consultar un Promocion Especial por su ID, necesario para actualizar
        PromocionesEspeciales Actualizar(PromocionesEspeciales entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}