using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public GameObject fire;
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
        AudioManager.instance.PlayAudioAt(transform.position, "FireDestroy");
        mr.enabled = false;
        sc.enabled = false;
        StartCoroutine("Firerain");
    }

    IEnumerator Firerain()
    {
        Vector3 rainorigin = transform.position + new Vector3(0f, 1f, 0f);
		float rainRate = 0.2f;

        Dropfire(rainorigin);
		yield return new WaitForSeconds(rainRate);
		MakeItRain (rainorigin, 2.0f, 1.0f);
		yield return new WaitForSeconds(rainRate);
		MakeItRain (rainorigin, 1.0f, 2.0f);

        Destroy(gameObject);
    }

	void MakeItRain(Vector3 pos, float thetaScale, float radius)
	{
		float theta_scale = thetaScale;
		int size = (int)((2.0f * Mathf.PI) / theta_scale);

		for (float theta = 0; theta < 2 * Mathf.PI; theta += theta_scale)
		{
			float x = radius * Mathf.Cos (theta);
			float y = radius * Mathf.Sin (theta);

			Vector3 dropPos = new Vector3 (x, 0.0f, y);
			Dropfire(pos + dropPos);
		}
	}

    void Dropfire(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = new Ray(position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            Debug.DrawLine(position, hit.point, Color.white, 1.0f);
            Instantiate(fire, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.DrawLine(position, position + Vector3.down*5.0f, Color.red, 1.0f);
        }
    }
		
}
