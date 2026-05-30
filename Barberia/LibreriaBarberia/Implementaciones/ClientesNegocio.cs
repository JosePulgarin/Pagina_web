using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class ClientesNegocio : IClientesNegocio
    {
        private IConexion? iConexion;
       

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Clientes Guardar(Clientes entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Cliente nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("El Nombre y el correo del Cliente son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();
      
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // Validamos que ningún otro cliente use el mismo correo
            bool correoRepetido = this.iConexion.Clientes!.Any(c => c.Correo == entidad.Correo && c.Id != entidad.Id);

            if (correoRepetido)
                throw new Exception("Error: Este correo ya pertenece a otro cliente registrado en el sistema.");
          


            
        
           
            
            // Buscamos si ese perfil ya fue reclamado por otro cliente distinto
            bool usuarioRepetido = this.iConexion.Clientes!.Any(c => c.IdUsuario == entidad.IdUsuario && c.Id != entidad.Id);

            if (usuarioRepetido)
            throw new Exception("Error: Este perfil de usuario ya está vinculado a otro cliente. Cada cliente debe tener un perfil único.");
            

            this.iConexion.Clientes!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Clientes> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Clientes!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Clientes ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Cliente que coincida con ese Id
            var ClienteEncontrado = this.iConexion.Clientes!.FirstOrDefault(c => c.Id == id);

            return ClienteEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Clientes Actualizar(Clientes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Cliente, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            bool correoRepetido = this.iConexion.Clientes!.Any(c => c.Correo == entidad.Correo && c.Id != entidad.Id);

            if (correoRepetido)
                throw new Exception("Error: Este correo ya pertenece a otro cliente registrado en el sistema.");



            // Buscamos si ese perfil ya fue reclamado por otro cliente distinto
            bool usuarioRepetido = this.iConexion.Clientes!.Any(c => c.IdUsuario == entidad.IdUsuario && c.Id != entidad.Id);

            if (usuarioRepetido)
                throw new Exception("Error: Este perfil de usuario ya está vinculado a otro cliente. Cada cliente debe tener un perfil único.");

            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Cliente fue editado
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

            // Primero buscamos si el Cliente existe
            var ClienteAEliminar = this.iConexion.Clientes!.FirstOrDefault(c => c.Id == id);

            if (ClienteAEliminar == null)
                throw new Exception("No se puede eliminar: El Cliente no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Clientes!.Remove(ClienteAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
