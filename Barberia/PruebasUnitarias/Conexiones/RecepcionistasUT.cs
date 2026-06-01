using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class RecepcionistasUT
    {
        [TestMethod]
        public void ConsultarRecepcionistas() // Debe retornar una lista de Recepcionistas (aunque esté vacía)
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Recepcionistas>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Recepcionista O Nulo
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Recepcionistas);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Recepcionistar en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarRecepcionistas() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();
            Recepcionistas RecepcionistaPasada = new Recepcionistas
            {
                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), // Hace 5 días
                Hora = new TimeOnly(14, 0),
                Estado = "Ocupado",
                IdBarbero = 1,


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(RecepcionistaPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarRecepcionistasExitoso() // POLICÍA BUENO
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();
            Recepcionistas RecepcionistaValida = new Recepcionistas
            {
                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), // Fecha válida (mañana)
                Hora = new TimeOnly(14, 0),
                Estado = "Ocupado",
                IdBarbero = 1
            };

            var respuesta = negocio.Guardar(RecepcionistaValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Recepcionista válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarRecepcionistas() //Debe Modificar Y Retornar
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();

            // Creamos una Recepcionista VÁLIDA (con fecha de mañana para que no explote tu validación)
            Recepcionistas temporal = new Recepcionistas
            {
                Id = 0,
                IdBarbero = 1,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Hora = new TimeOnly(8, 0)
            };
            Recepcionistas guardada = negocio.Guardar(temporal);

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
        public void EliminarRecepcionistas() //Debe Retornar True
        {
            RecepcionistasNegocio negocio = new RecepcionistasNegocio();

            // Creamos basura temporal VÁLIDA
            Recepcionistas basura = new Recepcionistas
            {
                Id = 0,
                IdBarbero = 1,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Hora = new TimeOnly(10, 0)
            };
            Recepcionistas guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}