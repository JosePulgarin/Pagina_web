using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ClientesUT
    {
        [TestMethod]
        public void ConsultarClientes() // Debe retornar una lista de Clientes (aunque esté vacía)
        {
            ClientesNegocio negocio = new ClientesNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Clientes>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Cliente O Nulo
        {
            ClientesNegocio negocio = new ClientesNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Clientes);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Clienter en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarClientes() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ClientesNegocio negocio = new ClientesNegocio();
            Clientes ClientePasada = new Clientes
            {
                Id = 0,
                Nombre = "Sarah",
                Telefono = "303030",
                Correo = "juanp@mail.com",
                IdUsuario = 2,
                IdSede = 3,
                IdMembresia = 2

            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ClientePasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarClientesExitoso() // POLICÍA BUENO
        {
            ClientesNegocio negocio = new ClientesNegocio();
            Clientes ClienteValida = new Clientes
            {
                Id = 0,
                Nombre = "Yeisson",
                Telefono = "33033030122",
                Correo = "yei@mail.com",
                IdUsuario = 2,
                IdSede = 3,
                IdMembresia = 2
            };

            var respuesta = negocio.Guardar(ClienteValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Cliente válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarClientes() //Debe Modificar Y Retornar
        {
            ClientesNegocio negocio = new ClientesNegocio();

            // Creamos una Cliente VÁLIDA (con fecha de mañana para que no explote tu validación)
            Clientes temporal = new Clientes
            {
                Id = 0,
                Nombre = "Yeisson",
                Telefono = "33033030122",
                Correo = "yei@mail.com",
                IdUsuario = 2,
                IdSede = 3,
                IdMembresia = 2
            };
            Clientes guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.Telefono = "202020202";
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual("202020202", respuesta.Telefono);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarClientes() //Debe Retornar True
        {
            ClientesNegocio negocio = new ClientesNegocio();

            // Creamos basura temporal VÁLIDA
            Clientes basura = new Clientes
            {
                Id = 0,
                Nombre = "Yeisson",
                Telefono = "33033030122",
                Correo = "yei@mail.com",
                IdUsuario = 2,
                IdSede = 3,
                IdMembresia = 2
            };
            Clientes guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}