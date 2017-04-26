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
    public static bool isArcadeMode;

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
    public GameObject mitocondriaCollector;

    private bool hasWon;

    public bool HasWon() { return hasWon; }

    void Start()
    {
        hasWon = false;
        score = 0;
        StartCoroutine("SpawnWaves");
        StartCoroutine("SpawnHazards");
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
            Invoke("ShrinkObjects", 2);
        }
    }

    void ShrinkObjects()
    {
        foreach (Transform child in enzymeCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in hazardCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in mitocondriaCollector.transform)
            child.GetComponent<_TSizeChange>().StartShrink();
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
                if (maxNumHazards > hazardCollector.transform.childCount)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    // GameObject haz = Instantiate(obsticles[Random.Range(0, obsticles.Length - 1)], spawnPosition, spawnRotation, hazardCollector.transform) as GameObject;
                    Instantiate(obsticles[Random.Range(0, obsticles.Length - 1)], spawnPosition, spawnRotation, hazardCollector.transform);
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
