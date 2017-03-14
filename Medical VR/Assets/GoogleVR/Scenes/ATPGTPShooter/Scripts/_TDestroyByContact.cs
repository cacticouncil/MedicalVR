using UnityEngine;
using System.Collections;

public class _TDestroyByContact : MonoBehaviour
{
    //public GameObject explosion;
    public int scoreValue;
    //private GameController gameController;

    private void Start()
    {
        //GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        //if (gameControllerObject)
        //{
        //    gameController = gameControllerObject.GetComponent<GameController>();
        //}
        //if (!gameController)
        //{
        //    Debug.Log("Cannot find 'GameController' script");
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        //Instantiate(explosion, transform.position, transform.rotation);

        
        //gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
 //       Destroy(gameObject);
    }
}
