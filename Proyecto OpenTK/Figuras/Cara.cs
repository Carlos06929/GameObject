using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Text.Json.Serialization;
using System.Windows;

namespace Proyecto_OpenTK.Figuras
{
    class Cara
    {
        public List<Point> vertices { get; set; }
        public Point centro { get; set; }
        public string color { get; set; }

        public Cara()
        {
            this.vertices = new List<Point>();
            this.centro = new Point(0f, 0f, 0f); ;
            this.color = "White";
        }

        [JsonConstructor]
        public Cara(List<Point> vertices, Point centro, string color)
        {
            this.vertices = vertices;

            this.centro = centro;
            this.color = color;

           
        }

        public List<Point> getVertices()
        {
            return this.vertices;
        }

        public void setVertices(List<Point> vertices)
        {
            this.vertices = vertices;
        }

        public Point getCentro()
        {
            return this.centro;
        }

        public void setCentro(Point centro)
        {
            this.centro = centro;
        }

        public string getColor()
        {
            return this.color;
        }

        public void setColor(string color)
        {
            this.color = color;
        }

        public void agregarVertice(Point vertice)
        {
            this.vertices.Add(vertice);
        }

        public void eliminarVertice(int posicion)
        {
            this.vertices.RemoveAt(posicion);
        }

        public void dibujar(Point centroPadre)
        {


            

            Console.WriteLine(this.color);
            GL.Color3(Color.FromName(color));
            GL.Begin(PrimitiveType.Polygon);

            foreach (Point vertice in this.vertices)
            {

                GL.Vertex3(
                    vertice.X + this.centro.X+centroPadre.X,
                    vertice.Y + this.centro.Y+ centroPadre.Y,
                    vertice.Z + this.centro.Z+ centroPadre.Z
                    );
            }

            GL.End();
            
        }

    }
}
