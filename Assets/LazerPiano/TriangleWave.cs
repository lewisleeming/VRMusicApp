using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CK_INT = System.Int32;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TriangleWave : MonoBehaviour
{
    private float frequencyAdapter;
    // [SerializeField, Range(0,500)] private float frequency2 = 200;
    public LazerGun lazerGun;

    private bool isRunning = false;
    public XRNode inputSource;
    //public InputHelpers.Button inputButton;
    public Vector2 myAxis;
    public HingeJoint dial;

    // [SerializeField] private HingeJoint lever;

    void Start()
    {

        GetComponent<ChuckSubInstance>().RunCode(@"
            global TriOsc waveName;
            fun void playImpact()
            {
                waveName => dac;
                while( true )
                {
                    1::second => now;
                }
            }
            
            global Event TEsst;
            
            while(true)
            {
                TEsst => now;
                spork ~ playImpact();
            }

        ");
    }

    void Update(){
        InputHelpers.TryReadAxis2DValue(InputDevices.GetDeviceAtXRNode(inputSource), InputHelpers.Axis2D.PrimaryAxis2D, out Vector2 myAxis);
        if(myAxis.x > -1 && myAxis.x < -0.05){
            frequencyAdapter+=1;
        }else if (myAxis.x > 0.05 && myAxis.x < 1){
            frequencyAdapter-=1;
        }else if (myAxis.x ==0 && frequencyAdapter>0) {
            frequencyAdapter-=1;
        }else if (myAxis.x ==0 && frequencyAdapter<0) {
            frequencyAdapter+=1;
        }
        
    }

    public void playSynthArray(string waveName, float frequency){
            if(!isRunning){
                frequencyAdapter = 0;
                GetComponent<ChuckSubInstance>().SetFloat( "stop", frequency);
                //GetComponent<ChuckSubInstance>().SetFloat( "newGain", 0.25);
                // GetComponent<ChuckSubInstance>().SetFloat( "newGain", ((lever.angle - 50)/100)+5);
                GetComponent<ChuckSubInstance>().SetString( "waveName", waveName);
                GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
            }
            isRunning = true;
            GetComponent<ChuckSubInstance>().SetFloat( "stop", frequency - frequencyAdapter);
            // GetComponent<ChuckSubInstance>().SetFloat( "newGain", ((lever.angle - 50)/100)+5); // must be less than 1, otherwise cracking
            GetComponent<ChuckSubInstance>().SetFloat( "newGain", dial.angle/1000);
            //GetComponent<ChuckSubInstance>().SetString( "waveName", "waveone");
            GetComponent<ChuckSubInstance>().RunCode( @"
                global TriOsc waveName;
                global float stop;
                global float newGain;
                newGain => waveName.gain;
				stop => waveName.freq;
			" );
            //GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
    }

    public void stopSynth(string waveName) {
        isRunning = false;
        GetComponent<ChuckSubInstance>().SetString( "waveName", waveName);
        GetComponent<ChuckSubInstance>().RunCode( @"
            global TriOsc waveName;
            waveName =< dac;
            1::second => now;
        ");
    }
}
