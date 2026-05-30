using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class HorariosLaboralesNegocio : IHorariosLaboralesNegocio
    {
        private IConexion? iConexion;

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public HorariosLaborales Guardar(HorariosLaborales entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un HorarioLaboral nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Dia))
                throw new Exception("El Día del HorarioLaboral son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.HorariosLaborales!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<HorariosLaborales> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.HorariosLaborales!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public HorariosLaborales ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer HorarioLaboral que coincida con ese Id
            var HorarioLaboralEncontrado = this.iConexion.HorariosLaborales!.FirstOrDefault(c => c.Id == id);

          

            return HorarioLaboralEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public HorariosLaborales Actualizar(HorariosLaborales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un HorarioLaboral, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este HorarioLaboral fue editado
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

            // Primero buscamos si el HorarioLaboral existe
            var HorarioLaboralAEliminar = this.iConexion.HorariosLaborales!.FirstOrDefault(c => c.Id == id);

            if (HorarioLaboralAEliminar == null)
                throw new Exception("No se puede eliminar: El HorarioLaboral no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.HorariosLaborales!.Remove(HorarioLaboralAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


