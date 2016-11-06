using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
    float burntimer;
    public float minBurnTime = 2.0f; 
	public LayerMask burnableLayerMask;
	public GameObject fire;

	// Use this for initialization
	void Start ()
	{
        burntimer = Time.time + minBurnTime;
	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time > burntimer)
        {
            Destroy(gameObject);
        }

		Collider[] cols = Physics.OverlapSphere (transform.position, 1.0f, burnableLayerMask);

		for (int i = 0; i < cols.Length; ++i)
		{
            AudioManager.instance.PlayAudioAt(cols[i].transform.position, "FireSpread");
            Instantiate (fire, cols [i].transform.position, Quaternion.identity);
			cols [i].SendMessage ("Burn");
		}
    }

    public void AddTime(float t)
    {
        burntimer = burntimer + t;
    }

}
