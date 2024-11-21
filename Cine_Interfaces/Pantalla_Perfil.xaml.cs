using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cine_Interfaces
{
    /// <summary>
    /// Lógica de interacción para Pantalla_Perfil.xaml
    /// </summary>
    public partial class Pantalla_Perfil : Window
    {
        private MainWindow mainScreen;
        public Pantalla_Perfil(MainWindow mainScreen, Usuario user)
        {
            this.mainScreen = mainScreen;
            InitializeComponent();

            // Configuración inicial del nombre del usuario
            string nombre = user.GetNombre();
            CuadroNombre.Text = nombre;

            // Obtener las entradas del usuario
            DatabaseHandler dbHandler = new DatabaseHandler();
            List<Entrada> listaEntradas = dbHandler.VerEntradas(nombre);

            // Añadir cada entrada al StackPanel
            foreach (var entrada in listaEntradas)
            {
                TextBlock entradaText = new TextBlock
                {
                    Text = $"Película: {entrada.Pelicula}, Cine: {entrada.Cine}, " +
                           $"Hora: {entrada.Hora}:{entrada.Minutos:D2}, Butaca: {entrada.NumButaca}",
                    FontSize = 20,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                EntradasPanel.Children.Add(entradaText);
            }
        }
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            mainScreen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para cerrar sesión, por ejemplo:
            MessageBox.Show("Sesión cerrada.");
            // Cierra la ventana actual y redirige al inicio de sesión:
            this.Close();
            mainScreen.IconoUserNoVisile();
            mainScreen.SetUsuario();
            mainScreen.Visibility = Visibility.Visible;
        }

    }
}
