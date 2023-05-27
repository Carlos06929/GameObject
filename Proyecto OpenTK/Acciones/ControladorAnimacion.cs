using Proyecto_OpenTK.Figuras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Acciones
{
    class ControladorAnimacion
    {
        private readonly Libreto libreto;
        Task tarea;
        bool sw;
        Escenario escenario;

        public ControladorAnimacion(Escenario escenario,string nombre)
        {
            this.libreto = Libreto.DeserializeJsonFile(nombre);
            this.escenario = escenario;
            tarea = new Task(play);

            sw = true;
        }

        public ControladorAnimacion(Escenario escenario)
        {
            this.libreto = new Libreto(this.getAcciones());
          
            this.escenario = escenario;
            Libreto.SerializeJsonFile("libreto2.json", libreto);
            tarea = new Task(play);

            sw = true;
        }
        public void iniciarAnimacion()
        {
            if (sw)
            {
                tarea.Start();
            }
            else
            {
                sw = true;
            }
        }

        public void play()
        {
            int dur = 25000;
            int tiempoInicial = Environment.TickCount & Int32.MaxValue;
            int tiempoActual = 0;
            Accion accion;
            /*int tiempo = Environment.TickCount & Int32.MaxValue;
            int tiempoFinal = tiempo + dur;
            int i = 0;
            Libreto libreto = this.libreto;
            Accion accion; //= this.libreto.acciones.ElementAt(0);
            Accion.SerializeJsonFile("../../archivos/" + "accion.json", accion);*/


            do
            {
                //Console.WriteLine(tiempoActual);
                for (int i = 0; i < this.libreto.getCantidad(); i++)
                {
                    tiempoActual = (Environment.TickCount & Int32.MaxValue) - tiempoInicial;
                    //Console.WriteLine(tiempoActual);
                    accion = this.libreto.getAccion(i);
                    if (tiempoActual >= accion.tiempoI && tiempoActual <= accion.tiempoF)
                    {
                        reproducirAccion(accion, tiempoActual);
                    }
                }
                if (tiempoActual >= dur && sw)
                {
                    sw = false;
                    tiempoActual = 0;
                    tiempoInicial = Environment.TickCount & Int32.MaxValue;
                    for (int i = 0; i < this.libreto.getCantidad(); i++)
                        this.libreto.getAccion(i).tiempoSiguiente = this.libreto.getAccion(i).tiempoI;
                }
                while (!sw)
                {
                }
                

            } while (tiempoActual <= dur);
            Console.WriteLine(tiempoInicial);
        }


        public void reproducirAccion(Accion a, int tiempoActual)
        {

            if (a.accion[0] == 1) //escalar
            {
                escalar(a, a.parametros, tiempoActual);
            }
            if (a.accion[1] == 1)//rotar
            {
                rotar(a, a.parametros, tiempoActual);
            }
            if (a.accion[2] == 1)//trasladar
            {
                trasladar(a, a.parametros, tiempoActual);
            }
            //Formulario.escenario.Dibujar();
        }



        public void trasladar(Accion a, List<float> par, int tiempoActual)
        {
            float tiempo = a.tiempoF - a.tiempoI;
            float cada = tiempo / a.cuantas;
            //Console.WriteLine(cada);
            //if (tiempoActual % cada == 0 && tiempoActual >= a.tiempoSiguiente)
            if (tiempoActual >= a.tiempoSiguiente)
            {
                a.tiempoSiguiente += (long)cada;
                //Console.WriteLine(tiempoActual);
                if (a.tipoObjeto == 0)
                {
                    escenario.Trasladar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).Trasladar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Trasladar(par[0], par[1], par[2]);
                }
            }

        }

        public void rotar(Accion a, List<float> par, int tiempoActual)
        {
            float tiempo = a.tiempoF - a.tiempoI;
            float cada = tiempo / a.cuantas;
            //Console.WriteLine(cada);
            //if (tiempoActual % cada == 0 && tiempoActual >= a.tiempoSiguiente)
            if (tiempoActual >= a.tiempoSiguiente)
            {
                a.tiempoSiguiente += (long)cada;
                //Console.WriteLine(tiempoActual);
                if (a.tipoObjeto == 0)
                {
                    escenario.Rotar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).Rotar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Rotar(par[0], par[1], par[2]);
                }
            }
        }

        public void escalar(Accion a, List<float> par, int tiempoActual)
        {
            float tiempo = a.tiempoF - a.tiempoI;
            float cada = tiempo / a.cuantas;
            //Console.WriteLine(cada);
            //if (tiempoActual % cada == 0 && tiempoActual >= a.tiempoSiguiente)
            if (tiempoActual >= a.tiempoSiguiente)
            {
                a.tiempoSiguiente += (long)cada;
                //Console.WriteLine(tiempoActual);
                if (a.tipoObjeto == 0)
                {
                    escenario.Escalar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).Escalar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    escenario.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Escalar(par[0], par[1], par[2]);
                }
            }
        }

        public List<Accion> getAcciones()
        {

            //aceleracion calcular no psar por parametro

            //Accion p = new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0}, new List<float>() { 0, -1, 0 }, 0, 500, 1, 12) ;

            List<Accion> lista = new List<Accion>()
            {
              
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, -1.1f, 0 }, 0, 3000, 0, 75),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1f, 0 }, 5000, 7000, 0, 50),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1f, 0 }, 7000, 9000, 0, 75),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 9000, 11000, 0, 110),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 11000, 13000, 0, 80),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1f, 0 }, 13000, 15000, 0, 65),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1f, 0 }, 17000, 20000, 0, 75),


                new Accion(new List<string>() { "highbaseBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0.1f, 0, 0 }, 3000, 5000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0.1f, 0, 0 }, 3000, 5000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0,0, 1 }, new List<float>() { 0.01f, -0.05f, -0.011f }, 3000, 5000, 1, 20),

                new Accion(new List<string>() { "highbaseBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { -0.1f, 0, 0 }, 15000, 17000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { -0.1f, 0, 0 }, 15000, 17000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0,0, 1 }, new List<float>() { -0.01f, 0.05f, 0.011f }, 15000, 17000, 1, 20),

                new Accion(new List<string>() { "highbaseBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { -0.1f, 0, 0 }, 20000, 23000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0, 1, 0 }, new List<float>() { -0.1f, 0, 0 }, 20000, 23000, 1, 300),
                new Accion(new List<string>() { "picoBird" }, new List<byte>() { 0,0, 1 }, new List<float>() { 0, 0.05f, -0.011f }, 20000, 23000, 1, 20),






             };

            //new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, -1.1f, 0 }, 7000, 9000, 0, 75),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, -1.1f, 0 }, 9000, 11000, 0, 100),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 11000, 13000, 0, 75),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.01f, 0 }, 13000, 15000, 0, 50),


            //new Accion(new List<string>() { "rueda1" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda2" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda3" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda4" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 0, 3000, 0, 100),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 3000, 5000, 0, 100),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 5000, 7000, 0, 100),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 5000, 7000, 0, 100),
            //    new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.01f, 0 }, 7000, 10000, 0, 300),


            //aceleracion calcular no psar por parametro highbaseBird

            //Accion p = new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0}, new List<float>() { 0, -1, 0 }, 0, 500, 1, 12) ;

            //List<Accion> lista = new List<Accion>() {
            //    new Accion(new List<string>() { "rueda1" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda2" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda3" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //    new Accion(new List<string>() { "rueda4" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 3f, 0, 0 }, 0, 10000, 1, 5000),
            //};
            //int a = 100;
            //int b = 0;
            //Single x = 0.01f;
            //Single z = 0.01f;

            //float radius = 0.1f; // Radio del círculo

            //for (int i = 0; i < 360; i++)
            //{


            //    lista.Add(new Accion(new List<string>() { "carroSuperior" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 0.5f, 0 }, b, a, 1, 1));
            //    lista.Add(new Accion(new List<string>() { "carroSuperior" }, new List<byte>() { 0, 0, 1 }, new List<float>() { x, 0, z}, b, a, 1, 100));

            //    double angleRad = i * Math.PI / 180; // Convertir el ángulo a radianes
            //    x = x + (float)(radius * Math.Cos(angleRad));
            //    z = z + (float)(radius * Math.Sin(angleRad));

            //    Console.WriteLine(angleRad);

            //    b = a;
            //    a = a + 1000;
            //}







            return lista;
        }
    }
}
