using UnityEngine;
using System.Collections;

public class PedestalScript : MonoBehaviour {

    public GameObject theCamera, station, camPos, selectedTrophy, descButton;
    public int speed;
    public bool inUse;
    bool inMove = false, isSoundPlaying;
    Vector3 target;
    Color orgButtonColor;
	// Use this for initialization
	void Start ()
    {
        orgButtonColor = descButton.GetComponent<Renderer>().material.color;
        isSoundPlaying = false;
        inUse = false;
        descButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(inMove)
        {
             theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, camPos.transform.position, speed*Time.deltaTime);
             if(theCamera.transform.position == camPos.transform.position)
             {
                inMove = false;
                inUse = true;
             }
        }
        if (theCamera.transform.position != camPos.transform.position)
        {
            isSoundPlaying = false;
            descButton.GetComponent<Renderer>().material.color = orgButtonColor;
            if(selectedTrophy != null)
            {
                selectedTrophy.GetComponent<AudioSource>().Stop();
                selectedTrophy.GetComponent<TrophyScript>().BringToPedestal();
            }
            inUse = false;
          
        }
        if(isSoundPlaying && inUse)
        {
            if(selectedTrophy.GetComponent<AudioSource>().isPlaying == false)
            {
                isSoundPlaying = false;
                descButton.GetComponent<Renderer>().material.color = orgButtonColor;
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
    public void DoButtonAction()
    {
        if(isSoundPlaying == false)
        {
            selectedTrophy.GetComponent<TrophyScript>().PlayDescription();
            descButton.GetComponent<Renderer>().material.color = Color.green;
            isSoundPlaying = true;
        }

    }
}
