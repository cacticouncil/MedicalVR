using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    int index = 0;
    public GameObject[] buttons;

    // Use this for initialization
    void Start()
    {
        UnityEngine.Cursor.visible = true;
        Vector3 temp = Camera.main.ScreenToWorldPoint(buttons[0].transform.position);
        float y = temp.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        buttons[0].GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {


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
                LoadScene("Strategy");
                break;
            case 3:
                LoadScene("Strategy");
                break;
            case 4:
                Exit();
                break;
        }
    }
}



