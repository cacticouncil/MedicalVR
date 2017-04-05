using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SimulateSun : MonoBehaviour
{
    public float degrees;

    // Update is called once per frame
    public void TurnUpdate()
    {
        StopCoroutine("R");
        StartCoroutine(R());
    }

    IEnumerator R()
    {
        float rotation = degrees / 60.0f;
        for (int i = 0; i < 60.0f; i++)
        {
            transform.Rotate(new Vector3(rotation, 0));
            yield return new WaitForSeconds(.016f);
        }
    }
}
