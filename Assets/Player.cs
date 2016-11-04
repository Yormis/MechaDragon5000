using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Player : MonoBehaviour {

    public Transform mechDragon;

    float pitch, yaw, roll;


	void Start ()
    {
        //VRSettings.renderScale = 1.5f;    //supersampling up/down (nosta/laske arvoa, default = 1f;)
        

	}
	
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            InputTracking.Recenter();
        }


        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Horizontal");



        //mechDragon.Rotate(new Vector3(pitch, yaw, 0));
        mechDragon.Rotate(mechDragon.right * pitch, Space.World);
        mechDragon.Rotate(mechDragon.up * yaw, Space.World);
    }


    public void Speed(float speed)
    {

    }

    public void Pitch(float x)
    {
        //pitch der dragon
    }

    public void Yaw(float y)
    {

    }
    public void Roll(float z)
    {

    }
}
