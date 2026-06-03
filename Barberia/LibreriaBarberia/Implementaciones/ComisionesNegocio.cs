using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class ComisionesNegocio : IComisionesNegocio
    {
        private IConexion? iConexion;
    

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Comisiones Guardar(Comisiones entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Comision nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.EstadoLiquidacion))
                throw new Exception("El estado de liquidación del Comision son absolutamente obligatorios.");

            var fechaDeHoy = DateOnly.FromDateTime(DateTime.Now);


            if (entidad.Fecha < fechaDeHoy)
            {
                throw new Exception("Error de validación: No puedes poner una comisión en el pasado. La fecha debe ser igual o mayor al día de hoy.");
            }

            // 2. Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.Comisiones!.Add(entidad);
            

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Comisiones> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Comisiones!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Comisiones ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Comision que coincida con ese Id
            var ComisionEncontrado = this.iConexion.Comisiones!.FirstOrDefault(c => c.Id == id);

         

            return ComisionEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Comisiones Actualizar(Comisiones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Comision, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            var fechaDeHoy = DateOnly.FromDateTime(DateTime.Now);


            if (entidad.Fecha < fechaDeHoy)
            {
                throw new Exception("Error de validación: No puedes poner una comisión en el pasado. La fecha debe ser igual o mayor al día de hoy.");
            }
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Comision fue editado
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

            // Primero buscamos si el Comision existe
            var ComisionAEliminar = this.iConexion.Comisiones!.FirstOrDefault(c => c.Id == id);

            if (ComisionAEliminar == null)
                throw new Exception("No se puede eliminar: El Comision no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Comisiones!.Remove(ComisionAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
