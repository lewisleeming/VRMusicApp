using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CK_INT = System.Int32;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class FMsynthesis : MonoBehaviour
{
    private float frequencyAdapter;
    public LazerGun lazerGun;
    private bool isRunning = false;
    public XRNode inputSource;
    //public InputHelpers.Button inputButton;
    public Vector2 myAxis;

    [SerializeField] private HingeJoint lever;

    void Start()
    {

        GetComponent<ChuckSubInstance>().RunCode(@"
            global SinOsc waveName;
            fun void playImpact()
            {
                // modulator to carrier
                SinOsc m => SinOsc c => dac;

                // carrier frequency
                440 => c.freq;
                // modulator frequency
                110 => m.freq;
                // index of modulation
                300 => m.gain;

                // phase modulation is FM synthesis (sync is 2)
                2 => c.sync;

                // time-loop
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
                GetComponent<ChuckSubInstance>().SetString( "waveName", waveName);
                GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
            }
            isRunning = true;
            GetComponent<ChuckSubInstance>().SetFloat( "firstFrequency", frequency - frequencyAdapter);
            GetComponent<ChuckSubInstance>().SetFloat( "secondFrequency", ((lever.angle - 50)/100)+5);
            GetComponent<ChuckSubInstance>().SetFloat( "newGain", ((lever.angle - 50)/100)+5); // must be less than 1, otherwise cracking
            //GetComponent<ChuckSubInstance>().SetString( "waveName", "waveone");
            GetComponent<ChuckSubInstance>().RunCode( @"
                global SinOsc waveName;
                global float stop;
                global float newGain;
                newGain => m.gain;
                // carrier frequency
                firstFrequency => c.freq;
                // modulator frequency
                secondFrequency => m.freq;
                // index of modulation
                300 => m.gain;
				stop => waveName.freq;
			");
            //GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
    }

    public void stopSynth(string waveName) {
        isRunning = false;
        GetComponent<ChuckSubInstance>().SetString( "waveName", waveName);
        GetComponent<ChuckSubInstance>().RunCode( @"
            global SinOsc waveName;
            waveName =< dac;
            1::second => now;
        ");
    }



}
