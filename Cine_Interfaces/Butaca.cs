using System;

namespace Cine_Interfaces
{
    // Clase Butaca
    public class Butaca
    {
        private int id;
        private bool estado;
        private string fila;
        private int columna;

        public Butaca(int id, bool estado, string fila, int columna)
        {
            this.id = id;
            this.estado = estado;
            this.fila = fila;
            this.columna = columna;
        }

        // Métodos getter
        public int GetId()
        {
            return id;
        }

        public bool GetEstado()
        {
            return estado;
        }

        public string GetFila()
        {
            return fila;
        }

        public int GetColumna()
        {
            return columna;
        }
    }
}
