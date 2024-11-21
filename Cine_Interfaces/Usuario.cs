using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cine_Interfaces
{
    public class Usuario
    {
        // Campos privados
        private string nombre;
        private string password;

        // Constructor
        public Usuario(string nombre, string password)
        {
            this.nombre = nombre;
            this.password = password;
        }

        public Usuario(string nombre)
        {
            this.nombre = nombre;
        }

        // Getters y Setters

        // Getter y Setter para 'nombre'
        public string GetNombre()
        {
            return nombre;
        }

        // Getter y Setter para 'password'
        public string GetPassword()
        {
            return password;
        }
    }
}
