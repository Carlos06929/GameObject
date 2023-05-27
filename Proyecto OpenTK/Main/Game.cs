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
using Punto = Proyecto_OpenTK.Figuras.Punto;
using Proyecto_OpenTK.Acciones;
using System.Windows.Forms;
using Proyecto_OpenTK.Main;
using Proyecto_OpenTK.Utilidades;

namespace Proyecto_OpenTK
{
    public partial class Game : GameWindow
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

        ControladorAnimacion ca;

        ControladorAnimacion ca1;

        public static Escenario escenario;
        public static Escenario escenario1;
        public static Escenario escenario2;


        public Camara cam;
        public Game(int width, int height) : base(width, height, GraphicsMode.Default, "Programacion Grafica")
        {
            //VSync = VSyncMode.On;

            //angle = 1;
            //s = 1;
            Location = new System.Drawing.Point(900, 100);
            cam = new Camara();

        }


        protected override void OnLoad(EventArgs e)
        {

            float[] luz_posicion = { 0, 100, 80 };
            float[] luz_ambiente = { 0.9f, 0.5f, 0.2f };
            GL.Light(LightName.Light0, LightParameter.Position, luz_posicion);
            GL.Light(LightName.Light0, LightParameter.Ambient, luz_ambiente);


            GL.ClearColor(Color4.Black);
            //------------------------------
            escenario = new Escenario();
            escenario1 = new Escenario();
            escenario2 = new Escenario();


            Objeto a = GetBaseCasa();
            Objeto b = GetTechoCasa();
            Objeto c = GetCarroInferior();
            Objeto d = GetCarroSuperior();
            Objeto z = GetRueda();
            Objeto f = GetRueda();
            Objeto g = GetRueda();
            Objeto h = GetRueda();
            Objeto j = GetSupport();
            Objeto k = GetSupport();



            escenario.SetObjeto("baseCasa", a);
            escenario.SetObjeto("techo", b);
            escenario.SetObjeto("supportBird", j);
            escenario.SetObjeto("supportBird2", k);

            escenario.GetObjeto("baseCasa").SetOrigen(0, 0, 0);
            escenario.GetObjeto("techo").SetOrigen(0, 0, 0);
            //escenario.GetObjeto("techo").Rotar(10, -30, 0);
            //escenario.GetObjeto("baseCasa").Rotar(10, -30, 0);
            escenario.GetObjeto("supportBird").SetOrigen(-0.1f, 20, 0f);
            escenario.GetObjeto("supportBird").Escalar(0.1f, 3f, 0.1f);
            escenario.GetObjeto("supportBird2").SetOrigen(0f, 26, 0f);
            escenario.GetObjeto("supportBird2").Escalar(0.5f, 0.1f, 0.6f);
            //escenario.GetObjeto("supportBird2").Rotar(0f, -30f, 0);
            escenario.Rotar(30f, -30f, 0);


            Objeto aa = GetBodyBird();
            Objeto ab = GetBodyBird();
            Objeto ac = GetBodyBird();
            Objeto ad = GetBodyBird();
            Objeto ae = GetBodyBird();
            Objeto af = GetTriangle();

            escenario2.Rotar(10f, -30f, 0f);
            escenario2.SetObjeto("baseBird", aa);
            escenario2.GetObjeto("baseBird").Escalar(0.3f, 0.8f, 0.3f);
            escenario2.GetObjeto("baseBird").Trasladar(0f, 30f, 0f);
            //escenario2.GetObjeto("baseBird").Rotar(10, -30, 0);

            escenario2.SetObjeto("lowbaseBird", ad);
            escenario2.GetObjeto("lowbaseBird").Escalar(0.2f, 0.2f, 0.2f);
            escenario2.GetObjeto("lowbaseBird").Trasladar(0f, 28f, 0f);
            // escenario2.GetObjeto("lowbaseBird").Rotar(10, -30, 0);

            escenario2.SetObjeto("highbaseBird", ae);
            escenario2.GetObjeto("highbaseBird").Escalar(0.2f, 0.5f, 0.5f);
            escenario2.GetObjeto("highbaseBird").Trasladar(0f, 32f, 0f);
            // escenario2.GetObjeto("lowbaseBird").Rotar(10, -30, 0);

            escenario2.SetObjeto("pataBird", ab);
            escenario2.GetObjeto("pataBird").Escalar(0.05f, 0.5f, 0.05f);
            escenario2.GetObjeto("pataBird").Trasladar(-0.5f, 27f, 0f);
            //escenario2.GetObjeto("pataBird").Rotar(10, -30, 0);

            escenario2.SetObjeto("pata2Bird", ac);
            escenario2.GetObjeto("pata2Bird").Escalar(0.05f, 0.5f, 0.05f);
            escenario2.GetObjeto("pata2Bird").Trasladar(0.5f, 27f, 0f);
            //escenario2.GetObjeto("pata2Bird").Rotar(10, -30, 0);

            escenario2.SetObjeto("picoBird", af);
            escenario2.GetObjeto("picoBird").Escalar(0.15f, 0.5f, 0.2f);
            escenario2.GetObjeto("picoBird").Trasladar(0f, 32f, 2f);
            escenario2.GetObjeto("picoBird").Rotar(90, 0, 0);
            escenario2.Rotar(30f, -30f, 0);



            escenario1.SetObjeto("carroInferior", c);
            escenario1.SetObjeto("carroSuperior", d);
            escenario1.GetObjeto("carroInferior").SetOrigen(-30, -3.5f, 0);
            escenario1.GetObjeto("carroInferior").Escalar(0.4f, 0.3f, 0.6f);
            escenario1.GetObjeto("carroSuperior").SetOrigen(-30, 1.5f, 0);
            escenario1.GetObjeto("carroSuperior").Escalar(0.2f, 0.2f, 0.3f);

            escenario1.SetObjeto("rueda1", z);
            escenario1.SetObjeto("rueda2", f);
            escenario1.SetObjeto("rueda3", g);
            escenario1.SetObjeto("rueda4", h);

            escenario1.GetObjeto("rueda1").SetOrigen(-34, -6.5f, 3);
            escenario1.GetObjeto("rueda2").SetOrigen(-26, -6.5f, 3);
            escenario1.GetObjeto("rueda3").SetOrigen(-34, -6.5f, -3);
            escenario1.GetObjeto("rueda4").SetOrigen(-26, -6.5f, -3);


            escenario1.GetObjeto("rueda1").Escalar(0.05f, 0.15f, 0.15f);
            escenario1.GetObjeto("rueda2").Escalar(0.05f, 0.15f, 0.15f);
            escenario1.GetObjeto("rueda3").Escalar(0.05f, 0.15f, 0.15f);
            escenario1.GetObjeto("rueda4").Escalar(0.05f, 0.15f, 0.15f);
            escenario1.Trasladar(0, -1f, 0);
            escenario1.Rotar(30f, -30f, 0);

            Objeto.SerializeJsonFile("baseCasa.json", a);
            Objeto.SerializeJsonFile("techoCasa.json", b);
            Objeto.SerializeJsonFile("supportBird.json", j);
            Objeto.SerializeJsonFile("supportBird2.json", k);

            Objeto.SerializeJsonFile("carroInferior.json", c);
            Objeto.SerializeJsonFile("carroSuperior.json", d);
            Objeto.SerializeJsonFile("rueda1.json", z);
            Objeto.SerializeJsonFile("rueda2.json", f);
            Objeto.SerializeJsonFile("rueda3.json", g);
            Objeto.SerializeJsonFile("rueda4.json", h);

            Objeto.SerializeJsonFile("baseBird.json", h);
            Objeto.SerializeJsonFile("lowbaseBird.json", h);
            Objeto.SerializeJsonFile("highbaseBird.json", h);
            Objeto.SerializeJsonFile("pataBird.json", h);
            Objeto.SerializeJsonFile("pata2Bird.json", h);
            Objeto.SerializeJsonFile("picoBird.json", h);


            //escenario.Escalar(1.1f, 0, 0);



            //escenario.SetObjeto("baseCasa", Objeto.DeserializeJsonFile("baseCasa.json"));
            //escenario.SetObjeto("techo", Objeto.DeserializeJsonFile("techoCasa.json"));
            //escenario1.SetObjeto("carroInferior", Objeto.DeserializeJsonFile("carroInferior.json"));
            //escenario1.SetObjeto("carroSuperior", Objeto.DeserializeJsonFile("carroSuperior.json"));
            //escenario1.SetObjeto("rueda1", Objeto.DeserializeJsonFile("rueda1.json"));
            //escenario1.SetObjeto("rueda2", Objeto.DeserializeJsonFile("rueda2.json"));
            //escenario1.SetObjeto("rueda3", Objeto.DeserializeJsonFile("rueda3.json"));
            //escenario1.SetObjeto("rueda4", Objeto.DeserializeJsonFile("rueda4.json"));


            ca = new ControladorAnimacion(escenario1, "libreto.json");
            ca.iniciarAnimacion();

            ca1 = new ControladorAnimacion(escenario2);
            ca1.iniciarAnimacion();


            base.OnLoad(e);
        }
        bool a = true;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.ColorMaterial);


            GL.Enable(EnableCap.Light0);

            GL.LoadIdentity();
            //-----------------------

            escenario.Dibujar();

            escenario1.Dibujar();

            escenario2.Dibujar();

            GL.Normal3(5.0, 0.0, 0.0);


            //-----------------------
            Context.SwapBuffers();
            base.OnRenderFrame(e);
            
        }

        protected override void OnResize(EventArgs e)
        {
            float d = 40;
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-d, d, -d, d, -d, d);
            //GL.Frustum(-80, 80, -80, 80, 4, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            base.OnUnload(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            this.capturarTeclado();
            base.OnUpdateFrame(e);
        }

        public void capturarTeclado()
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Q))
            {
                escenario.Rotar(0.8f, 0, 0);
                escenario1.Rotar(0.8f, 0, 0);
                escenario2.Rotar(0.8f, 0, 0);

            }
            if (input.IsKeyDown(Key.W))
            {
                escenario.Rotar(-0.8f, 0.0f, 0);
                escenario1.Rotar(-0.8f, 0.0f, 0);
                escenario2.Rotar(-0.8f, 0.0f, 0);


            }
            if (input.IsKeyDown(Key.E))
            {
                escenario.Rotar(0, 0, 0.8f);
                escenario1.Rotar(0, 0, 0.8f);
                escenario2.Rotar(0, 0, 0.8f);


            }
            if (input.IsKeyDown(Key.A))
            {
                escenario.Trasladar(-1f, 0, 0);
                escenario1.Trasladar(-1f, 0, 0);

            }
            if (input.IsKeyDown(Key.S))
            {
                escenario.Trasladar(1f, 0, 0);
                escenario1.Trasladar(1f, 0, 0);

            }
            if (input.IsKeyDown(Key.D))
            {
                escenario.Trasladar(0, 1.0f, 0);
                escenario1.Trasladar(0, 1.0f, 0);

            }
            if (input.IsKeyDown(Key.F))
            {
                escenario.Trasladar(0, -1.0f, 0);
                escenario1.Trasladar(0, -1.0f, 0);

            }
            if (input.IsKeyDown(Key.G))
            {
                escenario.Trasladar(0, 0, 1.0f);
                escenario1.Trasladar(0, 0, 1.0f);

            }
            if (input.IsKeyDown(Key.H))
            {
                escenario.Trasladar(0, 0, -1.0f);
                escenario1.Trasladar(0, 0, -1.0f);

            }

            if (input.IsKeyDown(Key.Z))
            {
                escenario.Escalar(0.9f, 0, 0);
                escenario1.Escalar(0.9f, 0, 0);
            }
            if (input.IsKeyDown(Key.X))
            {
                escenario.Escalar(1.1f, 0, 0);
                escenario1.Escalar(1.1f, 0, 0);
            }
            if (input.IsKeyDown(Key.C))
            {
                escenario.Escalar(0, 0.9f, 0);
                escenario1.Escalar(0, 0.9f, 0);
            }
            if (input.IsKeyDown(Key.V))
            {
                escenario.Escalar(0, 1.1f, 0);
                escenario1.Escalar(0, 1.1f, 0);
            }
            if (input.IsKeyDown(Key.B))
            {
                escenario.Escalar(0, 0, 0.9f);
                escenario1.Escalar(0, 0, 0.9f);

            }
            if (input.IsKeyDown(Key.N))
            {
                escenario.Escalar(0, 0, 1.1f);
                escenario1.Escalar(0, 0, 1.1f);
            }

        }
        public Objeto GetBaseCasa() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.Orange,
                                    new Punto(0, 0, -10.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(-10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.DarkGray,
                                    new Punto(-10.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.DarkGray,
                                    new Punto(10.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, 10.0f, 10.0f) }
                                    },
                                    Color.DarkGray,
                                    new Punto(0, 10.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-60.0f, -10.0f, -60.0f) },
                                        { "2", new Punto(-60.0f, -10.0f, 60.0f) },
                                        { "3", new Punto(60.0f, -10.0f, 60.0f) },
                                        { "4", new Punto(60.0f, -10.0f, -60.0f) }
                                    },
                                    Color.DarkSlateGray,
                                    new Punto(0, -10.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.DarkGray,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };
            return new Objeto(new Punto(0, 0, 0), 5, 5, 5, caras);
        }


        public Objeto GetTechoCasa() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto2", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(0.0f, 20.0f, -10.0f) },
                                    },
                                    Color.DarkGray,
                                    new Punto(0, 0, -10.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-13.0f, 8.0f, -13.0f) },
                                        { "punto2", new Punto(0.0f, 20.0f, -13.0f) },
                                        { "punto3", new Punto(0.0f, 20.0f, 13.0f) },
                                        { "punto4", new Punto(-13.0f, 8.0f, 13.0f) }
                                    },
                                    Color.Brown,
                                    new Punto(-10.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(13.0f, 8.0f, 13.0f) },
                                        { "punto2", new Punto(0.0f, 20.0f, 13.0f) },
                                        { "punto3", new Punto(0.0f, 20.0f, -13.0f) },
                                        { "punto4", new Punto(13.0f, 8.0f, -13.0f) }
                                    },
                                    Color.Brown,
                                    new Punto(10.0f, 0, 0)
                                )
                            },
                           
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto2", new Punto(0.0f, 20.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, 10.0f) },
                                    },
                                    Color.DarkGray,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };
            return new Objeto(new Punto(0, 0, 0), 5, 5, 5, caras);
        }

        public Objeto GetCarroInferior() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(0, 0, -10.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(-10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(-10.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(10.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, 10.0f, 10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(0, 10.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "2", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "3", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(0, -10.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.DarkBlue,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };

            return new Objeto(new Punto(0,0,0), 2, 2, 2, caras);
        }


        public Objeto GetCarroSuperior() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(0, 0, -10.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(-10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(-10.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(10.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, 10.0f, 10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(0, 10.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "2", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "3", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(0, -10.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.Blue,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };

            return new Objeto(new Punto(0, 0, 0), 2, 2, 2, caras);
        }



        public Objeto GetRueda() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {

                            
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.BlanchedAlmond,
                                    new Punto(0, 0, -10.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(-10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.Black,
                                    new Punto(-10.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.Black,
                                    new Punto(10.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, -10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, -10.0f) },
                                        { "punto4", new Punto(10.0f, 10.0f, 10.0f) }
                                    },
                                    Color.BlanchedAlmond,
                                    new Punto(0, 10.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-10.0f, -10.0f, -10.0f) },
                                        { "2", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "3", new Punto(10.0f, -10.0f, 10.0f) },
                                        { "4", new Punto(10.0f, -10.0f, -10.0f) }
                                    },
                                    Color.BlanchedAlmond,
                                    new Punto(0, -10.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-10.0f, -10.0f, 10.0f) },
                                        { "punto2", new Punto(-10.0f, 10.0f, 10.0f) },
                                        { "punto3", new Punto(10.0f, 10.0f, 10.0f) },
                                        { "punto4", new Punto(10.0f, -10.0f, 10.0f) }
                                    },
                                    Color.BlanchedAlmond,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };

            return new Objeto(new Punto(0, 0, 0), 2, 2, 2, caras);
        }

        public Objeto GetBodyBird() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(0, 0, -2.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto4", new Punto(-2.0f, -2.0f, 2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(-2.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "punto2", new Punto(2.0f, 2.0f, 2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(2.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, 2.0f, 2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(0, 2.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "2", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "3", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(0, -2.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, 2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, 2.0f) }
                                    },
                                    Color.LightGreen,
                                    new Punto(0, 0, 10)
                                )
                            },
                            


                         };
            return new Objeto(new Punto(0, 0, 0), 5, 5, 5, caras);
        }

        public Objeto GetSupport() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(0, 0, -2.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto4", new Punto(-2.0f, -2.0f, 2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(-2.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "punto2", new Punto(2.0f, 2.0f, 2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(2.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, -2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, 2.0f, 2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(0, 2.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "2", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "3", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "4", new Punto(2.0f, -2.0f, -2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(0, -2.0f, 0)
                                )
                            },
                           // frente
                            {
                                "cara6",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "punto2", new Punto(-2.0f, 2.0f, 2.0f) },
                                        { "punto3", new Punto(2.0f, 2.0f, 2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, 2.0f) }
                                    },
                                    Color.Maroon,
                                    new Punto(0, 0, 10)
                                )
                            },

                         };
            return new Objeto(new Punto(0, 0, 0), 5, 5, 5, caras);
        }
        public Objeto GetTriangle() //todos el mismo origen
        {
            Dictionary<string, Cara> caras = new Dictionary<string, Cara>
                         {
                            //atras
                            {
                                "cara1",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "punto3", new Punto(2.0f, -2.0f, -2.0f) },
                                        { "punto4", new Punto(2.0f, -2.0f, 2.0f) }
                                    },
                                    Color.Orange,
                                    new Punto(0, 0, -2.0f)
                                )
                            },
                            //izquierda
                            {
                                "cara2",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "punto3", new Punto(0f, 2.0f, 0f) },
                                    },
                                    Color.Orange,
                                    new Punto(-2.0f, 0, 0)
                                )
                            },
                            //derecha
                            {
                                "cara3",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(-2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(2.0f, -2.0f, -2.0f) },
                                        { "punto3", new Punto(0f, 2.0f, 0f) },
                                    },
                                    Color.Orange,
                                    new Punto(2.0f, 0, 0)
                                )
                            },
                            //superior
                            {
                                "cara4",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "punto1", new Punto(2.0f, -2.0f, -2.0f) },
                                        { "punto2", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "punto3", new Punto(0f, 2.0f, 0f) },
                                    },
                                    Color.Orange,
                                    new Punto(0, 2.0f, 0)
                                )
                            },
                            //inferior
                            {
                                "cara5",
                                new Cara(
                                    new Punto(0, 0, 0),
                                    PrimitiveType.Polygon,
                                    new Dictionary<string, Punto>
                                    {
                                        { "1", new Punto(-2.0f, -2.0f, 2.0f) },
                                        { "2", new Punto(2.0f, -2.0f, 2.0f) },
                                        { "3", new Punto(0f, 2.0f, 0f) },
                                        
                                    },
                                    Color.Orange,
                                    new Punto(0, -2.0f, 0)
                                )
                            },
                           

                         };
            return new Objeto(new Punto(0, 0, 0), 5, 5, 5, caras);
        }


    }




    /*
        public Dictionary<string, Objeto> listaDeObjetos()
            {
                //techo casa
                List<Punto> vertices1 = new List<Punto>();
                vertices1.Add(new Point(0f, 100f, 100f));
                vertices1.Add(new Point(-100f, 0f, 100f));
                vertices1.Add(new Point(100f, 0f, 100f));

                List<Punto> vertices2 = new List<Punto>();
                vertices2.Add(new Point(0f, 100f, -100f));
                vertices2.Add(new Point(-100f, 0f, -100f));
                vertices2.Add(new Point(100f, 0f, -100f));

                List<Punto> vertices3 = new List<Punto>();
                vertices3.Add(new Point(0f, 100f, 100f));
                vertices3.Add(new Point(-100f, 0f, 100f));
                vertices3.Add(new Point(-100f, 0f, -100f));
                vertices3.Add(new Point(0f, 100f, -100f));


                List<Punto> vertices4 = new List<Punto>();
                vertices4.Add(new Point(100f, 0f, 100f));
                vertices4.Add(new Point(0f, 100f, 100f));
                vertices4.Add(new Point(0f, 100f, -100f));
                vertices4.Add(new Point(100f, 0f, -100f));

                List<Punto> vertices5 = new List<Punto>();
                vertices5.Add(new Point(-100, -100, 100));
                vertices5.Add(new Point(100, -100f, 100));
                vertices5.Add(new Point(100, 100, 100));
                vertices5.Add(new Point(-100, 100, 100));

                List<Punto> vertices6 = new List<Punto>();
                vertices6.Add(new Point(-100, -100, -100));
                vertices6.Add(new Point(-100, -100, 100));
                vertices6.Add(new Point(-100, 100, 100));
                vertices6.Add(new Point(-100, 100, -100));

                List<Punto> vertices7 = new List<Punto>();
                vertices7.Add(new Point(-100f, -100f, -100f));
                vertices7.Add(new Point(100f, -100f, -100f));
                vertices7.Add(new Point(100f, -100f, 100f));
                vertices7.Add(new Point(-100f, -100f, 100f));

                List<Punto> vertices8 = new List<Punto>();
                vertices8.Add(new Point(-100f, 100f, -100f));
                vertices8.Add(new Point(-100f, 100f, 100f));
                vertices8.Add(new Point(100f, 100f, 100f));
                vertices8.Add(new Point(100f, 100f, -100f));

                List<Punto> vertices9 = new List<Punto>();
                vertices9.Add(new Point(100f, 100f, -100f));
                vertices9.Add(new Point(100f, 100f, 100f));
                vertices9.Add(new Point(100f, -100f, 100f));
                vertices9.Add(new Point(100f, -100f, -100f));


                List<Punto> vertices10 = new List<Punto>();
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

                //base casa
                vertices5 = new List<Punto>();
                vertices5.Add(new Point(-50, 0, 100));
                vertices5.Add(new Point(0, 0f, 100));
                vertices5.Add(new Point(0, 50f, 100));
                vertices5.Add(new Point(-50, 50f, 100));

                vertices6 = new List<Punto>();
                vertices6.Add(new Point(-50, 0, 0));
                vertices6.Add(new Point(0, 0, 0));
                vertices6.Add(new Point(0, 50, 0));
                vertices6.Add(new Point(-50, 50, 0));

                vertices7 = new List<Punto>();
                vertices7.Add(new Point(0f, 0f, 100f));
                vertices7.Add(new Point(0f, 0f, 0f));
                vertices7.Add(new Point(0f, 50f, 0f));
                vertices7.Add(new Point(0f, 50f, 100f));


                vertices8 = new List<Punto>();
                vertices8.Add(new Point(-50f, 0f, 100f));
                vertices8.Add(new Point(-50f, 0f, 0f));
                vertices8.Add(new Point(-50f, 50f, 0f));
                vertices8.Add(new Point(-50f, 50f, 100f));

                vertices9 = new List<Punto>();
                vertices9.Add(new Point(-50f, 50f, 100f));
                vertices9.Add(new Point(-50f, 50f, 0f));
                vertices9.Add(new Point(0f, 50f, 0f));
                vertices9.Add(new Point(0f, 50f, 100f));


                vertices10 = new List<Punto>();
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
                //auto
                vertices5 = new List<Punto>();
                vertices5.Add(new Point(-25, 0, 50));
                vertices5.Add(new Point(0, 0f, 50));
                vertices5.Add(new Point(0, 25f, 50));
                vertices5.Add(new Point(-25, 25f, 50));

                vertices6 = new List<Punto>();
                vertices6.Add(new Point(-25, 0, 0));
                vertices6.Add(new Point(0, 0, 0));
                vertices6.Add(new Point(0, 25, 0));
                vertices6.Add(new Point(-25, 25, 0));

                vertices7 = new List<Punto>();
                vertices7.Add(new Point(0f, 0f, 50));
                vertices7.Add(new Point(0f, 0f, 0f));
                vertices7.Add(new Point(0f, 25, 0f));
                vertices7.Add(new Point(0f, 25, 50));


                vertices8 = new List<Punto>();
                vertices8.Add(new Point(-25, 0f, 50));
                vertices8.Add(new Point(-25, 0f, 0f));
                vertices8.Add(new Point(-25, 25, 0f));
                vertices8.Add(new Point(-25, 25, 50));

                vertices9 = new List<Punto>();
                vertices9.Add(new Point(-25, 25, 50));
                vertices9.Add(new Point(-25, 25, 0f));
                vertices9.Add(new Point(0f, 25, 0f));
                vertices9.Add(new Point(0f, 25, 50));


                vertices10 = new List<Punto>();
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
                vertices5 = new List<Punto>();
                vertices5.Add(new Point(-10, 0, 22));
                vertices5.Add(new Point(0, 0f, 22));
                vertices5.Add(new Point(0, 25f, 22));
                vertices5.Add(new Point(-10, 25f, 22));

                vertices6 = new List<Punto>();
                vertices6.Add(new Point(-10, 0, 0));
                vertices6.Add(new Point(0, 0, 0));
                vertices6.Add(new Point(0, 25, 0));
                vertices6.Add(new Point(-10, 25, 0));

                vertices7 = new List<Punto>();
                vertices7.Add(new Point(0f, 0f, 22));
                vertices7.Add(new Point(0f, 0f, 0f));
                vertices7.Add(new Point(0f, 25, 0f));
                vertices7.Add(new Point(0f, 25, 22));


                vertices8 = new List<Punto>();
                vertices8.Add(new Point(-10, 0f, 22));
                vertices8.Add(new Point(-10, 0f, 0f));
                vertices8.Add(new Point(-10, 25, 0f));
                vertices8.Add(new Point(-10, 25, 22));

                vertices9 = new List<Punto>();
                vertices9.Add(new Point(-10, 25, 22));
                vertices9.Add(new Point(-10, 25, 0f));
                vertices9.Add(new Point(0f, 25, 0f));
                vertices9.Add(new Point(0f, 25, 22));


                vertices10 = new List<Punto>();
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
                objetos.Add("casa", new Objeto(caras, new Point(-100f, 0f, 0f), "casa"));
                //objetos.Add("Piramide 2", new Objeto(caras2, new Point(-100f, 100f, 0f), "Piramide 2"));
                objetos.Add("auto", new Objeto(caras3, new Point(100f, -190f, 0f), "auto"));

                return objetos;
            }
        }


         */
}
