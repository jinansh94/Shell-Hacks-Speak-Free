using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCurtainMovement : MonoBehaviour
{
    public bool updateOn = false;
    float currentPos;
    void Start()
    {
        currentPos = transform.localPosition.x;
    }

    void Update()
    {
        if (updateOn == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 0.50f);
        }

        if (transform.localPosition.x - currentPos < -5f)
        {
            this.gameObject.SetActive(false);
        }
    }

}