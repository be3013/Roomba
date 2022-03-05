using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBehaviour : MonoBehaviour
{
    private bool isCharging = false;
    private int ticks = 0;

    void Update()
    {
        ticks += 1;
        
        if (ticks >= 20)
        {
            if (isCharging)
            {
                if (MoveBehaviour.ModelRoomba.Battery <= 90)
                {
                    MoveBehaviour.ModelRoomba.stop = true;
                }
                else MoveBehaviour.ModelRoomba.stop = false;

                if (MoveBehaviour.ModelRoomba.Battery < 100)
                {
                    MoveBehaviour.ModelRoomba.Battery += 1;
                }
            }
            ticks = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isCharging = true;
        MoveBehaviour.ModelRoomba.isCharging = true;
    }

    void OnTriggerExit(Collider other)
    {
        MoveBehaviour.ModelRoomba.isCharging = false;
        isCharging = false;
    }
}
