using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Figuras
{
    class Escenario
    {


        public Dictionary<string, Objeto> objetos { get; set; }

        public Escenario(Dictionary<string, Objeto> objetos)
        {
            this.objetos = objetos;
        }

        [JsonConstructor]
        public Escenario()
        {
            this.objetos = new Dictionary<string, Objeto>();
        }

        public Dictionary<string, Objeto> getObjetos()
        {
            return this.objetos;
        }

        public void setObjetos(Dictionary<string, Objeto> objetos)
        {
            this.objetos = objetos;
        }

        public void agregarObjeto(string nombre, Objeto objeto)
        {
            this.objetos.Add(nombre, objeto);
        }

        public void eliminarObjeto(string nombre)
        {
            this.objetos.Remove(nombre);
        }

        public void dibujar()
        {
            foreach (KeyValuePair<string, Objeto> objeto in objetos)
            {
                objeto.Value.dibujar();
            }
        }

    }
}
