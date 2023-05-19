using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Proyecto_OpenTK
{
    class EventosTeclado 
    {
        public EventosTeclado()
        {       }

        public void CambioDeEscala(KeyboardState e, ref float scale)
        {
            if (e.IsKeyDown(Key.KeypadAdd) && scale > 0)
            {
                scale--;
            }
            if (e.IsKeyDown(Key.KeypadMinus) && scale < 300)
            {
                scale++;
            }
            if (e.IsKeyDown(Key.A) && scale < 300)
            {
                ;
            }
        }

    }


    
}
