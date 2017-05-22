using UnityEngine;
using System.Collections;

using TMPro;

public class _TTutorialATPGTP : MonoBehaviour
{
    enum TutorialState { dispEnzyme, dispATP, moveATP, dispGTP, moveGTP, pointsDesc, startGame, wait }
    enum TextChild { Top, Right, Left, Bottom }

    public float displayTime = 4;
    public float rotationSpeed = 1;

    public GameObject nucleus;
    public GameObject enzyme;
    public GameObject ATP;
    public GameObject GTP;
    public GameObject tutorialStuff;
    GameObject enz;
    GameObject atp;
    GameObject gtp;

    float startGameTime;

    private TutorialState tState;
    bool run;
    private float speed = 3;
    bool isInit = false;

    void Start()
    {
        // Initialize();
    }

    public void Initialize()
    {
        if (isInit)
            return;
        isInit = true;
        //run = GlobalVariables.tutorial;
        run = GetComponent<_TGameController>().IsTutorial();
        tState = TutorialState.dispEnzyme;
        GetComponent<_TGameController>().FadeScreen();
        foreach (Transform obj in tutorialStuff.transform.GetChild(0))
        {
            obj.GetComponent<_TSizeChange>().startSmall = true;
            obj.GetComponent<_TSizeChange>().Inititalize();
            obj.GetComponent<_TSizeChange>().ResetToSmall();
        }

    }

    void Update()
    {
        if (!run)
            return;
        TutorialMode();
    }

    #region Set Tutorial State
    TutorialState tmpTutState;
    void ChangeTutorialState(TutorialState gs, float time)
    {
        tState = TutorialState.wait;
        tmpTutState = gs;
        Invoke("SetTutState", time);
    }
    void ChangeTutorialState(TutorialState gs)
    {
        tState = gs;
        tmpTutState = TutorialState.wait;
    }
    void SetTutState()
    {
        tState = tmpTutState;
        tmpTutState = TutorialState.wait;
    }
    #endregion

    void TutorialMode()
    {
        switch (tState)
        {
            case TutorialState.dispEnzyme:
                Invoke("DispEnzyme", displayTime);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.dispATP:
                Invoke("DispATP", displayTime * 2 + 1);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.moveATP:
                if (MoveToEnzyme(atp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    Invoke("DispGTP", displayTime - 2);
                }
                break;
            case TutorialState.moveGTP:
                if (MoveToEnzyme(gtp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    ChangeTutorialState(TutorialState.pointsDesc);
                }
                break;
            case TutorialState.pointsDesc:
                Invoke("ScoreDescription", 1);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.startGame:
                StartCoroutine(StartGame(startGameTime));
                Debug.Log("current Time is" + Time.time);
                
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.wait:
                break;
        }
    }
    bool MoveToEnzyme(GameObject mol)
    {
        float step = speed * Time.deltaTime;
        mol.transform.position = Vector3.MoveTowards(mol.transform.position, enz.transform.position, step);
        if (Vector3.Distance(mol.transform.position, enz.transform.position) < 0.5f)
            return true;
        return false;
    }
    IEnumerator DispMol(GameObject Mol, float startTime, float duration)
    {
        yield return new WaitForSeconds(startTime);

        for (int i = 0; i < 3; ++i)
        {
            GameObject mol = Instantiate(Mol, new Vector3(((i - 1) * 1.5f), 1, 4), Quaternion.identity) as GameObject;
            Rigidbody rb = mol.GetComponent<Rigidbody>();
            rb.useGravity = false;

            mol.transform.localScale *= 1.5f;
            mol.GetComponent<_TSizeChange>().startSmall = true;

            mol.GetComponent<_TSizeChange>().Inititalize();
            mol.GetComponent<_TSizeChange>().StartGrow();
            mol.GetComponent<_TMover>().enabled = false;
            mol.GetComponent<_TDestroyByTime>().CancelDestroy();
            mol.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(DestroyMol(mol, duration));
        }
    }
    IEnumerator DestroyMol(GameObject mol, float duration)
    {
        yield return new WaitForSeconds(duration);

        mol.GetComponent<_TSizeChange>().StartShrink();
        Destroy(mol, 2);
    }
    IEnumerator StartGame(float startTime)
    {
        yield return new WaitForSeconds(startTime + 1);
        GetComponent<_TGameController>().runGameState(0);        
    }

    void DispEnzyme()
    {
        float rotateSpeed = rotationSpeed * 0.9f;
        float moveEnzymeTime = displayTime;
        enz = Instantiate(enzyme, new Vector3(0, 1, 4), Quaternion.identity) as GameObject;
        enz.GetComponent<_TRandomRotator>().enabled = false;
        Rigidbody rb = enz.GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, rotateSpeed, 0);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        
        enz.GetComponent<_TSizeChange>().startSmall = true;

        enz.GetComponent<_TSizeChange>().Inititalize();
        enz.GetComponent<_TSizeChange>().StartGrow();
        enz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
        enz.GetComponent<_TTravelToNucleus>().waitTime = moveEnzymeTime;

        float initTime = 1.0f;

        ChangeTutorialState(TutorialState.dispATP);
        string text = "This is the enzyme cGas.";

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1));

        initTime += displayTime;
        text = "This cGas detects Viral DNA.";

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1));
    }
    void DispATP()
    {
        atp = Instantiate(ATP, new Vector3(2, 1, 4), Quaternion.identity) as GameObject;
        Rigidbody rb = atp.GetComponent<Rigidbody>();
        rb.useGravity = false;

        atp.GetComponent<_TSizeChange>().startSmall = true;

        atp.GetComponent<_TSizeChange>().Inititalize();
        atp.GetComponent<_TSizeChange>().StartGrow();
        atp.GetComponent<_TMover>().enabled = false;
        atp.GetComponent<_TDestroyByTime>().CancelDestroy();
        ChangeTutorialState(TutorialState.moveATP, displayTime * 2 + 1);

        float initTime = 1.0f;

        string text = "This is ATP.";
        StartCoroutine(DisplayText(text, TextChild.Right, initTime, displayTime - 1));
        initTime += displayTime;

        text = "Attach the ATP to the Enzyme binding pocket.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1));
    }
    void DispGTP()
    {
        gtp = Instantiate(GTP, new Vector3(-2, 1, 4), Quaternion.identity) as GameObject;
        Rigidbody rb = gtp.GetComponent<Rigidbody>();
        rb.useGravity = false;

        gtp.GetComponent<_TSizeChange>().startSmall = true;

        gtp.GetComponent<_TSizeChange>().Inititalize();
        gtp.GetComponent<_TSizeChange>().StartGrow();
        gtp.GetComponent<_TMover>().enabled = false;
        gtp.GetComponent<_TDestroyByTime>().CancelDestroy();
        ChangeTutorialState(TutorialState.moveGTP, displayTime * 2 + 1);

        float initTime = 1.0f;

        string text = "This is GTP.";
        StartCoroutine(DisplayText(text, TextChild.Left, initTime, displayTime - 1));
        initTime += displayTime;

        text = "Attach the GTP to the Enzyme binding pocket.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1));
    }
    void ScoreDescription()
    {
        float initTime = 1.0f;

        string text = "cGas will then move to the Endoplasmic Reticulum";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1));
        initTime += displayTime;

        text = "You will get 3 rounds of ATP";
        StartCoroutine(DispMol(ATP, initTime, displayTime));
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime));
        initTime += displayTime + 1;

        text = "You will then get 3 rounds of GTP";
        StartCoroutine(DispMol(GTP, initTime, displayTime));
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime));
        initTime += displayTime + 1;

        // float timePerEnzyme = GetComponent<_TGameController>().timePerEnzyme;

        text = "When the cGas is Completed you get 10 points.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime));
        initTime += displayTime + 1;

        text = "The Timer will also increase.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime));
        initTime += displayTime + 1;

        text = "Build as many cGas as you can before time runs out.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime));
        initTime += displayTime + 1;

        text = "Ready!";
        float oldSize = tutorialStuff.transform.GetChild(0).transform.GetChild((int)TextChild.Bottom).gameObject.GetComponent<TextMeshPro>().fontSize;
        StartCoroutine(ChangeTextSize(48, TextChild.Bottom, initTime));
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, 1));
        initTime += 2;

        text = "Begin!";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, 1));

        startGameTime += initTime + 1;
        ChangeTutorialState(TutorialState.startGame);
    }
    IEnumerator ChangeTextSize(float size, TextChild childNum, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject.GetComponent<TextMeshPro>().fontSize = 36.0f;
    }
    IEnumerator DisplayText(string text, TextChild childNum, float startText, float displayDuration)
    {
        GameObject obj = tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject;
        obj.GetComponent<_TSizeChange>().startSmall = true;
        obj.GetComponent<_TSizeChange>().Inititalize();

        yield return new WaitForSeconds(startText);

        obj.GetComponent<_TSizeChange>().ResetToSmall();
        obj.SetActive(true);
        obj.GetComponent<TextMeshPro>().text = text;
        obj.GetComponent<_TSizeChange>().StartGrow();

        yield return new WaitForSeconds(displayDuration);

        obj.GetComponent<_TSizeChange>().StartShrink();

        yield return new WaitForSeconds(3);
    }
}