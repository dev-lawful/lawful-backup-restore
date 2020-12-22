using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace law
{
    public class Editor
    {
        public int EditorId { get; set; }
        public DateTime EdicionFecha { get; set; }
        public string EdicionAccion { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "backup" || args[0] == "b")
                {
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    Console.Write("Ingrese el nombre del archivo: ");
                    fileName += "\\" + Console.ReadLine() + ".bak";
                    try
                    {    
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                        using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Lawful;Integrated Security=SSPI;"))
                        {
                            connection.Open();

                            SqlCommand command = connection.CreateCommand();
                            command.Connection = connection;


                            command.CommandText = $"use master;";
                            command.ExecuteNonQuery();
                            command.CommandText = $"DECLARE @fileName nvarchar(400); DECLARE @DB_Name nvarchar(50);SET @DB_Name = 'Lawful' SET @fileName = '{fileName}' BACKUP database @DB_Name TO DISK = @fileName; ";
                            command.ExecuteNonQuery();
                        }
                        Console.WriteLine("Backup realizado exitosamente.");
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.ToString());
                    }
                }
                if (args[0] == "restore" || args[0] == "r")
                {
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    Console.Write("Ingrese el nombre del archivo: ");
                    fileName += "\\" + Console.ReadLine() + ".bak";
                    if (File.Exists(fileName))
                    {
                        using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Lawful;Integrated Security=SSPI;"))
                        {
                            connection.Open();

                            SqlCommand command = connection.CreateCommand();
                            command.Connection = connection;
                            command.CommandText = $"use master;";
                            command.ExecuteNonQuery();
                            command.CommandText = $"DROP DATABASE Lawful;";
                            command.ExecuteNonQuery();
                            command.CommandText = $"RESTORE DATABASE Lawful FROM DISK='{fileName}'";
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se ha encontrado el archivo");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opciones: \n\tlaw backup, law b: para realizar un backup de la base de datos\n\tlaw restore, law r: para utilizar un respaldo como base de datos");
            }
        }
    }
}
