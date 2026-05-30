using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class PromocionesEspecialesUT
    {
        [TestMethod]
        public void ConsultarPromocionesEspeciales() // Debe retornar una lista de PromocionesEspeciales (aunque esté vacía)
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<PromocionesEspeciales>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar PromocionEspecial O Nulo
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is PromocionesEspeciales);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje PromocionEspecialr en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarPromocionesEspeciales() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();
            PromocionesEspeciales PromocionEspecialPasada = new PromocionesEspeciales
            {
                Id = 0,
               
                Nombre = "Black friday",
                Descuento = "Promo30",
                FechaInicio = new DateOnly(2026, 05, 25),
                FechaFin = new DateOnly(2026, 05, 20) // FechaFin antes de FechaInicio para probar esa validación también


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(PromocionEspecialPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarPromocionesEspecialesExitoso() // POLICÍA BUENO
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();
            PromocionesEspeciales PromocionEspecialValida = new PromocionesEspeciales
            {
                Id = 0,
                Nombre = "Black friday",
                Descuento = "Promo30",
                FechaInicio = new DateOnly(2026, 05, 15),
                FechaFin = new DateOnly(2026, 05, 20)
            };

            var respuesta = negocio.Guardar(PromocionEspecialValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La PromocionEspecial válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarPromocionesEspeciales() //Debe Modificar Y Retornar
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();

            // Creamos una PromocionEspecial VÁLIDA (con fecha de mañana para que no explote tu validación)
            PromocionesEspeciales temporal = new PromocionesEspeciales
            {
                Id = 0,
                Nombre = "Black friday",
                Descuento = "Promo30",
                FechaInicio = new DateOnly(2026, 05, 15),
                FechaFin = new DateOnly(2026, 05, 20)
            };
            PromocionesEspeciales guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaFin = DateOnly.FromDateTime(DateTime.Now.AddDays(15));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(15)), respuesta.FechaFin);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarPromocionesEspeciales() //Debe Retornar True
        {
            PromocionesEspecialesNegocio negocio = new PromocionesEspecialesNegocio();

            // Creamos basura temporal VÁLIDA
            PromocionesEspeciales basura = new PromocionesEspeciales
            {
                Id = 0,
                Nombre = "Black friday",
                Descuento = "Promo30",
                FechaInicio = new DateOnly(2026, 05, 15),
                FechaFin = new DateOnly(2026, 05, 20)
            };
            PromocionesEspeciales guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}