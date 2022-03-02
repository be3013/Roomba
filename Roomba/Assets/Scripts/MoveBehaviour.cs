using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    GameObject Roomba;

    private float horizontalInput;
    private float speed = 2;

    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        Roomba = this.gameObject;
        rigidbodyComponent = Roomba.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
    }
}
