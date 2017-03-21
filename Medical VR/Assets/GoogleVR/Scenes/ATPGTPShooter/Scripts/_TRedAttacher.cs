using UnityEngine;
using System.Collections;

public class _TRedAttacher : MonoBehaviour
{
    enum Position { inProgress, center, final, none };

    public bool _debug;
    public GameObject shotToAttack;

    private Position caseSwitch = Position.none;
    private GameObject OurAttachedEnzyme;
    private Collider moleculeCol;
    private float moverSpeed;
    private Transform colliders;


    //    SphereCollider sc; = gameObject.AddComponent("SphereCollider") as SphereCollider;

    private void Start()
    {
        colliders = transform.parent.FindChild("Colliders");
        _debug = false;
    }

    private void FixedUpdate()
    {
        switch (caseSwitch)
        {
            case Position.inProgress:
                if (OurAttachedEnzyme)
                    MoveToPosition(transform.GetChild(0));
                break;
            case Position.center:
                MoveToPosition(transform);
                break;
            case Position.final:
                attachMolecule();
                break;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(shotToAttack.tag) && !OurAttachedEnzyme)
        {
            moleculeCol = other;
            OurAttachedEnzyme = other.gameObject;
            moverSpeed = OurAttachedEnzyme.GetComponent<_TMover>().speed;
            OurAttachedEnzyme.GetComponent<_TDestroyByTime>().CancelDestroy();
            OurAttachedEnzyme.tag = "Untagged";
            caseSwitch = Position.inProgress;
            Destroy(OurAttachedEnzyme.GetComponent<SphereCollider>());
            DebugStuff();
        }


        //        Check with colliding with a ricochet-able surface(OnCollisionEnter)
        //
        //    Extract the normal of the surface you are colliding via contact point

        //    Use the projectile / bullet's direction ( transform.forward ) and the normal to calculate the vector of the projectile/bullet when it is reflected Vector3.Reflect
        //
        //    Apply the reflected vector to your projectile / bullet





    }

    private void attachMolecule()
    {

        OurAttachedEnzyme.transform.SetParent(transform.parent.gameObject.transform, false);
        OurAttachedEnzyme.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        OurAttachedEnzyme.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        OurAttachedEnzyme.transform.localPosition = transform.localPosition;
        Destroy(OurAttachedEnzyme.GetComponent<Rigidbody>());
        ++caseSwitch;
        DebugStuff();
        SphereCollider sc = colliders.gameObject.AddComponent<SphereCollider>();
        sc.center = transform.localPosition;
        sc.radius = 0.3f;
     //   sc.transform.localPosition = transform.localPosition;
    }
    void MoveToPosition(Transform pos)
    {
        OurAttachedEnzyme.GetComponent<Rigidbody>().velocity = Vector3.zero;

        OurAttachedEnzyme.transform.LookAt(pos);
        OurAttachedEnzyme.transform.Translate(Vector3.forward * moverSpeed * Time.deltaTime);

        if (.03f >= Vector3.Distance(OurAttachedEnzyme.transform.position, pos.position))
        {
            ++caseSwitch;
            DebugStuff();
        }
    }
    void DebugStuff()
    {
        if (_debug)
            Debug.Log("Current position is " + caseSwitch);
    }
}


