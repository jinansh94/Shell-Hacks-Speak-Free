using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour {

    // Use this for initialization
    bool endRecord = false;
    bool startRecord = false;
    public static bool canStart = false;

	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            foreach (int value in Enum.GetValues(typeof(OVRInput.Button)))
            {
                Console.WriteLine(((OVRInput.Button)value).ToString());
                if (OVRInput.Get((OVRInput.Button)value))
                {

                    switch ((OVRInput.Button)value)
                    {
                        case OVRInput.Button.One:
                            Debug.Log("End Recording");
                            if (!endRecord)
                            {
                                endRecord = true;
                                StopRecording();
                            }

                            break;
                        case OVRInput.Button.Two:
                            Debug.Log("Button Required pressed!!");
                            if (!startRecord)
                            {
                                startRecord = true;
                                ButtonPressed();

                            }
                            break;
                    }
                }
            }
        }
    }

    void StopRecording()
    {
        
        transform.position = new Vector3(24f, -4.7f, -33.5f);
        transform.localEulerAngles = new Vector3(0f, -85.414f, 2f);
        AudioSource cc1 = GameObject.Find("CrowdChatter").GetComponent<AudioSource>();
        cc1.Stop();
        cc1 = GameObject.Find("CrowdChatter1").GetComponent<AudioSource>();
        cc1.Stop();
        GetComponent<CheckAudioLull>().stopRecording();
        //AudioRecord.stopRecording();
        

    }

    

    void ButtonPressed()
    {
        transform.position = new Vector3(0.59f, -3.32f, 2.36f);
        transform.localEulerAngles = new Vector3(0f, -85.414f, 2f);

        GetComponent<CheckAudioLull>().startRecording();
        // this.GetComponent<RightCurtainMovement>().updateOn
        GameObject.Find("LeftCurtainIMP").GetComponent<LeftCurtainMovement>().updateOn = true;
        GameObject.Find("RightCurtainIMP").GetComponent<RightCurtainMovement>().updateOn = true;
        // GetComponent<LeftCurtainMovement>().updateOn = true;


    }
}
