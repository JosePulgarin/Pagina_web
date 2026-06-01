using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace LibreriaBarberia.Implementaciones
{
    public class GastosOperativosNegocio : IGastosOperativosNegocio
    {
        private IConexion? iConexion;



        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public GastosOperativos Guardar(GastosOperativos entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un GastoOperativo nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.NumeroComprobante))
                throw new Exception("El Número de comprobante del GastoOperativo son absolutamente obligatorios.");

            // Conexión y guardado
            this.iConexion = new Conexion();

            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            bool NumeroComprobanteRepetido = this.iConexion.GastosOperativos!.Any(b => b.NumeroComprobante == entidad.NumeroComprobante && b.Id != entidad.Id);

            if (NumeroComprobanteRepetido)
                throw new Exception("Error: Este NumeroComprobante ya se encuentra registrado en el sistema. Debe ser único.");


            this.iConexion.GastosOperativos!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<GastosOperativos> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.GastosOperativos!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public GastosOperativos ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer GastoOperativo que coincida con ese Id
            var GastoOperativoEncontrado = this.iConexion.GastosOperativos!.FirstOrDefault(c => c.Id == id);

 

            return GastoOperativoEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public GastosOperativos Actualizar(GastosOperativos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un GastoOperativo, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este GastoOperativo fue editado


            bool NumeroComprobanteRepetido = this.iConexion.GastosOperativos!.Any(b => b.NumeroComprobante == entidad.NumeroComprobante && b.Id != entidad.Id);

            if (NumeroComprobanteRepetido)
                throw new Exception("Error: Este NumeroComprobante ya se encuentra registrado en el sistema. Debe ser único.");

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

            // Primero buscamos si el GastoOperativo existe
            var GastoOperativoAEliminar = this.iConexion.GastosOperativos!.FirstOrDefault(c => c.Id == id);

            if (GastoOperativoAEliminar == null)
                throw new Exception("No se puede eliminar: El GastoOperativo no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.GastosOperativos!.Remove(GastoOperativoAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
