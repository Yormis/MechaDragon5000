using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    float burntimer;
    public float minBurnTime = 2.0f; 

	// Use this for initialization
	void Start () {

        burntimer = Time.time + minBurnTime;
	}

    // Update is called once per frame
    void Update()
    {

        if (Time.time > burntimer)
        {
            Destroy(gameObject);
        }

    }

    public void AddTime(float t)
    {
        burntimer = burntimer + t;
    }

}
