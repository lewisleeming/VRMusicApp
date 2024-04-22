using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScriptV2 : MonoBehaviour
{

    //https://github.com/rhedgeco/Synthic/blob/main/Assets/Scripts/Synthic/SimpleSineGenerator.cs
    [SerializeField, Range(0, 1)] private float amplitude = 0.7f;
    [SerializeField, Range(0, 500)] private float frequency = 261.62f; // middle C

    [SerializeField, Range(0, 500)] public float modDepth = 0; // middle C

    [SerializeField, Range(0, 500)] public float FreqFrequency = 0;

    [SerializeField, Range(0,3)] private int waveSignal = 0;

    private double _phase;
    private int _sampleRate;
    [SerializeField] private int adapterValue;

    private double incrememnt;
    private double phase;
    private double freq_phase;
    private float value;
    private float freqModValue;

    private AudioSource audioSource;
    //private BiquadFilter bf;
    
    // Start is called before the first frame update
    private void Awake() {
        _sampleRate = AudioSettings.outputSampleRate;//44100
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
    }

    private void OnAudioFilterRead(float[] data, int channels) {
        double phaseIncrement =  frequency / _sampleRate;
        for (int sample = 0; sample < data.Length; sample += channels){
            if(waveSignal == 0){
                value = Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude;
            }else if(waveSignal == 1){
                value = Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
            }else if(waveSignal ==2){
                if(Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude >= 0 * amplitude){
                    value = amplitude * 0.6f;
                }else{
                    value = (-(float)amplitude) * 0.6f;
                }
            }else{
                value = Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude - Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
            }


            _phase = (_phase + phaseIncrement) % 1;
            for (int channel = 0; channel < channels; channel++){
                data[sample + channel] = value;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Hands")){
            //unnmute audio source
            audioSource.mute = false;
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Hands")){
            //get position
            //use position to make noise
            frequency = other.ClosestPoint(transform.position).y * 50 + adapterValue;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Hands")){
            //mute audio source
            audioSource.mute = true;
        }
    }
}
