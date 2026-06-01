using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;
using System;
using System.Collections.Generic; // Para usar List<T>

namespace PruebasUnitarias.Conexiones
{
    [TestClass]
    public class CategoriasProductosUT
    {
        [TestMethod]
        public void ConsultarCategoriasProductos() // Debe retornar una lista de CategoriasProductos (aunque esté vacía)
        {
            CategoriasProductosNegocio negocio = new CategoriasProductosNegocio();
            var respuesta = negocio.Consultar();
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta, typeof(List<CategoriasProductos>));
        }

        // ==========================================
        // 2. CONSULTAR POR ID
        // ==========================================

        [TestMethod]
        public void ConsultarPorId() // Debe Retornar CategoriaProducto O Nulo
        {
            CategoriasProductosNegocio negocio = new CategoriasProductosNegocio();
            int idPrueba = 1;
            var respuesta = negocio.ConsultarPorId(idPrueba);
            Assert.IsTrue(respuesta == null || respuesta is CategoriasProductos);
        }

        // ==========================================
        // 3. GUARDAR (Probando que NO deje CategoriaProductor en el pasado)
        // ==========================================

        [TestMethod]
        public void GuardarCategoriasProductosExitoso() // POLICÍA BUENO
        {
            CategoriasProductosNegocio negocio = new CategoriasProductosNegocio();
            CategoriasProductos CategoriaProductoValida = new CategoriasProductos
            {
                Id = 0,
                Nombre = "Cuidado de cabello",
                Descripcion = "Shampoos, pomadas, geles y tónicos para el cabello",
                AplicaImpuesto = false,
                Estado = true
            };

            var respuesta = negocio.Guardar(CategoriaProductoValida);

            Assert.IsNotNull(respuesta);
            Assert.IsTrue(respuesta.Id > 0, "La CategoriaProducto válida no se guardó.");
        }

        // ==========================================
        // 4. ACTUALIZAR (Probando guardado exitoso)
        // ==========================================
        [TestMethod]
        public void ActualizarCategoriasProductos() //Debe Modificar Y Retornar
        {
            CategoriasProductosNegocio negocio = new CategoriasProductosNegocio();

            // Creamos una CategoriaProducto VÁLIDA (con fecha de mañana para que no explote tu validación)
            CategoriasProductos temporal = new CategoriasProductos
            {
                Id = 0,
                Nombre = "Cuidado de cabello",
                Descripcion = "Shampoos, pomadas, geles y tónicos para el cabello",
                AplicaImpuesto = false,
                Estado = true
            };
            CategoriasProductos guardada = negocio.Guardar(temporal);

            // Modificamos a una fecha más lejana
            guardada.AplicaImpuesto = true;
            var respuesta = negocio.Actualizar(guardada);

            Assert.IsNotNull(respuesta);
            Assert.AreEqual(false, respuesta.AplicaImpuesto);
        }

        // ==========================================
        // 5. ELIMINAR
        // ==========================================
        [TestMethod]
        public void EliminarCategoriasProductos() //Debe Retornar True
        {
            CategoriasProductosNegocio negocio = new CategoriasProductosNegocio();

            // Creamos basura temporal VÁLIDA
            CategoriasProductos basura = new CategoriasProductos
            {
                Id = 0,
                Nombre = "Cuidado de cabello",
                Descripcion = "Shampoos, pomadas, geles y tónicos para el cabello",
                AplicaImpuesto = false,
                Estado = true
            };
            CategoriasProductos guardada = negocio.Guardar(basura);

            // Eliminamos
            var respuesta = negocio.Eliminar(guardada.Id);

            Assert.IsTrue(respuesta);
        }


    }
}