using UnityEngine;
using System.Collections;

public class SkipFaceBook : MonoBehaviour
{
    public static bool booked = false;
    public GameObject faceBookIn;
    public GameObject faceBookOut;
    public GameObject mainMenu;

    // Use this for initialization
    void Start()
    {
        if (booked)
        {
            faceBookIn.SetActive(false);
            faceBookOut.SetActive(false);

            if (GlobalVariables.VirusGameplayCompleted == 0)
            {
                mainMenu.GetComponent<MainMenuLocked>().CellGameplayGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
                mainMenu.GetComponent<MainMenuLocked>().TrophyRoomGameObject.GetComponent<Renderer>().material.color = new Color32(31, 46, 77, 255);
            }
                Fade.FadeIn(mainMenu);
        }
    }

    public void SetBool(bool b)
    {
        booked = b;
    }
}
