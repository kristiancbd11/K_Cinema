using System.Windows;

namespace Cine_Interfaces
{
    public partial class Pantalla_Cines : Window
    {
        private MainWindow mainScreen;
        private Usuario user;
        public Pantalla_Cines(MainWindow mainScreen, Usuario user)
        {
            this.user = user;
            this.mainScreen = mainScreen;
            InitializeComponent();
        }

        // Evento de clic en cualquier botón
        private void BotonCine_Click(object sender, RoutedEventArgs e)
        {
            // Crear una instancia de Pantalla_Peliculas
            Pantalla_Peliculas pantallaPeliculas = new Pantalla_Peliculas(mainScreen, user);

            // Mostrar la ventana Pantalla_Peliculas
            pantallaPeliculas.Show();

            // Cerrar la ventana actual (Pantalla_Cines)
            this.Close();
        }
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            mainScreen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
