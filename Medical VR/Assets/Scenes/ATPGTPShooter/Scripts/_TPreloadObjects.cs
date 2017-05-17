using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TPreloadObjects : MonoBehaviour
{
    GameObject enzyme;
    GameObject atpOrb;
    GameObject gtpOrb;

    bool ItemsAreSet = false;

    // Use this for initialization
    void Start()
    {
        InstantiateThese();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InstantiateThese()
    {
        GameObject enz = Instantiate(enzyme, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        enz.GetComponent<_TRandomRotator>().enabled = false;
        Rigidbody rb = enz.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        enz.GetComponent<_TSizeChange>().Inititalize();
        enz.GetComponent<_TTravelToNucleus>().enabled = false;


        GameObject atp = Instantiate(atpOrb, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        Rigidbody atpRB = atp.GetComponent<Rigidbody>();
        atpRB.useGravity = false;

        atp.transform.LookAt(enz.transform);
        atpRB.velocity = transform.forward * 5;

        atp.GetComponent<_TSizeChange>().Inititalize();
        atp.GetComponent<_TMover>().enabled = false;
        atp.GetComponent<_TDestroyByTime>().CancelDestroy();


        GameObject gtp = Instantiate(gtpOrb, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        Rigidbody gtpRB = gtp.GetComponent<Rigidbody>();
        gtpRB.useGravity = false;

        gtp.transform.LookAt(enz.transform);
        gtpRB.velocity = transform.forward * 5;

        gtp.GetComponent<_TSizeChange>().Inititalize();
        gtp.GetComponent<_TMover>().enabled = false;
        gtp.GetComponent<_TDestroyByTime>().CancelDestroy();
    }
}
