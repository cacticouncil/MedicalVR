using UnityEngine;
using System.Collections;

using TMPro;

public class _TTutorialATPGTP : MonoBehaviour
{
    enum TutorialState { dispEnzyme, dispATP, moveATP, dispGTP, moveGTP, pointsDesc, startGame, wait }
    enum TextChild { Top, Right, Left, Bottom }

    public GameObject nucleus;
    public GameObject enzyme;
    public GameObject ATP;
    public GameObject GTP;
    public GameObject tutorialStuff;
    GameObject enz;
    GameObject atp;
    GameObject gtp;

    private TutorialState tState;
    bool run;
    private float speed = 3;
    bool isInit = false;

    void Start()
    {
        //Initialize();
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
        foreach(Transform obj in tutorialStuff.transform.GetChild(0))
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
                Invoke("DispEnzyme", 5);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.dispATP:
                Invoke("DispATP", 6 + 6);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.moveATP:
                if(MoveToEnzyme(atp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    Invoke("DispGTP", 3);
                }
                break;
            case TutorialState.moveGTP:
                if(MoveToEnzyme(gtp))
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
                GetComponent<_TGameController>().runGameState(10);
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
        ChangeTutorialState(TutorialState.moveATP, 9);
        
        float initTime = 1.0f;

        string text = "This is ATP.";
        StartCoroutine(DisplayText(text, TextChild.Right, initTime, 4));
        initTime += 5;

        text = "Attach the ATP to the Enzyme.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
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
        ChangeTutorialState(TutorialState.moveGTP, 9);

        float initTime = 1.0f;

        string text = "This is GTP.";
        StartCoroutine(DisplayText(text, TextChild.Left, initTime, 4));
        initTime += 5;

        text = "Attach the GTP to the Enzyme.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
    }
    void DispEnzyme()
    {
        float moveEnzymeTime = 10;
        enz = Instantiate(enzyme, new Vector3(0, 1, 4), Quaternion.identity) as GameObject;
        enz.GetComponent<_TRandomRotator>().enabled = false;
        Rigidbody rb = enz.GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePosition;

        enz.GetComponent<_TSizeChange>().startSmall = true;

        enz.GetComponent<_TSizeChange>().Inititalize();
        enz.GetComponent<_TSizeChange>().StartGrow();
        enz.GetComponent<_TTravelToNucleus>().nucleus = nucleus;
        enz.GetComponent<_TTravelToNucleus>().waitTime = moveEnzymeTime;

        float initTime = 1.0f;

        ChangeTutorialState(TutorialState.dispATP);
        string text = "This is cGas.";

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));

        initTime += 5;
        text = "This cGas is infected with Viral DNA.";

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
    }
    void ScoreDescription()
    {
        float initTime = 1.0f;

        string text = "When the cGas is Completed you get 10 points.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
        initTime += 5;

        text = "10 seconds will also be added to your time.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
        initTime += 5;

        text = "cGas will then return to the Endoplasmic Reticulum";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, 4));
        initTime += 5;

        text = "Complete as many cGas as you can before time runs out.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, 4));
        initTime += 5;

        text = "Ready!";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, 2));
        initTime += 2;

        text = "Begin!";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, 2));
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