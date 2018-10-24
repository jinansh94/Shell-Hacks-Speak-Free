using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour {

    // Use this for initialization
    int arr = 0;// 0=nothing,1=nod, 2=shake, 3= small nod , 4=chitchat
    float actionTime = 0f;
    float actionTimeConst = 0f;
    float headRotateConst = 20f;
    float z,y,x;
    Vector3 startRotate;
    GameObject spine,spine1;
    float spinethresh = 15f, spine1thresh = 7f, headthresh = 60f, isTalking = 0f;
    float spineY, spine1Y;
    float stayTalk = 3f;
    public AudioSource audioSource;
    public AudioClip ac;
    //
    void Start () {
        int delay = (int)Random.Range(0, 10);
        arr = 6;
        actionTime = delay;
        //yield return new WaitForSeconds(delay);
        startRotate = transform.localEulerAngles;
        spine = transform.parent.parent.parent.parent.gameObject;
        spine1 = transform.parent.parent.parent.gameObject;
        z = startRotate.z;
        y = startRotate.y;
        x = startRotate.x;
        spineY = spine.transform.localEulerAngles.y;
        spine1Y = spine1.transform.localEulerAngles.y;
        //Debug.Log("starting rotation value"+startRotate.z);
	}
	
	// Update is called once per frame
	void Update () {
        if(arr==0)
        {
            int x = CheckAudioLull.performance;

           // Debug.Log("value of x is "+x);
            if(x>=5||x==0)
            {
                return;
            }
            else
            {
                startAction(x);
            }
        }
        else
        {
            switch(arr)
            {
                case 1:
                    headNod();
                    break;
                case 2:
                    headShake();
                    //arr = 0;
                    break;
                case 3:
                    arr = 0;
                    break;
                case 4:
                    chitChat();
                    break;
                case 5:
                    talking();
                    break;
                case 6:
                default:
                    randomDelay();
                    break;
            }
        }

	}

    public void startAction(int x)
    {
        switch(x)
        {
            case 1:
                arr = 1;
                actionTime = 0.75f;
                actionTimeConst = 0.75f;
                headShake();
                arr = 1;
                break;
            case 2:
                arr = 2;
                actionTime = 0.75f;
                actionTimeConst = 0.75f;
                headShake();
                break;
            case 3:
                arr = 3;
                break;
            case 4:
                arr = 4;
                actionTime = 1f;
                actionTimeConst = 1f;
                chitChat();
                break;
            case 5:
                arr = 5;
                actionTime = 1f;
                actionTimeConst = 1f;
                talking();
                break;
        }
    }

    public void headNod()
    {
      
   
        actionTime = actionTime - Time.deltaTime;//1.5 to 0
        float angleChange = (actionTime / actionTimeConst)*180;// 1.5=180 and 0-0

        // magnitude = const,  
        // 0-90 in half the time
  //      Debug.Log("head nod initiated "+angleChange +"   "+actionTime+"  "+z+"   "+ Mathf.Sin( angleChange));
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y,z+ headRotateConst*Mathf.Sin(angleChange*Mathf.PI/180));


        if(angleChange<=-180)
        {
            //actionTime = 0;
            transform.localEulerAngles = startRotate;
            arr = 6;
            actionTime = Random.Range(0, 5);
        }

    }
   public void headShake()
    {
       

        actionTime = actionTime - Time.deltaTime;//1.5 to 0
        float angleChange = (actionTime / actionTimeConst) * 180;// 1.5=180 and 0-0

        // magnitude = const,  
        // 0-90 in half the time
      //  Debug.Log("head nod initiated " + angleChange + "   " + actionTime + "  " + z + "   " + Mathf.Sin(angleChange));
        transform.localEulerAngles = new Vector3(x + headRotateConst * Mathf.Sin(angleChange * Mathf.PI / 180), transform.localEulerAngles.y,transform.localEulerAngles.z );


        if (angleChange <= -180)
        {
            //actionTime = 0;
            transform.localEulerAngles = startRotate;
            actionTime = Random.Range(0, 5);
            arr = 6;
        }

    }

    public void chitChat()
    {
       
        
        actionTime = actionTime - Time.deltaTime;//1.5 to 0
        float angleChange = (actionTime / actionTimeConst) *(180-isTalking);// 1.5=180 and 0-0
        transform.localEulerAngles = new Vector3(x + headthresh * Mathf.Sin(angleChange * Mathf.PI / 180), transform.localEulerAngles.y, transform.localEulerAngles.z);
        spine.transform.localEulerAngles = new Vector3(spine.transform.localEulerAngles.x, spineY + spinethresh * Mathf.Sin(angleChange * Mathf.PI / 180), spine.transform.localEulerAngles.z);
        spine1.transform.localEulerAngles = new Vector3(spine1.transform.localEulerAngles.x, spine1Y + spine1thresh * Mathf.Sin(angleChange * Mathf.PI / 180), spine1.transform.localEulerAngles.z);


        if(angleChange<=(90-isTalking))
        {
            if (isTalking == 0)
            {

                startAction(5);
            //    actionTime = 3f;
              //  actionTimeConst = 3f;
                //arr = 5;
            }
            else
            {
                isTalking = 0;
                actionTime = Random.Range(0,5);
                transform.localEulerAngles = startRotate;
                arr = 6;
            }
        }
    }

    public void talking()
    {
       

        actionTime = actionTime - Time.deltaTime;
        //Debug.Log("talking"+actionTime);
        if(actionTime <= 0)
        {
            
        //    actionTime = 1f;
        //    actionTimeConst = 1f;
        //    arr = 4;
            isTalking =90f;
            startAction(4);
        }

    }
    public void randomDelay()
    {
        actionTime -= Time.deltaTime;
        if(actionTime<=0)
        {
            arr = 0;
            actionTime = 0f;
        }
    }

}
