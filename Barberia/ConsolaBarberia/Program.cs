using LibreriaBarberia.Implementaciones;
using LibreriaBarberia.Interfaces;

Console.WriteLine("cns_presentacion");
Console.WriteLine("Conexion de Base de datos");

IConexion conexion = new Conexion();
conexion.StringConexion = "server=DESKTOP-BJQKKO0\\SQLEXPRESS;Integrated Security=True;TrustServerCertificate=true;database=db_barberia;";
var lista_sedes = conexion.Sedes!.ToList();

foreach (var item in lista_sedes) // Recorre cada elemento de la lista de sedes obtenida de la base de datos
    Console.WriteLine("Sedes:   " + item.Id + " " + item.Nombre);




Console.WriteLine("Final");