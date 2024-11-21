using System.Windows;
using System.Windows.Controls;

namespace Cine_Interfaces
{
    public partial class Pantalla_InicioSesion : Window
    {
        private MainWindow mainScreen;
        private Button b1;
        private Button b2;

        public Pantalla_InicioSesion(MainWindow mainScreen, Button b1, Button b2)
        {
            this.mainScreen = mainScreen;
            this.b1 = b1;
            this.b2 = b2;
            InitializeComponent();
        }

        // Evento para el botón Cancelar
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la ventana
        }

        // Evento para el botón Aceptar
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameField.Text;
            string password = PasswordField.Password;

            DatabaseHandler dbHandler = new DatabaseHandler();
            if (!dbHandler.ValidarNombre(username))
            {
                if (dbHandler.ComprobarContraseña(username, password))
                {
                    Usuario usuarioLogueado = new Usuario(username);
                    mainScreen.SetUsuario(usuarioLogueado);
                    mainScreen.IconoUserVisible();
                    b1.Visibility = Visibility.Hidden;
                    b2.Visibility = Visibility.Hidden;

                    MessageBox.Show("Sesión iniciada con éxito.", "Inicio de Sesión");
                    this.Close();
                    mainScreen.Visibility = Visibility.Visible;

                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta.", "Error.");
                    return;
                }
            }
        }

        // Evento para navegar a la pantalla de registro
        private void NavigateToRegister(object sender, RoutedEventArgs e)
        {
            Pantalla_Registrarse registerScreen = new Pantalla_Registrarse(mainScreen);
            registerScreen.Show();
            this.Close(); // Cierra la pantalla actual
        }

        // Evento para manejar el clic en la imagen de "volver"
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            mainScreen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
