using UnityEngine;
using System.Collections;
using System;

public class CellReceptors : MonoBehaviour
{
    GameObject Player;
    GameObject WaveManager;
    public GameObject VirusAttack;

    private bool AmITargeted = false;
    private bool NeverTargetAgain = false;

    public int Health = 4;
    private float SpeedForTutorial = 1.3f;
    public float TutorialCellReceptorTimer = 0.0f;
    Vector3 SavedLocation;
    void Start()
    {
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;

        if (!GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber != 1)
            GetComponent<Rigidbody>().AddForce(new Vector3(100, 100, 100));

        SavedLocation = WaveManager.GetComponent<WaveManager>().TutorialLocationEnd.transform.position;
    }

    void FixedUpdate()
    {
        if (GvrViewer.Instance.Triggered && AmITargeted == true && NeverTargetAgain == false)
        {
            NeverTargetAgain = true;
            StartCoroutine(FireAttackVirus(4, .3f));
        }

        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);

        if (GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber == 1)
        {
            AmITargeted = false;

            TutorialCellReceptorTimer += Time.deltaTime;

            //if (TutorialCellReceptorTimer <= 3.0f)
            //    Player.GetComponent<VirusPlayer>().isCellReceptorDone = true;
            
            if (transform.position != SavedLocation)
                transform.position = Vector3.MoveTowards(transform.position, SavedLocation, SpeedForTutorial * Time.fixedDeltaTime);

            if (transform.position == SavedLocation)
            {
                AmITargeted = true;
                Player.GetComponent<VirusPlayer>().ProceedTutorial = true;
            }     
        }
    }


    public void OnGazeEnter()
    {
        Player.GetComponent<VirusPlayer>().currSpeed = 0;
        AmITargeted = true;
    }

    public void OnGazeExit()
    {
        Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
        AmITargeted = false;
    }

    public void SpawnAttackVirus()
    {
        GameObject V = Instantiate(VirusAttack, Player.transform.position, Quaternion.identity) as GameObject;
        V.GetComponent<AttackVirus>().MainCamera = Player;
        V.GetComponent<AttackVirus>().target = gameObject;
        V.GetComponent<AttackVirus>().enabled = true;
    }

    IEnumerator FireAttackVirus(int num, float duration)
    {
        for (int i = 0; i < num; i++)
        {
            SpawnAttackVirus();
            yield return new WaitForSeconds(duration);
        }

        Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
    }

    void OnDestroy()
    {
        WaveManager.GetComponent<WaveManager>().CellReceptorCount -= 1;
        WaveManager.GetComponent<WaveManager>().CellReceptorsList.Remove(transform.gameObject);
    }
}

