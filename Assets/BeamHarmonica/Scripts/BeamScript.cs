using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    // double b0, b1, b2, a1, a2;
    // double x1, x2, y1, y2;

    //https://github.com/rhedgeco/Synthic/blob/main/Assets/Scripts/Synthic/SimpleSineGenerator.cs
    [SerializeField, Range(0, 1)] private float amplitude = 0.7f;
    [SerializeField, Range(0, 500)] private float frequency = 261.62f; // middle C

    [SerializeField, Range(0, 500)] public float modDepth = 0; // middle C

    [SerializeField, Range(0, 500)] public float fmFrequency = 0; // middle C

    [SerializeField, Range(0, 500)] public float rmFrequency = 0; // middle C

    [SerializeField] private string filterName;

    private double _phase;
    private int _sampleRate;
    [SerializeField] private int adapterValue;
    private float value;

    private AudioSource audioSource;


    public enum Modulators {
        fm,am,rm,normal
    }

    public Modulators modulator;

    public enum Wavetypes {
        sine,square,triangle
    }

    public Wavetypes wavetypes;
    private float fmModValue;
    private double fmPhase;

    //private BiquadFilter bf;

    // Start is called before the first frame update
    private void Awake() {
        _sampleRate = AudioSettings.outputSampleRate;
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
        //setBiquad("bq_type_lowpass", frequency/_sampleRate);
        //Debug.Log(b0+ b1+ b2+a1+a2);
    }

    // public float process(float input){
    //     double output = input * a0 + z1;
    //     z1 = input * a1 + z2 - b1 * output;
    //     z2 = input * a2 - b2 * output;
    //     //Debug.Log(output);
    //     return (float)output;


    //     // double output = b0 * input + b1 * x1 + b2 * x2 - a1 * y1 - a2 * y2;

    //     // x2 = x1;
    //     // x1 = input;
    //     // y2 = y1;
    //     // y1 = output;

    //     // return output;
    // }

    // public double Process(double input)
    // {
    //     // Apply the biquad filter to the input sample.
    //     double output = b0 * input + b1 * x1 + b2 * x2 - a1 * y1 - a2 * y2;

    //     // Shift the previous input and output samples for the next iteration.
    //     x2 = x1;
    //     x1 = input;
    //     y2 = y1;
    //     y1 = output;

    //     return output;
    // }

    // public void setBiquad(){
    //     double omega = 2.0 * Mathf.PI * 100 / _sampleRate;//0.142475
    //     double sn = Mathf.Sin((float)omega);
    //     double cs = Mathf.Cos((float)omega);//0.99210450.9999999999
    //     double alpha = sn / (2.0 * 0.707); // Q = 0.707 for a Butterworth low-pass filter.

    //     b0 = (1.0 - cs) / 2;
    //     b1 = 1.0 - cs;
    //     b2 = (1.0 - cs) / 2;
    //     a1 = -2.0 * cs;
    //     a2 = 1.0 - alpha;
    // }



    // public void setBiquad(string type, double Fc){
    //     this.type = type;
    //     this.Fc = Fc;
    //     z1 = z2 = 0.0;
    //     calcBiquad();
    // }

    // private void calcBiquad(){
    //     double norm;
    //     double V = Mathf.Pow(10, Mathf.Abs((float)PeakGain)/ 20.0f);
    //     double K = Mathf.Tan((float)(Mathf.PI * Fc));
    //     switch(this.type){
    //         case "bq_type_lowpass":
    //             norm = 1 / (1 + K / Q + K * K);
    //             a0 = K * K * norm;
    //             a1 = 2 * a0;
    //             a2 = a0;
    //             b1 = 2 * (K * K - 1) * norm;
    //             b2 = (1 - K / Q + K * K) * norm;
    //             break;
    //         case "bq_type_highpass":
    //             norm = 1 / (1 + K / Q + K * K);
    //             a0 = 1 * norm;
    //             a1 = -2 * a0;
    //             a2 = a0;
    //             b1 = 2 * (K * K - 1) * norm;
    //             b2 = (1 - K / Q + K * K) * norm;
    //             break;
            
    //         case "bq_type_bandpass":
    //             norm = 1 / (1 + K / Q + K * K);
    //             a0 = K / Q * norm;
    //             a1 = 0;
    //             a2 = -a0;
    //             b1 = 2 * (K * K - 1) * norm;
    //             b2 = (1 - K / Q + K * K) * norm;
    //             break;
            
    //         case "bq_type_notch":
    //             norm = 1 / (1 + K / Q + K * K);
    //             a0 = (1 + K * K) * norm;
    //             a1 = 2 * (K * K - 1) * norm;
    //             a2 = a0;
    //             b1 = a1;
    //             b2 = (1 - K / Q + K * K) * norm;
    //             break;
                
    //         case "bq_type_peak":
    //             if (PeakGain >= 0) {    // boost
    //                 norm = 1 / (1 + 1/Q * K + K * K);
    //                 a0 = (1 + V/Q * K + K * K) * norm;
    //                 a1 = 2 * (K * K - 1) * norm;
    //                 a2 = (1 - V/Q * K + K * K) * norm;
    //                 b1 = a1;
    //                 b2 = (1 - 1/Q * K + K * K) * norm;
    //             }
    //             else {    // cut
    //                 norm = 1 / (1 + V/Q * K + K * K);
    //                 a0 = (1 + 1/Q * K + K * K) * norm;
    //                 a1 = 2 * (K * K - 1) * norm;
    //                 a2 = (1 - 1/Q * K + K * K) * norm;
    //                 b1 = a1;
    //                 b2 = (1 - V/Q * K + K * K) * norm;
    //             }
    //             break;
    //         case "bq_type_lowshelf":
    //             if (PeakGain >= 0) {    // boost
    //                 norm = 1 / (1 + Mathf.Sqrt(2) * K + K * K);
    //                 a0 = (1 + Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
    //                 a1 = 2 * (V * K * K - 1) * norm;
    //                 a2 = (1 - Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
    //                 b1 = 2 * (K * K - 1) * norm;
    //                 b2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
    //             }
    //             else {    // cut
    //                 norm = 1 / (1 + Mathf.Sqrt(2*(float)V) * K + V * K * K);
    //                 a0 = (1 + Mathf.Sqrt(2) * K + K * K) * norm;
    //                 a1 = 2 * (K * K - 1) * norm;
    //                 a2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
    //                 b1 = 2 * (V * K * K - 1) * norm;
    //                 b2 = (1 - Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
    //             }
    //             break;
    //         case "bq_type_highshelf":
    //             if (PeakGain >= 0) {    // boost
    //                 norm = 1 / (1 + Mathf.Sqrt(2) * K + K * K);
    //                 a0 = (V + Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
    //                 a1 = 2 * (K * K - V) * norm;
    //                 a2 = (V - Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
    //                 b1 = 2 * (K * K - 1) * norm;
    //                 b2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
    //             }
    //             else {    // cut
    //                 norm = 1 / (V + Mathf.Sqrt(2*(float)V) * K + K * K);
    //                 a0 = (1 + Mathf.Sqrt(2) * K + K * K) * norm;
    //                 a1 = 2 * (K * K - 1) * norm;
    //                 a2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
    //                 b1 = 2 * (K * K - V) * norm;
    //                 b2 = (V - Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
    //             }
    //             break;

    //     }
    // }

    // private void OnAudioFilterRead(float[] data, int channels) {
    //     double phaseIncrement =  frequency / _sampleRate;
    //     //setBiquad();
    //     for (int sample = 0; sample < data.Length; sample += channels){
    //         if(waveSignal == 0){
    //             value = Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude;
    //             //Debug.Log(Mathf.Sin((float) _phase * 2 * Mathf.PI)); - this is the sign wave value going between -1 and 1
    //         }else if(waveSignal == 1){
    //             value = Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
    //         }else if(waveSignal ==2){
    //             //value = Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude * Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
    //             if(Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude >= 0 * amplitude){
    //                 value = amplitude * 0.6f;
    //             }else{
    //                 value = (-(float)amplitude) * 0.6f;
    //             }
    //         }else{
    //             value = Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude - Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
    //         }
    //         //float value = (float)(gain * Mathf.Sin((float)phase));
    //         _phase = (_phase + phaseIncrement) % 1;
    //         //BiquadFilter bf = new BiquadFilter("bq_type_lowpass", phaseIncrement, 0.707f,0f);
    //         //setBiquad("bq_type_lowpass", phaseIncrement, 0.707f,0f);
    //         //double filtervalue = Process(value);
    //         //Debug.Log(value);

    //         for (int channel = 0; channel < channels; channel++){
    //             data[sample + channel] = value;
    //         }
    //     }
    // }

    private void OnAudioFilterRead(float[] data, int channels) {

        double phaseIncrement = frequency / _sampleRate;
        double fmPhaseIncrement=0;
        double fmNewFrequency=0;
        fmPhaseIncrement = fmFrequency / _sampleRate;
        fmNewFrequency = frequency + fmModValue;
        phaseIncrement = (frequency + fmModValue) / _sampleRate;

        for (int sample = 0; sample < data.Length; sample += channels){
                fmModValue = (Mathf.Sin((float) fmPhase * 2 * Mathf.PI)) * modDepth;
                fmPhase = (fmPhase + fmPhaseIncrement) % 1;
                value = getWave();
                _phase = (_phase + phaseIncrement) % 1;
            //Debug.Log(freqModValue);
            //value = getWave();
            //_phase = (_phase + phaseIncrement) % 1;
            //change value here?
            // BiquadFilter bf1 = new BiquadFilter("bq_type_lowpass", );
            for (int channel = 0; channel < channels; channel++){
                data[sample + channel] = value;
            }
        }
    }

    private float  getWave(){
        switch(wavetypes)
        {
            case Wavetypes.sine:
            {
                return Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude;
            }
            case Wavetypes.square:
            {
                return Mathf.PingPong((float) _phase * 2 * Mathf.PI,1.0f) * amplitude;
            }
            default:
            {
                return Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude;
            }
        }
    }

    // private void OnAudioFilterRead(float[] data, int channels) {
    //     double phaseIncrement = frequency / _sampleRate;
    //     for (int sample = 0; sample < data.Length; sample += channels){
    //         value = (Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude);
    //         Debug.Log(value);
    //         _phase = (_phase + phaseIncrement) % 1;
    //         for (int channel = 0; channel < channels; channel++){
    //             data[sample + channel] = value;
    //         }
    //     }
    // }


    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log(other.contacts[0].normal);
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Hands")){
            //unnmute audio source
            //setBiquad("bq_type_lowpass", frequency/_sampleRate);
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

    // private void calcSineWave(float[] data, int channels){
    //     double phaseIncrement = FreqFrequency / _sampleRate;
    //     for (int i = 0; i < data.Length; i += channels){
    //         freqModValue = (Mathf.Sin((float) _phase * 2 * Mathf.PI) * amplitude) * modDepth;
    //         _phase = (_phase + phaseIncrement) % 1;
    //     }
    // }



}
