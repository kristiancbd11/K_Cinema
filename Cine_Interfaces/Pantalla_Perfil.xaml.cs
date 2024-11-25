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
                // Crear un contenedor para el texto y el botón
                StackPanel entradaPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 10)
                };

                // Crear el texto de la entrada
                TextBlock entradaText = new TextBlock
                {
                    Text = $"Película: {entrada.Pelicula}, Cine: {entrada.Cine}, " +
                           $"Hora: {entrada.Hora}:{entrada.Minutos:D2}, Butaca: {entrada.NumButaca}",
                    FontSize = 20,
                    Foreground = Brushes.White,
                    Margin = new Thickness(0, 0, 10, 0)
                };

                // Crear el botón rojo
                Button eliminarButton = new Button
                {
                    Content = "Eliminar",
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Width = 100,
                    Height = 30,
                    Margin = new Thickness(10, 0, 0, 0),
                    Tag = entrada // Asociar el objeto entrada al botón
                };

                // Manejar el evento Click del botón
                eliminarButton.Click += (sender, e) =>
                {
                    Button button = sender as Button;
                    Entrada entradaAsociada = button.Tag as Entrada;

                    // Abrir la ventana de solicitud de contraseña
                    SolicitarContraseña ventanaContraseña = new SolicitarContraseña();
                    if (ventanaContraseña.ShowDialog() == true) // Usuario confirmó
                    {
                        string contraseñaIngresada = ventanaContraseña.ContraseñaIngresada;

                        // Verificar la contraseña usando el método del DatabaseHandler
                        if (dbHandler.ComprobarContraseña(user.GetNombre(), contraseñaIngresada))
                        {
                            // Lógica para eliminar la entrada
                            bool resultado = dbHandler.eliminarEntrada(user.GetNombre(), entradaAsociada.CodSesion, entradaAsociada.NumButaca);
                            if (resultado)
                            {
                                MessageBox.Show($"Entrada eliminada: {entradaAsociada.Pelicula}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                                // Eliminar visualmente la entrada del StackPanel
                                EntradasPanel.Children.Remove(entradaPanel);
                            }
                            else
                            {
                                MessageBox.Show("Error al eliminar la entrada. Intente de nuevo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta. No se eliminó la entrada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        // Usuario canceló la operación
                        MessageBox.Show("Operación cancelada.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                };

                // Añadir el texto y el botón al contenedor
                entradaPanel.Children.Add(entradaText);
                entradaPanel.Children.Add(eliminarButton);

                // Añadir el contenedor al StackPanel principal
                EntradasPanel.Children.Add(entradaPanel);
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
