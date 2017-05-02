using UnityEngine;
using System.Collections;

public class _TTutorialATPGTP : MonoBehaviour
{
    enum TutorialState { dispEnzyme, dispATP, moveATP, dispGTP, moveGTP, startGame, wait }
    public GameObject enzyme;
    public GameObject ATP;
    public GameObject GTP;
    GameObject enz;
    GameObject atp;
    GameObject gtp;

    private TutorialState tState;
    bool run;
    private float speed = 3;
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
        //run = GlobalVariables.tutorial;
        run = true;
        tState = TutorialState.dispEnzyme;
        GetComponent<_TGameController>().FadeScreen();
    }

    void Update()
    {
        if (!run)
            return;
        TutorialMode();
    }

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

    void TutorialMode()
    {
        switch (tState)
        {
            case TutorialState.dispEnzyme:
                Invoke("DispEnzyme", 2);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.dispATP:
                Invoke("DispATP", 2);
                ChangeTutorialState(TutorialState.wait);
                break;
            case TutorialState.moveATP:
                if(MoveToEnzyme(atp))
                {
                    ChangeTutorialState(TutorialState.wait);
                    Invoke("DispGTP", 2);
                }
                break;
            case TutorialState.moveGTP:
                if(MoveToEnzyme(gtp))
                {
                    ChangeTutorialState(TutorialState.wait);

                }
                break;
            case TutorialState.startGame:
                GetComponent<_TGameController>().runGameState();
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
        ChangeTutorialState(TutorialState.moveATP, 2);
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
        ChangeTutorialState(TutorialState.moveGTP, 2);

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
        ChangeTutorialState(TutorialState.dispATP);
    }
}