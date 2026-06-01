using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class RolesNegocio : IRolesNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Roles Guardar(Roles entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Rol nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("El Nombre y la descripción del Rol son absolutamente obligatorios.");

            if(entidad.FechaCreacion >= DateTime.Now)
                throw new Exception("La fecha de creación del Rol no puede ser en el futuro. Por favor, verifica la fecha que estás enviando.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.Roles!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Roles> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Roles!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Roles ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Rol que coincida con ese Id
            var RolEncontrado = this.iConexion.Roles!.FirstOrDefault(c => c.Id == id);


            return RolEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Rol, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Rol fue editado
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

            // Primero buscamos si el Rol existe
            var RolAEliminar = this.iConexion.Roles!.FirstOrDefault(c => c.Id == id);

            if (RolAEliminar == null)
                throw new Exception("No se puede eliminar: El Rol no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Roles!.Remove(RolAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


