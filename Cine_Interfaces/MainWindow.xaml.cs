using System.Windows;
using System.Windows.Controls;

namespace Cine_Interfaces
{
    //Hay que añadir funcionalidad para eliminar entradas
    //Hay que añadir funcionalidad para eliminar usuario
    //Rediseñar los botones para que el color no sea confuso
    public partial class MainWindow : Window
    {
        Usuario usuarioLogeado;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPromociones(object sender, RoutedEventArgs e)
        {
            Pantalla_Promociones promociones = new Pantalla_Promociones();
            promociones.Show();
        }

        private void OpenCines(object sender, RoutedEventArgs e)
        {
            Pantalla_Cines cines = new Pantalla_Cines(this, usuarioLogeado);
            cines.Show();
            this.Hide();
        }

        private void OpenPeliculas(object sender, RoutedEventArgs e)
        {
            Pantalla_Peliculas peliculas = new Pantalla_Peliculas(this, usuarioLogeado);
            peliculas.Show();
            this.Hide();
        }

        public void OpenInicioSesion(object sender, RoutedEventArgs e)
        {
            Pantalla_InicioSesion inicioSesion = new Pantalla_InicioSesion(this, botonIn, botonRe);
            inicioSesion.Show();
            this.Hide();
        }

        private void OpenRegistrarse(object sender, RoutedEventArgs e)
        {
            Pantalla_Registrarse registrarse = new Pantalla_Registrarse(this);
            registrarse.Show();
            this.Hide();
        }

        public void SetUsuario(Usuario usuario)
        {
            this.usuarioLogeado = usuario;
        }

        public void SetUsuario()
        {
            this.usuarioLogeado = null;
        }

        public Usuario GetUsuario()
        {
            return this.usuarioLogeado;
        }

        // Método para hacer visible el icono del usuario
        public void IconoUserVisible()
        {
            // Cambiar la visibilidad de la imagen a visible
            iconoImagen.Visibility = Visibility.Visible;
        }
        public void IconoUserNoVisile()
        {
            botonIn.Visibility = Visibility.Visible;   
            botonRe.Visibility = Visibility.Visible;
            iconoImagen.Visibility = Visibility.Hidden;
        }

        // Método para abrir la ventana del perfil al hacer clic en la imagen
        private void OpenPerfil(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Pantalla_Perfil perfil = new Pantalla_Perfil(this, usuarioLogeado);
            perfil.Show();
            this.Hide();
        }
    }
}
