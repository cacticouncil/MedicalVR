using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public FacebookStuff fbManager;

    [HideInInspector]
    static public bool isArcadeMode;

    public GameObject fadeScreen;
    public GameObject gameCamera;
    public GameObject scoreBoard;
    public int scorePerEnzyme;
    public int winScore;

    [HideInInspector]
    public int score;
    public int maxNumEnzymes;
    public GameObject enzyme;
    public GameObject enzymeCollector;
    public GameObject nucleus;
    public Boundary spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public int maxNumHazards;
    public GameObject[] obsticles;
    public GameObject hazardCollector;
    
    private bool hasWon;

    public bool HasWon() { return hasWon; }

    void Start ()
    {
        hasWon = false;
        score = 0;
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnHazards());
        SetFacebook();
    }

    private void Update()
    {
        if (winScore <= score && !hasWon)
        {
            hasWon = true;
            StopCoroutine(SpawnWaves());
            StopCoroutine(SpawnHazards());
        }
        if (scoreBoard && hasWon && gameCamera)
        {
            //scoreBoard.SetActive(true);
            scoreBoard.transform.position = new Vector3(gameCamera.transform.position.x, gameCamera.transform.position.y, gameCamera.transform.position.z + 5);
        }
    }

    IEnumerator SpawnHazards()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                if (maxNumHazards > hazardCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject haz = Instantiate(obsticles[Random.Range(0, obsticles.Length - 1)], spawnPosition, spawnRotation, hazardCollector.transform) as GameObject;
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
                if (maxNumEnzymes > enzymeCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    GameObject haz = Instantiate(enzyme, spawnPosition, spawnRotation, enzymeCollector.transform) as GameObject;
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

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(5);
    }
    public void AddToScore(int _points)
    {
        score += _points;
    }

    void SetFacebook()
    {        
        fbManager.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + FacebookManager.Instance.GlobalScore;
        fbManager.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
    }
}
