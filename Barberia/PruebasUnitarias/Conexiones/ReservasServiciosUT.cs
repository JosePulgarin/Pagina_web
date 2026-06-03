using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ReservasServiciosUT
    {
        [TestMethod]
        public void ConsultarReservasServicios() // Debe retornar una lista de ReservasServicios (aunque esté vacía)
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<ReservasServicios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar ReservaServicio O Nulo
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is ReservasServicios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje ReservaServicior en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarReservasServicios() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();
            ReservasServicios ReservaServicioPasada = new ReservasServicios
            {
                Id = 0,
                Precio = -200.00m, // Precio negativo para romper validación
                Observacion = "Bastante bueno el servicio",
                IdServicio = 2,
                IdReserva = 1


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ReservaServicioPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarReservasServiciosExitoso() // POLICÍA BUENO
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();
            ReservasServicios ReservaServicioValida = new ReservasServicios
            {
                Id = 0,
              
                Precio = 200.00m,
                Observacion = "Bastante bueno el servicio",
                IdServicio = 2,
                IdReserva = 1
            };

            var respuesta = negocio.Guardar(ReservaServicioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La ReservaServicio válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarReservasServicios() //Debe Modificar Y Retornar
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();

            // Creamos una ReservaServicio VÁLIDA (con fecha de mañana para que no explote tu validación)
            ReservasServicios temporal = new ReservasServicios
            {
                Id = 0,
                Precio = 200.00m,
                Observacion = "Bastante bueno el servicio",
                IdServicio = 2,
                IdReserva = 1
            };
            ReservasServicios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Precio = 250.00m;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(250.00m , respuesta.Precio);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarReservasServicios() //Debe Retornar True
        {
            ReservasServiciosNegocio negocio = new ReservasServiciosNegocio();

            // Creamos basura temporal VÁLIDA
            ReservasServicios basura = new ReservasServicios
            {
                Id = 0,
                Precio = 200.00m,
                Observacion = "Bastante bueno el servicio",
                IdServicio = 2,
                IdReserva = 1
            };
            ReservasServicios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}