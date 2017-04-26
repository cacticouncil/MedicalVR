using UnityEngine;
using System.Collections;

public class EnterNameScript : MonoBehaviour {

    public GameObject letter1, letter2, letter3, scoreBoard;

    char[] alph = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    
    int index, index1, index2;
    // Use this for initialization
    void Start ()
    {
        index = 0;
        index1 = 0;
        index2 = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
   public void UpOne()
    {
        index++;
        index = index % 26;
        letter1.GetComponent<TMPro.TextMeshPro>().text = alph[index].ToString();
    }
    public void UpTwo()
    {
        index1++;
        index1 = index1 % 26;
        letter2.GetComponent<TMPro.TextMeshPro>().text = alph[index1].ToString();
    }
    public void UpThree()
    {
        index2++;
        index2 = index2 % 26;
        letter3.GetComponent<TMPro.TextMeshPro>().text = alph[index2].ToString();
    }

    public void DownOne()
    {
        if (index <= 0)
            index = 26;

        index--;
        
        letter1.GetComponent<TextMesh>().text = alph[index].ToString();
    }
    public void DownTwo()
    {
        if (index1 <= 0)
            index1 = 26;

        index1--;
        letter2.GetComponent<TMPro.TextMeshPro>().text = alph[index1].ToString();
    }
    public void DownThree()
    {
        if (index2 <= 0)
            index2 = 26;

        index2--;
        letter3.GetComponent<TMPro.TextMeshPro>().text = alph[index2].ToString();
    }
    public void SetName()
    {
        scoreBoard.GetComponent<ScoreBoardScript>().currName = alph[index].ToString() + alph[index1].ToString() + alph[index2].ToString();
        scoreBoard.transform.position = transform.position;
        scoreBoard.GetComponent<ScoreBoardScript>().PlaceName();
        gameObject.SetActive(false);
    }
}
