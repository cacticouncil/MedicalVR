using UnityEngine;
using System.Collections;

public class PedestalScript : MonoBehaviour {

    public GameObject theCamera, station, camPos;
    public int speed;
    bool inMove = false;
    Vector3 target;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(inMove)
        {
             theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, camPos.transform.position, speed*Time.deltaTime);
             if(theCamera.transform.position == target)
             {
                inMove = false;
             }
        }
    }

    public void MoveToPedestal()
    {
        inMove = true;
        switch ((int)station.transform.rotation.eulerAngles.y)
        {
            case 0:
                target = new Vector3(transform.position.x + 1, theCamera.transform.position.y, transform.position.z);
                break;
            case 180:
                target = new Vector3(transform.position.x - 1, theCamera.transform.position.y, transform.position.z);
                break;
            default:
                break;
        }
       

    }
}
