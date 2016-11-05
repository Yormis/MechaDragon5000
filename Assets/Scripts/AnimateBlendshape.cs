using UnityEngine;
using System.Collections;

public class AnimateBlendshape : MonoBehaviour
{
	public float speed = 1.0f;
	float v = 0.0f;
	float dir = 1.0f;
	public AnimationCurve curve;

	SkinnedMeshRenderer meshRend;

	void Start()
	{
		meshRend = GetComponent<SkinnedMeshRenderer> ();
	}

	void Update ()
	{
		if (v <= 0.0f)
		{
			dir = 1f;
			v = 0.1f;
		}

		if (v >= 100.0f)
		{
			dir = -1f;
			v = 99f;
		}

		v += speed * dir * Time.deltaTime;

		v = Mathf.Clamp (v, 0.0f, 100.0f);
		meshRend.SetBlendShapeWeight (0, curve.Evaluate(v/100.0f)*100.0f);
	}
}
