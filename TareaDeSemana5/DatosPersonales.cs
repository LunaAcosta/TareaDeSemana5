using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// SMIS083121- LUNA ACOSTA, KEVIN ALEXANDER 
// SMIS031021- GUEVARA GARCIA, ITALO ANTONIO
// SMIS093020- JIMENEZ LOPEZ, MELVIN ALEXIS

namespace TareaDeSemana5
{
    class DatosPersonales
    {
        static void Main()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = menu();
            }
            Console.ReadKey();


        }

        private static bool menu()
        {

            Console.WriteLine("BIENVENID@S A CLINICA LA ESPERANZA. ");
            Console.WriteLine();
            Console.WriteLine("SELECIONE LA OPERACION A REALIZAR: ");
            Console.WriteLine();
            Console.WriteLine("1. DESEA RESERVAR  CITA MEDICA: ");
            Console.WriteLine("2. DESEA MODIFICAR SUS DATOS: ");
            Console.WriteLine("3. DESEA CANCELAR SU CITA : ");
            Console.WriteLine("4. MOSTRAR LA CANTIDAD DE CITAS RESERVADAS : ");
            Console.WriteLine("5. SALIR: ");
            Console.Write("\nDIGITE UNA OPCION: ");

            switch(Console.ReadLine())
            {

                case "1":
                    register();
                    return true;
                case "2":
                    updateData();
                    Console.ReadKey();
                    return true;
                case "3":
                    deleteData();
                    Console.ReadKey();
                    return true;
                case "4":
                    Console.WriteLine("LISTA DE CITAS RESEVADAS: ");
                    foreach(KeyValuePair<object,object>data in readFile())
                    {
                        Console.WriteLine("{0}:{1}",data.Key, data.Value);

                    }
                    Console.ReadKey();


                    return true;
                case "5":
                    return false;
                default:
                    return false;
            }
        }

        private static string getPath()
        {
            string path = @"C:\DatosPersonales\RegistroDeUsuario.txt";
            return path;

        }

        private static void register()
        {
            Console.WriteLine();
            Console.WriteLine("INTRODUSCA SUS DATOS: ");

            Console.Write("SU NOMBRE COMPLETO: ");
            string fullname = Console.ReadLine();

            Console.Write("EDAD: ");
            int edad = Convert.ToInt32(Console.ReadLine());

          


            using (StreamWriter sw = File.AppendText(getPath()))
            {

                sw.WriteLine("{0}; {1}", fullname, edad);
         
               sw.Close();
            }
        }

        private static Dictionary<object,object> readFile()
        {
            Dictionary<object, object> LisData = new Dictionary<object, object>();

            using(var reader = new StreamReader(getPath()))
            {
                string lines;
                while ((lines= reader.ReadLine()) != null)
                {
                    string[] KeyValue = lines.Split(';');
                    if (KeyValue.Length == 2)
                    {
                        LisData.Add(KeyValue[0], KeyValue[1]);
                    
                    }
                }
            }

            return LisData;

        }

        private static bool search(string name)
        {
            if (!readFile().ContainsKey(name))
            {
                return false;

            }
            return true;
        }

        private static void updateData()
        {
            Console.Write("INGRESE EL NOMBRE DEL CLIENTE A MODIFICAR: ");

            var name = Console.ReadLine();
            if(search(name))
            {
                Console.WriteLine("EL NOMBRE DEL CLIENTE YA EXISTE: ");
                Console.Write("INGRESE SU NUEVA EDAD: ");
                var newage = Console.ReadLine();

                Dictionary<object, object> temp = new Dictionary<object, object>();
                temp = readFile();

                temp[name] = newage;
                Console.WriteLine();
                Console.WriteLine("¡ EL REGISTRO SE A CONFIGURADO EXITOSAMENTE !");
                File.Delete(getPath());


                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach(KeyValuePair<object,object> values in temp)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                        //sw.Close();
                    }
                }


            }
            else
            {
                Console.WriteLine("EL NOMBRE INGRESADO NO EXITE EN LA LISTA.");

            }
        }

        private static void deleteData()
        {
            Console.Write("INGRESE EL NOMBRE DEL CLIENTE A ELIMINAR: ");

            var name = Console.ReadLine();
            if (search(name))
            {


                Dictionary<object, object> temp = new Dictionary<object, object>();
                temp = readFile();

                temp.Remove(name);
                Console.WriteLine();
                Console.WriteLine("¡ LA CITA SE CANCELO EXITOSAMENTE !");
                File.Delete(getPath());


                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temp)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                        //sw.Close();
                    }
                }


            }
            else
            {
                Console.WriteLine("¡ EL NOMBRE INGRESADO NO EXITE EN LA LISTA !");

            }
        }

    }

}
