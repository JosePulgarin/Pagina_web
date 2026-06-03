using LibreriaBarberia.Entidades;

namespace LibreriaBarberia.Interfaces
{
    public interface IHistoricosNegocio //TODOS LOS MÉTODOS CRUD ACÁ
    {

        List<Historicos> Consultar();
        // C - CREATE (Crear)
        bool Guardar(Historicos entidad);
        // -------------------------------------------------

    }
}