using UnityEngine;
using System.Collections;
using System;

public class Parallaxing : MonoBehaviour
{
    // Array (list) of all the back- and foregrounds to be parallaxed 
    public Transform[] backgrounds;
    // The proportion of the camera's movement to move the backgrounds by 
    private float[] parallaxScales;
    // How smooth the parallax is going to be. Make sure to set this above 0  
    public float smoothing = 1f;
    // reference to the main cameras transform  
    public Transform cam;
    // the position of the camera in the previous frame  
    private Vector3 previousCamPos;
    

    //Is called before Start(). Great for references.  
    void Awake () {
        // set up camera reference  
        //cam = Camera.main.transform;
        try
        {
            cam = Camera.main.transform;
        }
        catch (NullReferenceException)
        {
            Debug.Log("Camera was not set in the inspector");
        }
    }

    // Use this for initialization  
    void Start () {
        // The previous frame had the current frame's camera position  
        previousCamPos = cam.position;
        // asigning coresponding parallaxScales  
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame  
    void Update () {
        // for each background  
        for (int i = 0; i < backgrounds.Length; i++) {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale  
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];
            // set a target x position which is the current position plus the parallax  
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            // create a target position which is the background's current position with it's target x position  
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            // fade between current position and the target position using lerp  
            backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);  }
        // set the previousCamPos to the camera's position at the end of the frame  
        previousCamPos = cam.position;
    }
}﻿
