using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Proyecto_OpenTK.Figuras
{
    class Auto
    {
        private double angle, rx, ry, rz;
        private double x, y, z, anchoX, altoY, largoZ;
        Color pintura;
        //Hashtable auto = new Hashtable();

        public Auto(Color color, double x = -14, double y = -4, double z = 10, double ancho = 1.0, double alto = 1.0, double largo = 1.0)
        {
            this.x = x; this.y = y; this.z = z;
            this.anchoX = ancho; this.altoY = alto; this.largoZ = largo;
            angle = 0; rx = 0; ry = 0; rz = 0;
            pintura = color;
        }

        public void Dibujar()
        {
            Color color = pintura;
            GL.PushMatrix();//Toda la Silla
            GL.Scale(this.anchoX, this.altoY, this.largoZ);//Variar Escala
            GL.Translate(this.x, this.y, this.z);          //Variar Posicion
            GL.Rotate(angle, rx, ry, rz);                  //Variar Rotacion
            BaseSuperior(pintura);
            BaseAuto(pintura);
            Rueda1(pintura);
            Rueda2(pintura);
            Rueda3(pintura);
            Rueda4(pintura);

            GL.PopMatrix();

        }

        private void BaseSuperior(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(2, 1, 3);
            //Piramide3D(color);
            Cubo3D(color);
            GL.PopMatrix();
        }

        private void BaseAuto(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(2,1.5,5);
            GL.Translate(0, -1.5, 0);
            //Piramide3D(color);
            Cubo3D(color);
            GL.PopMatrix();
        }

        private void Rueda1(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(0.5,1,0.8);
            GL.Translate(-3.2,-3.2,-4);
            //Piramide3D(color);
            Cubo3D(color);
            //Rueda3D(color);
            GL.PopMatrix();
        }

        private void Rueda2(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(0.5,1,0.8);
            GL.Translate(3.2,-3.2, -4);
            //Piramide3D(color);
            Cubo3D(color);
            GL.PopMatrix();
        }

        private void Rueda3(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(0.5,1,0.8);
            GL.Translate(-3.2,-3.2,4);
            //Piramide3D(color);
            Cubo3D(color);
            GL.PopMatrix();
        }

        private void Rueda4(Color color)
        {
            GL.PushMatrix();//Tabla base
            GL.Scale(0.5,1,0.8);
            GL.Translate(3.2,-3.2,4);
            //Piramide3D(color);
            Cubo3D(color);
            GL.PopMatrix();
        }


        

        public void Rotar(double angulo, double x, double y, double z)
        {
            angle = angulo; rx = x; ry = y; rz = z;
        }
        public void Escalar(double ancho, double alto, double largo)
        {
            this.anchoX = ancho; this.altoY = alto; this.largoZ = largo;
        }
        public void Trasladar(double x, double y, double z)
        {
            this.x = x; this.y = y; this.z = z;
        }
        private void Cubo3D(Color color)
        {
            GL.Begin(PrimitiveType.Quads);
            //Tomando referencia los numeros de un Dado, tomo los sgtes lados:
            GL.Color3(color);
            //Lado #1(Frente)
            GL.Normal3(0.0, 0.0, 1.0);
            GL.Vertex3(-1.0, -1.0, 1.0);
            GL.Vertex3(1.0, -1.0f, 1.0);
            GL.Vertex3(1.0, 1.0, 1.0);
            GL.Vertex3(-1.0, 1.0, 1.0);

            //Lado #2 (Izquierda)
            GL.Normal3(-1.0, 0.0, 0.0);
            GL.Vertex3(-1.0, -1.0, -1.0);
            GL.Vertex3(-1.0, -1.0, 1.0);
            GL.Vertex3(-1.0, 1.0, 1.0);
            GL.Vertex3(-1.0, 1.0, -1.0);

            //Lado #3 (Inferior)
            GL.Normal3(0.0, -1.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            //Lado #4 (Superior)
            GL.Normal3(0.0, 1.0, 0.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            //Lado #5 (Derecha)
            GL.Normal3(1.0, 0.0, 0.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            //Lado #6 (Atras)
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.End();
        }

        private void Rueda3D(Color color) {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(Color.Gray);
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(0, 0, 0);

            for (int i = 0; i <= 360; i += 10)
            {
                GL.Color3(Color.White);
                GL.Vertex3(Math.Cos(i * Math.PI / 180.0), Math.Sin(i * Math.PI / 180.0), 0.0);

                GL.Color3(Color.DarkGray);
                GL.Vertex3(Math.Cos(i * Math.PI / 180.0) * 0.8, Math.Sin(i * Math.PI / 180.0) * 0.8, 0.0);
            }

            GL.End();

            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(Color.DarkGray);

            for (int i = 0; i <= 360; i += 10)
            {
                GL.Vertex3(Math.Cos(i * Math.PI / 180.0), Math.Sin(i * Math.PI / 180.0), 0.0);
                GL.Vertex3(Math.Cos(i * Math.PI / 180.0) * 0.8, Math.Sin(i * Math.PI / 180.0) * 0.8, 0.0);
            }

            GL.End();
        }
        private void Piramide3D(Color color)
        {
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Yellow);
            GL.Normal3(0.0, 0.0, 1.0);
            GL.Vertex3(0f, 1f, 1f);
            GL.Vertex3(-1f, 0f, 1.0f);
            GL.Vertex3(1f, 0f, 1.0f); 

            GL.Color3(Color.White);
            GL.Normal3(0.0, 0.0, -1.0);
            GL.Vertex3(0f, 1f, -1f);
            GL.Vertex3(-1f, 0f, -1.0f);
            GL.Vertex3(1f, 0f, -1.0f);

            GL.End();

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.Green);
            GL.Normal3(0.0, 0.0, 1.0);
            GL.Vertex3(0f, 1f, 1f);
            GL.Vertex3(-1f, 0f, 1.0f);
            GL.Vertex3(-1f, 0f, -1.0f);
            GL.Vertex3(0f, 1f, -1f);

            GL.Color3(Color.White);
            GL.Normal3(0.0, 0.0, 1.0);
            GL.Vertex3(1f, 0f, 1.0f);
            GL.Vertex3(0f, 1f, 1f);
            GL.Vertex3(0f, 1f, -1f);
            GL.Vertex3(1f, 0f, -1.0f);



            GL.End();
        }

    }
}
