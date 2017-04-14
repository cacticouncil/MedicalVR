using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DodgeAntiBodyTutorial : MonoBehaviour
{
    public GameObject Subtitles;
    public GameObject Player;
    public GameObject RedCell;
    public GameObject RedCellTutorialLocation;
    public GameObject WhiteCell;
   public bool WhiteCellHitsPlayerFirstTime = false;
    Vector3 SavedRedCellPosition;
    public List<GameObject> WhiteCellsToActivate;

    void Start()
    {
        if (MovingCamera.TutorialMode == true)
        {
            //Set the player in the other tunnel
            Player.GetComponent<MovingCamera>().transform.position = transform.position;
            Player.GetComponent<MovingCamera>().stopMoving = true;

            //Save red cell orginal position
            SavedRedCellPosition = RedCell.transform.position;
            RedCell.transform.position = RedCellTutorialLocation.transform.position;
        }
    }

    void Update()
    {
        //Trigger Events
        if (MovingCamera.TutorialMode == true)
        {
            switch ((int)Subtitles.GetComponent<SubstitlesScript>().theTimer)
            {
                //Show them the red cell and then fade it back to where the orignal postion is
                case 10:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    RedCell.transform.position = Vector3.MoveTowards(RedCell.transform.position, SavedRedCellPosition, 8.0f);
                    if (RedCell.transform.position == SavedRedCellPosition)
                    {
                        Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                        Subtitles.GetComponent<SubstitlesScript>().Continue();
                    }
                    break;

                //Bring the white cell in view
                case 16:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    WhiteCell.transform.position = Vector3.MoveTowards(WhiteCell.transform.position, RedCellTutorialLocation.transform.position, 2.0f);

                    if (WhiteCell.transform.position == RedCellTutorialLocation.transform.position)
                    {
                        Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                        Subtitles.GetComponent<SubstitlesScript>().Continue();
                    }
                    break;

                //Move player and white cell towards each other
                case 20:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    Player.GetComponent<MovingCamera>().stopMoving = false;
                    Player.GetComponent<MovingCamera>().speed = 10.0f;
                    WhiteCell.transform.position = Vector3.MoveTowards(WhiteCell.transform.position, Player.GetComponent<MovingCamera>().transform.position, 2.0f);
                    break;

                default:
                    break;
            }
        }
    }

    //Function was made to be called after player collides with white cell
    public void MoveStoy()
    {
        Destroy(WhiteCell);
        Player.GetComponent<MovingCamera>().speed = 0.0f;
        Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
        Subtitles.GetComponent<SubstitlesScript>().Continue();
        DisplayWhiteCells();
    }

    void DisplayWhiteCells()
    {
        for (int i = 0; i < WhiteCellsToActivate.Count; i++)
        {
            WhiteCellsToActivate[i].gameObject.SetActive(true);
        }
    }

    public void RepawnPlayer()
    {
        //Player.GetComponent<MovingCamera>().stopMoving = false;
        Player.GetComponent<MovingCamera>().speed = 10.0f;
        Player.GetComponent<MovingCamera>().transform.position = transform.position;
    }
}
