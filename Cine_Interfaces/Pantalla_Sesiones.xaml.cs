using System;
using System.Windows;

namespace Cine_Interfaces
{
    public partial class Pantalla_Sesiones : Window
    {
        private Pantalla_Peliculas screen;
        private MainWindow mainScreen;
        private Usuario user;

        // Constructor que recibe el nombre de la película
        public Pantalla_Sesiones(MainWindow mainScree, Pantalla_Peliculas screen, string pelicula, Usuario user)
        {
            this.user = user;
            this.mainScreen = mainScree;
            this.screen = screen;
            InitializeComponent();

            // Mostrar el nombre de la película (puedes cambiar esto por cualquier otro control que quieras)
            DatabaseHandler dbHandler = new DatabaseHandler();
            Sesion[] listaSesiones = dbHandler.ConsultarSesiones(pelicula);
            peliculaLabel.Content = pelicula;
            int i = 0;

            // Asignar el contenido de cada botón con los datos de las sesiones
            boton1.Content = listaSesiones[0].GetPelicula() + " | " + listaSesiones[0].GetHora() + ":0" + listaSesiones[0].GetMinutos() + " en " + listaSesiones[0].GetCine();
            boton2.Content = listaSesiones[1].GetPelicula() + " | " + listaSesiones[1].GetHora() + ":0" + listaSesiones[1].GetMinutos() + " en " + listaSesiones[1].GetCine();
            boton3.Content = listaSesiones[2].GetPelicula() + " | " + listaSesiones[2].GetHora() + ":0" + listaSesiones[2].GetMinutos() + " en " + listaSesiones[2].GetCine();
            boton4.Content = listaSesiones[3].GetPelicula() + " | " + listaSesiones[3].GetHora() + ":0" + listaSesiones[3].GetMinutos() + " en " + listaSesiones[3].GetCine();

            // Agregar eventos de click para cada botón
            boton1.Click += (sender, e) => AbrirPantallaButacas(listaSesiones[0], user);
            boton2.Click += (sender, e) => AbrirPantallaButacas(listaSesiones[1], user);
            boton3.Click += (sender, e) => AbrirPantallaButacas(listaSesiones[2], user);
            boton4.Click += (sender, e) => AbrirPantallaButacas(listaSesiones[3], user);
        }

        // Método para abrir la ventana de Pantalla_ComprarEntrada pasando la sesión seleccionada
        private void AbrirPantallaButacas(Sesion sesion, Usuario user)
        {
            // Verificar si el usuario es null antes de continuar
            if (user == null)
            {
                MessageBox.Show("No se ha iniciado sesión. Por favor, inicie sesión.");
                return;
            }

            // Aquí pasas el objeto sesión a la siguiente ventana
            Pantalla_Butacas patallaButacas = new Pantalla_Butacas(mainScreen, this, sesion);
            patallaButacas.Show();
            this.Hide();
        }

        private void Volver_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close(); // Cierra la ventana actual
            screen.Visibility = Visibility.Visible; // Muestra la ventana principal
        }
    }
}
