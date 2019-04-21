﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float speed = 463f;
    [Tooltip("In m")] [SerializeField] float xRange = 172f;
    [Tooltip("In m")] [SerializeField] float yRange = 84f;

    [SerializeField] float positionPitchFactor = 1f;
    [SerializeField] float controlPitchFactor = .5f;
    [SerializeField] float positionYawFactor = 1f;
    [SerializeField] float controlRollFactor = 50f;


    float xThrow, yThrow;
    
    // Use this for initialization
    void Start ()
    {
		
	}
        void OnCollissionEnter (Collision collision)
    {
        print("El jugador ha golpeado la pelota"); //debug
    }
    private void OnTriggerEnter(Collider other)
    {
        print("El jugador ahora ha golpeado otra cosa");
    }

    // Update is called once per frame
    void Update ()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);  
    }    

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
