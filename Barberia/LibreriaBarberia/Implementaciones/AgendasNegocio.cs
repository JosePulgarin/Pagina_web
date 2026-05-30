using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class AgendasNegocio : IAgendasNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Agendas Guardar(Agendas entidad)
        {
            //Reglas de Negocio


            if (entidad.Id != 0)
                throw new Exception("Para guardar agenda debe tener un id igual a cero.");

            var fechaDeHoy = DateOnly.FromDateTime(DateTime.Now);

            
            if (entidad.Fecha < fechaDeHoy)
            {
                throw new Exception("Error de validación: No puedes agendar citas en el pasado. La fecha debe ser igual o mayor al día de hoy.");
            }

            //Conexión y guardado
            this.iConexion = new Conexion();
      
            this.iConexion.string_conexion = Nucleo.Configuraciones.obtener("string_conexion");

            bool agendaOcupada = this.iConexion.Agendas!.Any(a =>
            a.Fecha == entidad.Fecha &&
            a.Hora == entidad.Hora &&
            a.IdBarbero == entidad.IdBarbero &&
            a.Id != entidad.Id);

            if (agendaOcupada)
                throw new Exception("Ocupado: El barbero ya tiene una cita asignada en esa fecha y hora exacta.");


            this.iConexion.Agendas!.Add(entidad);

            this.iConexion.SaveChanges();


            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Agendas> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Agendas!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Agendas ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Agenda que coincida con ese Id
            var AgendaEncontrado = this.iConexion.Agendas!.FirstOrDefault(c => c.Id == id);

       

            return AgendaEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Agendas Actualizar(Agendas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Agenda, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            bool agendaOcupada = this.iConexion.Agendas!.Any(a =>
            a.Fecha == entidad.Fecha &&
            a.Hora == entidad.Hora &&
            a.IdBarbero == entidad.IdBarbero &&
            a.Id != entidad.Id);

            if (agendaOcupada)
                throw new Exception("Ocupado: El barbero ya tiene una cita asignada en esa fecha y hora exacta.");


            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Agenda fue editado
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

            // Primero buscamos si el Agenda existe
            var AgendaAEliminar = this.iConexion.Agendas!.FirstOrDefault(c => c.Id == id);

            if (AgendaAEliminar == null)
                throw new Exception("No se puede eliminar: El Agenda no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Agendas!.Remove(AgendaAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
