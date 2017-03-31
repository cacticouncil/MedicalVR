using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    public GameObject Player;
    public GameObject VirusPlayer;

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "BigVirus")
        {
            Player.GetComponent<Player>().VirusLeaveCount += 1;
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Virus")
        {
            if (Player.GetComponent<Player>())
                Player.GetComponent<Player>().VirusLeaveCount += 1;
            
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
        }
    }
}
