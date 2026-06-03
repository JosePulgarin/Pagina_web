using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IGastosOperativosNegocio
    {
        // C - CREATE (Crear)
        GastosOperativos Guardar(GastosOperativos entidad);
        // -------------------------------------------------
        // R - READ (Leer)
        List<GastosOperativos> Consultar();
        // -------------------------------------------------
        // U - UPDATE (Actualizar/Modificar)
        GastosOperativos ConsultarPorId(int id); // Método para consultar un Gasto operativo por su ID, necesario para actualizar
        GastosOperativos Actualizar(GastosOperativos entidad);
        // -------------------------------------------------
        // D - DELETE (Eliminar)
        bool Eliminar(int id);
        // -------------------------------------------------

    }
}