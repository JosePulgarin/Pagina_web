using Microsoft.VisualStudio.TestTools.UnitTesting; // Importa el espacio de nombres para las pruebas unitarias
using LibreriaBarberia.Entidades;
using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;

namespace PruebasUnitarias
{
    [TestClass]
    public class RecepcionistasUT
    {
        [TestMethod]
        public void Ejecutar()
        {
            IConexion conexion = new Conexion();
            conexion.StringConexion = "server=DESKTOP-BJQKKO0\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=true;database=db_barberia;";
            var lista = conexion.Recepcionistas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception();



        }
    }
}