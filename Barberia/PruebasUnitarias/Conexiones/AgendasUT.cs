using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class AgendasUT
    {
        [TestMethod]
        public void ConsultarAgendas() // Debe retornar una lista de agendas (aunque esté vacía)
        {
            AgendasNegocio negocio = new AgendasNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Agendas>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Agenda O Nulo
        {
            AgendasNegocio negocio = new AgendasNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Agendas);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje agendar en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarAgendas() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            AgendasNegocio negocio = new AgendasNegocio();
            Agendas agendaPasada = new Agendas
            {
                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)), // Hace 5 días
                Hora = new TimeOnly(14, 0),
                Estado = "Ocupado",
                IdBarbero = 1,
                  
               
            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(agendaPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarAgendasExitoso() // POLICÍA BUENO
        {
            AgendasNegocio negocio = new AgendasNegocio();
            Agendas agendaValida = new Agendas
            {
                Id = 0,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), // Fecha válida (mañana)
                Hora = new TimeOnly(14, 0),
                Estado = "Ocupado",
                IdBarbero = 1
            };

            var respuesta = negocio.Guardar(agendaValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La agenda válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarAgendas() //Debe Modificar Y Retornar
        {
            AgendasNegocio negocio = new AgendasNegocio();

            // Creamos una agenda VÁLIDA (con fecha de mañana para que no explote tu validación)
            Agendas temporal = new Agendas
            {
                Id = 0,
                IdBarbero = 1,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Hora = new TimeOnly(8, 0)
            };
            Agendas guardada = negocio.Guardar(temporal);

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
        public void EliminarAgendas() //Debe Retornar True
        {
            AgendasNegocio negocio = new AgendasNegocio();

            // Creamos basura temporal VÁLIDA
            Agendas basura = new Agendas
            {
                Id = 0,
                IdBarbero = 1,
                Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                Hora = new TimeOnly(10, 0)
            };
            Agendas guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


        }
}