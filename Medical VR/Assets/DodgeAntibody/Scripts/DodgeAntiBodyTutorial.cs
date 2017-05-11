using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DodgeAntiBodyTutorial : MonoBehaviour
{
    public GameObject Subtitles;
    public GameObject Player;
    public GameObject RedCell;
    public GameObject WhiteCell;
    public Transform PlayerTutorialLocation;
    public Transform CellTutorialLocation;
    public bool WhiteCellHitsPlayerFirstTime = false;
    Vector3 SavedRedCellPosition;
    public AntibodySpawnerScript spawner;
    public bool prevState;

    void Start()
    {
        if (GlobalVariables.tutorial)
        {
            prevState = GlobalVariables.arcadeMode;
            GlobalVariables.arcadeMode = false;

            //Set the player in the other tunnel
            Player.transform.position = PlayerTutorialLocation.position;
            Player.GetComponent<MovingCamera>().stopMoving = true;

            //Save red cell orginal position
            SavedRedCellPosition = RedCell.transform.position;
            RedCell.transform.position = CellTutorialLocation.position;
        }
        else
            enabled = false;
    }

    void FixedUpdate()
    {
        //Trigger Events
        if (GlobalVariables.tutorial)
        {
            switch ((int)Subtitles.GetComponent<SubstitlesScript>().theTimer)
            {
                //Show them the red cell and then fade it back to where the orignal postion is
                case 10:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    RedCell.transform.position = Vector3.MoveTowards(RedCell.transform.position, SavedRedCellPosition, 10.0f * Time.fixedDeltaTime);
                    if (RedCell.transform.position == SavedRedCellPosition)
                    {
                        Subtitles.GetComponent<SubstitlesScript>().theTimer += 1;
                        Subtitles.GetComponent<SubstitlesScript>().Continue();
                    }
                    break;

                //Bring the white cell in view
                case 16:
                    Subtitles.GetComponent<SubstitlesScript>().Stop();
                    WhiteCell.transform.position = Vector3.MoveTowards(WhiteCell.transform.position, CellTutorialLocation.position, 10.0f * Time.fixedDeltaTime);

                    if (WhiteCell.transform.position == CellTutorialLocation.position)
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
                    if (WhiteCell)
                        WhiteCell.transform.position = Vector3.MoveTowards(WhiteCell.transform.position, Player.transform.position, 1.0f * Time.fixedDeltaTime);
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
        spawner.enabled = true;
    }

    public void RespawnPlayer()
    {
        //Player.GetComponent<MovingCamera>().stopMoving = false;
        Player.GetComponent<MovingCamera>().speed = 10.0f;
        Player.GetComponent<MovingCamera>().transform.position = transform.position;
    }
}
