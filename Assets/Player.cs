using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject fireball;
    public Vector3 fireballOffset;
    public float projectileSpeed;

    public Transform mechDragon;

    public LayerMask playerCollisionMask;
    Vector3 overlapOffset1 = new Vector3 (0,0,1.6f) , overLapOffset2 = new Vector3 (0,0, -1.6f);

    float pitch, yaw, roll, thrust;
    public float acceleration, minSpeed = 0.1f,  MaxSpeed = 1f;
    
    Vector3 previousPos;
    public float velocity;

    bool GameStarted;

    void Start ()
    {
        //VRSettings.renderScale = 1.5f;    //supersampling up/down (nosta/laske arvoa, default = 1f;
	}
	
    void Update()
    {
        velocity = (mechDragon.position - previousPos).magnitude;
        previousPos = mechDragon.position;
        
        if (GameStarted)
        {

            if (Input.GetButton("Fire1"))
            {   //TO DO: Fine tune velocityn vaikutus projectile speediin
                GameObject fb = Instantiate(fireball, mechDragon.position + mechDragon.forward, Quaternion.identity) as GameObject;
                fb.GetComponent<Fireball>().Shoot(mechDragon.forward, projectileSpeed * 6 * velocity);
            }
            
            //TO DO

            pitch = Input.GetAxis("Pitch");
            yaw = Input.GetAxis("Yaw");                //yaw = Input.GetAxis("Left_stick_horizontal");
            roll = -Input.GetAxis("Roll");
            //thrust = Mathf.Clamp(Input.GetAxis("Gas"),0.1f, 1f);  //legacy thrust
            
            mechDragon.Rotate(mechDragon.right * pitch, Space.World);   //climb up or down  ("elevator")
            mechDragon.Rotate(mechDragon.up * yaw, Space.World);        //idly barrel roll  ("ailerons")
            mechDragon.Rotate(mechDragon.forward * roll, Space.World);  //steer left/right  ("rudder")

            //turn based on the roll ("banking)
            //mechDragon.Rotate(new Vector3(0, -Vector3.Dot(Vector3.up, mechDragon.right), 0));

            if (Input.GetAxis("Gas") > 0.01f && thrust < MaxSpeed)
            {
                thrust = thrust + Time.deltaTime * acceleration;
            }
            else if (thrust > minSpeed)
            {
                thrust = thrust - Time.deltaTime * acceleration / 2;
            }
            else
            {
                thrust = minSpeed;
            }

            mechDragon.Translate(mechDragon.forward * thrust, Space.World);
        }
        else
            if (Input.GetButton("BreathFire"))
                StartGame();
            
        if (Input.GetKeyDown(KeyCode.R))
            InputTracking.Recenter();
        
    }

    void Accelerate()
    {

    }
    void DeAccelerate()
    {

    }

    void FixedUpdate()
    {
        if (GameStarted)
            if (Hitsomething(playerCollisionMask))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    bool Hitsomething(LayerMask mask)
    {
        Collider[] hitColliders = Physics.OverlapCapsule(
            mechDragon.position + overlapOffset1, mechDragon.position + overLapOffset2, 2f, playerCollisionMask);

        if (hitColliders.Length > 0)
            return true;
        else
            return false;
    }

    void OnGUI()
    {
        if (!GameStarted)
            GUILayout.Label("Press A to Start");
    }

    void StartGame()
    {
        GameStarted = true;
    }


    //high score systeemi
}
