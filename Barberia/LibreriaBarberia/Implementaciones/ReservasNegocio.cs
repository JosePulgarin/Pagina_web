using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class ReservasNegocio : IReservasNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Reservas Guardar(Reservas entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Reserva nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Notas) || entidad.Fecha > DateOnly.FromDateTime(DateTime.Now))
                throw new Exception("La reserva debe tener notas para más información y la fecha debe ser valida");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            bool citaYaReservada = this.iConexion.Reservas!.Any(r => r.IdAgenda == entidad.IdAgenda && r.Id != entidad.Id);

            if (citaYaReservada)
            {
                throw new Exception("Error de disponibilidad: Este espacio ya fue reservado por otro cliente. Por favor, elige un horario diferente.");
            }

            this.iConexion.Reservas!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Reservas> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Reservas!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Reservas ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Reserva que coincida con ese Id
            var ReservaEncontrado = this.iConexion.Reservas!.FirstOrDefault(c => c.Id == id);

            return ReservaEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Reserva, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Reserva fue editado
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

            // Primero buscamos si el Reserva existe
            var ReservaAEliminar = this.iConexion.Reservas!.FirstOrDefault(c => c.Id == id);

            if (ReservaAEliminar == null)
                throw new Exception("No se puede eliminar: El Reserva no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Reservas!.Remove(ReservaAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


