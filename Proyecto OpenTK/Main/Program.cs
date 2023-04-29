using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using Proyecto_OpenTK.Figuras;

namespace Proyecto_OpenTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Game windows = new Game(1024, 680);
            windows.Run(1.0 / 60.0);
            
            
        }
    }
}
