using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Cine_Interfaces
{
    public class DatabaseHandler
    {
        // Cadena de conexión para AWS RDS MySQL Aurora
        private string connectionString = "Server=bbddinterfaces.c4qxliqpbgjv.us-east-1.rds.amazonaws.com;Port=3306;Database=cine;Uid=admin;Pwd=admin123456789;";

        // Método para consultar sesiones (ya existente)
        public Sesion[] ConsultarSesiones(string pelicula)
        {
            string query = "SELECT * FROM Sesion WHERE Pelicula = @Pelicula";
            List<Sesion> sesionesList = new List<Sesion>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@Pelicula", MySqlDbType.VarChar).Value = pelicula;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int codSesion = reader.GetInt32("CodSesion");
                            string cine = reader.IsDBNull(reader.GetOrdinal("Cine")) ? string.Empty : reader.GetString("Cine");
                            int hora = reader.GetInt32("Hora");
                            int minutos = reader.GetInt32("Minutos");

                            Sesion sesion = new Sesion(codSesion, pelicula, cine, hora, minutos);
                            sesionesList.Add(sesion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la consulta de sesiones: {ex.Message}");
                }
            }

            return sesionesList.ToArray();
        }

        // Método para consultar butacas (ya existente)
        public Butaca[] ConsultarButacas(int codSesion)
        {
            string query = "SELECT CodSesion, NumButaca, Libre FROM Butacas WHERE CodSesion = @CodSesion";
            List<Butaca> butacasList = new List<Butaca>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@CodSesion", MySqlDbType.Int32).Value = codSesion;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int numButaca = reader.GetInt32("NumButaca");
                            bool libre = reader.GetBoolean("Libre");

                            Butaca butaca = new Butaca(numButaca, libre, "N/A", 0);
                            butacasList.Add(butaca);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la consulta de butacas: {ex.Message}");
                }
            }

            return butacasList.ToArray();
        }

        // Nuevo método para modificar la disponibilidad de las butacas
        public void ModificarDisponibilidad(int codSesion, int[] numButacas)
        {
            string query = "UPDATE Butacas SET Libre = FALSE WHERE CodSesion = @CodSesion AND NumButaca = @NumButaca";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (var numButaca in numButacas)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.Add("@CodSesion", MySqlDbType.Int32).Value = codSesion;
                        cmd.Parameters.Add("@NumButaca", MySqlDbType.Int32).Value = numButaca;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al modificar la disponibilidad de las butacas: {ex.Message}");
                }
            }
        }

        // Método para comprobar si el nombre de usuario existe en la base de datos
        public bool ValidarNombre(string nombre)
        {
            string query = "SELECT COUNT(*) FROM Usuario WHERE NombreUser = @NombreUser";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@NombreUser", MySqlDbType.VarChar).Value = nombre;

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count == 0;  // Si count es 0, el nombre no existe
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al validar el nombre de usuario: {ex.Message}");
                    return false;
                }
            }
        }

        // Método para registrar un nuevo usuario
        public bool RegistrarUsuario(Usuario user)
        {
            string query = "INSERT INTO Usuario (NombreUser, Passwd) VALUES (@NombreUser, @Passwd)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@NombreUser", MySqlDbType.VarChar).Value = user.GetNombre();
                    cmd.Parameters.Add("@Passwd", MySqlDbType.VarChar).Value = user.GetPassword();

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al registrar el usuario: {ex.Message}");
                    return false;
                }
            }
        }

        // Nuevo método para comprobar si la contraseña es correcta
        public bool ComprobarContraseña(string user, string password)
        {
            // Consulta SQL para obtener la contraseña del usuario
            string query = "SELECT Passwd FROM Usuario WHERE NombreUser = @NombreUser";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@NombreUser", MySqlDbType.VarChar).Value = user;

                    // Ejecutar el comando y obtener el resultado
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string storedPassword = result.ToString();
                        // Compara la contraseña almacenada con la proporcionada
                        return storedPassword == password;
                    }
                    return false;  // Si no se encuentra el usuario, retornamos false
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al comprobar la contraseña: {ex.Message}");
                    return false;
                }
            }
        }

        // Nuevo método para guardar entradas en la base de datos
        public void guardarEntradas(Usuario user, Sesion sesion, List<int> butacas)
        {
            string query = "INSERT INTO Entradas (NombreUser, Pelicula, Cine, Hora, Minutos, NumButaca) " +
                           "VALUES (@NombreUser, @Pelicula, @Cine, @Hora, @Minutos, @NumButaca)";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (int butaca in butacas)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.Add("@NombreUser", MySqlDbType.VarChar).Value = user.GetNombre();
                        cmd.Parameters.Add("@Pelicula", MySqlDbType.VarChar).Value = sesion.GetPelicula();
                        cmd.Parameters.Add("@Cine", MySqlDbType.VarChar).Value = sesion.GetCine();
                        cmd.Parameters.Add("@Hora", MySqlDbType.Int32).Value = sesion.GetHora();
                        cmd.Parameters.Add("@Minutos", MySqlDbType.Int32).Value = sesion.GetMinutos();
                        cmd.Parameters.Add("@NumButaca", MySqlDbType.Int32).Value = butaca;

                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Entradas guardadas correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar entradas: {ex.Message}");
                }
            }
        }

        public List<Entrada> VerEntradas(String user)
        {
            string query = "SELECT NombreUser, Pelicula, Cine, Hora, Minutos, NumButaca FROM Entradas WHERE NombreUser = @NombreUser";
            List<Entrada> entradasList = new List<Entrada>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add("@NombreUser", MySqlDbType.VarChar).Value = user;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreUser = reader.GetString("NombreUser");
                            string pelicula = reader.GetString("Pelicula");
                            string cine = reader.GetString("Cine");
                            int hora = reader.GetInt32("Hora");
                            int minutos = reader.GetInt32("Minutos");
                            int numButaca = reader.GetInt32("NumButaca");

                            // Crear objeto Entrada con los datos obtenidos
                            Entrada entrada = new Entrada(nombreUser, pelicula, cine, hora, minutos, numButaca);
                            entradasList.Add(entrada);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al consultar las entradas: {ex.Message}");
                }
            }

            return entradasList;
        }
    }
}
