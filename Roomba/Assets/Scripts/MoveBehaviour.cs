using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBehaviour : MonoBehaviour
{
    GameObject Roomba;

    public GameObject BatteryText;

    private float speed = 2;
    private float ticks = 0;
    private float tickDelimeter = 100;

    private bool CanFix = true, start = false;

    public static Rigidbody rigidbodyComponent;

    public static Roomba ModelRoomba;

    public static List<Patch> patches = new List<Patch>();

    // Start is called before the first frame update
    void Start()
    {
        Roomba = this.gameObject;
        ModelRoomba = new Roomba();
        rigidbodyComponent = Roomba.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ticks += 1;
        TickTreatment();
        BatteryText.GetComponent<Text>().text = "Battery: " + ModelRoomba.Battery;  

        if (ModelRoomba.Battery <= 0 || ModelRoomba.stop || !start) return;

        if (!ModelRoomba.isAdjusting)
        {
            if(ModelRoomba.TargetMovement == global::Roomba.Movement.FOWARD)
            {
                rigidbodyComponent.velocity = Roomba.transform.rotation * Vector3.right * speed;
            }
            else
            {
                rigidbodyComponent.velocity = Roomba.transform.rotation * -Vector3.right * speed;
            }
        }

        if(CanFix) FixRotation();
        ValidateChangeRotation();
        CheckOnFloor();
        CheckAhead();
    }

    void ValidateChangeRotation()
    {
        if(ModelRoomba.StraightCleans >= 500)
        {
            ModelRoomba.StraightCleans = 0;
            ModelRoomba.RandomTargetRotation();
        }
    }

    void CheckOnFloor()
    {
        bool foundIt = false;
        for(int i = 4; i < transform.parent.childCount; i++)
        {
            float posX = transform.parent.GetChild(i).transform.position.x;
            float posZ = transform.parent.GetChild(i).transform.position.z;
            if (Roomba.transform.position.x <= posX + 0.5 && Roomba.transform.position.x >= posX - 0.5)
            {
                if (Roomba.transform.position.z <= posZ + 0.5 && Roomba.transform.position.z >= posZ - 0.5)
                {
                    if (transform.parent.GetChild(i).GetComponent<Renderer>().material.color != Color.black) ModelRoomba.StraightCleans += 1;
                    else ModelRoomba.StraightCleans = 0;
                    transform.parent.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    foundIt = true;
                    break;
                }
            }
        }

        if (!foundIt)
        {
            CanFix = false;
            ticks = 0;
            ModelRoomba.TargetMovement = global::Roomba.Movement.BACKWARD;
        }
    }

    void FixRotation()
    {
        float eulerY = Roomba.transform.localRotation.eulerAngles.y;

        if (Mathf.Abs(eulerY - (float)ModelRoomba.TargetRotation) >= 1f)
        {
            ModelRoomba.isAdjusting = true;
            if ((float)ModelRoomba.TargetRotation > eulerY)
                Roomba.transform.Rotate(new Vector3(0, 1f));
            else Roomba.transform.Rotate(new Vector3(0, -1f));
        }
        else
        {
            ModelRoomba.isAdjusting = false;
            ModelRoomba.TargetMovement = global::Roomba.Movement.FOWARD;
        }
    }

    void CheckAhead() 
    {
        float posX, posZ, RposX, RposZ, eulerY, fixX = 0, fixZ = 0;
        bool foundIt = false;
        for (int i = 4; i < transform.parent.childCount; i++)
        {
            posX = transform.parent.GetChild(i).transform.position.x;
            posZ = transform.parent.GetChild(i).transform.position.z;
            RposX = Roomba.transform.position.x;
            RposZ = Roomba.transform.position.z;
            eulerY = Roomba.transform.localRotation.eulerAngles.y;

            if (eulerY >= 0 && eulerY < 90) { fixX = 0.5f; fixZ = -0.5f; }
            else if (eulerY >= 90 && eulerY < 180) { fixX = -0.5f; fixZ = -0.5f; }
            else if (eulerY >= 180 && eulerY < 270) { fixX = 0.5f; fixZ = 0.5f; }
            else if (eulerY >= 270 && eulerY < 360) { fixX = -0.5f; fixZ = 0.5f; }

            if (RposX + fixX <= posX + 1 && RposX + fixX >= posX - 1)
            {
                if (RposZ + fixZ <= posZ + 1 && RposZ + fixZ >= posZ - 1)
                {
                    foundIt = true;
                    break;
                }
            }
        }

        if (!foundIt)
        {
            CanFix = false;
            ticks = 0;
            ModelRoomba.TargetMovement = global::Roomba.Movement.BACKWARD;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ModelRoomba.isCharging) return;
        if (collision.gameObject.tag == "Floor") return;

        CanFix = false;
        ticks = 0;
        ModelRoomba.TargetMovement = global::Roomba.Movement.BACKWARD;
    }

    private void TickTreatment()
    {
        if (ticks >= tickDelimeter)
        {
            start = true;
            if (!ModelRoomba.isCharging)
            {
                if (ModelRoomba.Battery > 0)
                {
                    ModelRoomba.Battery -= 1;
                }
            }
            
            if (ModelRoomba.TargetMovement == global::Roomba.Movement.BACKWARD)
            {
                CanFix = true;
                if(!ModelRoomba.isAdjusting) ModelRoomba.RandomTargetRotation();
                ModelRoomba.isAdjusting = true;
            }

            tickDelimeter = 50;
            ticks = 0;
        }
    }
}
