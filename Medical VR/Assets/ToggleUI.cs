using UnityEngine;
using System.Collections;

public class ToggleUI : MonoBehaviour {

    public GameObject toggle1;
    public GameObject toggle2;
    
    public void toggle()
    {
        toggle1.SetActive(!toggle1.activeSelf);
        toggle2.SetActive(!toggle2.activeSelf);
    }
}
