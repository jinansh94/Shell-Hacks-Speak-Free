using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecord : MonoBehaviour {

    // Use this for initialization
    public static AudioSource aud;
    bool playSound;
    static int recordTime = 500;
    float TotalTime=0f;
    void Start () {
        aud = GetComponent<AudioSource>();
// aud.clip = Microphone.Start(null, true, recordTime, 44100);
        playSound = false;
	}
	
	// Update is called once per frame
	void Update () {
  /*      TotalTime = TotalTime + Time.deltaTime;
        //Debug.Log(aud.clip.length);
		if(TotalTime>(recordTime+5)&&!playSound)
        {
            playSound = true;
            Debug.Log(aud.time);
            Debug.Log(aud.clip.length);
            aud.Play();
            Debug.Log("Reached here and sound should have been played");
        }*/
	}
    public static void startRecording()
    {
        Debug.Log("Started Recording YATTTART");
        aud.clip = Microphone.Start(null, true, recordTime, 44100);
    }
    public static void stopRecording()
    {
        Microphone.End(null);
        Debug.Log("Audio Time!!!" + aud.time);
        Debug.Log(aud.clip.length);
        aud.Play();
    }
}
