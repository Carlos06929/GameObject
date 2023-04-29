using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Graphics;
using Proyecto_OpenTK.Figuras;

using System.IO;

using System.Text.Json;
using Point = Proyecto_OpenTK.Figuras.Point;

namespace Proyecto_OpenTK
{
    class Game : GameWindow
    {
        int angle;
        int s;

        float speed = 0.2f;

        Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 front = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);

        float largo = 80;
        float alto = 80;
        float ancho = 80;

        Escenario escenario;

        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Programacion Grafica")
        {
            //VSync = VSyncMode.On;

            //angle = 1;
            //s = 1;                                 
        }


        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Indigo);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Ortho(-300, 300, -300, 300, -300, 300);



            base.OnLoad(e);
        }
        bool a = true;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);

            /*Matrix4 modelview = Matrix4.LookAt(position, position + front, up);            
            GL.LoadMatrix(ref modelview);
            
            GL.Translate(0.0f, 0.0f, -15.0f);

            angle = angle + 5;

            GL.PushMatrix();
            GL.Rotate(s, 0.0f, 1.0f, 0.0f);
            GL.Scale(1.5f, 1.5f, 1.5f);
                       
            GL.Color3(0.0f, 2.0f, 1.0f);            
            GL.PopMatrix();
           
            GL.PushMatrix();
            GL.Translate(5.0f, 0.0f, 0.0f);
            GL.Scale(1.5f, 1.5f, 1.5f);                                 

            GL.PopMatrix();*/


            //Dictionary<string, Objeto> objetos = this.listaDeObjetos();
            //this.escenario = new Escenario(objetos);
            //escenario.dibujar();

            string fileName = "escenario3.json";
            string fileJson = File.ReadAllText(fileName);
            this.escenario = JsonSerializer.Deserialize<Escenario>(fileJson);
            escenario.dibujar();

            //escenario.Dibujar();
            
              ///  string fileName = "escenario3.json";
               // string fileJson = JsonSerializer.Serialize(escenario);
              //  File.WriteAllText(fileName, fileJson);
             
            

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        /*protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, this.Width, this.Height);

            base.OnResize(e);
            

            float aspect_ratio = Width / (float)Height;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            //Matrix4 projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView((float)(Math.PI * 57 / 180.0), aspect_ratio, 1.0f, 3048 - 1848);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }*/

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            GL.Rotate(1.0f, 0.0f, 0.1f, 0.0f);

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Key.ControlLeft))
            {
                s = s + 1;
            }

           

            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.W))
            {
                position += front * speed;
            }

            if (input.IsKeyDown(Key.S))
            {
                position -= front * speed;
            }

            if (input.IsKeyDown(Key.A))
            {
                position -= Vector3.Normalize(Vector3.Cross(front, up)) * speed;
            }

            if (input.IsKeyDown(Key.D))
            {
                position += Vector3.Normalize(Vector3.Cross(front, up)) * speed;
            }

            if (input.IsKeyDown(Key.Space))
            {
                position += up * speed;
            }

            if (input.IsKeyDown(Key.LShift))
            {
                position -= up * speed;
            }

            if (state.IsKeyDown(Key.Escape))
            {
                Exit();
            }


            base.OnUpdateFrame(e);
        }

        public Dictionary<string, Objeto> listaDeObjetos()
        {
            //PIRAMIDE
            List<Point> vertices1 = new List<Point>();
            vertices1.Add(new Point(0f, 100f, 100f));
            vertices1.Add(new Point(-100f, 0f, 100f));
            vertices1.Add(new Point(100f, 0f, 100f));

            List<Point> vertices2 = new List<Point>();
            vertices2.Add(new Point(0f, 100f, -100f));
            vertices2.Add(new Point(-100f, 0f, -100f));
            vertices2.Add(new Point(100f, 0f, -100f));

            List<Point> vertices3 = new List<Point>();
            vertices3.Add(new Point(0f, 100f, 100f));
            vertices3.Add(new Point(-100f, 0f, 100f));
            vertices3.Add(new Point(-100f, 0f, -100f));
            vertices3.Add(new Point(0f, 100f, -100f));


            List<Point> vertices4 = new List<Point>();
            vertices4.Add(new Point(100f, 0f, 100f));
            vertices4.Add(new Point(0f, 100f, 100f));
            vertices4.Add(new Point(0f, 100f, -100f));
            vertices4.Add(new Point(100f, 0f, -100f));

            List<Point> vertices5 = new List<Point>();
            vertices5.Add(new Point(-100, -100, 100));
            vertices5.Add(new Point(100, -100f, 100));
            vertices5.Add(new Point(100, 100, 100));
            vertices5.Add(new Point(-100, 100, 100));

            List<Point> vertices6 = new List<Point>();
            vertices6.Add(new Point(-100, -100, -100));
            vertices6.Add(new Point(-100, -100, 100));
            vertices6.Add(new Point(-100, 100, 100));
            vertices6.Add(new Point(-100, 100, -100));

            List<Point> vertices7 = new List<Point>();
            vertices7.Add(new Point(-100f, -100f, -100f));
            vertices7.Add(new Point(100f, -100f, -100f));
            vertices7.Add(new Point(100f, -100f, 100f));
            vertices7.Add(new Point(-100f, -100f, 100f));

            List<Point> vertices8 = new List<Point>();
            vertices8.Add(new Point(-100f, 100f, -100f));
            vertices8.Add(new Point(-100f, 100f, 100f));
            vertices8.Add(new Point(100f, 100f, 100f));
            vertices8.Add(new Point(100f, 100f, -100f));

            List<Point> vertices9 = new List<Point>();
            vertices9.Add(new Point(100f, 100f, -100f));
            vertices9.Add(new Point(100f, 100f, 100f));
            vertices9.Add(new Point(100f, -100f, 100f));
            vertices9.Add(new Point(100f, -100f, -100f));


            List<Point> vertices10 = new List<Point>();
            vertices10.Add(new Point(-100f, -100f, -100f));
            vertices10.Add(new Point(-100f, 100f, -100f));
            vertices10.Add(new Point(100f, 100f, -100f));
            vertices10.Add(new Point(100f, -100f, -100f));


            List<Cara> caras = new List<Cara>();
            caras.Add(new Cara(vertices1, new Point(1.0f, 1.0f, 1.0f), "Violet"));
            caras.Add(new Cara(vertices2, new Point(1.0f, 1.0f, 1.0f), "Red"));
            caras.Add(new Cara(vertices3, new Point(1.0f, 1.0f, 1.0f), "LightBlue"));
            caras.Add(new Cara(vertices4, new Point(1.0f, 1.0f, 1.0f), "LightYellow"));
            caras.Add(new Cara(vertices5, new Point(1.0f, -100f, 1.0f), "Brown"));
            caras.Add(new Cara(vertices6, new Point(1.0f, -100f, 1.0f), "Black"));
            caras.Add(new Cara(vertices7, new Point(1.0f, -1000f, 1.0f), "BlueViolet"));
            caras.Add(new Cara(vertices8, new Point(1.0f, -100f, 1.0f), "LightCoral"));
            caras.Add(new Cara(vertices9, new Point(1.0f, -100f, 1.0f), "Orange"));
            caras.Add(new Cara(vertices10, new Point(1.0f, -100f, 1.0f), "Green"));


            List<Cara> caras2 = new List<Cara>();
            caras2.Add(new Cara(vertices1, new Point(1.0f, -1.0f, 1.0f), "Black"));
            caras2.Add(new Cara(vertices2, new Point(1.0f, -1.0f, 1.0f), "FloralWhite"));
            caras2.Add(new Cara(vertices3, new Point(1.0f, -1.0f, 1.0f), "LightSeaGreen"));
            caras2.Add(new Cara(vertices4, new Point(1.0f, -1.0f, 1.0f), "Blue"));

            //CUBO
            vertices5 = new List<Point>();
            vertices5.Add(new Point(-50, 0, 100));
            vertices5.Add(new Point(0, 0f, 100));
            vertices5.Add(new Point(0, 50f, 100));
            vertices5.Add(new Point(-50, 50f, 100));

            vertices6 = new List<Point>();
            vertices6.Add(new Point(-50, 0, 0));
            vertices6.Add(new Point(0, 0, 0));
            vertices6.Add(new Point(0, 50, 0));
            vertices6.Add(new Point(-50, 50, 0));

            vertices7 = new List<Point>();
            vertices7.Add(new Point(0f, 0f, 100f));
            vertices7.Add(new Point(0f, 0f, 0f));
            vertices7.Add(new Point(0f, 50f, 0f));
            vertices7.Add(new Point(0f, 50f, 100f));


            vertices8 = new List<Point>();
            vertices8.Add(new Point(-50f, 0f, 100f));
            vertices8.Add(new Point(-50f, 0f, 0f));
            vertices8.Add(new Point(-50f, 50f, 0f));
            vertices8.Add(new Point(-50f, 50f, 100f));

            vertices9 = new List<Point>();
            vertices9.Add(new Point(-50f, 50f, 100f));
            vertices9.Add(new Point(-50f, 50f, 0f));
            vertices9.Add(new Point(0f, 50f, 0f));
            vertices9.Add(new Point(0f, 50f, 100f));


            vertices10 = new List<Point>();
            vertices10.Add(new Point(-100f, -100f, -100f));
            vertices10.Add(new Point(-100f, 100f, -100f));
            vertices10.Add(new Point(100f, 100f, -100f));
            vertices10.Add(new Point(100f, -100f, -100f));

            List<Cara> caras3 = new List<Cara>();
            caras3.Add(new Cara(vertices5, new Point(0f, 0f, 0f), "Violet"));
            caras3.Add(new Cara(vertices6, new Point(0f, 0f, 0f), "LightSkyBlue"));
            caras3.Add(new Cara(vertices7, new Point(0f, 0f, 0f), "Red"));
            caras3.Add(new Cara(vertices8, new Point(0f, 0f, 0f), "Pink"));
            caras3.Add(new Cara(vertices9, new Point(0f, 0f, 0f), "White"));
            //caras3.Add(new Cara(vertices10,   new Point(0f, 0f, 0f), "Azure));


            ///////////////////////////////////////
            ///
            //CUBO
            vertices5 = new List<Point>();
            vertices5.Add(new Point(-25, 0, 50));
            vertices5.Add(new Point(0, 0f, 50));
            vertices5.Add(new Point(0, 25f, 50));
            vertices5.Add(new Point(-25, 25f, 50));

            vertices6 = new List<Point>();
            vertices6.Add(new Point(-25, 0, 0));
            vertices6.Add(new Point(0, 0, 0));
            vertices6.Add(new Point(0, 25, 0));
            vertices6.Add(new Point(-25, 25, 0));

            vertices7 = new List<Point>();
            vertices7.Add(new Point(0f, 0f, 50));
            vertices7.Add(new Point(0f, 0f, 0f));
            vertices7.Add(new Point(0f, 25, 0f));
            vertices7.Add(new Point(0f, 25, 50));


            vertices8 = new List<Point>();
            vertices8.Add(new Point(-25, 0f, 50));
            vertices8.Add(new Point(-25, 0f, 0f));
            vertices8.Add(new Point(-25, 25, 0f));
            vertices8.Add(new Point(-25, 25, 50));

            vertices9 = new List<Point>();
            vertices9.Add(new Point(-25, 25, 50));
            vertices9.Add(new Point(-25, 25, 0f));
            vertices9.Add(new Point(0f, 25, 0f));
            vertices9.Add(new Point(0f, 25, 50));


            vertices10 = new List<Point>();
            vertices10.Add(new Point(-100f, -100f, -100f));
            vertices10.Add(new Point(-100f, 100f, -100f));
            vertices10.Add(new Point(100f, 100f, -100f));
            vertices10.Add(new Point(100f, -100f, -100f));

            //caras3 = new List<Cara>();
            caras3.Add(new Cara(vertices5, new Point(-15, 50, 25), "Violet"));
            caras3.Add(new Cara(vertices6, new Point(-15, 50, 25), "LightSkyBlue"));
            caras3.Add(new Cara(vertices7, new Point(-15, 50, 25), "Red"));
            caras3.Add(new Cara(vertices8, new Point(-15, 50, 25), "Pink"));
            caras3.Add(new Cara(vertices9, new Point(-15, 50, 25), "White"));
            //caras3.Add(new Cara(vertices10,   new Point(0f, 0f, 0f), "Azure));


            ///////////////////////////////////////
            ///
            //CUBO
            vertices5 = new List<Point>();
            vertices5.Add(new Point(-10, 0, 22));
            vertices5.Add(new Point(0, 0f, 22));
            vertices5.Add(new Point(0, 25f, 22));
            vertices5.Add(new Point(-10, 25f, 22));

            vertices6 = new List<Point>();
            vertices6.Add(new Point(-10, 0, 0));
            vertices6.Add(new Point(0, 0, 0));
            vertices6.Add(new Point(0, 25, 0));
            vertices6.Add(new Point(-10, 25, 0));

            vertices7 = new List<Point>();
            vertices7.Add(new Point(0f, 0f, 22));
            vertices7.Add(new Point(0f, 0f, 0f));
            vertices7.Add(new Point(0f, 25, 0f));
            vertices7.Add(new Point(0f, 25, 22));


            vertices8 = new List<Point>();
            vertices8.Add(new Point(-10, 0f, 22));
            vertices8.Add(new Point(-10, 0f, 0f));
            vertices8.Add(new Point(-10, 25, 0f));
            vertices8.Add(new Point(-10, 25, 22));

            vertices9 = new List<Point>();
            vertices9.Add(new Point(-10, 25, 22));
            vertices9.Add(new Point(-10, 25, 0f));
            vertices9.Add(new Point(0f, 25, 0f));
            vertices9.Add(new Point(0f, 25, 22));


            vertices10 = new List<Point>();
            vertices10.Add(new Point(-100f, -100f, -100f));
            vertices10.Add(new Point(-100f, 100f, -100f));
            vertices10.Add(new Point(100f, 100f, -100f));
            vertices10.Add(new Point(100f, -100f, -100f));

            //caras3 = new List<Cara>();
            caras3.Add(new Cara(vertices5, new Point(5, -10, 5), "Black"));
            caras3.Add(new Cara(vertices6, new Point(5, -10, 5), "Black"));
            caras3.Add(new Cara(vertices7, new Point(5, -10, 5), "Brown"));
            caras3.Add(new Cara(vertices8, new Point(5, -10, 5), "Brown"));
            caras3.Add(new Cara(vertices9, new Point(5, -10, 5), "White"));

            caras3.Add(new Cara(vertices5, new Point(-45, -10, 5), "Black"));
            caras3.Add(new Cara(vertices6, new Point(-45, -10, 5), "Black"));
            caras3.Add(new Cara(vertices7, new Point(-45, -10, 5), "Brown"));
            caras3.Add(new Cara(vertices8, new Point(-45, -10, 5), "Brown"));
            caras3.Add(new Cara(vertices9, new Point(-45, -10, 5), "White"));

            caras3.Add(new Cara(vertices5, new Point(5, -10, 70), "Black"));
            caras3.Add(new Cara(vertices6, new Point(5, -10, 70), "Black"));
            caras3.Add(new Cara(vertices7, new Point(5, -10, 70), "Brown"));
            caras3.Add(new Cara(vertices8, new Point(5, -10, 70), "Brown"));
            caras3.Add(new Cara(vertices9, new Point(5, -10, 70), "White"));

            caras3.Add(new Cara(vertices5, new Point(-45, -10, 70), "Black"));
            caras3.Add(new Cara(vertices6, new Point(-45, -10, 70), "Black"));
            caras3.Add(new Cara(vertices7, new Point(-45, -10, 70), "Brown"));
            caras3.Add(new Cara(vertices8, new Point(-45, -10, 70), "Brown"));
            caras3.Add(new Cara(vertices9, new Point(-45, -10, 70), "White"));

            Dictionary<string, Objeto> objetos = new Dictionary<string, Objeto>();
            objetos.Add("Piramide 1", new Objeto(caras, new Point(-100f, 0f, 0f), "Piramide 1"));
            //objetos.Add("Piramide 2", new Objeto(caras2, new Point(-100f, 100f, 0f), "Piramide 2"));
            objetos.Add("Cubo 1", new Objeto(caras3, new Point(100f, -190f, 0f), "Cubo 1"));

            return objetos;
        }
    }

    /*
     
     
     */
}
