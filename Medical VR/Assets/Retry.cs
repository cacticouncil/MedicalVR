using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void R()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
