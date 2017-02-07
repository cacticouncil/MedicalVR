using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    int index = 0;
    bool change = false;
    public GameObject[] buttons;
    public GameObject TutorialImage;
    public GameObject Bg, soul, cam;
    public GameObject Title;
    public Button yes;
    public Button no;



    bool locked = false;

    // Use this for initialization
    void Start()
    {
        UnityEngine.Cursor.visible = true;
        Vector3 temp = Camera.main.ScreenToWorldPoint(buttons[0].transform.position);
        float y = temp.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        buttons[0].GetComponent<Animator>().enabled = true;
        yes.enabled = true;
        yes.gameObject.SetActive(false);
        no.enabled = true;
        no.gameObject.SetActive(false);
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
                        buttons[i].GetComponent<Animator>().enabled = false;
                    }
                    else {
                        buttons[i].GetComponent<Animator>().enabled = true;
                    }
                }
                Vector3 temp = Camera.main.ScreenToWorldPoint(buttons[index].transform.position);
                float y = temp.y;
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }

            change = false;
        }

        if ((Random.Range(1, 300) % 20) == 0)
            Instantiate(soul, new Vector3(Random.Range(-5.0f, 5.01f), Random.Range(-5.0f, 5.01f), 0), new Quaternion());


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
                /*CheckForTutorial();*/ LoadScene("Strategy");
                break;
            case 1:
                LoadScene("Instructions");
                break;
            case 2:
                LoadScene("Options");
                break;
            case 3:
                LoadScene("Credits");
                break;
            case 4:
                Exit();
                break;
        }
    }

    public void Highlight(int anything)
    {
        index = anything;
        change = true;

    }
    public void CheckForTutorial()
    {
        TutorialImage.SetActive(true);
        yes.gameObject.SetActive(true);
        no.gameObject.SetActive(true);
        Bg.SetActive(false);
        Title.SetActive(false);
        gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);

        }

    }
}



