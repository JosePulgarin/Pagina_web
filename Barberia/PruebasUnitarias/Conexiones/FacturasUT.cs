using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class FacturasUT
    {
        [TestMethod]
        public void ConsultarFacturas() // Debe retornar una lista de Facturas (aunque esté vacía)
        {
            FacturasNegocio negocio = new FacturasNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Facturas>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Factura O Nulo
        {
            FacturasNegocio negocio = new FacturasNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Facturas);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Facturar en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarFacturas() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            FacturasNegocio negocio = new FacturasNegocio();
            Facturas FacturaPasada = new Facturas
            {
                Id = 0,
           
                NumeroFactura = "FAC8-0032",
                MontoSubTotal = 250.00m,
                IVA = 0.19m,
                Total = -200,
                IdReserva = 2,
                IdMetodo = 1


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(FacturaPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("Total"), "El error del total no fue el esperado.");
        }

        [TestMethod]
        public void GuardarFacturasExitoso() // POLICÍA BUENO
        {
            FacturasNegocio negocio = new FacturasNegocio();
            Facturas FacturaValida = new Facturas
            {
                Id = 0,
                NumeroFactura = "FAC8-0032",
                MontoSubTotal = 250.00m,
                IVA = 0.19m,
                Total = 400.00m,
                IdReserva = 2,
                IdMetodo = 1
            };

            var respuesta = negocio.Guardar(FacturaValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Factura válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarFacturas() //Debe Modificar Y Retornar
        {
            FacturasNegocio negocio = new FacturasNegocio();

            // Creamos una Factura VÁLIDA (con fecha de mañana para que no explote tu validación)
            Facturas temporal = new Facturas
            {
                Id = 0,
                NumeroFactura = "FAC8-0032",
                MontoSubTotal = 250.00m,
                IVA = 0.19m,
                Total = 400.00m,
                IdReserva = 2,
                IdMetodo = 1
            };
            Facturas guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.IVA = 0.18m;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(0.18m, respuesta.IVA);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarFacturas() //Debe Retornar True
        {
            FacturasNegocio negocio = new FacturasNegocio();

            // Creamos basura temporal VÁLIDA
            Facturas basura = new Facturas
            {
                Id = 0,
                NumeroFactura = "FAC8-0032",
                MontoSubTotal = 250.00m,
                IVA = 0.19m,
                Total = 400.00m,
                IdReserva = 2,
                IdMetodo = 1
            };
            Facturas guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}