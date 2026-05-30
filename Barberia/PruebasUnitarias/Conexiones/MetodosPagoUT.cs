using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class MetodosPagoUT
    {
        [TestMethod]
        public void ConsultarMetodosPago() // Debe retornar una lista de MetodosPago (aunque esté vacía)
        {
            MetodosPagoNegocio negocio = new MetodosPagoNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<MetodosPago>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar MetodoPago O Nulo
        {
            MetodosPagoNegocio negocio = new MetodosPagoNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is MetodosPago);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje MetodoPagor en el pasado)
        // ==========================================


        [TestMethod]
        public void GuardarMetodosPagoExitoso() // POLICÍA BUENO
        {
            MetodosPagoNegocio negocio = new MetodosPagoNegocio();
            MetodosPago MetodoPagoValida = new MetodosPago
            {
                Id = 0,
              
                TipoMetodo = "Transferencia",
                Banco = "Bancolombia",
                Moneda = "USD"
            };

            var respuesta = negocio.Guardar(MetodoPagoValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La MetodoPago válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarMetodosPago() //Debe Modificar Y Retornar
        {
            MetodosPagoNegocio negocio = new MetodosPagoNegocio();

            // Creamos una MetodoPago VÁLIDA (con fecha de mañana para que no explote tu validación)
            MetodosPago temporal = new MetodosPago
            {
                Id = 0,
                TipoMetodo = "Transferencia",
                Banco = "Bancolombia",
                Moneda = "USD"
            };
            MetodosPago guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.TipoMetodo = "Tarjeta";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("Tarjeta", respuesta.TipoMetodo);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarMetodosPago() //Debe Retornar True
        {
            MetodosPagoNegocio negocio = new MetodosPagoNegocio();

            // Creamos basura temporal VÁLIDA
            MetodosPago basura = new MetodosPago
            {
                Id = 0,
                TipoMetodo = "Transferencia",
                Banco = "Bancolombia",
                Moneda = "USD"
            };
            MetodosPago guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}