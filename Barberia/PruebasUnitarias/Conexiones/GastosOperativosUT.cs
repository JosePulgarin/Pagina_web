using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class GastosOperativosUT
    {
        [TestMethod]
        public void ConsultarGastosOperativos() // Debe retornar una lista de GastosOperativos (aunque esté vacía)
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<GastosOperativos>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar GastoOperativo O Nulo
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is GastosOperativos);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje GastoOperativor en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarGastosOperativos() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();
            GastosOperativos GastoOperativoPasada = new GastosOperativos
            {
                Id = 0,
               
                Categoria = "Internet",
                Monto = 330.00m,
                FechaPago = new DateOnly(2025, 04, 03),
                NumeroComprobante = "PUB-050", // Repetido
                IdSede = 2


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(GastoOperativoPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarGastosOperativosExitoso() // POLICÍA BUENO
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();
            GastosOperativos GastoOperativoValida = new GastosOperativos
            {
                Id = 0,
                Categoria = "Internet",
                Monto = 330.00m,
                FechaPago = new DateOnly(2025, 04, 03),
                NumeroComprobante = "PUB-030", // Repetido
                IdSede = 2
            };

            var respuesta = negocio.Guardar(GastoOperativoValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La GastoOperativo válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarGastosOperativos() //Debe Modificar Y Retornar
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();

            // Creamos una GastoOperativo VÁLIDA (con fecha de mañana para que no explote tu validación)
            GastosOperativos temporal = new GastosOperativos
            {
                Id = 0,
                Categoria = "Internet",
                Monto = 330.00m,
                FechaPago = new DateOnly(2025, 04, 03),
                NumeroComprobante = "PUB-030", // Repetido
                IdSede = 2
            };
            GastosOperativos guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaPago = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(2)), respuesta.FechaPago);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarGastosOperativos() //Debe Retornar True
        {
            GastosOperativosNegocio negocio = new GastosOperativosNegocio();

            // Creamos basura temporal VÁLIDA
            GastosOperativos basura = new GastosOperativos
            {
                Id = 0,
                Categoria = "Internet",
                Monto = 330.00m,
                FechaPago = new DateOnly(2025, 04, 03),
                NumeroComprobante = "PUB-030", // Repetido
                IdSede = 2
            };
            GastosOperativos guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}