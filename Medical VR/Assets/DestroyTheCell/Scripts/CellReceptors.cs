using UnityEngine;
using System.Collections;
using System;

public class CellReceptors : MonoBehaviour, TimedInputHandler
{
    public GameObject VirusAttack;
    GameObject WaveManager;
    GameObject Player;
    bool SpawnFiveAttackVirus = false;
    float SpawnTimer;
    int Count = 5;
    public int health = 5;
    GameObject OriginalTutorialGameObject;
    void Start()
    {
        SpawnTimer = 0.0f;
        WaveManager = gameObject.transform.parent.gameObject;
        Player = WaveManager.GetComponent<WaveManager>().Player;

        if (!GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber != 1)
            GetComponent<Rigidbody>().AddForce(new Vector3(100, 100, 100));

        OriginalTutorialGameObject = WaveManager.GetComponent<WaveManager>().TutorialLocationEnd;
    }

    void Update()
    {
        if (SpawnFiveAttackVirus == true && Count > 0)
        {
            SpawnTimer += Time.deltaTime;

            if (SpawnTimer >= .3f)
            {
                SpawnTimer = 0.0f;
                SpawnAttackVirus();
                Count -= 1;
                if (Count == 0)
                    Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
            }
        }

        if (Player.GetComponent<VirusPlayer>().isGameover)
            Destroy(this.gameObject);

        //Check Rigidbody
        //if (GlobalVariables.tutorial == true && WaveManager.GetComponent<WaveManager>().WaveNumber == 1)
        //{
        //    if (transform.position != OriginalTutorialGameObject.transform.position)
        //        Vector3.MoveTowards(transform.position, OriginalTutorialGameObject.transform.position, 10);
        //}

        //Vector3.MoveTowards(transform.position, OriginalTutorialGameObject.transform.position, 10);
        //GetComponent<Rigidbody>().MovePosition(OriginalTutorialGameObject.transform.position);
    }

    public void SpawnAttackVirus()
    {
        GameObject V = Instantiate(VirusAttack, Player.transform.position, Quaternion.identity) as GameObject;
        V.GetComponent<AttackVirus>().MainCamera = Player;
        V.GetComponent<AttackVirus>().target = gameObject;
        V.GetComponent<AttackVirus>().enabled = true;
    }

    public void OnGazeEnter()
    {
        if (Count > 0)
            Player.GetComponent<VirusPlayer>().currSpeed = 0;
    }

    public void OnGazeExit()
    {
        Player.GetComponent<VirusPlayer>().currSpeed = Player.GetComponent<VirusPlayer>().baseSpeed;
        SpawnFiveAttackVirus = false;
    }

    public void HandleTimeInput()
    {
        if (WaveManager.GetComponent<WaveManager>().CanDestroyProteins == true)
        {
            SpawnFiveAttackVirus = true;
        }
    }

    void OnDestroy()
    {
        WaveManager.GetComponent<WaveManager>().CellReceptorCount -= 1;
        WaveManager.GetComponent<WaveManager>().CellReceptorsList.Remove(transform.gameObject);
    }
}
