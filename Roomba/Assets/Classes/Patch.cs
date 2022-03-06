using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Classes
{
    public class Patch
    {
        public float posX { get; set; }
        public float posY { get; set; }
        public GameObject Plane { get; set; }
        public Color Color { get; set; }
        public bool Scouted { get; set; }
        public bool Clean { get; set; }
        public int StraightCleans { get; set; }

        public Patch(GameObject plane)
        {
            this.Plane = plane;
        }
    }
}
