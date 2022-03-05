using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float x_Start, y_Start;
    public float x_Space, y_Space;

    public int columnLenght, rowLenght;

    public GameObject prefab;

    void Start()
    {
        for (int i = 0; i < columnLenght * rowLenght; i++)
        {
            Instantiate(prefab, new Vector3(x_Start + (x_Space * (i % columnLenght)), 0, y_Start + (y_Space * (i / columnLenght))), Quaternion.identity, transform.parent);
        }
    }
}
