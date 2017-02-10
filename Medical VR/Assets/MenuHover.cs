using UnityEngine;
using System.Collections;

public class MenuHover : MonoBehaviour {

    public float sightlenght = 100f;
    public GameObject selectedObj;
    public float hoverForwardDistance = 5f;
	// Use this for initialization
	void FixedUpdate ()
    {
        RaycastHit seen;
        Ray raydirection = new Ray(transform.position, transform.forward);

        if(seen.collider.tag == "Button")
        {
            if(selectedObj != null && selectedObj != seen.transform.gameObject)
            {
                GameObject hitObject = seen.transform.gameObject;
                MoveMenuButton(hitObject);
            }
            selectedObj = seen.transform.gameObject;
        }
	}
	
	// Update is called once per frame
	void MoveMenuButton (GameObject hitObject)
    {
        Vector3 newZ = hitObject.transform.position;
        newZ.z -= hoverForwardDistance;
        selectedObj.transform.position = newZ;

        newZ.z += hoverForwardDistance * 2;
        hitObject.transform.position = newZ;
	}
}
