using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class ReservasServiciosNegocio : IReservasServiciosNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public ReservasServicios Guardar(ReservasServicios entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un ReservaServicio nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (entidad.Precio<=0 || string.IsNullOrWhiteSpace(entidad.Observacion))
                throw new Exception("Ingrese un precio mayor a 0 y una observación");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.ReservasServicios!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<ReservasServicios> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.ReservasServicios!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public ReservasServicios ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer ReservaServicio que coincida con ese Id
            var ReservaServicioEncontrado = this.iConexion.ReservasServicios!.FirstOrDefault(c => c.Id == id);


            return ReservaServicioEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public ReservasServicios Actualizar(ReservasServicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un ReservaServicio, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este ReservaServicio fue editado
            this.iConexion.Entry(entidad).State = EntityState.Modified;
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // D - DELETE (Eliminar)
        // ==========================================
        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // Primero buscamos si el ReservaServicio existe
            var ReservaServicioAEliminar = this.iConexion.ReservasServicios!.FirstOrDefault(c => c.Id == id);

            if (ReservaServicioAEliminar == null)
                throw new Exception("No se puede eliminar: El ReservaServicio no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.ReservasServicios!.Remove(ReservaServicioAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


