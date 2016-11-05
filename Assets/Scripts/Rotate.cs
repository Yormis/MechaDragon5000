using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public Vector3 rotate;
	public bool local = false;

	void Update ()
	{
		transform.Rotate (rotate * Time.deltaTime, local ? Space.Self : Space.World);
	}
}
