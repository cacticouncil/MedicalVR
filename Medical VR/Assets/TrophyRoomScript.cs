using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrophyRoomScript : MonoBehaviour
{
    public GameObject Text;
    public GameObject TrophyStand1;
    public GameObject TrophyStand2;

    void Start()
    {

    }

    void Update()
    {

    }

    IEnumerator DisplayText(string message, float duration)
    {
        Text.GetComponent<TextMeshPro>().enabled = true;
        Text.GetComponent<TextMeshPro>().text = message;
        yield return new WaitForSeconds(duration);
        Text.GetComponent<TextMeshPro>().enabled = false;
    }
}
