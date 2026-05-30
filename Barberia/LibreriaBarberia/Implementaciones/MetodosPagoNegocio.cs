using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class MetodosPagoNegocio : IMetodosPagoNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public MetodosPago Guardar(MetodosPago entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un MetodoPago nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Moneda) || string.IsNullOrWhiteSpace(entidad.TipoMetodo))
                throw new Exception("La moneda y el tipo de metodo del MetodoPago son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();
           
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.MetodosPago!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<MetodosPago> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.MetodosPago!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public MetodosPago ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer MetodoPago que coincida con ese Id
            var MetodoPagoEncontrado = this.iConexion.MetodosPago!.FirstOrDefault(c => c.Id == id);


            return MetodoPagoEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public MetodosPago Actualizar(MetodosPago entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un MetodoPago, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este MetodoPago fue editado
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

            // Primero buscamos si el MetodoPago existe
            var MetodoPagoAEliminar = this.iConexion.MetodosPago!.FirstOrDefault(c => c.Id == id);

            if (MetodoPagoAEliminar == null)
                throw new Exception("No se puede eliminar: El MetodoPago no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.MetodosPago!.Remove(MetodoPagoAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


