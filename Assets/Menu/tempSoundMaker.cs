using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSoundMaker : MonoBehaviour
{
    public double[] myMidiNotes = { 60, 65, 69, 72 };
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ChuckSubInstance>().RunCode( @"
            fun void playSound(){
                SinOsc foo => dac;
                repeat( 5 )
                {
                    Math.random2f( 300, 1000 ) => foo.freq;
                    100::ms => now;
                }
            }

            global Event plsounds;

            while(true)
            {
                plsounds => now;
                spork ~ playSound();
            } 
        ");
        
    }


    public void callPlaySound(){
        //GetComponent<ChuckSubInstance>().SetFloatArray( "myFloatNotes", myMidiNotes);
        GetComponent<ChuckSubInstance>().BroadcastEvent( "plsounds");
    }

}
