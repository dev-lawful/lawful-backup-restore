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
                    string fileName = Directory.GetCurrentDirectory();
                    Console.Write("Ingrese el nombre del archivo: ");
                    fileName += "\\" + Console.ReadLine()+ ".txt";
                    try
                    {
                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        // Create a new file     
                        using (StreamWriter sw = File.CreateText(fileName))
                        {
                            using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Lawful;Integrated Security=SSPI;"))
                            {
                                connection.Open();

                                SqlCommand command = connection.CreateCommand();
                                command.Connection = connection;


                                command.CommandText = $"SELECT id, descripcion, icon_name FROM acciones;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT acciones ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO acciones (id, descripcion, icon_name) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}');");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT acciones OFF;");
                                }


                                command.CommandText = $"SELECT id, codigo, descripcion, estado from grupos;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT grupos ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO grupos (id, codigo, descripcion, estado) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}',{(response.GetBoolean(3) ? 1:0)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT grupos OFF;");
                                }


                                command.CommandText = $"SELECT id, grupo_id, accion_id FROM grupos_acciones;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT grupos_acciones ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO grupos_acciones (id, grupo_id, accion_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT grupos_acciones OFF;");
                                }


                                command.CommandText = $"SELECT id, descripcion, class_name FROM iniciativas_tipos;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT iniciativas_tipos ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO iniciativas_tipos (id, descripcion, class_name) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}');");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT iniciativas_tipos OFF;");
                                }


                                command.CommandText = $"SELECT id, username, contrasena, email, nombre, apellido, estado, editor_id, edicion_fecha, edicion_accion, need_new_password FROM usuarios;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    Dictionary<int, Editor> idEditorId = new Dictionary<int, Editor>();
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios ON;");
                                    while (response.Read())
                                    {
                                        if (!response.IsDBNull(7))
                                        {
                                            idEditorId.Add(response.GetInt32(0), new Editor() { EditorId = response.GetInt32(7), EdicionFecha = response.GetDateTime(8), EdicionAccion = response.GetString(9)});
                                        }
                                        
                                        sw.WriteLine($"INSERT INTO usuarios (id, username, contrasena, email, nombre, apellido, estado, edicion_fecha, edicion_accion, need_new_password) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetString(3)}','{response.GetString(4)}','{response.GetString(5)}',{(response.GetBoolean(6)? 1 : 0)},'{response.GetDateTime(8)}','{response.GetString(9)}',{(response.GetBoolean(10) ? 1 : 0)});");
                                    }
                                    foreach (KeyValuePair<int, Editor> item in idEditorId)
                                    {
                                        sw.WriteLine($"UPDATE usuarios SET editor_id = {item.Value.EditorId}, edicion_fecha = '{item.Value.EdicionFecha}', edicion_accion = '{item.Value.EdicionAccion}' WHERE id = {item.Key}");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios OFF;");
                                }


                                command.CommandText = $"SELECT id,usuario_id,username,contrasena,email,nombre,apellido,estado,editor_id,edicion_fecha,edicion_accion,need_new_password FROM usuarios_auditorias;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios_auditorias ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO usuarios_auditorias (id,usuario_id,username,contrasena,email,nombre,apellido,estado,editor_id,edicion_fecha,edicion_accion,need_new_password) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},'{response.GetString(2)}','{response.GetString(3)}','{response.GetString(4)}','{response.GetString(5)}','{response.GetString(6)}',{(response.GetBoolean(7) ? 1 : 0)},{response.GetInt32(8)},'{response.GetDateTime(9)}','{response.GetString(10)}',{(response.GetBoolean(11) ? 1 : 0)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios_auditorias OFF;");
                                }


                                command.CommandText = $"SELECT id,usuario_id,grupo_id FROM usuarios_grupos;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios_grupos ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO usuarios_grupos (id,usuario_id,grupo_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT usuarios_grupos OFF;");
                                }


                                command.CommandText = $"SELECT id,descripcion,icon_name,class_name,visible_in_shellpage FROM vistas;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT vistas ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO vistas (id,descripcion,icon_name,class_name,visible_in_shellpage) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetString(3)}',{(response.GetBoolean(4) ? 1 : 0)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT vistas OFF;");
                                }


                                command.CommandText = $"SELECT id,vista_id,accion_id FROM vistas_acciones;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT vistas_acciones ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO vistas_acciones (id,vista_id,accion_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT vistas_acciones OFF;");
                                }


                                command.CommandText = $"SELECT id,emisor_id,receptor_id,texto,fecha FROM mensajes_usuarios;";
                                using (SqlDataReader response = command.ExecuteReader())
                                {
                                    sw.WriteLine("SET IDENTITY_INSERT mensajes_usuarios ON;");
                                    while (response.Read())
                                    {
                                        sw.WriteLine($"INSERT INTO mensajes_usuarios (id,emisor_id,receptor_id,texto,fecha) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)},'{response.GetString(3)}','{response.GetDateTime(4)}');");
                                    }
                                    sw.WriteLine("SET IDENTITY_INSERT mensajes_usuarios OFF;");
                                }
                            }
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

                }
            }
            else
            {
                Console.WriteLine("Opciones: \n\tlaw backup, law b: para realizar un backup de la base de datos\n\tlaw restore, law r: para utilizar un respaldo como base de datos");
                Console.ReadKey();
            }
        }
    }
}
