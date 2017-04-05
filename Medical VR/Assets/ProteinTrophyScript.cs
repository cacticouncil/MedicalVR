using UnityEngine;
using System.Collections;

enum ColorProteins { GreenProtein = 0, RedProtein = 1, BlueProtein = 2, YellowProtein = 3 }
public class ProteinTrophyScript : MonoBehaviour
{
    public Material Green;
    public Texture Green1;

    public Material Red;
    public Texture Red1;

    public Material Blue;
    public Texture Blue1;

    public Material Yellow;
    public Texture Yellow1;

    ColorProteins C;
    float TimeToSwitch = 0.0f;
   
    void Start ()
    {
        C = ColorProteins.BlueProtein;
	}
	
	void Update ()
    {
        TimeToSwitch += Time.deltaTime;

        if (TimeToSwitch >= 4.0f)
        {
            //C += 1;

            //if (C >= (ColorProteins)4)
            //    C = 0;

            C = 0;
            SetTxture();
            TimeToSwitch = 0.0f;
        }
    }

    void SetTxture()
    {
        switch (C)
        {
            case ColorProteins.GreenProtein:
                //Green.SetTexture("ProteinProtainGreen_Albedo", Green1);
                //Green.mainTexture = Green;
                
                break;

            case ColorProteins.RedProtein:
                break;

            case ColorProteins.BlueProtein:
                break;

            case ColorProteins.YellowProtein:
                break;

            default:
                break;
        }
    }
}
