//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//public class SpawnVirus : MonoBehaviour
//{
//    float Speed = .03f;
//    public bool LeaveCell = false;
//    public GameObject VirusAttack;
//    public List<GameObject> VirusAttackList = new List<GameObject>();
//    public int SpawnVirusNumber;
//    Vector3 SpawnVirusAttack;

//    void Update()
//    {
//        for (int i = 0; i < VirusAttackList.Count; i++)
//        {
//            for (int j = 0; j < this.gameObject.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().ProteinList.Count; j++)
//            {
//                if (this.gameObject.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().ProteinList[j].GetComponent<ProteinScript>().AttackMe == true)
//                {
//                    VirusAttackList[i].transform.position = Vector3.MoveTowards(VirusAttackList[i].transform.position, this.gameObject.GetComponent<VirusPlayer>().WaveManager.GetComponent<WaveManager>().ProteinList[j].transform.position, Speed);
//                }
//            }
//        }
//    }

//    void FixedUpdate()
//    {
//        if (LeaveCell == true)
//            transform.position += transform.forward * Speed;
//    }

//    public void SpawnAttackViruses()
//    {
//        SpawnVirusAttack = transform.position;
//        VirusAttackList.Add(Instantiate(VirusAttack, SpawnVirusAttack, Quaternion.identity) as GameObject);
//    }
//}
