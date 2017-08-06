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
    }

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
