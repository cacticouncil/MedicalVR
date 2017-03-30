using UnityEngine;
using System.Collections;

public class _TRedAttacher : MonoBehaviour
{
    enum Position { inProgress, center, final, none };

    public bool _debug;
    public GameObject shotToAttack;
    public GameObject ConnectionParticle;
    public float ParticleSize;

    private Position caseSwitch = Position.none;
    private GameObject OurAttachedEnzyme;
    //private Collider moleculeCol;
    private float moverSpeed;
    private Transform colliders;


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
            //    moleculeCol = other;
            OurAttachedEnzyme = other.gameObject;
            moverSpeed = 3;
            // moverSpeed = OurAttachedEnzyme.GetComponent<_TMover>().speed;
            OurAttachedEnzyme.GetComponent<_TDestroyByTime>().CancelDestroy();
            OurAttachedEnzyme.tag = "Untagged";
            caseSwitch = Position.inProgress;
            Destroy(OurAttachedEnzyme.GetComponent<SphereCollider>());
            DebugStuff();
            OurAttachedEnzyme.GetComponent<Rigidbody>().useGravity = false;
        }
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

        SetParticle();

        if (OurAttachedEnzyme.name == "ATP Cell" || OurAttachedEnzyme.name == "ATP Cell(Clone)")
            transform.parent.GetComponent<_TEnzymeController>().SetATP();
        else if (OurAttachedEnzyme.name == "GTP Cell" || OurAttachedEnzyme.name == "GTP Cell(Clone)")
            transform.parent.GetComponent<_TEnzymeController>().SetGTP();
        else
            Debug.Log("Could not determine Object");

   //     GameObject haz = Instantiate(hazard, spawnPosition, spawnRotation) as GameObject;

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

    void SetParticle()
    {
        GameObject flash;
        if (ConnectionParticle)
        {
            Vector3 flashPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            flash = Instantiate(ConnectionParticle, transform.position, transform.rotation, transform) as GameObject;
            flash.transform.localScale = new Vector3(ParticleSize, ParticleSize, ParticleSize);
            Debug.Log("current Position: " + flash.transform.position);
            Debug.Log("Transforms Position: " + transform.position);
            flash.transform.localPosition = new Vector3(0, -0.1f, 0);
        }
        else
            Debug.Log("No Connection Particle to Instatiate.");
    }
    void DebugStuff()
    {
        if (_debug)
            Debug.Log("Current position is " + caseSwitch);
    }
}


