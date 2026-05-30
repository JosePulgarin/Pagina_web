using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ServiciosUT
    {
        [TestMethod]
        public void ConsultarServicios() // Debe retornar una lista de Servicios (aunque esté vacía)
        {
            ServiciosNegocio negocio = new ServiciosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Servicios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Servicio O Nulo
        {
            ServiciosNegocio negocio = new ServiciosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Servicios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Servicior en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarServicios() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ServiciosNegocio negocio = new ServiciosNegocio();
            Servicios ServicioPasada = new Servicios
            {
                Id = 0,
                
                Nombre = "Lavado completo",
                Costo = 200.0m,
                Tiempo = -2, // Tiempo negativo para romper validación
                Nota = "Servicio de prueba"

            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ServicioPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarServiciosExitoso() // POLICÍA BUENO
        {
            ServiciosNegocio negocio = new ServiciosNegocio();
            Servicios ServicioValida = new Servicios
            {
                Id = 0,
                Nombre = "Lavado completo",
                Costo = 200.0m,
                Tiempo = 3, 
                Nota = "Servicio de prueba"
            };

            var respuesta = negocio.Guardar(ServicioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Servicio válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarServicios() //Debe Modificar Y Retornar
        {
            ServiciosNegocio negocio = new ServiciosNegocio();

            // Creamos una Servicio VÁLIDA (con fecha de mañana para que no explote tu validación)
            Servicios temporal = new Servicios
            {
                Id = 0,
                Nombre = "Lavado completo",
                Costo = 200.0m,
                Tiempo = 3,
                Nota = "Servicio de prueba"
            };
            Servicios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Tiempo = 5;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(5, respuesta.Tiempo);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarServicios() //Debe Retornar True
        {
            ServiciosNegocio negocio = new ServiciosNegocio();

            // Creamos basura temporal VÁLIDA
            Servicios basura = new Servicios
            {
                Id = 0,
                Nombre = "Lavado completo",
                Costo = 200.0m,
                Tiempo = 3,
                Nota = "Servicio de prueba"
            };
            Servicios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}