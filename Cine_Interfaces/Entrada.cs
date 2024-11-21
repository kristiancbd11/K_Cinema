using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine_Interfaces
{
    // Clase Entrada
    public class Entrada
    {
        public string NombreUser { get; set; }
        public string Pelicula { get; set; }
        public string Cine { get; set; }
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public int NumButaca { get; set; }

        public Entrada(string nombreUser, string pelicula, string cine, int hora, int minutos, int numButaca)
        {
            NombreUser = nombreUser;
            Pelicula = pelicula;
            Cine = cine;
            Hora = hora;
            Minutos = minutos;
            NumButaca = numButaca;
        }
    }
}
