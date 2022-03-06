using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float x_Start, y_Start;
    public float x_Space, y_Space;

    public int columnLenght, rowLenght;

    public GameObject prefab, pilar, parent;

    void Start()
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < columnLenght * rowLenght; i++)
        {
            Instantiate(prefab, new Vector3(x_Start + (x_Space * (i % columnLenght)), 0, y_Start + (y_Space * (i / columnLenght))), Quaternion.identity, transform.parent);
        }

        for (int i = 0; i < 15; i++)
        {
            Instantiate(pilar, new Vector3(x_Start + (x_Space * rnd.Next(1, rowLenght)), 10, y_Start + (y_Space * rnd.Next(1, rowLenght))), Quaternion.identity, parent.transform);
        }

        for (int i = 5; i < transform.parent.childCount; i++)
        {
            if (rnd.Next(1,3) == 1)
            {
                transform.parent.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            }
            else
            {
                transform.parent.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
        }
    }
}
