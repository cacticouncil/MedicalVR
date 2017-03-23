using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RGBSlider : MonoBehaviour {

    public Color myColor;

    public Material RecticleMaterial;

    public Image RecticleImage;

    public Slider R;
    public Slider G;
    public Slider B;

    void OnGUI()
    {
        myColor = RGBS(new Rect(0, 135,200, 20), myColor);
    }

    Color RGBS(Rect screenRect, Color rgb)
    {
        rgb.r = R.value;
        
        screenRect.y += 20;

        rgb.g = G.value;

        screenRect.y += 20;

        rgb.b = B.value;

        rgb.a = 255;

        return rgb;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RecticleMaterial.color = myColor;
        RecticleImage.color = myColor;
	}
}
