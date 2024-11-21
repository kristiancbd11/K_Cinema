using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Cine_Interfaces
{
    public partial class Pantalla_Butacas : Window
    {
        private Sesion sesion;
        private MainWindow mainScreen;
        private Pantalla_Sesiones screen;
        private List<Button> botonesSeleccionados = new List<Button>(); // Lista para almacenar los botones seleccionados

        public Pantalla_Butacas(MainWindow mainScreen, Pantalla_Sesiones screen, Sesion sesion)
        {
            this.mainScreen = mainScreen;
            this.screen = screen;
            this.sesion = sesion;
            InitializeComponent();
            CrearButacas();
        }

        private void CrearButacas()
        {
            DatabaseHandler dbHandler = new DatabaseHandler();
            Butaca[] listaButacas = dbHandler.ConsultarButacas(sesion.GetCodSesion());

            bool[,] butacasLayout = new bool[6, 10]
            {
                { true, true, true, false, false, false, false, true, true, true },
                { true, true, true, false, false, false, false, true, true, true },
                { true, true, true, true, true, true, true, true, true, true },
                { true, true, true, true, true, true, true, true, true, true },
                { true, true, true, true, true, true, true, true, true, true },
                { false, true, true, true, true, true ,true, true, true, false }
            };

            int buttonNumber = 1;

            for (int row = 0; row < butacasLayout.GetLength(0); row++)
            {
                for (int col = 0; col < butacasLayout.GetLength(1); col++)
                {
                    if (butacasLayout[row, col])
                    {
                        Butaca butacaInfo = listaButacas[buttonNumber - 1];

                        Button butaca = new Button
                        {
                            Content = buttonNumber.ToString(),
                            Margin = new Thickness(2),
                            Width = 35,
                            Height = 35,
                            FontSize = 10
                        };

                        if (butacaInfo.GetEstado())
                        {
                            butaca.Background = Brushes.Blue;
                            butaca.Click += Butaca_Click; // Asignar evento para seleccionar
                        }
                        else
                        {
                            butaca.Background = Brushes.Gray;
                            butaca.IsEnabled = false; // No se puede hacer clic
                        }

                        // Colocar el botón en la posición correspondiente
                        Grid.SetRow(butaca, row);
                        Grid.SetColumn(butaca, col);

                        // Agregar el botón al Grid
                        ButacasGrid.Children.Add(butaca);

                        buttonNumber++;
                    }
                }
            }
        }

        private void Butaca_Click(object sender, RoutedEventArgs e)
        {
            Button butaca = sender as Button;

            // Alternar entre color azul y rojo al hacer clic en la butaca
            if (butaca.Background == Brushes.Blue)
            {
                butaca.Background = Brushes.Red;
                botonesSeleccionados.Add(butaca); // Añadir a la lista de seleccionados
            }
            else
            {
                butaca.Background = Brushes.Blue;
                botonesSeleccionados.Remove(butaca); // Quitar de la lista de seleccionados
            }
        }

        // Método para manejar el evento de clic en el botón "Confirmar"
        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se ha seleccionado al menos una butaca
            if (botonesSeleccionados.Count == 0)
            {
                // Mostrar mensaje emergente si no se ha seleccionado ninguna butaca
                MessageBox.Show("Por favor, selecciona al menos una butaca.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Si se ha seleccionado al menos una butaca, navegar a la siguiente pantalla
                Pantalla_ComprarEntradas pantallaComprar = new Pantalla_ComprarEntradas(mainScreen, this, sesion, botonesSeleccionados); // Pasa la lista de botones seleccionados
                pantallaComprar.Show();
                this.Hide(); // Cerrar la pantalla actual
            }
        }
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            screen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
