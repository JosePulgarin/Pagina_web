using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class ProductosNegocio : IProductosNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Productos Guardar(Productos entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Producto nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.MarcaProducto))
                throw new Exception("La marca del Producto son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if(entidad.Precio <= 0)
                throw new Exception("El precio del Producto debe ser mayor a cero.");

            this.iConexion.Productos!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Productos> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Productos!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Productos ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Producto que coincida con ese Id
            var ProductoEncontrado = this.iConexion.Productos!.FirstOrDefault(c => c.Id == id);

       

            return ProductoEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Productos Actualizar(Productos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Producto, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Producto fue editado
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

            // Primero buscamos si el Producto existe
            var ProductoAEliminar = this.iConexion.Productos!.FirstOrDefault(c => c.Id == id);

            if (ProductoAEliminar == null)
                throw new Exception("No se puede eliminar: El Producto no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Productos!.Remove(ProductoAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


