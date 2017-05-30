using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class ObjectsCollector
{
    public GameObject Player;
    public GameObject shotsUI;
    public GameObject scoreUI;
    public GameObject hazardCollector;
    public GameObject mitocondriaCollector;
    public GameObject enzymeCollector;
    public GameObject ViralEnzymeCollector;
    public GameObject fadeScreen;
    public GameObject listOfLines;
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

enum GameState { tutorial, arcadePlay, storyPlay, end }

public class _TGameController : MonoBehaviour
{
    public bool testTutorial;
    public bool testStory;
    public int currentSceneNum = 6;
    public GameObject gvrReticle;

    [HideInInspector]
    public static float finalATPScore;
    private GameState gameState;
    public GameObject[] uiElements;
    public ObjectsCollector shrinkStuff;
    public FacebookStuff fbManager;

    public GameObject gameCamera;
    public GameObject scoreBoard;
    public int scorePerEnzyme;
    public int winScore;

    [HideInInspector]
    public int score;
    public float winTime;
    [HideInInspector]
    public float remainingTime;
    public float timePerEnzyme;
    public float startTimeDelay;
    public int maxNumEnzymes;
    public GameObject[] enzyme;
    public GameObject nucleus;
    public Boundary spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public int maxNumHazards;
    public GameObject[] obsticles;

    private bool hasWon;
    private bool isTutorial;
    public bool IsTutorial() { return isTutorial; }
    bool isArcade;

    public bool HasWon() { return hasWon; }
    bool isInit = false;

    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        if (isInit)
            return;
        isInit = true;
        hasWon = false;
        score = 0;
        SetFacebook();
        isTutorial = GlobalVariables.tutorial;
        isArcade = GlobalVariables.arcadeMode;
        // For Testing //
        if (testTutorial)
            isTutorial = testTutorial;
        if (testStory)
            isArcade = false;

        remainingTime = winTime;

        shrinkStuff.listOfLines.SetActive(false);
        foreach (GameObject obj in uiElements)
        {
            _TSizeChange sc = obj.GetComponent<_TSizeChange>();
            if (sc)
            {
                sc.startSmall = true;
                obj.GetComponent<_TSizeChange>().Inititalize();
                obj.GetComponent<_TSizeChange>().ResetToSmall();
            }
        }

        if (isTutorial)
        {
            gameState = GameState.tutorial;

        }
        else
        {
            runGameState(1);
        }
        //gameState = GameState.tutorial;
        GetComponent<_TTutorialATPGTP>().Initialize();
        shrinkStuff.Player.GetComponent<_TPlayerController>().Initialize();
        gvrReticle.SetActive(false);
        shrinkStuff.fadeScreen.GetComponent<_TFadeScreen>().StartScene();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.arcadePlay:
                RunArcadeMode();
                TimeDisplay();
                break;
            case GameState.storyPlay:
                RunStoryMode();
                TimeDisplay();
                break;
        }
    }
    void RunStoryMode()
    {
        // Lose Condition

        if (remainingTime <= 0 && !hasWon && winScore > score)
        {
            StopGameObjects();
            StartCoroutine(ReloadGame(8));
        }

        else if (winScore <= score && !hasWon)
        {
            StopGameObjects();
            StartCoroutine(MoveToNewScene(8));
        }
    }

    void StopGameObjects()
    {
        hasWon = true;
        StopCoroutine("SpawnWaves");
        StopCoroutine("SpawnHazards");

        shrinkStuff.Player.GetComponent<_TPlayerController>().SetActiveShooting(false);
        Invoke("ShrinkObjects", 2);
        shrinkStuff.listOfLines.SetActive(false);
    }

    void RunArcadeMode()
    {
        if (remainingTime <= 0 && !hasWon)
        {
            StopGameObjects();
            if (scoreBoard && gameCamera)
            {
                scoreBoard.SetActive(true);
                scoreBoard.GetComponent<_TSizeChange>().Inititalize();
                scoreBoard.GetComponent<_TSizeChange>().ResetToSmall();
                //       scoreBoard.GetComponent<_TSizeChange>().StartGrow();
                scoreBoard.transform.position = new Vector3(gameCamera.transform.position.x, gameCamera.transform.position.y, gameCamera.transform.position.z + 5);
                fbManager.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString();

            }

            Invoke("DisplayScore", 3);
            if (finalATPScore < score)
            {

                finalATPScore = score;
            }

            if (finalATPScore > PlayerPrefs.GetFloat("ATPScore"))
            {
                PlayerPrefs.SetFloat("ATPScore", finalATPScore);
            }
            else
            {
                finalATPScore = PlayerPrefs.GetFloat("ATPScore");
            }
            gvrReticle.SetActive(true);
        }
    }

    bool startTime = false;
    void TimeDisplay()
    {
        if (hasWon)
            return;
        if (startTime && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
                remainingTime = 0;
            return;
        }
        else if (startTimeDelay >= 0.0f)
        {
            startTimeDelay -= Time.deltaTime;
        }
        else
        {
            startTimeDelay = 0;
            startTime = true;
        }
    }

    void DisplayScore()
    {
        scoreBoard.GetComponent<_TSizeChange>().ResetToSmall();
        shrinkStuff.Player.GetComponent<_TRaycastTarget>().hasWon = true;
        scoreBoard.transform.GetComponent<_TSizeChange>().StartGrow();
    }

    void ShrinkObjects()
    {
        foreach (Transform child in shrinkStuff.enzymeCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in shrinkStuff.ViralEnzymeCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in shrinkStuff.hazardCollector.transform)
            ShrinkChild(child);

        foreach (Transform child in shrinkStuff.mitocondriaCollector.transform)
            child.GetComponent<_TSizeChange>().StartShrink();

        // shrinkStuff.shotsUI.GetComponent<_TSizeChange>().StartShrink();
        // shrinkStuff.scoreUI.GetComponent<_TSizeChange>().StartShrink();
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
                if (maxNumEnzymes > shrinkStuff.enzymeCollector.transform.childCount + shrinkStuff.ViralEnzymeCollector.transform.childCount || shrinkStuff.ViralEnzymeCollector.transform.childCount < 3)
                {
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(spawnValues.xMin, spawnValues.xMax),
                        Random.Range(spawnValues.yMin, spawnValues.yMax),
                        Random.Range(spawnValues.zMin, spawnValues.zMax));
                    Quaternion spawnRotation = Quaternion.identity;
                    int rand = Random.Range(0, enzyme.Length);
                    GameObject haz;
                    if (rand == 1 || shrinkStuff.ViralEnzymeCollector.transform.childCount < 3)
                        haz = Instantiate(enzyme[1], spawnPosition, spawnRotation, shrinkStuff.ViralEnzymeCollector.transform) as GameObject;
                    else
                        haz = Instantiate(enzyme[rand], spawnPosition, spawnRotation, shrinkStuff.enzymeCollector.transform) as GameObject;
                    haz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
                    if (haz.GetComponent<_TEnzymeController>())
                        haz.GetComponent<_TEnzymeController>().pointsValue = scorePerEnzyme;
                    else
                        Debug.Log("Unable to access Enzyme Controller");
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddToScore(int _points)
    {
        score += _points;
        remainingTime += timePerEnzyme;
    }

    void SetFacebook()
    {
        Debug.Log(score);
        fbManager.userName.GetComponent<TMPro.TextMeshPro>().text = FacebookManager.Instance.ProfileName + ": " + score.ToString();
        if (FacebookManager.Instance.ProfilePic)
        {
            fbManager.facebookPic.GetComponent<Image>().sprite = FacebookManager.Instance.ProfilePic;
        }
    }
    void FadeStuff()
    {
        foreach (GameObject obj in uiElements)
        {
            _TSizeChange sc = obj.GetComponent<_TSizeChange>();
            if (sc)
            {
                sc.StartShrink();
            }
        }

        shrinkStuff.fadeScreen.GetComponent<_TFadeScreen>().enabled = true;
        shrinkStuff.fadeScreen.GetComponent<_TFadeScreen>().StartBlackScreen();
    }
    IEnumerator ReloadGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - 2);
        FadeStuff();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("ATPGTPShooter");
    }
    IEnumerator MoveToNewScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime - 2);
        FadeStuff();
        yield return new WaitForSeconds(2);
        CellGameplayScript.loadCase = 1;
        SceneManager.LoadScene("CellGameplay");
    }

    public void StartHAzards()
    {
        StartCoroutine("SpawnHazards");

    }
    void StartScene()
    {
        GlobalVariables.tutorial = false;
        StartCoroutine("SpawnWaves");
        StartCoroutine("SpawnHazards");
        shrinkStuff.mitocondriaCollector.SetActive(true);
        foreach (Transform child in shrinkStuff.mitocondriaCollector.transform)
        {
            child.GetComponent<_TSizeChange>().Inititalize();
            child.GetComponent<_TSizeChange>().ResetToSmall();
            child.GetComponent<_TSizeChange>().StartGrow();
        }

        shrinkStuff.listOfLines.SetActive(true);
        foreach (GameObject obj in uiElements)
        {
            _TSizeChange sc = obj.GetComponent<_TSizeChange>();
            if (sc)
            {
                sc.StartGrow();
            }
        }
        shrinkStuff.Player.GetComponent<_TPlayerController>().SetActiveShooting(true);
    }

    void TurnOnUILights()
    {
        uiElements[0].transform.GetChild(2).gameObject.SetActive(true);
    }
    public void runGameState(float time)
    {
        if (isArcade)
            gameState = GameState.arcadePlay;
        else
            gameState = GameState.storyPlay;

        Invoke("StartScene", time);
    }
    public void FadeScreen()
    {
        shrinkStuff.fadeScreen.GetComponent<_TFadeScreen>().StartBlackScreen();
        FadeStuff();
    }
}