using UnityEngine;
using System.Collections;

enum Letters
{C, G, A, U, T
};

enum GNE
{
    GAC, ACT, GAC2, TCT, CGT, TAC, TCT2, GAC3, CAT
}

public class Randomsphere : MonoBehaviour {

    public GameObject[]  Spheres = new GameObject[5];

    private int i;
    private int j;
    private int k;

    private GameObject clonei;
    private GameObject clonej;
    private GameObject clonek;

    private GNE Genes;
    // Use this for initialization
    void Start () {
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
                    k = 3;
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

        clonei = (GameObject)Instantiate(Spheres[i], new Vector3(-20,26,30), Spheres[0].transform.rotation);

         clonej = (GameObject)Instantiate(Spheres[j], new Vector3(-5, 26, 30), Spheres[0].transform.rotation);

         clonek = (GameObject)Instantiate(Spheres[k], new Vector3(10, 26, 30), Spheres[0].transform.rotation);


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(clonei);
            Destroy(clonej);
            Destroy(clonek);

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
                        k = 3;
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

            clonei = (GameObject)Instantiate(Spheres[i], new Vector3(-20, 26, 30), Spheres[0].transform.rotation);

            clonej = (GameObject)Instantiate(Spheres[j], new Vector3(-5, 26, 30), Spheres[0].transform.rotation);

            clonek = (GameObject)Instantiate(Spheres[k], new Vector3(-10, 26, 30), Spheres[0].transform.rotation);

        }
    }
}
