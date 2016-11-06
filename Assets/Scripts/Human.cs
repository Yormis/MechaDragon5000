using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour
{
	public AnimationCurve jumpCurve;
	float v;
	public float jumpSpeed = 1.0f;
	public float jumpHeight = 0.1f;
	public Transform graphics;

	Vector3 startPos;

	float waypointTimer;
	Vector3 targetPos;
	public float walkSpeed = 2.0f;
	public float minWalkDelay = 2.0f;
	public float maxWalkDelay = 8.0f;
	public float maxWalkRadius = 1.0f;

	void Start ()
	{
		startPos = graphics.localPosition;
		v = Random.Range (0.0f, 1.0f);
		waypointTimer = Time.time + Random.Range (minWalkDelay, maxWalkDelay);
		targetPos = transform.position;
	}

	void Update ()
	{
		v += Time.deltaTime * jumpSpeed;

		if (v > 1.0f)
		{
			v = 0.0f;
		}

		graphics.localPosition = startPos + Vector3.up * jumpCurve.Evaluate (v) * jumpHeight;

		transform.position = Vector3.MoveTowards (transform.position, targetPos, walkSpeed * Time.deltaTime);

		Vector3 lookRotation = targetPos - transform.position;
		if(lookRotation != Vector3.zero)
		{
			transform.forward = Vector3.Slerp (transform.forward, lookRotation, 10.0f * Time.deltaTime);
		}

		if (Time.time > waypointTimer)
		{
			waypointTimer = Time.time + Random.Range (minWalkDelay, maxWalkDelay);
			Vector3 move = new Vector3 (Random.Range (-maxWalkRadius, maxWalkRadius), 0.0f, Random.Range (-maxWalkRadius, maxWalkRadius));
			Vector3 newWaypoint = transform.position + Vector3.up + move;
			
			if(!Physics.Linecast(transform.position + Vector3.up, transform.position + Vector3.up + move))
			{
				RaycastHit hit;
				if (Physics.Raycast (newWaypoint, Vector3.down, out hit, 1.1f))
				{
					if (hit.transform.gameObject.GetComponent<Human> () == null)
					{
						targetPos = hit.point;
					}
				}
			}
		}
	}
}
