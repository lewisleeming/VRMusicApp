using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWave : MonoBehaviour
{

    public void setCurrentWaveOnButton(){
        if(transform.GetComponent<SynthSounds>().enabled){
            transform.GetComponent<SynthSounds>().enabled = false;
        }else{
            transform.GetComponent<SynthSounds>().enabled = true;
        }
    }
    public void setTriangleWave(){
        if(transform.GetComponent<TriangleWave>().enabled){
            transform.GetComponent<TriangleWave>().enabled = false;
        }else{
            transform.GetComponent<TriangleWave>().enabled = true;
        }
    }
}
