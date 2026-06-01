using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class ReseñasClientesNegocio : IReseñasClientesNegocio
    {
        private IConexion? iConexion;
       

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public ReseñasClientes Guardar(ReseñasClientes entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un ReseñaCliente nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (entidad.Puntuacion<0 || entidad.Puntuacion>5)
                throw new Exception("La puntuación debe ser de 0 a 5 estrellas.");

            if(entidad.FechaPublicacion > DateOnly.FromDateTime(DateTime.Now))
                throw new Exception("La fecha de publicación no puede ser futura.");

            // Conexión y guardado
            this.iConexion = new Conexion();
    
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.ReseñasClientes!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<ReseñasClientes> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            //Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.ReseñasClientes!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public ReseñasClientes ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer ReseñaCliente que coincida con ese Id
            var ReseñaClienteEncontrado = this.iConexion.ReseñasClientes!.FirstOrDefault(c => c.Id == id);

            return ReseñaClienteEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public ReseñasClientes Actualizar(ReseñasClientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un ReseñaCliente, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este ReseñaCliente fue editado
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

            // Primero buscamos si el ReseñaCliente existe
            var ReseñaClienteAEliminar = this.iConexion.ReseñasClientes!.FirstOrDefault(c => c.Id == id);

            if (ReseñaClienteAEliminar == null)
                throw new Exception("No se puede eliminar: El ReseñaCliente no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.ReseñasClientes!.Remove(ReseñaClienteAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


