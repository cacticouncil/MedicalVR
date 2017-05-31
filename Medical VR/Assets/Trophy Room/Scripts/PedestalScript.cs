using UnityEngine;
using System.Collections;

public class PedestalScript : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject station, camPos, selectedTrophy, descButton;
    public int speed;
    public bool inUse, isSoundPlaying;
    bool inMove = false;
    public bool ActivateFirstClick = false;
    //Vector3 target;
    Color orgButtonColor;

    bool FirstClickOnly = false;
    bool SecondClickOnly = false;

    void Start()
    {
        if (descButton != null)
        {
            orgButtonColor = descButton.GetComponent<Renderer>().material.color;
            descButton.SetActive(false);
        }
        isSoundPlaying = false;
        inUse = false;

    }

    void Update()
    {
        if (inMove)
        {
            theCamera.transform.position = Vector3.MoveTowards(theCamera.transform.position, camPos.transform.position, speed * Time.deltaTime);
            if (theCamera.transform.position == camPos.transform.position)
            {
                inMove = false;
                inUse = true;
            }
        }
        if (theCamera.transform.position != camPos.transform.position)
        {
            isSoundPlaying = false;
            if (descButton != null)
                descButton.GetComponent<Renderer>().material.color = orgButtonColor;
            if (selectedTrophy != null)
            {
                selectedTrophy.GetComponent<AudioSource>().Stop();
                selectedTrophy.GetComponent<TrophyScript>().BringToPedestal();
            }
            inUse = false;

        }
        if (isSoundPlaying && inUse)
        {
            if (selectedTrophy.GetComponent<AudioSource>().isPlaying == false)
            {
                isSoundPlaying = false;
                descButton.GetComponent<Renderer>().material.color = orgButtonColor;
            }
        }
    }

    public void MoveToPedestal()
    {
        inMove = true;
        //switch ((int)station.transform.rotation.eulerAngles.y)
        //{
        //    case 0:
        //        target = new Vector3(transform.position.x + 1, theCamera.transform.position.y, transform.position.z);
        //        break;
        //    case 180:
        //        target = new Vector3(transform.position.x - 1, theCamera.transform.position.y, transform.position.z);
        //        break;
        //    default:
        //        break;
        //}
    }

    //public void FirstClick()
    //{
    //    if (theCamera.MoveText == 4 && FirstClickOnly == false)
    //    {
    //        theCamera.MoveText += 1;
    //        theCamera.CanIRead = true;
    //        FirstClickOnly = true;
    //    }
    //}

    //public void SecondClick()
    //{
    //    if (theCamera.MoveText == 5 && SecondClickOnly == false)
    //    {
    //        theCamera.MoveText += 1;
    //        theCamera.CanIRead = true;
    //        SecondClickOnly = true;
    //    }
    //}

    public void DoButtonAction()
    {
        if (isSoundPlaying == false)
        {
            selectedTrophy.GetComponent<TrophyScript>().PlayDescription();
            descButton.GetComponent<Renderer>().material.color = Color.green;
            isSoundPlaying = true;
        }
    }

    public void RevertButtonAction()
    {
        isSoundPlaying = false;
        descButton.GetComponent<Renderer>().material.color = orgButtonColor;
    }
}
