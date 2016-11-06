using UnityEngine;
using System.Collections;

public class Needle : MonoBehaviour
{
	public Vector3 minEuler;
	public Vector3 maxEuler;
	float value;
	public float targetValue;
	float timer;
	float random;

	void Update()
	{
		if (Time.time > timer)
		{
			timer = Time.time + Random.Range (0.25f, 0.75f);
			random = Random.Range (0.0f, 0.05f);
		}

		value = Mathf.Lerp (value, targetValue + random, 2.0f * Time.deltaTime);
		transform.localEulerAngles = Vector3.Lerp (minEuler, maxEuler, value);
	}

	public void SetTarget(float v)
	{
		targetValue = v;
	}
}
