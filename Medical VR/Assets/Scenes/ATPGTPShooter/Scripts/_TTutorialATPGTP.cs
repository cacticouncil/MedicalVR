using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TMPro;


public class _TTutorialATPGTP : MonoBehaviour
{
    public float testQuanterneon;

    enum TutorialState { dispEnzyme, dispATP, moveATP, dispGTP, moveGTP, pointsDesc, startGame, wait }
    enum TextChild { Top, Right, Left, Bottom }
    enum CurrentMolecule { ATP, GTP, None }

    CurrentMolecule curMol = CurrentMolecule.None;

    bool last = false;
    bool text = false;
    bool finish = false;

    public GameObject player;

    public float displayTime = 4;
    public float rotationSpeed = 1;

    private int cNum = 0;

    public GameObject nucleus;
    public GameObject enzyme;
    public GameObject ATP;
    public GameObject GTP;
    public GameObject tutorialStuff;
    GameObject enz;
    GameObject atp;
    GameObject gtp;

    float startGameTime;
    float enzymeTime;
    bool moveEnzyme = false;
    bool hold = false;

    public AudioClip[] voiceSounds;
    public string[] texts;
    private List<Coroutine> stop = new List<Coroutine>();

    int curAudioClip = 0;
    private TutorialState tState;
    bool run;
    private float speed = 3;
    private AudioSource source;
    bool isInit = false;

    void Awake()
    {
        if (player)
            source = player.GetComponent<AudioSource>();
        else
            Debug.Log("Falied to load Audio Source");
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
            /////////////////
            //      obj.GetComponent<_TSizeChange>().ResetToNaturalSize();
            /////////////////
        }
    }

    void Update()
    {
        if (!run)
            return;
        //    TutorialMode2();
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

    void TutorialMode2()
    {

        bool held = Input.GetButton("Fire1");

        switch (curMol)
        {
            case CurrentMolecule.None:
                if (held && !last && !hold)
                {
                    if (text)
                    {
                        finish = true;
                    }
                    else
                    {
                        SwitchMode();
                    }
                }
                break;
            case CurrentMolecule.ATP:
                hold = false;
                if (enz.transform.rotation.eulerAngles.y > 170 && enz.transform.rotation.eulerAngles.y < 225)
                    moveEnzyme = true;
                if (moveEnzyme)
                    if (MoveToEnzyme(atp))
                    {
                        moveEnzyme = false;
                        curMol = CurrentMolecule.None;
                    }
                break;
            case CurrentMolecule.GTP:
                hold = false;
                if (enz.transform.rotation.eulerAngles.y < 170 && enz.transform.rotation.eulerAngles.y > 115)
                    moveEnzyme = true;
                if (moveEnzyme)
                    if (MoveToEnzyme(gtp))
                    {
                        moveEnzyme = false;
                        curMol = CurrentMolecule.None;
                    }
                break;
        }
        last = held;
    }

    void SwitchMode()
    {

        switch (cNum)
        {
            case 0:
                DispEnzyme2();
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top, 1f));
                break;
            case 1:
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top));
                break;
            case 2:
                ClearText();
                DispATP2();
                stop.Add(StartCoroutine(TurnTextOn(cNum++, TextChild.Right, 1f)));
                break;
            case 3:
                hold = true;
                Invoke("PrepareATPmove", 1);
                StopCurrentTexts();
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top));
                break;
            case 4:
                ClearText();
                DispGTP2();
                stop.Add(StartCoroutine(TurnTextOn(cNum++, TextChild.Left, 1f)));
                break;
            case 5:
                hold = true;
                Invoke("PrepareGTPmove", 1);
                StopCurrentTexts();
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top));
                break;
            case 6:
                hold = true;

                ClearText();
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top));
                break;
            case 7:
                ClearText();
                StartCoroutine(TurnTextOn(cNum++, TextChild.Top));
                break;
        }
    }

    void StopCurrentTexts()
    {
        foreach (Coroutine co in stop)
        {
            StopCoroutine(co);
        }
        stop.Clear();
        ClearText();
    }

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

    void PrepareATPmove()
    {
        curMol = CurrentMolecule.ATP;
    }
    void PrepareGTPmove()
    {
        curMol = CurrentMolecule.GTP;
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

    void DispEnzyme2()
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
    }
    void DispATP2()
    {
        enzymeTime = Time.time;
        atp = Instantiate(ATP, new Vector3(2, 1, 4), Quaternion.identity) as GameObject;
        Rigidbody rb = atp.GetComponent<Rigidbody>();
        rb.useGravity = false;

        atp.GetComponent<_TSizeChange>().startSmall = true;

        atp.GetComponent<_TSizeChange>().Inititalize();
        atp.GetComponent<_TSizeChange>().StartGrow();
        atp.GetComponent<_TMover>().enabled = false;
        atp.GetComponent<_TDestroyByTime>().CancelDestroy();
    }

    void DispGTP2()
    {
        gtp = Instantiate(GTP, new Vector3(-2, 1, 4), Quaternion.identity) as GameObject;
        Rigidbody rb = gtp.GetComponent<Rigidbody>();
        rb.useGravity = false;

        gtp.GetComponent<_TSizeChange>().startSmall = true;

        gtp.GetComponent<_TSizeChange>().Inititalize();
        gtp.GetComponent<_TSizeChange>().StartGrow();
        gtp.GetComponent<_TMover>().enabled = false;
        gtp.GetComponent<_TDestroyByTime>().CancelDestroy();
    }

    void ClearText()
    {
        foreach (Transform child in tutorialStuff.transform.GetChild(0).transform)
        {
            child.GetComponent<TextMeshPro>().text = "";
        }
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

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1, voiceSounds[curAudioClip++]));

        initTime += displayTime;
        text = "This cGas detects Viral DNA.";

        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1, voiceSounds[curAudioClip++]));
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
        StartCoroutine(DisplayText(text, TextChild.Right, initTime, displayTime - 2, voiceSounds[curAudioClip++]));
        initTime += displayTime - 1;

        text = "Attach the ATP to the Enzyme binding pocket.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime, voiceSounds[curAudioClip++]));
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
        StartCoroutine(DisplayText(text, TextChild.Left, initTime, displayTime - 2, voiceSounds[curAudioClip++]));
        initTime += displayTime - 1;

        text = "Attach the GTP to the Enzyme binding pocket.";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime, voiceSounds[curAudioClip++]));
    }
    void ScoreDescription()
    {
        float initTime = 1.0f;

        string text = "cGas will then move to the Endoplasmic Reticulum";
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime, voiceSounds[curAudioClip++]));
        initTime += displayTime + 1;

        text = "You will get 3 rounds of ATP";
        StartCoroutine(DispMol(ATP, initTime, displayTime - 1));
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1, voiceSounds[curAudioClip++]));
        initTime += displayTime;

        text = "You will then get 3 rounds of GTP";
        StartCoroutine(DispMol(GTP, initTime, displayTime - 1));
        StartCoroutine(DisplayText(text, TextChild.Top, initTime, displayTime - 1, voiceSounds[curAudioClip++]));
        initTime += displayTime;

        // float timePerEnzyme = GetComponent<_TGameController>().timePerEnzyme;

        text = "When the cGas is Completed you get 10 points.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime, voiceSounds[curAudioClip++]));
        initTime += displayTime + 1;

        text = "The Timer will also increase.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime - 1, voiceSounds[curAudioClip++]));
        initTime += displayTime;

        text = "Build as many cGas as you can before time runs out.";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, displayTime, voiceSounds[curAudioClip++]));
        initTime += displayTime + 1;

        text = "Ready!";
        float oldSize = tutorialStuff.transform.GetChild(0).transform.GetChild((int)TextChild.Bottom).gameObject.GetComponent<TextMeshPro>().fontSize;
        StartCoroutine(ChangeTextSize(48, TextChild.Bottom, initTime));
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, .5f, voiceSounds[curAudioClip++]));
        initTime += 1.5f;

        text = "Begin!";
        StartCoroutine(DisplayText(text, TextChild.Bottom, initTime, .5f, voiceSounds[curAudioClip++]));

        startGameTime += initTime + 1;
        ChangeTutorialState(TutorialState.startGame);
    }
    IEnumerator ChangeTextSize(float size, TextChild childNum, float startTime)
    {
        yield return new WaitForSeconds(startTime);
        tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject.GetComponent<TextMeshPro>().fontSize = 36.0f;
    }
    IEnumerator DisplayText(string text, TextChild childNum, float startText, float displayDuration, AudioClip vo = null)
    {
        float startVO = 0.5f;
        GameObject obj = tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject;
        obj.GetComponent<_TSizeChange>().startSmall = true;
        obj.GetComponent<_TSizeChange>().Inititalize();

        yield return new WaitForSeconds(startText);

        obj.GetComponent<_TSizeChange>().ResetToSmall();
        obj.SetActive(true);
        obj.GetComponent<TextMeshPro>().text = text;
        obj.GetComponent<_TSizeChange>().StartGrow();
        yield return new WaitForSeconds(startVO);

        if (vo)
            source.PlayOneShot(vo);
        else
            Debug.Log("No Audio Clip");

        yield return new WaitForSeconds(displayDuration - startVO);

        obj.GetComponent<_TSizeChange>().StartShrink();
    }

    IEnumerator TurnTextOn(int index, TextChild childNum, float waitTime = 0)
    {
        while (text)
            yield return 0;

        yield return new WaitForSeconds(waitTime);

        text = true;

        TextMeshPro tmp = tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject.GetComponent<TextMeshPro>();
        tutorialStuff.transform.GetChild(0).transform.GetChild((int)childNum).gameObject.SetActive(true);
        source.PlayOneShot(voiceSounds[index]);

        tmp.text = "_";

        while (tmp.text != texts[index] && !finish)
        {
            yield return new WaitForSeconds(GlobalVariables.textDelay);

            if (tmp.text.Length == texts[index].Length)
            {
                tmp.text = texts[index];
            }
            else
            {
                tmp.text = tmp.text.Insert(tmp.text.Length - 1, texts[index][tmp.text.Length - 1].ToString());
            }
        }
        tmp.text = texts[index];
        finish = false;
        text = false;
    }
}