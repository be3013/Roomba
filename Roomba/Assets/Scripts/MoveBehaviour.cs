using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    GameObject Roomba;

    private float speed = 2;
    private float ticks = 0;

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
        //Debug.Log(ModelRoomba.Battery);

        if (ModelRoomba.Battery <= 0 || ModelRoomba.stop) return;

        if (Input.GetKey(KeyCode.A))
        {
            Roomba.transform.Rotate(0, -1f, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Roomba.transform.Rotate(0, 1f, 0);
        }
        else if (Input.GetKey(KeyCode.W)) 
        {
            rigidbodyComponent.velocity = Roomba.transform.rotation * Vector3.right * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbodyComponent.velocity = Roomba.transform.rotation * -Vector3.right * speed;
        }

        CheckOnFloor();

        TickTreatment();
    }

    void CheckOnFloor()
    {
        for(int i = 5; i < transform.parent.childCount; i++)
        {
            float posX = transform.parent.GetChild(i).transform.position.x;
            float posZ = transform.parent.GetChild(i).transform.position.z;
            if (Roomba.transform.position.x <= posX + 0.5 && Roomba.transform.position.x >= posX - 0.5)
            {
                if (Roomba.transform.position.z <= posZ + 0.5 && Roomba.transform.position.z >= posZ - 0.5)
                {
                    transform.parent.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    break;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ModelRoomba.isCharging) return;
        if (collision.gameObject.tag == "Floor") return;

        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
    }

    private void TickTreatment()
    {
        if (ticks >= 20)
        {
            if (!ModelRoomba.isCharging)
            {
                ModelRoomba.Battery -= 1;
            }
            ticks = 0;
        }
    }
}
