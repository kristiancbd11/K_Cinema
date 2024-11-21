using System.Text.RegularExpressions;
using System.Windows;

namespace Cine_Interfaces
{
    public partial class Pantalla_Registrarse : Window
    {
        private MainWindow mainScreen;

        public Pantalla_Registrarse(MainWindow mainScreen)
        {
            this.mainScreen = mainScreen;
            InitializeComponent();
        }

        // Evento para el botón Cancelar
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la ventana
        }

        // Evento para el botón Registrarse
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameField.Text.Trim(); // Obtenemos el nombre de usuario
            string password = PasswordField.Password;    // Obtenemos la contraseña
            string confirmPassword = ConfirmPasswordField.Password; // Obtenemos la confirmación de la contraseña

            // Validación 1: Comprobar si alguno de los campos está vacío
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Error");
                return;
            }

            // Validación 2: El nombre de usuario no puede contener espacios en blanco
            if (username.Contains(" "))
            {
                MessageBox.Show("El nombre de usuario no debe contener espacios en blanco.", "Error");
                return;
            }

            // Validación 3: La contraseña debe tener entre 8 y 16 caracteres, incluir letras mayúsculas, minúsculas y números
            if (!IsPasswordValid(password))
            {
                MessageBox.Show("La contraseña debe tener entre 8 y 16 caracteres, incluir letras mayúsculas, minúsculas y números.", "Error");
                return;
            }

            // Validación 4: Contraseña y Confirmar Contraseña deben coincidir
            if (password != confirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error");
                return;
            }

            // Validar si el nombre de usuario ya existe
            DatabaseHandler dbHandler = new DatabaseHandler();
            bool esNombreValido = dbHandler.ValidarNombre(username); // Se pasa el nombre introducido

            if (esNombreValido)
            {
                // Si pasa todas las validaciones, inicializamos el objeto Usuario con los datos introducidos
                Usuario user = new Usuario(username, password); // Inicializamos el objeto Usuario con el nombre y la contraseña

                // Intentamos registrar al usuario en la base de datos
                if (dbHandler.RegistrarUsuario(user))
                {
                    MessageBox.Show($"Usuario {username} registrado con éxito.", "Registro Completado");
                    mainScreen.Visibility = Visibility.Visible; 
                    this.Close(); // Cerrar la ventana de registro
                }
                else
                {
                    MessageBox.Show("Se produjo un error durante el registro, vuelva a intentarlo.", "Error");
                }
            }
            else
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Error");
            }
        }

        // Método auxiliar para validar la contraseña
        private bool IsPasswordValid(string password)
        {
            // La contraseña debe tener entre 8 y 16 caracteres, incluir letras mayúsculas, minúsculas y números
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$";
            return Regex.IsMatch(password, pattern);
        }

        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            mainScreen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
