using LibreriaBarberia.Entidades;
using LibreriaBarberia.Interfaces;
using LibreriaBarberia.Nucleo; 
using Microsoft.EntityFrameworkCore; 

namespace LibreriaBarberia.Implementaciones
{
    public class BarberosNegocio : IBarberosNegocio
    {
        private IConexion? iConexion;
        

        // ==========================================
        // C - CREATE (Guardar)
        // ==========================================
        public Barberos Guardar(Barberos entidad)
        {
            //Reglas de Negocio


            if (entidad.Id != 0)
                throw new Exception("Para guardar un barbero debe tener un id igual a cero.");



            // Conexión y guardado
            this.iConexion = new Conexion();
           
            this.iConexion.string_conexion = Nucleo.Configuraciones.obtener("string_conexion");

            bool correoRepetido = this.iConexion.Barberos!.Any(b => b.Correo == entidad.Correo && b.Id != entidad.Id);

            if (correoRepetido)
                throw new Exception("Error: Este correo ya se encuentra registrado en el sistema. Debe ser único.");

            this.iConexion.Barberos!.Add(entidad);

            this.iConexion.SaveChanges();


            return entidad;
        }

        // ==========================================
        // R - READ (Consultar Todos)
        // ==========================================
        public List<Barberos> Consultar()
        {
            // Reglas de Negocio 
            Conexion conexionlocal = new Conexion();

            conexionlocal.string_conexion = Configuraciones.obtener("string_conexion");

            this.iConexion = conexionlocal;
            //Conexión y consulta
            // Va a la base de datos, toma la tabla completa y la convierte en una lista
            return this.iConexion.Barberos!.ToList();
        }

        // ==========================================
        // R - READ (Consultar por Id)
        // ==========================================
        public Barberos ConsultarPorId(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");

            // FirstOrDefault busca el primer Barbero que coincida con ese Id
            var BarberoEncontrado = this.iConexion.Barberos!.FirstOrDefault(c => c.Id == id);

         

            return BarberoEncontrado!;
        }

        // ==========================================
        // U - UPDATE (Modificar)
        // ==========================================
        public Barberos Actualizar(Barberos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("Error: Para modificar un barbero, debes enviar su número de Id.");

            this.iConexion = new Conexion();
            this.iConexion.string_conexion = Configuraciones.obtener("string_conexion");
            // ¡Aquí brilla el EntityEntry! Le avisa a la base de datos que este Barbero fue editado
            bool correoRepetido = this.iConexion.Barberos!.Any(b => b.Correo == entidad.Correo && b.Id != entidad.Id);

            if (correoRepetido)
                throw new Exception("Error: Este correo ya se encuentra registrado en el sistema. Debe ser único.");

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

            // Primero buscamos si el Barbero existe
            var BarberoAEliminar = this.iConexion.Barberos!.FirstOrDefault(c => c.Id == id);

            if (BarberoAEliminar == null)
                throw new Exception("No se puede eliminar: El Barbero no existe.");

            // Si existe, lo borramos usando Remove
            this.iConexion.Barberos!.Remove(BarberoAEliminar);
            this.iConexion.SaveChanges();

            return true; // Le avisamos al Mesero que todo salió perfecto
        }
    }
}
