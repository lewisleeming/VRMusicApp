using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class LazerGun : MonoBehaviour
{
    public float gunRange = 50f;
    public float laserDuration = 0.05f;
 
    LineRenderer laserLine;
    float fireTimer;
    [SerializeField] private Material[] material;

    public GameObject tempObject;

    public SynthSounds synthSounds;
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;


 
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.material = material[0];
        laserLine.SetPosition(0, transform.position);
    }
 
    void Update()
    {
        int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        RaycastHit hit;
        //set linerenderer positions
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, transform.position + (transform.forward * gunRange));
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool IsPressed, inputThreshold);

        if(IsPressed){
            //change laser colour to red
            laserLine.material = material[0];
            //check if target hit
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask)){
                //store gameobject(target) hit
                tempObject = hit.transform.gameObject;
                //change colour of target
                tempObject.GetComponent<Renderer>().material.color = Color.red;
                synthSounds.playSynthArray(tempObject.name,tempObject.GetComponent<EggScript>().Startingfrequency);
            }else{
                //if target not hit
            }
        }else{
            if(tempObject!= null){
                synthSounds.stopSynth(tempObject.name);
                tempObject.GetComponent<Renderer>().material.color = Color.white;
                laserLine.material = material[1];
                //tempObject.GetComponent<SynthSounds>().stopSynth();
            }
            //change colour of laser to white
            laserLine.material = material[1];
        }
        
    }
}
