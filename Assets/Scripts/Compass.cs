﻿using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

    public Transform mount;
    public Transform ball;
    
	void Start ()
    {
	
	}
	
	void Update ()
    {

        ball.rotation = Quaternion.Slerp(ball.rotation, Quaternion.identity, 1);
	}
}