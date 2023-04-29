using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto_OpenTK.Figuras
{
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        [JsonConstructor]
        public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        
        
    }
}
