using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cine_Interfaces
{
    public partial class Pantalla_Butacas : Window
    {
        private Sesion sesion;
        private MainWindow mainScreen;
        private Pantalla_Sesiones screen;
        private List<Button> botonesSeleccionados = new List<Button>();

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
                            FontSize = 10,
                            Style = (Style)FindResource("ButacaStyle") // Asignar estilo
                        };

                        if (butacaInfo.GetEstado())
                        {
                            butaca.Click += Butaca_Click;
                        }
                        else
                        {
                            butaca.Background = Brushes.Gray;
                            butaca.IsEnabled = false;
                        }

                        Grid.SetRow(butaca, row);
                        Grid.SetColumn(butaca, col);
                        ButacasGrid.Children.Add(butaca);

                        buttonNumber++;
                    }
                }
            }
        }

        private void Butaca_Click(object sender, RoutedEventArgs e)
        {
            Button butaca = sender as Button;

            // Alternar entre azul y rojo al hacer clic en la butaca
            if (butaca.Background == Brushes.Blue)
            {
                butaca.Background = Brushes.Red; // Cambiar a rojo al seleccionar
                botonesSeleccionados.Add(butaca);
            }
            else
            {
                butaca.Background = Brushes.Blue; // Volver a azul si se deselecciona
                botonesSeleccionados.Remove(butaca);
            }
        }


        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            if (botonesSeleccionados.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos una butaca.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Pantalla_ComprarEntradas pantallaComprar = new Pantalla_ComprarEntradas(mainScreen, this, sesion, botonesSeleccionados);
                pantallaComprar.Show();
                this.Hide();
            }
        }

        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
            screen.Visibility = Visibility.Visible;
        }
    }
}
