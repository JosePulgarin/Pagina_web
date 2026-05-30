using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class PerfilUsuariosNegocio : IPerfilUsuariosNegocio
    {
        private IConexion? iConexion;
       

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public PerfilUsuarios Guardar(PerfilUsuarios entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un PerfilUsuario nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Correo) || string.IsNullOrWhiteSpace(entidad.Contraseña))
                throw new Exception("El Correo y la contraseña del PerfilUsuario son absolutamente obligatorios.");




            // Conexión y guardado
            this.iConexion = new Conexion();
           
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            bool correoRepetido = this.iConexion.PerfilUsuarios!.Any(p => p.Correo == entidad.Correo && p.Id != entidad.Id);

            if (correoRepetido)
                throw new Exception("Error de registro: Este correo ya está asociado a otra cuenta en el sistema.");
            


            if (string.IsNullOrWhiteSpace(entidad.Contraseña) || entidad.Contraseña.Length < 6)
                throw new Exception("Error de seguridad: La contraseña es obligatoria y debe tener al menos 6 caracteres.");

            this.iConexion.PerfilUsuarios!.Add(entidad);

            this.iConexion.SaveChanges();


            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<PerfilUsuarios> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.PerfilUsuarios!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public PerfilUsuarios ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer PerfilUsuario que coincida con ese Id
            var PerfilUsuarioEncontrado = this.iConexion.PerfilUsuarios!.FirstOrDefault(c => c.Id == id);

          

            return PerfilUsuarioEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public PerfilUsuarios Actualizar(PerfilUsuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un PerfilUsuario, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este PerfilUsuario fue editado
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

            // Primero buscamos si el PerfilUsuario existe
            var PerfilUsuarioAEliminar = this.iConexion.PerfilUsuarios!.FirstOrDefault(c => c.Id == id);

            if (PerfilUsuarioAEliminar == null)
                throw new Exception("No se puede eliminar: El PerfilUsuario no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.PerfilUsuarios!.Remove(PerfilUsuarioAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


