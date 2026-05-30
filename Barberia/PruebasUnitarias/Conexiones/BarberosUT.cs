using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class BarberosUT
    {
        [TestMethod]
        public void ConsultarBarberos() // Debe retornar una lista de Barberos (aunque esté vacía)
        {
            BarberosNegocio negocio = new BarberosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Barberos>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Barbero O Nulo
        {
            BarberosNegocio negocio = new BarberosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Barberos);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Barberor en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarBarberos() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            BarberosNegocio negocio = new BarberosNegocio();
            Barberos BarberoPasada = new Barberos
            {
                Id = 0,
                Nombre = "Pedrin",
                Correo = "pedrin@gmail.com",
                FechaNacimiento = new DateOnly(2024, 04, 02), // menos de 18 años
                Especialidad = "Bigotes",
                Biografia = "10 años de experiencia",
                IdUsuario = 2,
                IdSede = 1


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(BarberoPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarBarberosExitoso() // POLICÍA BUENO
        {
            BarberosNegocio negocio = new BarberosNegocio();
            Barberos BarberoValida = new Barberos
            {
                Id = 0,
                Nombre = "Samuel",
                Correo = "Samuel@gmail.com",
                FechaNacimiento = new DateOnly(1998, 03, 02), // menos de 18 años
                Especialidad = "Trenzas",
                Biografia = "2 años de experiencia",
                IdUsuario = 1,
                IdSede = 2
            };

            var respuesta = negocio.Guardar(BarberoValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Barbero válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarBarberos() //Debe Modificar Y Retornar
        {
            BarberosNegocio negocio = new BarberosNegocio();

            // Creamos una Barbero VÁLIDA (con fecha de mañana para que no explote tu validación)
            Barberos temporal = new Barberos
            {
                Id = 0,
                Nombre = "Samuel",
                Correo = "Samuel@gmail.com",
                FechaNacimiento = new DateOnly(1998, 03, 02), // menos de 18 años
                Especialidad = "Trenzas",
                Biografia = "2 años de experiencia",
                IdUsuario = 1,
                IdSede = 2
            };
            Barberos guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(5)), respuesta.FechaNacimiento);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarBarberos() //Debe Retornar True
        {
            BarberosNegocio negocio = new BarberosNegocio();

            // Creamos basura temporal VÁLIDA
            Barberos basura = new Barberos
            {
                Id = 0,
                Nombre = "Samuel",
                Correo = "Samuel@gmail.com",
                FechaNacimiento = new DateOnly(1998, 03, 02), // menos de 18 años
                Especialidad = "Trenzas",
                Biografia = "2 años de experiencia",
                IdUsuario = 1,
                IdSede = 2
            };
            Barberos guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}