using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahScript : MonoBehaviour {

    // Use this for initialization
    public List<AudioClip> ac;
    public AudioClip endClip;
    float delayBetweenEachSound = 1f;
    AudioSource asrc;
	void Start () {
        asrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!asrc.isPlaying)
        {
            delayBetweenEachSound -= Time.deltaTime;
            if (delayBetweenEachSound < 0f)
            {
                AudioClip gotClip = nextAudio();
                if (gotClip != null)
                {
                    asrc.clip = gotClip;
                    asrc.Play();
                }
                else
                {
                    PlayerScript.canStart = true;
                }
            }
        }
	}

    AudioClip nextAudio()
    {
        delayBetweenEachSound = 1f;
        //Debug.LogError("Capacity is :"+ac.Count);
        if (ac.Count > 0)
        {
            AudioClip asrc = ac[0];
            ac.RemoveAt(0);
            return asrc;
        }
        return null;
        
    }
}
