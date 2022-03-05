using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba
{
    public float Battery { get; set; }
    public Vector3[] Map { get; set; }
    public bool stop { get; set; }
    public bool isCharging { get; set; }

    public Roomba() 
    {
        this.Battery = 100;
        this.stop = false;
        this.isCharging = false;
    }
}
