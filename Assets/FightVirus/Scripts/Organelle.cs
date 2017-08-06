//using UnityEngine;
//using System.Collections;

//enum OrganelleMovement { MoveRight = 0, MoveLeft = 1, MoveUp = 2, MoveDown = 3 }
//public class Organelle : MonoBehaviour
//{
//    public GameObject OrganelleManager;
//    public GameObject FightVirusPlayer;
//    public GameObject DestroyTheCellPlayer;
//    public bool isDead;
//    //OrganelleMovement CM;
//    //Vector3 StartPos;
//    void Start()
//    {
//        OrganelleManager = gameObject.transform.parent.gameObject;
//        FightVirusPlayer = OrganelleManager.GetComponent<OrganelleManager>().FightVirusPlayer;
//        DestroyTheCellPlayer = OrganelleManager.GetComponent<OrganelleManager>().DestoryTheCellPlayer;
//        isDead = false;
//        //StartPos = transform.position;
//        //CM = (OrganelleMovement)Random.Range(0, 4);
//    }

//    void Update()
//    {
//        //switch (CM)
//        //{
//        //    case OrganelleMovement.MoveRight:
//        //        transform.position = new Vector3(StartPos.x + Mathf.Sin(Time.time), StartPos.y, StartPos.z);
//        //        break;

//        //    case OrganelleMovement.MoveLeft:
//        //        transform.position = new Vector3(StartPos.x - Mathf.Sin(Time.time), StartPos.y, StartPos.z);
//        //        break;

//        //    case OrganelleMovement.MoveUp:
//        //        transform.position = new Vector3(StartPos.x, StartPos.y + Mathf.Sin(Time.time), StartPos.z);
//        //        break;

//        //    case OrganelleMovement.MoveDown:
//        //        transform.position = new Vector3(StartPos.x, StartPos.y - Mathf.Sin(Time.time), StartPos.z);
//        //        break;

//        //    default:
//        //        break;
//        //}

//        if (this.gameObject.GetComponent<Player>())
//        {
//            if (FightVirusPlayer.GetComponent<Player>().isGameOver)
//                Destroy(this.gameObject);
//        }

//        else if (this.gameObject.GetComponent<VirusPlayer>())
//        {
//            if (DestroyTheCellPlayer.GetComponent<VirusPlayer>().isGameover)
//                Destroy(this.gameObject);
//        }
//    }
//}
