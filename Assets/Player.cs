using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public Transform mechDragon;

    public LayerMask playerCollisionMask;
    Vector3 overlapOffset1 = new Vector3 (0,0,1.6f) , overLapOffset2 = new Vector3 (0,0, -1.6f);

    float pitch, yaw, roll, thrust;


    bool broken;

    void Start ()
    {
        //VRSettings.renderScale = 1.5f;    //supersampling up/down (nosta/laske arvoa, default = 1f;
	}
	
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            InputTracking.Recenter();
        }
        
        if (!broken)
        {
            pitch = Input.GetAxis("Pitch");
            yaw = -Input.GetAxis("Yaw");                //yaw = Input.GetAxis("Left_stick_horizontal");
            roll = -Input.GetAxis("Roll");
            thrust = Mathf.Clamp(Input.GetAxis("Accelerator"),0.1f, 1f);
            
            mechDragon.Rotate(mechDragon.right * pitch, Space.World);   //climb up or down  ("elevator")
            mechDragon.Rotate(mechDragon.up * yaw, Space.World);        //idly barrel roll  ("ailerons")
            mechDragon.Rotate(mechDragon.forward * roll, Space.World);  //steer left/right  ("rudder")

            //turn based on the roll ("banking)
            //mechDragon.Rotate(new Vector3(0, -Vector3.Dot(Vector3.up, mechDragon.right), 0));

            mechDragon.Translate(mechDragon.forward * thrust, Space.World);
        }
        else
        {
            if (Input.GetButton("Fire1"))
            {
                Reset();
            }
        }


    }
    void FixedUpdate()
    {
        if (!broken)
        {
            if (Hitsomething(playerCollisionMask))
            {
                Destroy(mechDragon.gameObject);
                broken = true;
            }
        }

    }


    bool Hitsomething(LayerMask mask)
    {
        //Collider[] hitColliders = Physics.OverlapSphere(mechDragon.position + overlapOffset1, 2f, playerCollisionMask);
        Collider[] hitColliders = Physics.OverlapCapsule(
            mechDragon.position + overlapOffset1, mechDragon.position + overLapOffset2, 2f, playerCollisionMask);

        if (hitColliders.Length > 0)
            return true;
        else
            return false;
    }

    void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
