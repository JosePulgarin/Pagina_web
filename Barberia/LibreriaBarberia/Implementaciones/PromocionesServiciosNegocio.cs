using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class PromocionesServiciosNegocio : IPromocionesServiciosNegocio
    {
        private IConexion? iConexion;

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public PromocionesServicios Guardar(PromocionesServicios entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un PromocionServicio nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion.PromocionesServicios!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<PromocionesServicios> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            //Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.PromocionesServicios!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public PromocionesServicios ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer PromocionServicio que coincida con ese Id
            var PromocionServicioEncontrado = this.iConexion.PromocionesServicios!.FirstOrDefault(c => c.Id == id);

            return PromocionServicioEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public PromocionesServicios Actualizar(PromocionesServicios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un PromocionServicio, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este PromocionServicio fue editado
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

            // Primero buscamos si el PromocionServicio existe
            var PromocionServicioAEliminar = this.iConexion.PromocionesServicios!.FirstOrDefault(c => c.Id == id);

            if (PromocionServicioAEliminar == null)
                throw new Exception("No se puede eliminar: El PromocionServicio no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.PromocionesServicios!.Remove(PromocionServicioAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}


