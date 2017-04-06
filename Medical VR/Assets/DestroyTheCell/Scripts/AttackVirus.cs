using UnityEngine;
using System.Collections;

public class AttackVirus : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject WaveManager;
    Vector3 SavedProteinLocation;
    float Speed = .03f;
    float TempTimerForAttackVirusToLeaveCell = 0.0f;
    public bool CanLeaveCell = false;

    void Start()
    {
        WaveManager = MainCamera.GetComponent<VirusPlayer>().WaveManager;
    }

    void Update()
    {
        for (int i = 0; i < MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count; i++)
        {
            if (MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList[i].GetComponent<CellReceptors>().AttackMe == true)
            {
                SavedProteinLocation = MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList[i].transform.position;
            }
        }
    }

    void FixedUpdate()
    {
        TempTimerForAttackVirusToLeaveCell += Time.deltaTime;
        if (TempTimerForAttackVirusToLeaveCell >= 7.5f)
            Destroy(this.gameObject);
        
        if (CanLeaveCell == false)
            transform.position = Vector3.MoveTowards(transform.position, SavedProteinLocation, Speed);

        else if (CanLeaveCell == true)
            transform.position += transform.forward * Speed;

        if (transform.position == SavedProteinLocation)
        {
            for (int i = 0; i < MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList.Count; i++)
            {
                if (transform.position == MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList[i].transform.position)
                {
                    Destroy(MainCamera.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().CellReceptorsList[i].gameObject);
                }
            }
            CanLeaveCell = true;
        }
    }
}
