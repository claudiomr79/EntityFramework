using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            int seguir = 0;
            string entrada;
            int id;
            bool ok;
            
            do
            {
                usuarios us = new usuarios();
               
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("1º) Consulta\n2º) Alta\n3º) Baja\n4º) Modificación\n5º) Consulta con filtro\n");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Consulta..");
                        Console.Write("ID:");
                        entrada = Console.ReadLine();
                        id = int.Parse(entrada);
                        try
                        {
                            us = Consulta(id);
                            Console.WriteLine("Nombre: " + us.nombre + "\nApellido: " + us.apellido + "\nDNI: " + us.nro_doc);
                        }
                        catch (Exception ex)
                        {
                            if (us == null)
                                Console.WriteLine("ID inexistente");
                        }
                        break;
                    case "2":
                        Console.WriteLine("Alta..");
                        Console.Write("Nombre: ");
                        us.nombre = Console.ReadLine();
                        Console.Write("Apellido: ");
                        us.apellido = Console.ReadLine();
                        Console.Write("Nro DNI: ");
                        us.nro_doc = int.Parse(Console.ReadLine());
                        try
                        {
                            Insertar(us);
                            MostrarListado();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;


                    case "3":
                        Console.WriteLine("Baja..");
                        Console.Write("ID:");
                        entrada = Console.ReadLine();
                        id = int.Parse(entrada);
                        try
                        {
                            Delete(id);
                            MostrarListado();
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("Id inexistente");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Modificación..");
                        Console.Write("ID:");
                        entrada = Console.ReadLine();
                        id = int.Parse(entrada);
                        try
                        {
                            Console.Write("Nombre: ");
                            us.nombre = Console.ReadLine();
                            Console.Write("Apellido: ");
                            us.apellido = Console.ReadLine();
                            Console.Write("Nro DNI: ");
                            us.nro_doc = int.Parse(Console.ReadLine());
                            Modificar(id, us);
                            MostrarListado();
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("NO EXISTE ESE ID");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Consulta con filtro...");
                        Console.Write("Ingrese una silaba a buscar entre los nombres: ");
                        entrada = Console.ReadLine();
                        ConsultaConFiltro(entrada);
                        break;
                    default:
                        Console.Write("Opcion inexistente..");

                        break;
                }
                
                Console.WriteLine("\n\n\nDESEA SEGUIR 1-SI");
                do
                {
                    ok = int.TryParse(Console.ReadLine(), out seguir);
                } while (!ok);
                    
            } while (seguir == 1);
          
        }
        public static void MostrarListado()
        {
            using (var db = new academiaEntities())
            {
                Console.WriteLine("\n\n");
                foreach (var oUsuarios in db.usuarios)
                {
                    Console.WriteLine(oUsuarios.id + " " + oUsuarios.nombre + " " + oUsuarios.apellido + " "
                        + oUsuarios.nro_doc);
                }
            }
        }
        public static usuarios Consulta(int id)
        {
            using (academiaEntities db = new academiaEntities())
            {
                
                    var lst = db.usuarios;
                    usuarios res = lst.Find(id);
                    return res;
                           
            }
            
        }
        public static void ConsultaConFiltro(string filtro)
        {
            using (academiaEntities db = new academiaEntities())
            {

                foreach(var usuario in db.usuarios.Where(u=>u.nombre.Contains(filtro)))
                {
                    Console.WriteLine(usuario.nombre);
                }
            }

        }
        public static void Delete(int id)
        {
            using(academiaEntities db= new academiaEntities())
            {
                var obj = db.usuarios;
                usuarios res = obj.Find(id);
                try
                {
                    db.usuarios.Remove(res);
                    db.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Usuario removido");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception)
                {

                    throw new NullReferenceException();
                }
               
                
            }
        }
        public static void Insertar(usuarios us)
        {
            using (academiaEntities db = new academiaEntities())
            {
                db.usuarios.Add(us);
                db.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Alta realizada");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        public static void Modificar (int id, usuarios us)
        {
            using (academiaEntities db = new academiaEntities())
            {
                var obj = db.usuarios;
                try
                {
                    usuarios res = obj.Find(id);
                    res.apellido = us.apellido;
                    res.nombre = us.nombre;
                    res.nro_doc = us.nro_doc;
                }
                catch (Exception )
                {

                    throw new NullReferenceException();
                }
                
               
                db.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Modificación realizada");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
