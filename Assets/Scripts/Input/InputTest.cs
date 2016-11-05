using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour
{
    float pitch = 0f;
    float roll = 0f;
    float yaw = 0f;
    float gas = 0f;

    bool resetCamera = false;
    bool breathFire = false;
    bool fartOil = false;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        pitch = Input.GetAxis("Pitch");
        roll = Input.GetAxis("Roll");
        yaw = Input.GetAxis("Yaw");
        gas = Input.GetAxis("Gas");

        resetCamera = Input.GetButton("ResetCamera");
        breathFire = Input.GetButton("BreathFire");
        fartOil = Input.GetButton("FartOil");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 500, 2000), "Pitch - " + pitch + "\nRoll - " + roll + "\nYaw - " + yaw + "\nGas " + gas
            + "\n\nResetCamera: " + resetCamera + "\nBreathFire: " + breathFire + "\nFartOil: " + fartOil);
    }
}
