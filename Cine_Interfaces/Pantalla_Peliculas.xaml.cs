using System;
using System.Windows;

namespace Cine_Interfaces
{
    public partial class Pantalla_Peliculas : Window
    {
        private Usuario user;
        private MainWindow mainScreen;

        public Pantalla_Peliculas(MainWindow mainScreen, Usuario user)
        {
            this.user = user;
            this.mainScreen = mainScreen;
            InitializeComponent();
        }

        // Evento que se dispara cuando se hace clic en una de las imágenes
        private void Imagen_Click(object sender, RoutedEventArgs e)
        {
            string pelicula = "";

            // Comprobamos qué imagen fue clicada
            if (sender is System.Windows.Controls.Image image)
            {
                // Determinamos la película según la imagen
                if (image.Source.ToString().Contains("malditos.jpg"))
                {
                    pelicula = "Malditos Bastardos";
                }
                else if (image.Source.ToString().Contains("favorita.jpg"))
                {
                    pelicula = "La Favorita";
                }

                // Abrimos la ventana Pantalla_Sesiones y pasamos el nombre de la película
                Pantalla_Sesiones pantallaSesiones = new Pantalla_Sesiones(mainScreen, this, pelicula, user);
                pantallaSesiones.Show();
                this.Hide(); // Opcional: Cerrar la ventana actual (Pantalla_Peliculas)
            }
        }
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            mainScreen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
