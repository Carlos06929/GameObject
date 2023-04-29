using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Figuras
{
    class Objeto
    {

        public List<Cara> caras { get; set; }
        public Point centro { get; set; }
        public string nombre { get; set; }

        [JsonConstructor]
        public Objeto()
        {
            this.caras = new List<Cara>();
            this.centro = new Point(0f, 0f, 0f);
            this.nombre = "";
        }

        public Objeto(List<Cara> caras, Point centro, string nombre)
        {
            this.caras = caras;
            this.centro = centro;
            
            this.nombre = nombre;
        }

        public List<Cara> getCaras()
        {
            return this.caras;
        }

        public void setCaras(List<Cara> caras)
        {
            this.caras = caras;
        }

        public Point getCentro()
        {
            return this.centro;
        }

        public void setCentro(Point centro)
        {
            this.centro = centro;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public void agregarCara(Cara cara)
        {
            this.caras.Add(cara);
        }

        public void eliminarCara(int posicion)
        {
            this.caras.RemoveAt(posicion);
        }

        public void dibujar()
        {

            foreach (Cara cara in this.caras)
            {
                
                cara.dibujar(this.centro);
            }
        }

    }
}
