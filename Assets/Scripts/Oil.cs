using UnityEngine;
using System.Collections;

public class Oil : MonoBehaviour
{

	public GameObject oilPuddle;
	Rigidbody rb;
	MeshRenderer mr;
	SphereCollider sc;

	// Use this for initialization
	void Start()
	{

		rb = GetComponent<Rigidbody>();
		mr = GetComponent<MeshRenderer>();
		sc = GetComponent<SphereCollider>();
	}


	public void Shoot(Vector3 dir, float force)
	{
		if (rb == null)
		{
			rb = GetComponent<Rigidbody>();
		}
		rb.AddForce(dir * force);
	}

	void OnCollisionEnter(Collision collision)
	{
		mr.enabled = false;
		sc.enabled = false;

		DropOil (transform.position);

		Destroy(gameObject);

	}

	void DropOil(Vector3 position)
	{
		RaycastHit hit;
		Ray ray = new Ray(position, Vector3.down);

		if (Physics.Raycast(ray, out hit, 5.0f))
		{
			Debug.DrawLine(position, hit.point, Color.white, 1.0f);
			Instantiate(oilPuddle, hit.point, Quaternion.identity);
		}
		else
		{
			Debug.DrawLine(position, position + Vector3.down*5.0f, Color.red, 1.0f);
		}
	}
}
