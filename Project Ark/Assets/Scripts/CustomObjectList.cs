using UnityEngine;

namespace Assets.Scripts
{
    public class CoordinatesObject
    {
        public CoordinatesObject()
        {
        }

        // The following constructor has parameters for two of the three  
        // properties.  
        public CoordinatesObject(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        // Properties. 
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public override string ToString()
        {
            return "x:"+ x + ", y:" + y + ",z: " + z;
        }
    }
}
