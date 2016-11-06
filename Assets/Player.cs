using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public BreakTimerManager m_timerManager;

    AudioManager audioManager;

    public Animator anim;
    public GameObject fireball;
    public GameObject oilDrop;
    public Transform oilDropPos;
    public Vector3 fireballOffset;
    public Transform jaw;
    public float projectileSpeed;

    public float hitRadius = 0.5f;

    public Transform mechDragon;

    public LayerMask playerCollisionMask;
    Vector3 overlapOffset1 = new Vector3 (0,0,1.6f) , overLapOffset2 = new Vector3 (0,0, -1.6f);

    float pitch, yaw, roll, thrust;
    public float acceleration, minSpeed = 0.1f,  MaxSpeed = 1f;
    
    Vector3 previousPos;
    public float velocity;

    bool GameStarted;

    private DragonInputController m_breakableControls = null;

    void Start ()
    {
        //VRSettings.renderScale = 1.5f;    //supersampling up/down (nosta/laske arvoa, default = 1f;

        audioManager = AudioManager.instance;

        m_breakableControls = DragonInputController.Instance;
	}
	
    void SetAnimFloat(string name, float f, float minValue)
    {
        bool positive = minValue > 0.0f;

        if(positive)
        {
            if (f > minValue)
            {
                anim.SetFloat(name, f);
            }
            else
            {
                anim.SetFloat(name, 0.0f);
            }
        }
        else
        {
            if (f < minValue)
            {
                f = Mathf.Abs(f);
                anim.SetFloat(name, f);
            }
            else
            {
                anim.SetFloat(name, 0.0f);
            }
        }

        
    }

    float fireAnimV;
    bool isUpdate = true;

    void Update()
    {
        if (!isUpdate)
            return;

        if(m_breakableControls == null)
        {
            m_breakableControls = DragonInputController.Instance;
        }

        velocity = (mechDragon.position - previousPos).magnitude;
        previousPos = mechDragon.position;
        
        if(DragonValues.Instance.FuelAmount <= 0.0f)
        {
            AudioManager.instance.PlayAudioAt(mechDragon.position, "LowFuel", mechDragon);
            gameObject.AddComponent<Rigidbody>();
            isUpdate = false;
        }

        if (GameStarted)
        {

            if (/*Input.GetButtonDown("Fire1")*/ m_breakableControls.BurpFire())
            {   //TO DO: Fine tune velocityn vaikutus projectile speediin
                GameObject fb = Instantiate(fireball, jaw.position, Quaternion.identity) as GameObject;
                fb.GetComponent<Fireball>().Shoot(mechDragon.forward, projectileSpeed * 10 * velocity /2 );
                AudioManager.instance.PlayAudioAt(mechDragon.position + new Vector3(0, 0, 23), "FireBreath");
                fireAnimV = 1.0f;
            }

            //TODO drop oil
            if (/*Input.GetButtonDown("Fire2")*/ m_breakableControls.DropFuel())
            {
                GameObject fb = Instantiate(oilDrop, oilDropPos.position, Quaternion.identity) as GameObject;
                fb.GetComponent<Oil>().Shoot(Vector3.down, projectileSpeed);
                AudioManager.instance.PlayAudioAt(mechDragon.position + new Vector3(0, -2.6f, -8.5f), "OilDrop");
            }

            fireAnimV -= Time.deltaTime * 1.5f;
            fireAnimV = Mathf.Clamp01(fireAnimV);
            anim.SetFloat("Shoot", fireAnimV);

            pitch = -m_breakableControls.GetPitch(); //-Input.GetAxis("Pitch");    
            yaw = m_breakableControls.GetYaw(); //Input.GetAxis("Yaw");         
            roll = -m_breakableControls.GetRoll(); //Input.GetAxis("Roll");

            float minValue = 0.1f;
            SetAnimFloat("Pitch+", pitch, minValue);
            SetAnimFloat("Pitch-", pitch, -minValue);
            SetAnimFloat("Yaw+", yaw, minValue);
            SetAnimFloat("Yaw-", yaw, -minValue);
            SetAnimFloat("Roll+", roll, minValue);
            SetAnimFloat("Roll-", roll, -minValue);

            anim.SetFloat("Idle", 1.0f);

            //thrust = Mathf.Clamp(Input.GetAxis("Gas"),0.1f, 1f);  //legacy thrust

            mechDragon.Rotate(mechDragon.right * pitch, Space.World);   //climb up or down  ("elevator")
            mechDragon.Rotate(mechDragon.up * yaw, Space.World);        //idly barrel roll  ("ailerons")
            mechDragon.Rotate(mechDragon.forward * roll, Space.World);  //steer left/right  ("rudder")

            //turn based on the roll ("banking)
            //mechDragon.Rotate(new Vector3(0, -Vector3.Dot(Vector3.up, mechDragon.right), 0));

            //muista muokata fuel consumptionia thrustin/kaasun mukaan
            if (/*Input.GetAxis("Gas")*/ m_breakableControls.GetGasSpeed() > 0.01f && thrust < MaxSpeed)
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
        {
            StartGame();
            audioManager.PlayAudioAt(mechDragon.position, "GameStart");
        }
            
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("ResetCamera"))
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
            mechDragon.position + overlapOffset1, mechDragon.position + overLapOffset2, hitRadius, playerCollisionMask);

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
        m_timerManager.enabled = !m_timerManager.enabled;
    }


    //high score systeemi
}
