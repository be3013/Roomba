using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba
{
    public float Battery { get; set; }
    public Vector3[] Map { get; set; }
    public bool stop { get; set; }
    public bool isCharging { get; set; }
    public bool isAdjusting { get; set; }
    public int StraightCleans { get; set; }
    public Rotation TargetRotation { get; set; }
    public Movement TargetMovement { get; set; }

    public Roomba() 
    {
        this.Battery = 100;
        this.stop = true;
        this.StraightCleans = 0;
        this.isCharging = false;
        this.TargetRotation = Rotation.DOWN;
        this.TargetMovement = Movement.FOWARD;
    }

    public void RandomTargetRotation()
    {
        System.Random random = new System.Random();
        Rotation previous = this.TargetRotation;

        while(this.TargetRotation == previous)
        {
            switch (random.Next(1, 5))
            {
                case 1:
                    this.TargetRotation = Rotation.UP;
                    break;
                case 2:
                    this.TargetRotation = Rotation.DOWN;
                    break;
                case 3:
                    this.TargetRotation = Rotation.LEFT;
                    break;
                case 4:
                    this.TargetRotation = Rotation.RIGHT;
                    break;
            }
        }

    }

    public enum Rotation
    {
        UP = 2,
        DOWN = 180,
        RIGHT = 270,
        LEFT = 90
    }

    public enum Movement 
    {
        BACKWARD,
        FOWARD
    }
}
