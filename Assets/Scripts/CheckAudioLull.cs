using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAudioLull : MonoBehaviour {

    // Use this for initialization
    public AudioSource audioSource,audioSource1;
    int recordTime = 100;
    float startTime = 0f;
    bool isPlaying = false;
    float audioPlayTime = 0f;
    float totalSilentTime = 0f;
    TextMesh myText;
    public static float speakingThreshold=0f;
    float speakingThresholdHelper = 0f;
    public static int performance;
    AudioSource cchatter1, cchatter2;
    float[] clipSampleData = new float[1024];
    bool startRecord = false;
    AudioSource Sarah;
    bool SarahEndSpeakStart = false;
	void Start () {
        Sarah = GameObject.Find("Sarah").GetComponent<AudioSource>();
        Debug.Log("chikncisandiasd");
        audioSource = GetComponent<AudioSource>();

         cchatter1 = GameObject.Find("CrowdChatter").GetComponent<AudioSource>();
         cchatter2 = GameObject.Find("CrowdChatter1").GetComponent<AudioSource>();
        cchatter1.loop = true;
        cchatter2.loop = true;
        cchatter1.volume = 0;
        cchatter2.volume = 0;

        cchatter1.Play();
        cchatter2.Play();


        // myText = GameObject.Find("MyText").gameObject.GetComponent<TextMesh>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if(!Sarah.isPlaying&&SarahEndSpeakStart)
        {
            Debug.LogError("SHOULD START SPEECH!!");
            audioSource1.Play();
            SarahEndSpeakStart = false;
        }
        if (!startRecord)
            return;
        startTime = startTime + Time.deltaTime;
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
        float clipLoudness = 0f;
        foreach (var sample in clipSampleData)
        {
            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness = clipLoudness / 1024;
        if (clipLoudness < 0.01f)
        {
            totalSilentTime += Time.deltaTime;
            if (totalSilentTime > 2f)
            {
                speakingThresholdHelper += Time.deltaTime;
               
                
                if(speakingThresholdHelper>speakingThreshold)
                {
                    speakingThreshold += Time.deltaTime;
                    if (speakingThreshold > 10)
                        speakingThreshold = 10;
                }
                //Debug.Log("Too much time silent!!");
            }
            else
            {
                speakingThresholdHelper = 0;
                speakingThreshold *= 0.97f;
                
               // Debug.Log("Silent");
            }
        }
        else
        {
            totalSilentTime = 0f;
            //Debug.Log("Not Silent");
           
        }

        float volume = (speakingThreshold - 4) / 6;
        cchatter1.volume = cchatter2.volume = volume;
        int speakingThresh = (int)speakingThreshold;

        float varPerformance;
        Debug.Log(speakingThresh);
        switch (speakingThresh)
        {
            case 0:
                performance = 0;
                break;
            case 1:               
                performance = 1;
                break;           
            case 2:           
            case 3:              
                performance = 2;
                break;
            case 4:
            case 5: 
               varPerformance= Random.Range(0, 10);
                if (varPerformance > 7)
                {
                    performance = 4;

                }

                else
                    performance = 2;
                break;          

            default:
                
               

                varPerformance = Random.Range(0, 10);
                if (varPerformance > 7)
                    performance = 2;
                else
                    performance = 4;
                break;


        }
       

    }
    public void stopRecording()
    {
        Microphone.End(null);
        Debug.LogError("Audio Time!!!" + audioSource1.time);
        Sarah.clip = GameObject.Find("Sarah").GetComponent<SarahScript>().endClip;
        Sarah.Play();
        SarahEndSpeakStart = true;
        
        //Debug.LogError(audioSource1.)
        Debug.LogError(audioSource1.clip.length);
        audioSource1.volume = 1;
        audioSource1.mute = false;
       // audioSource1.Play();
    }

    public void postStopRecording()
    {

    }

    public void startRecording()
    {
        startRecord = true;
        audioSource.clip = Microphone.Start("", true, recordTime, 44100);
        audioSource1.clip = audioSource.clip;
        audioSource.mute = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

}



