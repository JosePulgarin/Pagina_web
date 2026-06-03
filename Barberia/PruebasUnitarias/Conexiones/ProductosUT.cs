using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class ProductosUT
    {
        [TestMethod]
        public void ConsultarProductos() // Debe retornar una lista de Productos (aunque esté vacía)
        {
            ProductosNegocio negocio = new ProductosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<Productos>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar Producto O Nulo
        {
            ProductosNegocio negocio = new ProductosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is Productos);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje Productor en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarProductos() // Este rompe a proposito las validaciones (POLICIA MALO)
        {
            ProductosNegocio negocio = new ProductosNegocio();
            Productos ProductoPasada = new Productos
            {
                Id = 0,
                
                MarcaProducto = "Suavitel",
                NombreArticulo = "Shampoo",
                Precio = -200.0m, // Precio negativo para romper validación 
                StockActual = 20,
                IdInventario = 2,
                IdProveedor = 1,
                IdCategoria = 3

            };

            var excepcion = Assert.ThrowsException<Exception>(() => negocio.Guardar(ProductoPasada));
            // Validamos que el mensaje contenga "pasado" (como lo escribiste en tu Exception)
            Assert.IsTrue(excepcion.Message.Contains("pasado"), "El error de fecha no fue el esperado.");
        }

        [TestMethod]
        public void GuardarProductosExitoso() // POLICÍA BUENO
        {
            ProductosNegocio negocio = new ProductosNegocio();
            Productos ProductoValida = new Productos
            {
                Id = 0,
                MarcaProducto = "Suavitel",
                NombreArticulo = "Shampoo",
                Precio = 200.0m, 
                StockActual = 20,
                IdInventario = 2,
                IdProveedor = 1,
                IdCategoria = 3
            };

            var respuesta = negocio.Guardar(ProductoValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La Producto válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarProductos() //Debe Modificar Y Retornar
        {
            ProductosNegocio negocio = new ProductosNegocio();

            // Creamos una Producto VÁLIDA (con fecha de mañana para que no explote tu validación)
            Productos temporal = new Productos
            {
                Id = 0,
                MarcaProducto = "Suavitel",
                NombreArticulo = "Shampoo",
                Precio = 200.0m,
                StockActual = 20,
                IdInventario = 2,
                IdProveedor = 1,
                IdCategoria = 3
            };
            Productos guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.StockActual = 30;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(30, respuesta.StockActual);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarProductos() //Debe Retornar True
        {
            ProductosNegocio negocio = new ProductosNegocio();

            // Creamos basura temporal VÁLIDA
            Productos basura = new Productos
            {
                Id = 0,
                MarcaProducto = "Suavitel",
                NombreArticulo = "Shampoo",
                Precio = 200.0m,
                StockActual = 20,
                IdInventario = 2,
                IdProveedor = 1,
                IdCategoria = 3
            };
            Productos guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}