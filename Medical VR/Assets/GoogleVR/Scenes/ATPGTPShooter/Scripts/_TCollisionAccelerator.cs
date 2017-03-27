using UnityEngine;
using System.Collections;



public class _TCollisionAccelerator : MonoBehaviour
{
    enum dataCollectOrder { CollectData, Run }

    dataCollectOrder runOrder;

    private Vector3 velocity;
    private bool DataIsCollected;

    private void Start()
    {
        runOrder = dataCollectOrder.CollectData;
    }


    private void Update()
    {
        switch (runOrder)
        {
            case dataCollectOrder.Run:
                break;






            case dataCollectOrder.CollectData:
                if (GetComponent<_TMover>().DataIsSet)
                    CollectData();
                break;
        }
    }

    private void CollectData()
    {
        runOrder = dataCollectOrder.Run;
        velocity = GetComponent<Rigidbody>().velocity;
    }
}
