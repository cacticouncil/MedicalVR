using UnityEngine;
using System.Collections;

using TMPro;

public class _TTutorialATPGTP : MonoBehaviour
{
    enum TutorialState { dispEnzyme, dispATP, moveATP, dispGTP, moveGTP, pointsDesc, startGame, wait }

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
                Invoke("DispATP", 6);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.moveATP:
                if(MoveToEnzyme(atp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    Invoke("DispGTP", 4);
                }
                break;
            case TutorialState.moveGTP:
                if(MoveToEnzyme(gtp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    ChangeTutorialState(TutorialState.startGame);
                }
                break;
            case TutorialState.pointsDesc:
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

        StartCoroutine(TurnOnText(1, initTime));
        initTime += 5;
        SetText(1, "This is ATP.");
        StartCoroutine(TurnOffText(1, initTime));

        initTime += 1;
        StartCoroutine(TurnOnText(0, initTime));
        SetText(0, "Attach the ATP to the Enzyme.");
        initTime += 5;
        StartCoroutine(TurnOffText(0, initTime));
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

        StartCoroutine(TurnOnText(2, initTime));
        initTime += 5;
        SetText(2, "This is GTP.");
        StartCoroutine(TurnOffText(2, initTime));

        initTime += 1;
        StartCoroutine(TurnOnText(0, initTime));
        SetText(0, "Attach the GTP to the Enzyme.");
        initTime += 5;
        StartCoroutine(TurnOffText(0, initTime));

    }
    void DispEnzyme()
    {
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
        enz.GetComponent<_TTravelToNucleus>().waitTime = 5;

        ChangeTutorialState(TutorialState.dispATP);
        StartCoroutine(TurnOnText(0, 1.0f));
        SetText(0, "This is Viral DNA.");
        StartCoroutine(TurnOffText(0, 5));
    }

    bool SetText(int childNum, string text)
    {
        GameObject obj = tutorialStuff.transform.GetChild(0).transform.GetChild(childNum).gameObject;
        if (!obj)
            return false;
        obj.GetComponent<TextMeshPro>().text = text;
        return true;
    }
    IEnumerator PointsDesc(float time)
    {
        TextMeshPro text = tutorialStuff.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        yield return new WaitForSeconds(time);

    }

    IEnumerator TurnOffText(int childNum, float time)
    {
        yield return new WaitForSeconds(time);
        tutorialStuff.transform.GetChild(0).transform.GetChild(childNum).gameObject.GetComponent<_TSizeChange>().StartShrink();

        //yield return new WaitForSeconds(time + 2.0f);
        //tutorialStuff.transform.GetChild(0).transform.GetChild(childNum).gameObject.SetActive(false);
        //tutorialStuff.transform.GetChild(childNum).GetComponent<_TSizeChange>().StartShrink();
    }
    IEnumerator TurnOnText(int childNum, float time)
    {
        yield return new  WaitForSeconds(time);
        GameObject obj = tutorialStuff.transform.GetChild(0).transform.GetChild(childNum).gameObject;
        obj.SetActive(true);
        obj.GetComponent<_TSizeChange>().startSmall = false;
        obj.GetComponent<_TSizeChange>().Inititalize();
        obj.GetComponent<_TSizeChange>().ResetToSmall();
        obj.GetComponent<_TSizeChange>().StartGrow();
    }
}