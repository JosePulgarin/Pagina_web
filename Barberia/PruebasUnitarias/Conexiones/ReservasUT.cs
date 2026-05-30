using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ReservasUT
    {
        [TestMethod]
        public void ConsultarReservas() // Debe retornar una lista de Reservas (aunque esté vacía)
        {
            ReservasNegocio negocio = new ReservasNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Reservas>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Reserva O Nulo
        {
            ReservasNegocio negocio = new ReservasNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Reservas);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Reservar en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarReservas() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ReservasNegocio negocio = new ReservasNegocio();
            Reservas ReservaPasada = new Reservas
            {
                Id = 0,
              
                Recordatorio = "SMS",
                Fecha = new DateOnly(2027, 03, 12), // Esta en el futuro
                Estado = "Confirmada",
                Notas = "Ninguna",
                IdAgenda = 6,
                IdCliente = 2

            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ReservaPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarReservasExitoso() // POLICÍA BUENO
        {
            ReservasNegocio negocio = new ReservasNegocio();
            Reservas ReservaValida = new Reservas
            {
                Id = 0,
                Recordatorio = "SMS",
                Fecha = new DateOnly(2024, 03, 12), 
                Estado = "Confirmada",
                Notas = "Ninguna",
                IdAgenda = 6,
                IdCliente = 2
            };

            var respuesta = negocio.Guardar(ReservaValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Reserva válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarReservas() //Debe Modificar Y Retornar
        {
            ReservasNegocio negocio = new ReservasNegocio();

            // Creamos una Reserva VÁLIDA (con fecha de mañana para que no explote tu validación)
            Reservas temporal = new Reservas
            {
                Id = 0,
                Recordatorio = "SMS",
                Fecha = new DateOnly(2024, 03, 12),
                Estado = "Confirmada",
                Notas = "Ninguna",
                IdAgenda = 6,
                IdCliente = 2
            };
            Reservas guardada = negocio.Guardar(temporal);

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
        public void EliminarReservas() //Debe Retornar True
        {
            ReservasNegocio negocio = new ReservasNegocio();

            // Creamos basura temporal VÁLIDA
            Reservas basura = new Reservas
            {
                Id = 0,
                Recordatorio = "SMS",
                Fecha = new DateOnly(2024, 03, 12),
                Estado = "Confirmada",
                Notas = "Ninguna",
                IdAgenda = 6,
                IdCliente = 2
            };
            Reservas guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}