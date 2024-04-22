using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Oculus.Interaction;

public class Drum : MonoBehaviour
{
    void Start(){
        GetComponent<ChuckSubInstance>().RunCode(@"
            fun void playImpact( float intensity, string fileName )
            {
                SndBuf impactBuf => dac;
                me.dir() + fileName => impactBuf.read;

                // start at the beginning of the clip
                0 => impactBuf.pos;
                
                // set rate: least intense is fast, most intense is slow; range 0.4 to 1.6
                1.5 - intensity + Math.random2f( -0.1, 0.1 ) => impactBuf.rate;
                
                chout <= ""Rate is "" <= impactBuf.rate() <= IO.newline();

                // set gain: least intense is quiet, most intense is loud; range 0.05 to 1
                0.05 + 0.95 * intensity => impactBuf.gain;

                // pass time so that the file plays
                impactBuf.length() / impactBuf.rate() => now;
            }
            
            global float impactIntensity;
            global string fileName;
            global Event impactHappened;

            while( true )
            {
                impactHappened => now;
                spork ~ playImpact( impactIntensity, fileName);
            }
        ");
    }


    public void playDrumSound(Collision other, string clipName){
        //Debug.Log("drum hit");
        float intensity = Mathf.Clamp01(other.relativeVelocity.magnitude );
        intensity = intensity * intensity;
        GetComponent<ChuckSubInstance>().SetString( "fileName", clipName);
        GetComponent<ChuckSubInstance>().SetFloat("impactIntensity", intensity);
        GetComponent<ChuckSubInstance>().BroadcastEvent( "impactHappened");
        //float intensity = other.gameObject.GetComponent<Drumsticks>().speed;
        //ActivateSound();
    }

    
}
