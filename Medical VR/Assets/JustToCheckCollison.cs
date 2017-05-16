using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustToCheckCollison : MonoBehaviour
{
    public DodgeAntiBodyTutorial DABT;
    public MovingCamera MC;

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Player" && DABT.MoveText == 6)
    //    {
    //        MC.speed = 0;
    //        DABT.MoveText += 1;
    //        DABT.StopInput = false;
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && DABT.MoveText == 6)
        {
            MC.speed = 0;
            DABT.MoveText += 1;
            DABT.StopInput = false;
        }
    }
}
