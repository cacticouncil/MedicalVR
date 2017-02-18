using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimonSays : MonoBehaviour {

    // Use this for initialization
    public GameObject yellow;
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    Color cY, cR, cB, cG;
    enum theColors
    {
        YELLOW,
        RED,
        BLUE,
        GREEN
    }

    theColors selectedColor;
    List<theColors> theList = new List<theColors>();

    void Start ()
    {
        cY = yellow.GetComponent<Renderer>().material.color;
        cR = red.GetComponent<Renderer>().material.color;
        cB = blue.GetComponent<Renderer>().material.color;
        cG = green.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        switch (selectedColor)
        {
            case theColors.YELLOW:
                yellow.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case theColors.RED:
                red.GetComponent<Renderer>().material.color = Color.red;
                break;
            case theColors.BLUE:
                blue.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case theColors.GREEN:
                green.GetComponent<Renderer>().material.color = Color.green;
                break;
            default:
                break;
        }
    }
    void GeneratePattern()
    {
        theColors tmpColor = new theColors();
        tmpColor = (theColors)Random.Range(0, 3);
        theList.Add(tmpColor);
    }
    void TurnOffLights()
    {
        yellow.GetComponent<Renderer>().material.color = cY;
        red.GetComponent<Renderer>().material.color = cR;
        blue.GetComponent<Renderer>().material.color = cB;
        green.GetComponent<Renderer>().material.color = cG;
    }
   public void selectYellow()
    {
        selectedColor = theColors.YELLOW;
        TurnOffLights();
    }
    public void selectRed()
    {
        selectedColor = theColors.RED;
        TurnOffLights();
    }
    public void selectBlue()
    {
        selectedColor = theColors.BLUE;
        TurnOffLights();
    }
    public void selectGreen()
    {
        selectedColor = theColors.GREEN;
        TurnOffLights();
    }
}
