using System.Windows;

namespace Cine_Interfaces
{
    /// <summary>
    /// Ventana para solicitar la contraseña del usuario.
    /// </summary>
    public partial class SolicitarContraseña : Window
    {
        public string ContraseñaIngresada { get; private set; }

        public SolicitarContraseña()
        {
            InitializeComponent();
        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            ContraseñaIngresada = PasswordBoxContraseña.Password; // Obtén la contraseña ingresada
            DialogResult = true; // Cierra la ventana con éxito
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Cierra la ventana sin éxito
        }
    }
}
