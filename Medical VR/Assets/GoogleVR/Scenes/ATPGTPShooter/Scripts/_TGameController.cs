using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ObjectsCollector
{
    public GameObject Player;
    public GameObject shotsUI;
    public GameObject hazardCollector;
    public GameObject mitocondriaCollector;
    public GameObject enzymeCollector;
    public GameObject fadeScreen;

}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax;
    public float yMin, yMax;
    public float zMin, zMax;
}

[System.Serializable]
public class FacebookStuff
{
    public GameObject facebookPic;
    public GameObject userName;

    private int score;
}

public class _TGameController : MonoBehaviour
{
    public ObjectsCollector shrinkStuff;
    public FacebookStuff fbManager;

    [HideInInspector]
    public static bool isArcadeMode;

    public GameObject gameCamera;
    public GameObject scoreBoard;
    public int scorePerEnzyme;
    public int winScore;

    [HideInInspector]
    public int score;
    public int maxNumEnzymes;
    public GameObject enzyme;
    public GameObject nucleus;
    public Boundary spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public int maxNumHazards;
    public GameObject[] obsticles;

    private bool hasWon;

    public bool HasWon() { return hasWon; }

    void Start()
    {
        //isArcadeMode = true;
        hasWon = false;
        score = 0;
        Invoke("StartScene", 0);
        SetFacebook();
    }

    private void Update()
    {
        if (winScore <= score && !hasWon)
        {
            hasWon = true;
            StopCoroutine("SpawnWaves");
            StopCoroutine("SpawnHazards");
            if (scoreBoard && gameCamera)
                scoreBoard.transform.position = new Vector3(gameCamera.transform.position.x, gameCamera.transform.position.y, gameCamera.transform.position.z + 5);
            if (isArcadeMode)
                Invoke("DisplayScore", 3);
            else
                Invoke("MoveToNewScene", 5);
            Invoke("ShrinkObjects", 2);
        }
    }

    void DisplayScore()
    {
        shrinkStuff.Player.GetComponent<_TRaycastTarget>().hasWon = true;
        scoreBoard.transform.GetComponent<_TSizeChange>().StartGrow();
    }

    void ShrinkObjects()
    {
        foreach (Transform child in shrinkStuff.enzymeCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in shrinkStuff.hazardCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in shrinkStuff.mitocondriaCollector.transform)
            child.GetComponent<_TSizeChange>().StartShrink();

        shrinkStuff.shotsUI.GetComponent<_TSizeChange>().StartShrink();
    }
    void ShrinkChild(Transform child)
    {
        if (child.GetComponent<_TSizeChange>())
        {
            child.GetComponent<_TSizeChange>().StartShrink();
            child.GetComponent<_TSizeChange>().StartDestroy(8);
        }
        else
            Debug.Log("Unable to retrieve StartShrink Script");
    }

    IEnumerator SpawnHazards()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                if (maxNumHazards > shrinkStuff.hazardCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    // GameObject haz = Instantiate(obsticles[Random.Range(0, obsticles.Length - 1)], spawnPosition, spawnRotation, hazardCollector.transform) as GameObject;
                    Instantiate(obsticles[Random.Range(0, obsticles.Length - 1)], spawnPosition, spawnRotation, shrinkStuff.hazardCollector.transform);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                if (maxNumEnzymes > shrinkStuff.enzymeCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject haz = Instantiate(enzyme, spawnPosition, spawnRotation, shrinkStuff.enzymeCollector.transform) as GameObject;
                    haz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
                    if (haz.GetComponent<_TEnzymeController>())
                        haz.GetComponent<_TEnzymeController>().pointsValue = scorePerEnzyme;
                    else
                        Debug.Log("Unable to access Enzyme Controller");

                    // Debug.Log("Velocity:" haz.transform.G)

                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddToScore(int _points)
    {
        score += _points;
    }

    void SetFacebook()
    {
        fbManager.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString()/*+ FacebookManager.Instance.GlobalScore*/;
        fbManager.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }

    void MoveToNewScene()
    {
        CellGameplayScript.loadCase = 1;
        SceneManager.LoadScene("CellGameplay");
    }
    void StartScene()
    {
        shrinkStuff.fadeScreen.GetComponent<_TFadeScreen>().StartScene();
        StartCoroutine("SpawnWaves");
        StartCoroutine("SpawnHazards");
    }
}
