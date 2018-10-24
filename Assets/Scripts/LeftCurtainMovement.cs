using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCurtainMovement : MonoBehaviour {
	public bool updateOn = false;
    float currentPos;
    bool playSarah = true;
	void Start ()
	{
        currentPos = transform.localPosition.x;
        CheckAudioLull.speakingThreshold = 0f;
        
	}

	void Update()
	{
		if (updateOn == true) 
		{
           
			transform.Translate (Vector3.right * Time.deltaTime * 0.50f);
            if(playSarah)
            {
                GameObject.Find("StageSarah").GetComponent<AudioSource>().Play();
                playSarah = false;
            }
         
		}
        if(transform.localPosition.x-currentPos>5f)
        {
           // AudioRecord.startRecording();
            this.gameObject.SetActive(false);

        }
	}

	
}