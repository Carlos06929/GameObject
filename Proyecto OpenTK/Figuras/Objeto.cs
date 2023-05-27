using Newtonsoft.Json;
using OpenTK;
using System.Collections.Generic;
using System.IO;

namespace Proyecto_OpenTK.Figuras
{
    [JsonObject(MemberSerialization.OptIn)]


    public class Objeto
    {
        [JsonProperty] public Punto origen;
        [JsonProperty] public float ancho;
        [JsonProperty] public float alto;
        [JsonProperty] public float profundidad;
        [JsonProperty] public Dictionary<string, Cara> lista;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;


        public Objeto()
        {
            this.lista = new Dictionary<string, Cara>();
            this.origen = new Punto();
            this.ancho = this.alto = this.profundidad = 0;
        }
        
        public Objeto(Punto origen, float ancho, float alto, float profundidad, Dictionary<string, Cara> caras)
        {
            this.origen = origen;
            this.ancho = ancho;
            this.alto = alto;
            this.profundidad = profundidad;
            this.lista = caras;
            foreach (var _caras in lista.Values)
            {
                _caras.origen.x = this.origen.x;
                _caras.origen.y = this.origen.y;
                _caras.origen.z = this.origen.x;
            }
            
        }
        public Objeto(Objeto objeto)
        {
            this.origen = new Punto(objeto.origen);
            this.ancho = objeto.ancho;
            this.alto = objeto.alto;
            this.profundidad = objeto.profundidad;
            this.lista = new Dictionary<string, Cara>();
            foreach (var caras in objeto.lista)
                this.SetCara(caras.Key, new Cara(caras.Value));
        }
        //--------------------------------------------------------------------------------------------------------------------

        public static void SerializeJsonFile(string path, Objeto obj)
        {
            string textJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText("../../archivos/"+path, textJson);
        }
        public static Objeto DeserializeJsonFile(string json)
        {
            string textJson = new StreamReader("../../archivos/"+json).ReadToEnd();
            return JsonConvert.DeserializeObject<Objeto>(textJson);
        }
        
        public void SetCara(string name, Cara x)
        {
            if (lista.ContainsKey(name))
            {
                lista.Remove(name);
            }
            lista.Add(name, x);
        }

        public void SetOrigen(float x, float y, float z)
        {
            foreach (var caras in lista.Values)
            {
                caras.origen.x = x;
                caras.origen.y = y;
                caras.origen.z = z;
            }
        }
        public Cara GetCara(string name)
        {
            return (lista.ContainsKey(name)) ? lista[name] : null;
        }
        
        public void Dibujar()
        {
            foreach (var caras in lista.Values)
                caras.Dibujar();
        }

        
        public void Rotar(float x, float y, float z)
        {
            foreach (var caras in lista.Values)
                caras.RotarO(x, y, z);
        }
        
        public void Escalar(float x, float y, float z)
        {
            foreach (var caras in lista.Values)
                caras.Escalar(x, y, z);
        }
        
        public void Trasladar(float x, float y, float z)
        {
            foreach (var caras in lista.Values)
                caras.Trasladar(x, y, z);
        }
        
        public void RotarE(float x, float y, float z)
        {
            foreach (var caras in lista.Values)
                caras.RotarE(x, y, z);
        }

    }
}
