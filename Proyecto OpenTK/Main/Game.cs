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

namespace Proyecto_OpenTK
{
    class Game : GameWindow
    {
        static float scale = 50.0f;
        EventosTeclado ev;
        Auto auto=new Auto(Color.Orange,6,0,0);
        Casa casa = new Casa(Color.Blue, -5, 0, 0);

        static double angle = 0;
        public Game(int width, int height)
             : base(width, height)
        {
            ev = new EventosTeclado();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.Black);
            //Perspectiva
            GL.Enable(EnableCap.DepthTest);

            //Iluminacion
            Iluminar();

            base.OnLoad(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            ev.CambioDeEscala(input, ref scale);

            base.OnKeyDown(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            LoadSpace();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            auto.Dibujar();
            casa.Dibujar();
            VariarAngulo(ref angle, 1);
            auto.Rotar(angle, 0, 1, 0) ;
            casa.Rotar(angle, 1, 0, 0);

            this.SwapBuffers();
        }






        private void VariarAngulo(ref double angulo, double speed)
        {
            //ESTO AUMENTA O REDUCE EL ANGULO A ROTAR
            angulo += speed *0.1;
            if (angulo > 360 || angulo < -360)
            {
                angulo = 0;
            }
        }





        private void LoadSpace()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //GL.Ortho(-escala, escala, -escala, escala, -escala, escala); Este es solo para 2D
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), base.Width / base.Height, 1.0f, 100.0f);
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Translate(0.0, 0.0, -scale);
        }

        private void Iluminar()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);

            float[] luz_posicion = { 20, 20, 80 };
            float[] luz_ambiente = { 0.5f, 0.0f, 0.0f };
            GL.Light(LightName.Light0, LightParameter.Position, luz_posicion);
            GL.Light(LightName.Light0, LightParameter.Ambient, luz_ambiente);
            
            GL.Enable(EnableCap.Light0);

        }
    }
}
