using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ReseñasClientesUT
    {
        [TestMethod]
        public void ConsultarReseñasClientes() // Debe retornar una lista de ReseñasClientes (aunque esté vacía)
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<ReseñasClientes>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar ReseñaCliente O Nulo
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is ReseñasClientes);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje ReseñaClienter en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarReseñasClientes() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();
            ReseñasClientes ReseñaClientePasada = new ReseñasClientes
            {
                Id = 0,
                
                Puntuacion = 6, // Rompe la validación
                Comentario = "Estupendo!",
                FechaPublicacion = new DateOnly(2022, 03, 03),
                Etiquetas ="Hermoso!",
                IdReserva = 1


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ReseñaClientePasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarReseñasClientesExitoso() // POLICÍA BUENO
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();
            ReseñasClientes ReseñaClienteValida = new ReseñasClientes
            {
                Id = 0,
                Puntuacion = 5, 
                Comentario = "Estupendo!",
                FechaPublicacion = new DateOnly(2022, 03, 03),
                Etiquetas = "Hermoso!",
                IdReserva = 1
            };

            var respuesta = negocio.Guardar(ReseñaClienteValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La ReseñaCliente válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarReseñasClientes() //Debe Modificar Y Retornar
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();

            // Creamos una ReseñaCliente VÁLIDA (con fecha de mañana para que no explote tu validación)
            ReseñasClientes temporal = new ReseñasClientes
            {
                Id = 0,
                Puntuacion = 5,
                Comentario = "Estupendo!",
                FechaPublicacion = new DateOnly(2022, 03, 03),
                Etiquetas = "Hermoso!",
                IdReserva = 1
            };
            ReseñasClientes guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaPublicacion = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(5)), respuesta.FechaPublicacion);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarReseñasClientes() //Debe Retornar True
        {
            ReseñasClientesNegocio negocio = new ReseñasClientesNegocio();

            // Creamos basura temporal VÁLIDA
            ReseñasClientes basura = new ReseñasClientes
            {
                Id = 0,
                Puntuacion = 5,
                Comentario = "Estupendo!",
                FechaPublicacion = new DateOnly(2022, 03, 03),
                Etiquetas = "Hermoso!",
                IdReserva = 1
            };
            ReseñasClientes guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}