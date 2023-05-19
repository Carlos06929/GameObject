using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Acciones
{
    class Libreto
    {
        public List<Escena> escenas { get; set; }

        public Libreto()
        {
            escenas = new List<Escena>();
        }

        public Libreto(List<Escena> escenas)
        {
            this.escenas = escenas;
        }

        public Escena GetEscena(int i)
        {
            return escenas.ElementAt(i);
        }


        public void SetEscena(Escena e)
        {
            escenas.Add(e);
        }
    }
}
