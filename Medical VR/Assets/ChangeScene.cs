using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, TimedInputHandler
{

    int index = 0;
    bool change = false;
    public GameObject[] buttons;
    public int highlight;



    bool locked = false;

    // Use this for initialization
    void Start()
    {
        //UnityEngine.Cursor.visible = true;
        //Vector3 temp = Camera.main.ScreenToWorldPoint(buttons[0].transform.position);
        //float y = temp.y;
        //transform.position = new Vector3(transform.position.x, y, transform.position.z);
        //buttons[0].GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Move Selection
        if (!locked)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                index = index - 1 < 0 ? buttons.Length - 1 : index - 1;
                change = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                index = index + 1 >= buttons.Length ? 0 : index + 1;
                change = true;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                EnterEvent();
            }

            if (change)
            {
                //SoundManager.PlaySFX("MenuSelect");
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (i != index)
                    {
                        //buttons[i].GetComponent<Animator>().enabled = false;
                    }
                    else {
                        //buttons[i].GetComponent<Animator>().enabled = true;
                    }
                }
                Vector3 temp = Camera.main.ScreenToWorldPoint(buttons[index].transform.position);
                float y = temp.y;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }

            change = false;
        }
    }
    public void LoadScene(string scene)
    {
        //SoundManager.PlaySFX("MenuEnter");
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
        //SoundManager.PlaySFX("MenuEnter");
        Application.Quit();
    }

   public void EnterEvent()
    {
        switch (index)
        {
            case 0:
                LoadScene("Strategy");
                break;
            case 1:
                LoadScene("Basic Scene");
                break;
            case 2:
                LoadScene("FightVirus"); 
                break;
            case 3:
                LoadScene("Strategy");
                break;
            case 4:
                LoadScene("Strategy");
                break;
            case 5:
                Exit();
                break;
        }
    }

    public void Highlight(int anything)
    {
        index = anything;
        change = true;

    }

    public void HandleTimeInput()
    {
        Highlight(highlight);
        EnterEvent();
    }
}



