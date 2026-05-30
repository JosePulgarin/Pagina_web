using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class PortafoliosNegocio : IPortafoliosNegocio
    {
        private IConexion? iConexion;
       

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Portafolios Guardar(Portafolios entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Portafolio nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.TituloCorte) || string.IsNullOrWhiteSpace(entidad.Descripcion))
                throw new Exception("El Titulo y la descripción del Portafolio son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.Portafolios!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Portafolios> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Portafolios!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Portafolios ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Portafolio que coincida con ese Id
            var PortafolioEncontrado = this.iConexion.Portafolios!.FirstOrDefault(c => c.Id == id);


            return PortafolioEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Portafolios Actualizar(Portafolios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Portafolio, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Portafolio fue editado
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

            // Primero buscamos si el Portafolio existe
            var PortafolioAEliminar = this.iConexion.Portafolios!.FirstOrDefault(c => c.Id == id);

            if (PortafolioAEliminar == null)
                throw new Exception("No se puede eliminar: El Portafolio no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Portafolios!.Remove(PortafolioAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


