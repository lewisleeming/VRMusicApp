using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V4BeamScript : MonoBehaviour
{
    [SerializeField, Range(0,1000)] private float stop;
    private int running;
    private float adapterValue = 0;
    

    //private int stop;
    void Start()
    {

        GetComponent<ChuckSubInstance>().RunCode(@"
            global SinOsc foo;
            fun void playImpact()
            {
                foo => dac;
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

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Hands")){
            //Debug.Log("happened");
            GetComponent<ChuckSubInstance>().SetFloat( "stop", stop );
            GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
        }
    }

    // private void OnTriggerExit(Collider other) {
    //     if(other.CompareTag("Hands")){
    //         stop = 0;
    //         GetComponent<ChuckSubInstance>().SetFloat( "stop", stop );
    //         GetComponent<ChuckSubInstance>().BroadcastEvent( "TEsst");
    //     }
    // }
    
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Hands")){
            stop = other.ClosestPoint(transform.position).y * 5 + adapterValue;
            GetComponent<ChuckSubInstance>().SetFloat( "stop", stop );
            GetComponent<ChuckSubInstance>().RunCode( @"
                global SinOsc foo;
                global float stop;
				stop => foo.freq;
			" );
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Hands")){
            GetComponent<ChuckSubInstance>().RunCode( @"
                global SinOsc foo;
                foo =< dac;
	            1::second => now;
			");
        }
    }
}
