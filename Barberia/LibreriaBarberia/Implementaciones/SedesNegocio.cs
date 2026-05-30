using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class SedesNegocio : ISedesNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Sedes Guardar(Sedes entidad)
        {
            // Reglas de Negocio


            if (entidad.Id != 0)
                throw new Exception("Para guardar un Sede nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre) || string.IsNullOrWhiteSpace(entidad.Direccion))
                throw new Exception("El Nombre y la Direccion del Sede son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Nucleo.Configuraciones.obtener("string_conexion");

            this.iConexion.Sedes!.Add(entidad);

            this.iConexion.SaveChanges();


            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Sedes> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Sedes!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Sedes ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Sede que coincida con ese Id
            var SedeEncontrado = this.iConexion.Sedes!.FirstOrDefault(c => c.Id == id);

         return SedeEncontrado!;
          }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Sedes Actualizar(Sedes entidad)
{
    if (entidad.Id == 0)
        throw new Exception("Error: Para modificar un Sede, debes enviar su número de Id.");

    this.iConexion = new Conexion();
    this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
    // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Sede fue editado
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

    // Primero buscamos si el Sede existe
    var SedeAEliminar = this.iConexion.Sedes!.FirstOrDefault(c => c.Id == id);

    if (SedeAEliminar == null)
        throw new Exception("No se puede eliminar: El Sede no existe.");

    // Si existe, lo borramos usando Remove
    this.iConexion.Sedes!.Remove(SedeAEliminar);
    this.iConexion.SaveChanges();

    return true; // Le avisamos al Mesero que todo salió perfecto
}
    }
    }

