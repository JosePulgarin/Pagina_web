using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class RecepcionistasNegocio : IRecepcionistasNegocio
    {
        private IConexion? iConexion;
       

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Recepcionistas Guardar(Recepcionistas entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Recepcionista nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Turno))
                throw new Exception("El Nombre y el turno del Recepcionista son absolutamente obligatorios.");

            var hace18Anos = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));

      
            if (entidad.FechaNacimiento > hace18Anos)
            {
                throw new Exception("Error de registro: La persona debe ser mayor de 18 años para ingresar al sistema.");
            }

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.Recepcionistas!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Recepcionistas> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Recepcionistas!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Recepcionistas ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Recepcionista que coincida con ese Id
            var RecepcionistaEncontrado = this.iConexion.Recepcionistas!.FirstOrDefault(c => c.Id == id);

            return RecepcionistaEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Recepcionistas Actualizar(Recepcionistas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Recepcionista, debes enviar su número de Id.");

            var hace18Anos = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));

            if (entidad.FechaNacimiento > hace18Anos)
            {
                throw new Exception("Error de registro: La persona debe ser mayor de 18 años para ingresar al sistema.");
            }

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Recepcionista fue editado
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

            // Primero buscamos si el Recepcionista existe
            var RecepcionistaAEliminar = this.iConexion.Recepcionistas!.FirstOrDefault(c => c.Id == id);

            if (RecepcionistaAEliminar == null)
                throw new Exception("No se puede eliminar: El Recepcionista no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Recepcionistas!.Remove(RecepcionistaAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


