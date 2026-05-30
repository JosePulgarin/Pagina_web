using LibreriaBarberia.Nucleo;
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class HistoricosNegocio : IHistoricosNegocio
    {
        private Conexion? iConexion;

        // R - READ: Traer toda la bitácora para el profe
        public List<Historicos> Consultar()
        {
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = LibreriaBarberia.Nucleo.Configuraciones.obtener("string_conexion");

            // Traemos la lista de históricos ordenados por fecha, lo más reciente primero
            return this.iConexion.Historicos!.OrderByDescending(h => h.Fecha).ToList();
        }

        // C - CREATE: El "Gatillo" que guarda las acciones
        public bool Guardar(Historicos entidad)
        {
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = LibreriaBarberia.Nucleo.Configuraciones.obtener("string_conexion");

            if (entidad == null) return false;

            // Aseguramos que siempre lleve la fecha y hora actual del servidor
            entidad.Fecha = DateTime.Now;

            this.iConexion.Historicos!.Add(entidad);
            return this.iConexion.SaveChanges() > 0;
        }
    }
}