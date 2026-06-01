using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ComisionesUT
    {
        [TestMethod]
        public void ConsultarComisiones() // Debe retornar una lista de Comisiones (aunque esté vacía)
        {
            ComisionesNegocio negocio = new ComisionesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Comisiones>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Comision O Nulo
        {
            ComisionesNegocio negocio = new ComisionesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Comisiones);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Comisionr en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarComisiones() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ComisionesNegocio negocio = new ComisionesNegocio();
            Comisiones ComisionPasada = new Comisiones
            {
                Id = 0, 
                IdBarbero = 1,
                PorcentajeAplicado = 0.50m,
                Monto = 122.00m,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), //Fecha del pasado
                EstadoLiquidacion = "Pendiente",
                IdFactura = 2


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ComisionPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarComisionesExitoso() // POLICÍA BUENO
        {
            ComisionesNegocio negocio = new ComisionesNegocio();
            Comisiones ComisionValida = new Comisiones
            {
                Id = 0,
                IdBarbero = 1,
                PorcentajeAplicado = 0.50m,
                Monto = 122.00m,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5)), 
                EstadoLiquidacion = "Liquidado",
                IdFactura = 2
            };

            var respuesta = negocio.Guardar(ComisionValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Comision válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarComisiones() //Debe Modificar Y Retornar
        {
            ComisionesNegocio negocio = new ComisionesNegocio();

            // Creamos una Comision VÁLIDA (con fecha de mañana para que no explote tu validación)
            Comisiones temporal = new Comisiones
            {
                Id = 0,
                IdBarbero = 1,
                PorcentajeAplicado = 0.50m,
                Monto = 122.00m,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                EstadoLiquidacion = "Liquidado",
                IdFactura = 2
            };
            Comisiones guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(5)), respuesta.Fecha);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarComisiones() //Debe Retornar True
        {
            ComisionesNegocio negocio = new ComisionesNegocio();

            // Creamos basura temporal VÁLIDA
            Comisiones basura = new Comisiones
            {
                Id = 0,
                IdBarbero = 1,
                PorcentajeAplicado = 0.50m,
                Monto = 122.00m,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                EstadoLiquidacion = "Liquidado",
                IdFactura = 2
            };
            Comisiones guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}