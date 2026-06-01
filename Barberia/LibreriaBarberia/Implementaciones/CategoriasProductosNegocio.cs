using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class CategoriasProductosNegocio : ICategoriasProductosNegocio
    {
        private IConexion? iConexion;
     

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public CategoriasProductos Guardar(CategoriasProductos entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un CategoriaProducto nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("El Nombre y la Direccion del CategoriaProducto son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();
  
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");


            this.iConexion.CategoriasProductos!.Add(entidad);
            

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================

        public List<CategoriasProductos> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.CategoriasProductos!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public CategoriasProductos ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer CategoriaProducto que coincida con ese Id
            var CategoriaProductoEncontrado = this.iConexion.CategoriasProductos!.FirstOrDefault(c => c.Id == id);

            return CategoriaProductoEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================

        public CategoriasProductos Actualizar(CategoriasProductos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un CategoriaProducto, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este CategoriaProducto fue editado
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

            // Primero buscamos si el CategoriaProducto existe
            var CategoriaProductoAEliminar = this.iConexion.CategoriasProductos!.FirstOrDefault(c => c.Id == id);

            if (CategoriaProductoAEliminar == null)
                throw new Exception("No se puede eliminar: El CategoriaProducto no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.CategoriasProductos!.Remove(CategoriaProductoAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
