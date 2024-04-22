using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamHarmonicaSynths : MonoBehaviour
{
    private float adapterValue = 0;
    private bool isRunning = false;
    void Start()
    {

        GetComponent<ChuckSubInstance>().RunCode(@"
            global SinOsc waveName;
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


    public void playSynthArray(string waveName, float frequency){
            if(!isRunning){
                GetComponent<ChuckSubInstance>().SetFloat( "stop", frequency);
                GetComponent<ChuckSubInstance>().SetString( "waveName", waveName);
                GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
            }
            isRunning = true;
            GetComponent<ChuckSubInstance>().SetFloat( "stop", frequency);
            GetComponent<ChuckSubInstance>().SetString( "waveName", "waveone");
            GetComponent<ChuckSubInstance>().RunCode( @"
                global SinOsc waveName;
                global float stop;
				stop => waveName.freq;
			" );
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
