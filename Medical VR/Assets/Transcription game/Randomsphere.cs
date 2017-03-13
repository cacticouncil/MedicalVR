using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum Letters
{C, G, U, A, T, A2
};

public enum GNE
{
    GAC, ACT, GAC2, TCT, CGT, TAC, TCT2, GAC3, CAT
}

public class Randomsphere : MonoBehaviour {
    public GameObject memoryui;
    public GameObject[] Spheres;
    public GameObject[] Spheres2;
    private List<GameObject> testSpheres;
    public List<string> ans;
    private int i;
    private int j;
    private int k;

    int correct;

    private GameObject clonei;
    private GameObject clonej;
    private GameObject clonek;

    private Color C1color;
    private Color G1color;
    private Color U1color;
    private Color A1color;
    private Color T1color;
    private Color A2color;

    private Vector3 C1position;
    private Vector3 G1position;
    private Vector3 U1position;
    private Vector3 A1position;
    private Vector3 T1position;
    private Vector3 A2position;

    public GNE Genes;

    // Use this for initialization
    void Start () {

        testSpheres = new List<GameObject>();
        ans = new List<string>();

        correct = 0;

        C1color = Spheres2[(int)Letters.C].GetComponent<Renderer>().sharedMaterial.color;
        G1color = Spheres2[(int)Letters.G].GetComponent<Renderer>().sharedMaterial.color;
        U1color = Spheres2[(int)Letters.U].GetComponent<Renderer>().sharedMaterial.color;
        A1color = Spheres2[(int)Letters.A].GetComponent<Renderer>().sharedMaterial.color;
        T1color = Spheres2[(int)Letters.T].GetComponent<Renderer>().sharedMaterial.color;
        A2color = Spheres2[(int)Letters.A2].GetComponent<Renderer>().sharedMaterial.color;

        C1position = Spheres2[(int)Letters.C].transform.position;
        G1position = Spheres2[(int)Letters.G].transform.position;
        U1position = Spheres2[(int)Letters.U].transform.position;
        A1position = Spheres2[(int)Letters.A].transform.position;
        T1position = Spheres2[(int)Letters.T].transform.position;
        A2position = Spheres2[(int)Letters.A2].transform.position;

        Genes = (GNE)Random.Range(0, 9);

        switch (Genes)
        {
            case GNE.GAC:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.ACT:
                {
                    i = 2;
                    j = 0;
                    k = 3;
                }
                break;
            case GNE.GAC2:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.TCT:
                {
                    i = 3;
                    j = 0;
                    k = 3;
                }
                break;
            case GNE.CGT:
                {
                    i = 0;
                    j = 1;
                    k = 3;
                }
                break;
            case GNE.TAC:
                {
                    i = 3;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.TCT2:
                {
                    i = 3;
                    j = 0;
                    k = 5;
                }
                break;
            case GNE.GAC3:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.CAT:
                {
                    i = 0;
                    j = 2;
                    k = 3;
                }
                break;
            default:
                break;
        }

        clonei =  Instantiate(Spheres[i], new Vector3(-20,26,30), Spheres[0].transform.rotation)  as GameObject;
                                                                                                  
         clonej = Instantiate(Spheres[j], new Vector3(-5, 26, 30), Spheres[0].transform.rotation) as GameObject;
                                                                                                  
         clonek = Instantiate(Spheres[k], new Vector3(10, 26, 30), Spheres[0].transform.rotation) as GameObject;

        for (int x = 0; x < Spheres2.Length; x++)
        {
            testSpheres.Add(Spheres2[x]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < testSpheres.Count; i++)
        {
            if (testSpheres[i].GetComponent<TimeInputObject>().IsCorrect == true)
            {
                testSpheres[i].GetComponent<TimeInputObject>().IsCorrect = false;
                testSpheres.Remove(testSpheres[i]);
                correct += 1;
            }
               
        }

        if (correct == 3)
        {
            Reset();

            memoryui.GetComponent<MemoryUI>().Level += 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Reset();
        }
    }

    public void Reset()
    {
        memoryui.GetComponent<MemoryUI>().Finish();

        Destroy(clonei);
        Destroy(clonej);
        Destroy(clonek);

        ans.Clear();

        

        GNE temp = Genes;

        Genes = (GNE)Random.Range(0, 9);

        if (temp == Genes)
            Genes = (GNE)Random.Range(0, 9);



        switch (Genes)
        {
            case GNE.GAC:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.ACT:
                {
                    i = 2;
                    j = 0;
                    k = 5;
                }
                break;
            case GNE.GAC2:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.TCT:
                {
                    i = 3;
                    j = 0;
                    k = 5;
                }
                break;
            case GNE.CGT:
                {
                    i = 0;
                    j = 1;
                    k = 5;
                }
                break;
            case GNE.TAC:
                {
                    i = 3;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.TCT2:
                {
                    i = 3;
                    j = 0;
                    k = 5;
                }
                break;
            case GNE.GAC3:
                {
                    i = 1;
                    j = 2;
                    k = 0;
                }
                break;
            case GNE.CAT:
                {
                    i = 0;
                    j = 2;
                    k = 5;
                }
                break;
            default:
                break;
        }

        clonei = (GameObject)Instantiate(Spheres[i], new Vector3(-20, 26, 30), Spheres[0].transform.rotation);

        clonej = (GameObject)Instantiate(Spheres[j], new Vector3(-5, 26, 30), Spheres[0].transform.rotation);

        clonek = (GameObject)Instantiate(Spheres[k], new Vector3(10, 26, 30), Spheres[0].transform.rotation);

        Spheres2[(int)Letters.C].transform.position = C1position;
        Spheres2[(int)Letters.G].transform.position = G1position;
        Spheres2[(int)Letters.U].transform.position = U1position;
        Spheres2[(int)Letters.A].transform.position = A1position;
        Spheres2[(int)Letters.T].transform.position = T1position;
        Spheres2[(int)Letters.A2].transform.position = A2position;

        Spheres2[(int)Letters.C].GetComponent<Renderer>().sharedMaterial.color = C1color;
        Spheres2[(int)Letters.G].GetComponent<Renderer>().sharedMaterial.color = G1color;
        Spheres2[(int)Letters.U].GetComponent<Renderer>().sharedMaterial.color = U1color;
        Spheres2[(int)Letters.A].GetComponent<Renderer>().sharedMaterial.color = A1color;
        Spheres2[(int)Letters.T].GetComponent<Renderer>().sharedMaterial.color = T1color;
        Spheres2[(int)Letters.A2].GetComponent<Renderer>().sharedMaterial.color = A2color;



        testSpheres.Clear();
        for (int x = 0; x < Spheres2.Length; x++)
        {
            testSpheres.Add(Spheres2[x]);
        }
        correct = 0;

        memoryui.GetComponent<MemoryUI>().finnished = false;
        memoryui.GetComponent<MemoryUI>().startTime -= 10;
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
