using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class PromocionesEspecialesNegocio : IPromocionesEspecialesNegocio
    {
        private IConexion? iConexion;
     

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public PromocionesEspeciales Guardar(PromocionesEspeciales entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un PromocionEspecial nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("El Nombre del PromocionEspecial son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            if (entidad.FechaFin < entidad.FechaInicio)
                throw new Exception("Error lógico: La fecha final de la promoción no puede ser anterior a la fecha de inicio.");
           
            var fechaDeHoy = DateOnly.FromDateTime(DateTime.Now);

            if (entidad.FechaFin < fechaDeHoy)
                throw new Exception("Error: No puedes registrar una promoción cuya fecha final ya pasó.");
      
            this.iConexion.PromocionesEspeciales!.Add(entidad);

            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<PromocionesEspeciales> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.PromocionesEspeciales!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public PromocionesEspeciales ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer PromocionEspecial que coincida con ese Id
            var PromocionEspecialEncontrado = this.iConexion.PromocionesEspeciales!.FirstOrDefault(c => c.Id == id);

            return PromocionEspecialEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public PromocionesEspeciales Actualizar(PromocionesEspeciales entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un PromocionEspecial, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este PromocionEspecial fue editado
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

            // Primero buscamos si el PromocionEspecial existe
            var PromocionEspecialAEliminar = this.iConexion.PromocionesEspeciales!.FirstOrDefault(c => c.Id == id);

            if (PromocionEspecialAEliminar == null)
                throw new Exception("No se puede eliminar: El PromocionEspecial no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.PromocionesEspeciales!.Remove(PromocionEspecialAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


