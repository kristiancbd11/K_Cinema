using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions; // Para usar Regex en la validación

namespace Cine_Interfaces
{
    public partial class Pantalla_ComprarEntradas : Window
    {
        private MainWindow mainScreen;
        private Pantalla_Butacas screen;
        private Sesion sesion;
        private List<Button> botonesSeleccionados;

        // Constructor
        public Pantalla_ComprarEntradas(MainWindow mianScreen, Pantalla_Butacas screen, Sesion sesion, List<Button> botonesSeleccionados)
        {
            this.mainScreen = mianScreen;
            this.screen = screen;
            this.sesion = sesion;
            this.botonesSeleccionados = botonesSeleccionados;
            InitializeComponent();
            MostrarInformacionEntrada();
        }

        // Mostrar la información de la entrada seleccionada
        private void MostrarInformacionEntrada()
        {
            txtPelicula.Text = $"Película: {sesion.GetPelicula()}";
            txtCine.Text = $"Cine: {sesion.GetCine()}";
            txtHora.Text = $"Hora: {sesion.GetHora()}:{sesion.GetMinutos():D2}";

            // Mostrar los números de las butacas seleccionadas
            List<string> butacasSeleccionadas = new List<string>();
            foreach (var boton in botonesSeleccionados)
            {
                butacasSeleccionadas.Add(boton.Content.ToString());
            }

            if (butacasSeleccionadas.Count > 0)
            {
                txtButacas.Text = $"Número de las butacas seleccionadas: {string.Join(", ", butacasSeleccionadas)}";
            }
            else
            {
                txtButacas.Text = "No se ha seleccionado ninguna butaca.";
            }

            // Calcular el precio total
            int precioPorEntrada = 10; // 10€ por entrada
            int numeroDeEntradas = botonesSeleccionados.Count;
            int precioTotal = numeroDeEntradas * precioPorEntrada;

            txtPrecio.Text = $"Precio Total: {precioTotal}€";
        }

        // Método para manejar el clic en el botón "Confirmar Compra"
        private void ConfirmarCompra_Click(object sender, RoutedEventArgs e)
        {
            string numTarjeta = txtNumTarjeta.Text;
            string claveSeguridad = txtCodigoSeguridad.Password;

            // Verificar que todos los campos estén completos
            if (string.IsNullOrEmpty(txtNumTarjeta.Text) ||
                string.IsNullOrEmpty(txtTitular.Text) ||
                string.IsNullOrEmpty(txtCodigoSeguridad.Password))
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar el formato de la tarjeta
            if (!EsNumeroTarjetaValido(numTarjeta))
            {
                MessageBox.Show("El número de tarjeta es inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar la clave de seguridad según el número de tarjeta
            if (!EsClaveSeguridadValida(numTarjeta, claveSeguridad))
            {
                MessageBox.Show("La clave de seguridad es incorrecta para este número de tarjeta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Recoger los números de las butacas seleccionadas
            List<int> butacasSeleccionadas = new List<int>();
            foreach (var boton in botonesSeleccionados)
            {
                // Convertir el contenido del botón (número de la butaca) a entero
                if (int.TryParse(boton.Content.ToString(), out int numButaca))
                {
                    butacasSeleccionadas.Add(numButaca);
                }
            }

            // Verificar si hay butacas seleccionadas
            if (butacasSeleccionadas.Count == 0)
            {
                MessageBox.Show("No se ha seleccionado ninguna butaca.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Llamar al método de la clase DatabaseHandler para modificar la disponibilidad
            DatabaseHandler dbHandler = new DatabaseHandler();
            dbHandler.ModificarDisponibilidad(sesion.GetCodSesion(), butacasSeleccionadas.ToArray());
            Usuario user = mainScreen.GetUsuario();
            dbHandler.guardarEntradas(user, sesion, butacasSeleccionadas);
            // Mostrar mensaje de confirmación
            MessageBox.Show("Compra realizada con éxito.", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

            // Cerrar la ventana
            mainScreen.Visibility = Visibility.Visible;
            this.Close();
        }


        // Validar el formato del número de tarjeta
        private bool EsNumeroTarjetaValido(string numeroTarjeta)
        {
            string patron = @"^(1111-2222-3333-4444|5555-5555-5555-5555)$";
            return Regex.IsMatch(numeroTarjeta, patron);
        }

        // Validar la clave de seguridad según el número de tarjeta
        private bool EsClaveSeguridadValida(string numeroTarjeta, string claveSeguridad)
        {
            if (numeroTarjeta == "1111-2222-3333-4444" && claveSeguridad == "123")
            {
                return true;
            }
            else if (numeroTarjeta == "5555-5555-5555-5555" && claveSeguridad == "555")
            {
                return true;
            }
            return false;
        }

        // Método para validar que no se ingresen números en el nombre del titular
        private void ValidarNombreTitular(object sender, TextChangedEventArgs e)
        {
            string nombreTitular = txtTitular.Text;
            if (Regex.IsMatch(nombreTitular, @"\d"))
            {
                MessageBox.Show("El nombre del titular no puede contener números.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Eliminar el último carácter ingresado si es un número
                txtTitular.Text = nombreTitular.Substring(0, nombreTitular.Length - 1);
                txtTitular.SelectionStart = txtTitular.Text.Length; // Colocar el cursor al final
            }
        }

        // Método para formatear automáticamente el número de tarjeta
        private void FormatearNumeroTarjeta(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto actual sin los guiones
            string texto = txtNumTarjeta.Text.Replace("-", "");

            // Verificar si la longitud total de la cadena es mayor de 16 caracteres
            if (texto.Length > 16)
            {
                return; // No permitir más de 16 números
            }

            // Insertar los guiones en la posición adecuada
            if (texto.Length > 4)
            {
                texto = texto.Insert(4, "-");
            }
            if (texto.Length > 9)
            {
                texto = texto.Insert(9, "-");
            }
            if (texto.Length > 14)
            {
                texto = texto.Insert(14, "-");
            }

            // Si la longitud total de la cadena (con guiones) alcanza los 19 caracteres, no permitir más caracteres
            if (texto.Length >= 19)
            {
                txtNumTarjeta.Text = texto; // Asignar el texto formateado al campo de texto
                txtNumTarjeta.SelectionStart = texto.Length; // Colocar el cursor al final
                return; // No permitir que se sigan escribiendo más caracteres
            }

            // Asignar el texto formateado al campo de texto
            txtNumTarjeta.Text = texto;

            // Colocar el cursor al final del texto
            txtNumTarjeta.SelectionStart = texto.Length;
        }
        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            screen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
