using UnityEngine;
using System.Collections;

public class _TRaycastTarget : MonoBehaviour
{
    public GameObject gun;
    public GameObject oldReticle;
    public GameObject newReticle;
    

    private Transform targetTransform;
    private Ray theRay;

    // Use this for initialization
    void Start()
    {
        if (!gun)
            Debug.Log("failed to load Gun");
//        if (!oldReticle)
//            Debug.Log("failed to load Reticle");
    }

    // Update is called once per frame
    void Update()
    {        
        theRay.origin = gun.transform.position;
        theRay.direction = gun.transform.TransformDirection(Vector3.forward);

        RaycastHit info;     

       
        if(Physics.Raycast(theRay, out info))
        {            
            if(info.collider.CompareTag("Virus"))
            {
  //              print("There is a virus in front of the object!");
            }
        }       
    }
}
