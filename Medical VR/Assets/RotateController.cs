using UnityEngine;
using System.Collections;

public class RotateController : MonoBehaviour {

	public void ToggleX()
	{
		foreach (Rotate x in GetComponentsInChildren<Rotate>()) {
			x.x = !x.x;
		}
	}
	public void ToggleY()
	{
		foreach (Rotate x in GetComponentsInChildren<Rotate>()) {
			x.y = !x.y;
		}
	}
	public void ToggleZ()
	{
		foreach (Rotate x in GetComponentsInChildren<Rotate>()) {
			x.z = !x.z;
		}
	}
}
