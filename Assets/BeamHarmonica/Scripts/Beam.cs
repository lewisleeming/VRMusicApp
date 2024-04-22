using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] public float beamFrequency;
    public BeamHarmonicaSynths beamHarmonicaSynths;
    private float adapterValue = 0;

    void Start(){
        adapterValue = beamFrequency;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Hands") && other.name == "Beam1"){
            beamHarmonicaSynths.playSynthArray(transform.name, beamFrequency);
        }
    }
    
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Hands") && other.name == "Beam1"){
            beamFrequency = other.ClosestPoint(transform.position).y * 50 + adapterValue;
            beamHarmonicaSynths.playSynthArray(transform.name, beamFrequency);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Hands") && other.name == "Beam1"){
            beamHarmonicaSynths.stopSynth(transform.name);
        }
    }

}
