using UnityEngine;

public class SubtitlesStart : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (GlobalVariables.subtitles == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        GetComponent<TMPro.TextMeshPro>().fontSize = GlobalVariables.textSize;
    }
}
