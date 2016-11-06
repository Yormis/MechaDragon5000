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

        Dropfire(rainorigin);
        yield return new WaitForSeconds(0.5f);

        /*
        Vector3[] rainPositions = new Vector3[8];

        for (...)
        {
            Vector3 pos = new Vector3(x, y, 0);
            rainPositions[i] = pos;
        }
        */

        Destroy(gameObject);
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
