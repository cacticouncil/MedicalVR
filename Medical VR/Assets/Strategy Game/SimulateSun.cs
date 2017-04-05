using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class SimulateSun : MonoBehaviour
{
    public float degrees;
    private int turn = 0;
    void Start()
    {
        StartCoroutine(R());
    }

    // Update is called once per frame
    public void TurnUpdate()
    {
        turn += 60;
    }

    IEnumerator R()
    {
        float rotation = degrees / 60.0f;
        while (true)
        {
            while (turn > 0)
            {
                transform.Rotate(new Vector3(rotation, 0));
                turn--;
                yield return new WaitForSeconds(.016f);
            }
            yield return 0;
        }
    }
}
