using UnityEngine;
using System.Collections;

public class DodgeAntiBodyTutorial : MonoBehaviour
{
    public GameObject Subtitles;
    public GameObject Player;
    public GameObject RedCell;
    public GameObject RedCellTutorialLocation;

    Vector3 SavedRedCellPosition;
    public static bool TutorialMode = false;

    void Start()
    {
        if (TutorialMode == true)
        {
            //Make sure nothing is populating in the scene other than the player
            SavedRedCellPosition = RedCell.transform.position;
            RedCell.transform.position = RedCellTutorialLocation.transform.position;
            Player.GetComponent<MovingCamera>().stopMoving = false;
        }
    }

    void Update()
    {
        //Trigger Events
        switch ((int)Subtitles.GetComponent<SubstitlesScript>().theTimer)
        {
            //Show them the red cell and then fade it back to where the orignal postion is
            case 6:
                Subtitles.GetComponent<SubstitlesScript>().Stop();
                break;

            //Add the line "You must avoid the white cells or they will reset you back to the beginning

            //Demonstrate this by allowing the player to only move forward into a white cell

            //Freeze their movemnt again

            //Explain the rest and let them play with fewer white cells and only complete one level
            default:
                break;
        }
    }
}
