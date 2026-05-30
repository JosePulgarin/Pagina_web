using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo;
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class FacturasNegocio : IFacturasNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Facturas Guardar(Facturas entidad)
        {
            // Reglas de Negocio
            if (entidad.Id != 0)
                throw new Exception("Para guardar un Factura nuevo, el Id debe ser 0. Si envías un Id, significa que ya existe.");

            if (string.IsNullOrWhiteSpace(entidad.NumeroFactura))
                throw new Exception("El Numero de Factura son absolutamente obligatorios.");

            if (entidad.IdMetodo<=0)
                throw new Exception("Ingresa un método de pago.");

            // Conexión y guardado
            this.iConexion = new Conexion();
     
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // Más validaciones

            bool facturaExistente = this.iConexion.Facturas!.Any(f => f.IdReserva == entidad.IdReserva && f.Id != entidad.Id);

            if (facturaExistente)
                throw new Exception("Error financiero: Ya existe una factura generada para esta cita. No puedes cobrar dos veces el mismo servicio.");
          
            if (entidad.Total <= 0)
                throw new Exception("Error lógico: El total de la factura debe ser mayor a cero.");
          



            bool numeroFacturaRepetido = this.iConexion.Facturas!.Any(b => b.NumeroFactura == entidad.NumeroFactura && b.Id != entidad.Id);

            if (numeroFacturaRepetido)
                throw new Exception("Error: Este numeroFactura ya se encuentra registrado en el sistema. Debe ser único.");

            this.iConexion.Facturas!.Add(entidad);
            this.iConexion.SaveChanges();

            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Facturas> Consultar()
        {
            // Reglas de Negocio
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            // Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Facturas!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Facturas ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Factura que coincida con ese Id
            var FacturaEncontrado = this.iConexion.Facturas!.FirstOrDefault(c => c.Id == id);

    

            return FacturaEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un Factura, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Factura fue editado
            bool numeroFacturaRepetido = this.iConexion.Facturas!.Any(b => b.NumeroFactura == entidad.NumeroFactura && b.Id != entidad.Id);

            if (numeroFacturaRepetido)
                throw new Exception("Error: Este numeroFactura ya se encuentra registrado en el sistema. Debe ser único.");


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

            // Primero buscamos si el Factura existe
            var FacturaAEliminar = this.iConexion.Facturas!.FirstOrDefault(c => c.Id == id);

            if (FacturaAEliminar == null)
                throw new Exception("No se puede eliminar: El Factura no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Facturas!.Remove(FacturaAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
