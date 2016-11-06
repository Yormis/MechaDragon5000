using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    public Transform mount;
    public Transform ball;
    Quaternion rot;
    Transform followTransform;

	void Awake ()
    {
        rot = transform.rotation;
        followTransform = transform.parent;
        transform.parent = null;
    }
	
	void Update ()
    {
        if (ball != null)
            Quaternion.Slerp(ball.rotation, rot, 3.0f * Time.deltaTime);

        transform.position = followTransform.position;

    }
}
