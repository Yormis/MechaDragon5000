using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    
    public void PlayAudioAt(Vector3 pos, string clipName)
    {
        bool success = false;
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<AudioSource>().clip.name == clipName)
            {
                child.position = pos;
                child.GetComponent<AudioSource>().Play();
                success = true;
            }
        }
        if (!success)
        {
            Debug.Log("audio clip _" + clipName + "_ not found");
        }
    }

}
