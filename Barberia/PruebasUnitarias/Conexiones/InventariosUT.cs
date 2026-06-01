using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class InventariosUT
    {
        [TestMethod]
        public void ConsultarInventarios() // Debe retornar una lista de Inventarios (aunque esté vacía)
        {
            InventariosNegocio negocio = new InventariosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Inventarios>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Inventario O Nulo
        {
            InventariosNegocio negocio = new InventariosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Inventarios);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Inventarior en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarInventarios() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            InventariosNegocio negocio = new InventariosNegocio();
            Inventarios InventarioPasada = new Inventarios
            {
                Id = 0,
              
              
                Nombre = "Cuchilla master",
                Descripcion = "Pa quitar barba",
                CantidadActual = -20, // Cantidad negativa para romper validación
                FechaAbastecimiento = new DateOnly(2020, 1, 1),
                IdSede = 3


            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(InventarioPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarInventariosExitoso() // POLICÍA BUENO
        {
            InventariosNegocio negocio = new InventariosNegocio();
            Inventarios InventarioValida = new Inventarios
            {
                Id = 0,
                Nombre = "Cuchilla master",
                Descripcion = "Pa quitar barba",
                CantidadActual = 20, 
                FechaAbastecimiento = new DateOnly(2020, 1, 1),
                IdSede = 3
            };

            var respuesta = negocio.Guardar(InventarioValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Inventario válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarInventarios() //Debe Modificar Y Retornar
        {
            InventariosNegocio negocio = new InventariosNegocio();

            // Creamos una Inventario VÁLIDA (con fecha de mañana para que no explote tu validación)
            Inventarios temporal = new Inventarios
            {
                Id = 0,
                Nombre = "Cuchilla master",
                Descripcion = "Pa quitar barba",
                CantidadActual = 20, // Cantidad negativa para romper validación
                FechaAbastecimiento = new DateOnly(2020, 1, 1),
                IdSede = 3
            };
            Inventarios guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.FechaAbastecimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(10));
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(10)), respuesta.FechaAbastecimiento);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarInventarios() //Debe Retornar True
        {
            InventariosNegocio negocio = new InventariosNegocio();

            // Creamos basura temporal VÁLIDA
            Inventarios basura = new Inventarios
            {
                Id = 0,
                Nombre = "Cuchilla master",
                Descripcion = "Pa quitar barba",
                CantidadActual = 20, // Cantidad negativa para romper validación
                FechaAbastecimiento = new DateOnly(2020, 1, 1),
                IdSede = 3
            };
            Inventarios guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}