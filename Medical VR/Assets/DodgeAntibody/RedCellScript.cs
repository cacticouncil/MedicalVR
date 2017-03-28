using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RedCellScript : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public GameObject virus, spawner, banner;

	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "virus")
        {
            if(MovingCamera.arcadeMode == true)
            {
                virus.GetComponent<MovingCamera>().WinresetPos();
                virus.GetComponent<MovingCamera>().speed++;
                spawner.GetComponent<AnitbodySpawnerScript>().GenerateObstacles();
                
            }
            else
            {
                SceneManager.LoadScene("Virus Gameplay Scene"); 
            }
            if (PlayerPrefs.GetInt("Red Cell") == -1)
            {
                banner.GetComponent<BannerScript>().ShowUp();
                PlayerPrefs.SetInt("Red Cell", 1);
                SoundManager.PlaySFX("MenuEnter");
            }

        }
    }
}
