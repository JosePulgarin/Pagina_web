using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class MembresiasNegocio : IMembresiasNegocio
    {
        private IConexion? iConexion;
    

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Membresias Guardar(Membresias entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Membresia nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.NombrePlan))
                throw new Exception("El Nombre del plan de la Membresia son absolutamente obligatorios.");

            if (entidad.DescuentoPorcentaje <= 0 || entidad.DescuentoPorcentaje > 100)
            {
                throw new Exception("Error: Toda membresía debe ofrecer un descuento real. El valor debe ser mayor a 0 y máximo 100.");
            }

            // Conexión y guardado
            this.iConexion = new Conexion();
         
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.Membresias!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Membresias> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Membresias!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Membresias ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Membresia que coincida con ese Id
            var MembresiaEncontrado = this.iConexion.Membresias!.FirstOrDefault(c => c.Id == id);

        

            return MembresiaEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Membresias Actualizar(Membresias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Membresia, debes enviar su número de Id.");

            if (entidad.DescuentoPorcentaje <= 0 || entidad.DescuentoPorcentaje > 100)
            {
                throw new Exception("Error: Toda membresía debe ofrecer un descuento real. El valor debe ser mayor a 0 y máximo 100.");
            }

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Membresia fue editado
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

            // Primero buscamos si el Membresia existe
            var MembresiaAEliminar = this.iConexion.Membresias!.FirstOrDefault(c => c.Id == id);

            if (MembresiaAEliminar == null)
                throw new Exception("No se puede eliminar: El Membresia no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Membresias!.Remove(MembresiaAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


