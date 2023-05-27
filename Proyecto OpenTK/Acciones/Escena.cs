using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Acciones
{
    [JsonObject(MemberSerialization.OptIn)]
    class Escena
    {
        [JsonProperty] string nombre { get; set; }
        [JsonProperty] public List<Accion> acciones { get; set; }
        [JsonProperty] long tiempoDeDuracion;
        public Escena()
        {
            acciones = new List<Accion>();
            tiempoDeDuracion = 0;
            nombre = "";
        }
        public Escena(string nombre, List<Accion> lista)
        {
            this.nombre = nombre;
            acciones = lista;
            tiempoDeDuracion = 0;
            for (int i = 0; i < lista.Count; i++)
                tiempoDeDuracion += (lista.ElementAt(i).tiempoF - lista.ElementAt(i).tiempoI);
        }
        public void setAccion(Accion x)
        {
            acciones.Add(x);
        }
        public Accion getAccion(int i)
        {
            return acciones.ElementAt(i);
        }
        public int getCantidad()
        {
            return acciones.Count;
        }

        public static void SerializeJsonFile(string path, Escena obj)
        {
            string textJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
            File.WriteAllText("../../archivos/" + path, textJson);
        }
        public static Escena DeserializeJsonFile(string json)
        {
            string textJson = new StreamReader("../../archivos/" + json).ReadToEnd();
            return JsonConvert.DeserializeObject<Escena>(textJson);
        }

    }
}
