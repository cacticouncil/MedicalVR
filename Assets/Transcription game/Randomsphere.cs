using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

enum Letters
{
    C, G, U, A, T, A2
};

public enum GNE
{
    GAC, ACT, GAC2, TCT, CGT, TAC, TCT2, GAC3, CAT
}

public class Randomsphere : MonoBehaviour
{
    public GameObject EventSystem;
    public GameObject memoryui;
    public GameObject victoryeffect, NextLevel;
    public GameObject[] Spheres;
    public GameObject[] Spheres2;

    public bool aPlayed = false;
    public string que;

    public static int correct;

    private GameObject clonei;
    private GameObject clonej;
    private GameObject clonek;

    private Color[] colors = new Color[6];
    private Vector3[] positions = new Vector3[6];

    public GNE Genes;

    // Use this for initialization
    void Start()
    {
        NextLevel.SetActive(false);

        correct = 0;

        colors[(int)Letters.C] = Spheres2[(int)Letters.C].GetComponent<Renderer>().sharedMaterial.color;
        colors[(int)Letters.G] = Spheres2[(int)Letters.G].GetComponent<Renderer>().sharedMaterial.color;
        colors[(int)Letters.U] = Spheres2[(int)Letters.U].GetComponent<Renderer>().sharedMaterial.color;
        colors[(int)Letters.A] = Spheres2[(int)Letters.A].GetComponent<Renderer>().sharedMaterial.color;
        colors[(int)Letters.T] = Spheres2[(int)Letters.T].GetComponent<Renderer>().sharedMaterial.color;
        colors[(int)Letters.A2] = Spheres2[(int)Letters.A2].GetComponent<Renderer>().sharedMaterial.color;

        positions[(int)Letters.C] = Spheres2[(int)Letters.C].transform.position;
        positions[(int)Letters.G] = Spheres2[(int)Letters.G].transform.position;
        positions[(int)Letters.U] = Spheres2[(int)Letters.U].transform.position;
        positions[(int)Letters.A] = Spheres2[(int)Letters.A].transform.position;
        positions[(int)Letters.T] = Spheres2[(int)Letters.T].transform.position;
        positions[(int)Letters.A2] = Spheres2[(int)Letters.A2].transform.position;

        if (GlobalVariables.arcadeMode == true)
            Genes = (GNE)Random.Range(0, 9);
        else
            Genes = 0;

        switch (Genes)
        {
            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 2;
            //j = 0;
            //k = 5;
            case GNE.ACT:
                {
                    que = "ACT";
                    clonei = Instantiate(Spheres[2], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC2:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 0;
            //k = 5;
            case GNE.TCT:
                {
                    que = "TCT";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 0;
            //j = 1;
            //k = 5;
            case GNE.CGT:
                {
                    que = "CGT";
                    clonei = Instantiate(Spheres[0], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[1], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 2;
            //k = 0;
            case GNE.TAC:
                {
                    que = "TAC";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 0;
            //k = 5;
            case GNE.TCT2:
                {
                    que = "TCT";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC3:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 0;
            //j = 2;
            //k = 5;
            case GNE.CAT:
                {
                    que = "CAT";
                    clonei = Instantiate(Spheres[0], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;
            default:
                break;
        }
    }

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Reset();
    //    }
    //}

    public void Correct()
    {
        correct++;
        if (correct == 3)
        {
            NextLevel.SetActive(true);
            NextLevel.GetComponent<TMPro.TextMeshPro>().text = "Next Level";
            victoryeffect.GetComponent<ParticleSystem>().Play();
            EventSystem.SetActive(false);

            Invoke("Reset", 3.0f);
        }
    }

    public void Reset()
    {
        NextLevel.SetActive(false);
        EventSystem.SetActive(true);
        memoryui.GetComponent<MemoryUI>().Level += 1;

        memoryui.GetComponent<MemoryUI>().Finish();
        aPlayed = false;

        Destroy(clonei);
        Destroy(clonej);
        Destroy(clonek);

        GNE temp = Genes;

        if (GlobalVariables.arcadeMode == true)
        {
            Genes = (GNE)Random.Range(0, 9);
            while (temp == Genes)
                Genes = (GNE)Random.Range(0, 9);
        }
        else
        {
            Genes += 1;
        }

        switch (Genes)
        {
            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 2;
            //j = 0;
            //k = 5;
            case GNE.ACT:
                {
                    que = "ACT";
                    clonei = Instantiate(Spheres[2], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC2:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 0;
            //k = 5;
            case GNE.TCT:
                {
                    que = "TCT";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 0;
            //j = 1;
            //k = 5;
            case GNE.CGT:
                {
                    que = "CGT";
                    clonei = Instantiate(Spheres[0], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[1], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 2;
            //k = 0;
            case GNE.TAC:
                {
                    que = "TAC";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 3;
            //j = 0;
            //k = 5;
            case GNE.TCT2:
                {
                    que = "TCT";
                    clonei = Instantiate(Spheres[3], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[0], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 1;
            //j = 2;
            //k = 0;
            case GNE.GAC3:
                {
                    que = "GAC";
                    clonei = Instantiate(Spheres[1], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[0], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;

            //i = 0;
            //j = 2;
            //k = 5;
            case GNE.CAT:
                {
                    que = "CAT";
                    clonei = Instantiate(Spheres[0], new Vector3(-15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonej = Instantiate(Spheres[2], new Vector3(0, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                    clonek = Instantiate(Spheres[5], new Vector3(15, 26, 30), Spheres[0].transform.rotation, transform) as GameObject;
                }
                break;
            default:
                CellGameplayScript.loadCase = 3;
                Set.SetAndEnterStatic(15);
                break;
        }

        Spheres2[(int)Letters.C].transform.position = positions[(int)Letters.C];
        Spheres2[(int)Letters.G].transform.position = positions[(int)Letters.G];
        Spheres2[(int)Letters.U].transform.position = positions[(int)Letters.U];
        Spheres2[(int)Letters.A].transform.position = positions[(int)Letters.A];
        Spheres2[(int)Letters.T].transform.position = positions[(int)Letters.T];
        Spheres2[(int)Letters.A2].transform.position = positions[(int)Letters.A2];

        Spheres2[(int)Letters.C].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.C];
        Spheres2[(int)Letters.G].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.G];
        Spheres2[(int)Letters.U].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.U];
        Spheres2[(int)Letters.A].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.A];
        Spheres2[(int)Letters.T].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.T];
        Spheres2[(int)Letters.A2].GetComponent<Renderer>().sharedMaterial.color = colors[(int)Letters.A2];

        foreach (GameObject item in Spheres2)
        {
            item.GetComponent<Collider>().enabled = true;
        }
        correct = 0;

        memoryui.GetComponent<MemoryUI>().finnished = false;
        if (!(memoryui.GetComponent<MemoryUI>().startTime < 15))
            memoryui.GetComponent<MemoryUI>().startTime -= 5;
        memoryui.GetComponent<MemoryUI>().timeRemaining = memoryui.GetComponent<MemoryUI>().startTime;
    }


    public GameObject Getclonei()
    {
        if (clonei)
            return clonei;
        else
        {
            Debug.Log("Unable to return clonei");
            return null;
        }
    }
    public GameObject Getclonej()
    {
        if (clonej)
            return clonej;
        else
        {
            Debug.Log("Unable to return clonej");
            return null;
        }
    }
    public GameObject Getclonek()
    {
        if (clonek)
            return clonek;
        else
        {
            Debug.Log("Unable to return clonek");
            return null;
        }
    }
}
