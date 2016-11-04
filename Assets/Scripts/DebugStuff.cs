using UnityEngine;
using System.Collections;

public class DebugStuff : MonoBehaviour {

    // Use this for initialization

    public GameObject fireball;
    public float force;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject go = Instantiate(fireball, transform.position,Quaternion.identity) as GameObject;
            go.GetComponent<Fireball>().Shoot(transform.forward, force);
        }
    }
}
