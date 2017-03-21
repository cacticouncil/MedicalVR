using UnityEngine;
using System.Collections;

public class MenuReturn : MonoBehaviour {
    public string SceneName = "MainMenu";
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyUp(KeyCode.Escape)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
        }
	}
}
