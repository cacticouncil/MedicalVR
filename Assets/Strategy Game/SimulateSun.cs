using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SimulateSun : MonoBehaviour
{
    public float degrees;
    private float turn = 0;

    void Update()
    {
        if (turn > 0)
        {
            transform.Rotate(new Vector3(degrees * Time.deltaTime, 0));
            turn -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    public void TurnUpdate()
    {
        turn += .3f;
    }
}
