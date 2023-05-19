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
        Libreto libreto;
        Thread hilo;
        bool sw;

        public ControladorAnimacion()
        {
            this.libreto = new Libreto(this.getEscena());
            //this.guion = new Guion();
            //this.guion.SetEscena(Escena.DeserializeJsonFile("escena.json"));
            hilo = new Thread(new ThreadStart(play));
            sw = true;
        }
        public void iniciarAnimacion()
        {
            if (sw)
            {
                hilo.Start();
            }
            else
            {
                sw = true;
            }
        }

        public void play()
        {
            int dur = 10000;
            int tiempoInicial = Environment.TickCount & Int32.MaxValue;
            //int tiempo = Environment.TickCount & Int32.MaxValue;
            //int tiempoFinal = tiempo + dur;
            int tiempoActual = 0;
            //int i = 0;
            Escena escena = this.libreto.escenas.ElementAt(0);
            Accion accion = this.libreto.escenas.ElementAt(0).acciones.ElementAt(0);
            //Accion.SerializeJsonFile("accion.json", accion);
            //Escena.SerializeJsonFile("escena.json", escena);

            do
            {
                //Console.WriteLine(tiempoActual);
                for (int i = 0; i < escena.getCantidad(); i++)
                {
                    tiempoActual = (Environment.TickCount & Int32.MaxValue) - tiempoInicial;
                    //Console.WriteLine(tiempoActual);
                    accion = escena.getAccion(i);
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
                    for (int i = 0; i < escena.getCantidad(); i++)
                        escena.getAccion(i).tiempoSiguiente = escena.getAccion(i).tiempoI;
                }
                while (!sw)
                {
                }

            } while (tiempoActual <= dur);
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
                    Game.escenario1.Trasladar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).Trasladar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Trasladar(par[0], par[1], par[2]);
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
                    Game.escenario1.Rotar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).Rotar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Rotar(par[0], par[1], par[2]);
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
                    Game.escenario1.Escalar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 1)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).Escalar(par[0], par[1], par[2]);
                }
                else if (a.tipoObjeto == 2)
                {
                    Game.escenario1.GetObjeto(a.nombreObjeto[0]).GetCara(a.nombreObjeto[1]).Escalar(par[0], par[1], par[2]);
                }
            }
        }

        public List<Escena> getEscena()
        {

            //Accion p = new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0}, new List<float>() { 0, -1, 0 }, 0, 500, 1, 12) ;

            Escena e = new Escena("moverAuto", new List<Accion>()
            {
                new Accion(new List<string>() { "rueda1" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 1.9f, 0, 0 }, 0, 10000, 1, 1000),
                new Accion(new List<string>() { "rueda2" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 1.9f, 0, 0 }, 0, 10000, 1, 1000),
                new Accion(new List<string>() { "rueda3" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 1.4f, 0, 0 }, 0, 10000, 1, 1000),
                new Accion(new List<string>() { "rueda4" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 1.4f, 0, 0 }, 0, 10000, 1, 1000),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 0, 3000, 0, 100),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 3000, 5000, 0, 100),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 5000, 7000, 0, 100),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.1f, 0 }, 5000, 7000, 0, 100),
                new Accion(new List<string>() { "" }, new List<byte>() { 0, 1, 0 }, new List<float>() { 0, 1.01f, 0 }, 7000, 10000, 0, 300),
                

             });
            List<Escena> lista = new List<Escena>() { e };
            return lista;
        }
    }
}
