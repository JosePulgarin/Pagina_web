using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class SedesUT
    {
        [TestMethod]
        public void ConsultarSedes() // Debe retornar una lista de Sedes (aunque esté vacía)
        {
            SedesNegocio negocio = new SedesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Sedes>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Sede O Nulo
        {
            SedesNegocio negocio = new SedesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Sedes);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Seder en el pasado)
        // ==========================================

       
        [TestMethod]
        public void GuardarSedesExitoso() // POLICÍA BUENO
        {
            SedesNegocio negocio = new SedesNegocio();
            Sedes SedeValida = new Sedes
            {
                Id = 0,
              
                Nombre = "Berlin sede",
                Direccion = "calle 90 $34-22",
                Ciudad = "Medellín",
                Correo = "berlin@gmail.com"
            };

            var respuesta = negocio.Guardar(SedeValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Sede válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarSedes() //Debe Modificar Y Retornar
        {
            SedesNegocio negocio = new SedesNegocio();

            // Creamos una Sede VÁLIDA (con fecha de mañana para que no explote tu validación)
            Sedes temporal = new Sedes
            {
                Id = 0,
                Nombre = "Berlin sede",
                Direccion = "calle 90 $34-22",
                Ciudad = "Medellín",
                Correo = "berlin@gmail.com"
            };
            Sedes guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Correo = "ber@gmail.com";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("ber@gmail.com", respuesta.Correo);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarSedes() //Debe Retornar True
        {
            SedesNegocio negocio = new SedesNegocio();

            // Creamos basura temporal VÁLIDA
            Sedes basura = new Sedes
            {
                Id = 0,
                Nombre = "Berlin sede",
                Direccion = "calle 90 $34-22",
                Ciudad = "Medellín",
                Correo = "berlin@gmail.com"
            };
            Sedes guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}