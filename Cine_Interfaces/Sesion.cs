using System;

namespace Cine_Interfaces
{
    public class Sesion
    {
        private int codSesion;
        private string pelicula;
        private string cine;
        private int hora;
        private int minutos;

        // Constructor
        public Sesion(int codSesion, string pelicula, string cine, int hora, int minutos)
        {
            this.codSesion = codSesion;
            this.pelicula = pelicula;
            this.cine = cine;
            this.hora = hora;
            this.minutos = minutos;
        }

        public int GetCodSesion()
        {
            return codSesion;
        }

        // Getter para 'Pelicula'
        public string GetPelicula()
        {
            return pelicula;
        }

        // Getter para 'Cine'
        public string GetCine()
        {
            return cine;
        }

        // Getter para 'Hora'
        public int GetHora()
        {
            return hora;
        }

        // Getter para 'Minutos'
        public int GetMinutos()
        {
            return minutos;
        }
    }
}
