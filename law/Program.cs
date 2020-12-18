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
            string format = "yyyy-MM-dd HH:mm:ss";
            if (args.Length > 0)
            {
                if (args[0] == "backup" || args[0] == "b")
                {
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    Console.Write("Ingrese el nombre del archivo: ");
                    fileName += "\\" + Console.ReadLine()+ ".bak";
                    try
                    {
                        // Check if file already exists. If yes, delete it.     
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

                            // Create a new file     
                            //using (StreamWriter sw = File.CreateText(fileName))
                            //{
                            //    using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Lawful;Integrated Security=SSPI;"))
                            //    {
                            //        connection.Open();

                            //        SqlCommand command = connection.CreateCommand();
                            //        command.Connection = connection;


                            //        command.CommandText = $"SELECT id, descripcion, icon_name FROM acciones;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT acciones ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO acciones (id, descripcion, icon_name) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}');");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT acciones OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id, codigo, descripcion, estado from grupos;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT grupos ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO grupos (id, codigo, descripcion, estado) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}',{(response.GetBoolean(3) ? 1:0)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT grupos OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id, grupo_id, accion_id FROM grupos_acciones;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT grupos_acciones ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO grupos_acciones (id, grupo_id, accion_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT grupos_acciones OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id, descripcion, class_name FROM iniciativas_tipos;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT iniciativas_tipos ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO iniciativas_tipos (id, descripcion, class_name) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}');");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT iniciativas_tipos OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id, username, contrasena, email, nombre, apellido, estado, editor_id, edicion_fecha, edicion_accion, need_new_password FROM usuarios;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            Dictionary<int, Editor> idEditorId = new Dictionary<int, Editor>();
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios ON;");
                            //            while (response.Read())
                            //            {
                            //                if (!response.IsDBNull(7))
                            //                {
                            //                    idEditorId.Add(response.GetInt32(0), new Editor() { EditorId = response.GetInt32(7), EdicionFecha = response.GetDateTime(8), EdicionAccion = response.GetString(9)});
                            //                }

                            //                sw.WriteLine($"INSERT INTO usuarios (id, username, contrasena, email, nombre, apellido, estado, edicion_fecha, edicion_accion, need_new_password) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetString(3)}','{response.GetString(4)}','{response.GetString(5)}',{(response.GetBoolean(6)? 1 : 0)},'{response.GetDateTime(8).ToString(format)}','{response.GetString(9)}',{(response.GetBoolean(10) ? 1 : 0)});");
                            //            }
                            //            foreach (KeyValuePair<int, Editor> item in idEditorId)
                            //            {
                            //                sw.WriteLine($"UPDATE usuarios SET editor_id = {item.Value.EditorId}, edicion_fecha = '{item.Value.EdicionFecha.ToString(format)}', edicion_accion = '{item.Value.EdicionAccion}' WHERE id = {item.Key}");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,usuario_id,username,contrasena,email,nombre,apellido,estado,editor_id,edicion_fecha,edicion_accion,need_new_password FROM usuarios_auditorias;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_auditorias ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO usuarios_auditorias (id,usuario_id,username,contrasena,email,nombre,apellido,estado,editor_id,edicion_fecha,edicion_accion,need_new_password) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},'{response.GetString(2)}','{response.GetString(3)}','{response.GetString(4)}','{response.GetString(5)}','{response.GetString(6)}',{(response.GetBoolean(7) ? 1 : 0)},{response.GetInt32(8)},'{response.GetDateTime(9).ToString(format)}','{response.GetString(10)}',{(response.GetBoolean(11) ? 1 : 0)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_auditorias OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,usuario_id,grupo_id FROM usuarios_grupos;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_grupos ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO usuarios_grupos (id,usuario_id,grupo_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_grupos OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,descripcion,icon_name,class_name,visible_in_shellpage FROM vistas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT vistas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO vistas (id,descripcion,icon_name,class_name,visible_in_shellpage) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetString(3)}',{(response.GetBoolean(4) ? 1 : 0)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT vistas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,vista_id,accion_id FROM vistas_acciones;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT vistas_acciones ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO vistas_acciones (id,vista_id,accion_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT vistas_acciones OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,emisor_id,receptor_id,texto,fecha FROM mensajes_usuarios;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT mensajes_usuarios ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO mensajes_usuarios (id,emisor_id,receptor_id,texto,fecha) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)},'{response.GetString(3)}','{response.GetDateTime(4).ToString(format)}');");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT mensajes_usuarios OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,descripcion,estado,fecha_creacion,fecha_cierre,everyone_can_edit,titulo,usuario_id FROM temas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT temas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO temas (id,descripcion,estado,fecha_creacion,fecha_cierre,everyone_can_edit,titulo,usuario_id) VALUES ({response.GetInt32(0)},'{response.GetString(1)}',{(response.GetBoolean(2) ? 1 : 0)},'{response.GetDateTime(3).ToString(format)}','{response.GetDateTime(4).ToString(format)}',{(response.GetBoolean(5) ? 1 : 0)},'{response.GetString(6)}',{response.GetInt32(7)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT temas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,titulo,descripcion,fecha_por_hacer,fecha_en_curso,fecha_finalizada,importancia,usuario_id,tema_id,estado FROM tareas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT tareas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO tareas (id,titulo,descripcion,fecha_por_hacer,fecha_en_curso,fecha_finalizada,importancia,usuario_id,tema_id,estado) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetDateTime(3).ToString(format)}','{response.GetDateTime(4).ToString(format)}','{response.GetDateTime(5).ToString(format)}',{response.GetInt32(6)},{response.GetInt32(7)},{response.GetInt32(8)},{response.GetInt32(9)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT tareas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,descripcion,fecha,usuario_id,tarea_id FROM tareas_comentarios;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT tareas_comentarios ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO tareas_comentarios (id,descripcion,fecha,usuario_id,tarea_id) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetDateTime(2).ToString(format)}',{response.GetInt32(3)},{response.GetInt32(4)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT tareas_comentarios OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,emisor_id,tema_id,texto,fecha FROM mensajes_temas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT mensajes_temas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO mensajes_temas (id,emisor_id,tema_id,texto,fecha) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)},'{response.GetString(3)}','{response.GetDateTime(4).ToString(format)}');");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT mensajes_temas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,usuario_id,tema_id FROM usuarios_temas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_temas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO usuarios_temas (id,usuario_id,tema_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT usuarios_temas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,titulo,descripcion,fecha_creacion,usuario_id,iniciativa_tipo_id,fecha_evento,lugar,fecha_limite_confirmacion,respuesta_correcta_id,relevancia,max_opciones_seleccionables,tema_id,icon_name,fecha_cierre FROM iniciativas;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT iniciativas ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO iniciativas (id,titulo,descripcion,fecha_creacion,usuario_id,iniciativa_tipo_id,fecha_evento,lugar,fecha_limite_confirmacion,respuesta_correcta_id,relevancia,max_opciones_seleccionables,tema_id,icon_name,fecha_cierre) VALUES ({response.GetInt32(0)},'{response.GetString(1)}','{response.GetString(2)}','{response.GetDateTime(3).ToString(format)}',{response.GetInt32(4)},{response.GetInt32(5)},'{(response.IsDBNull(6) ? DateTime.Now.ToString(format) : response.GetDateTime(6).ToString(format))}','{(response.IsDBNull(7) ? "''" : response.GetString(7))}','{(response.IsDBNull(8) ? DateTime.Now.ToString(format) : response.GetDateTime(8).ToString(format))}',{(response.IsDBNull(9) ? 0 : response.GetInt32(9))},{(response.IsDBNull(10) ? 0 : response.GetInt32(10))},{(response.IsDBNull(11) ? 0 : response.GetInt32(11))},{response.GetInt32(12)},'{response.GetString(13)}','{response.GetDateTime(14)}');");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT iniciativas OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,descripcion,usuario_id,iniciativa_id FROM comentarios;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT comentarios ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO comentarios (id,descripcion,usuario_id,iniciativa_id) VALUES ({response.GetInt32(0)},'{response.GetString(1)}',{response.GetInt32(2)},{response.GetInt32(3)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT comentarios OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,descripcion,iniciativa_id FROM opciones;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT opciones ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO opciones (id,descripcion,iniciativa_id) VALUES ({response.GetInt32(0)},'{response.GetString(1)}',{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT opciones OFF;");
                            //        }


                            //        command.CommandText = $"SELECT id,usuario_id,opcion_id FROM votos;";
                            //        using (SqlDataReader response = command.ExecuteReader())
                            //        {
                            //            sw.WriteLine("SET IDENTITY_INSERT votos ON;");
                            //            while (response.Read())
                            //            {
                            //                sw.WriteLine($"INSERT INTO votos (id,usuario_id,opcion_id) VALUES ({response.GetInt32(0)},{response.GetInt32(1)},{response.GetInt32(2)});");
                            //            }
                            //            sw.WriteLine("SET IDENTITY_INSERT votos OFF;");
                            //        }
                            //    }
                            //}



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
