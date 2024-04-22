using Synthic.Native.Buffers;
using Synthic.Native.Data;
using Unity.Burst;
using UnityEngine;
using System;

namespace Synthic
{
    [BurstCompile]
    public class BeamScriptV3 : SynthProvider
    {
        public enum Modulators {
            fm,am,rm,normal
        }

        public Modulators modulator;

        [SerializeField, Range(0, 1)] private float amplitude = 0.5f;
        [SerializeField, Range(16.35f, 7902.13f)] private float frequency = 261.62f; // middle C
        
        private AudioSource audioSource;
        [SerializeField] private int adapterValue;

        private static BurstSineDelegate _burstSine;
        private double _phase;
        private int _sampleRate;
        [SerializeField, Range(0f, 7902.13f)] private int fmFrequency;
        [SerializeField, Range(0f, 7902.13f)] private float modDepth;
        private static float fmModValue;
        private static double _fmPhase;

        private void Awake()
        {
            _sampleRate = AudioSettings.outputSampleRate;
            _burstSine ??= BurstCompiler.CompileFunctionPointer<BurstSineDelegate>(BurstSine).Invoke;
            audioSource = GetComponent<AudioSource>();
            audioSource.mute = true;
        }

        protected override void ProcessBuffer(ref SynthBuffer buffer)
        {
            try{
                _phase = _burstSine(ref buffer, _phase, _sampleRate, amplitude, frequency, modulator, fmFrequency, modDepth, fmModValue, _fmPhase);
            }catch(Exception e){
                Debug.Log(e);
            }
        }

        private delegate double BurstSineDelegate(ref SynthBuffer buffer,
            double phase, int sampleRate, float amplitude, float frequency, Modulators modulator, int fmFrequency, float modDepth, float fmModValue, double fmPhase);

        [BurstCompile]
        private static double BurstSine(ref SynthBuffer buffer,
            double phase, int sampleRate, float amplitude, float frequency, Modulators modulator, int fmFrequency, float modDepth, float fmModValue, double fmPhase)
        {
            // calculate how much the phase should change after each sample
            double phaseIncrement = (frequency + fmModValue) / sampleRate;
            double fmPhaseIncrement = fmFrequency / sampleRate;
            double fmNewFrequency=frequency + fmModValue;
            
            // if(modulator ==  Modulators.fm){
            //     //fmPhaseIncrement = fmFrequency / sampleRate;
            //     fmNewFrequency = frequency + fmModValue;
            //     phaseIncrement = (frequency + fmModValue) / sampleRate;
            // }

            for (int sample = 0; sample < buffer.Length; sample++)
            {
                fmModValue = (Mathf.Sin((float) fmPhase * 2 * Mathf.PI)) * modDepth;
                fmPhase = (fmPhase + fmPhaseIncrement) % 1;
                //Debug.Log((Mathf.Sin((float) fmPhase * 2 * Mathf.PI)));

                // calculate and set buffer sample
                //buffer[sample] = new StereoData((float)(Mathf.PingPong((float) phase * 2 * Mathf.PI,1.0f) * amplitude));
                buffer[sample] = new StereoData(Mathf.Sin((float)phase * 2 * Mathf.PI) * amplitude);
                //fmPhase = (fmPhase + fmPhaseIncrement) % 1;
                // increment _phase value for next iteration
                phase = (phase + phaseIncrement) % 1;
            }

            // return the updated phase
            return phase;
        }

        private static StereoData waveType(Modulators modulator, double phase, float amplitude){
            switch(modulator)
            {
                case Modulators.normal:
                {
                    return new StereoData(Mathf.Sin((float)phase * 2 * Mathf.PI) * amplitude);
                }
                case Modulators.fm:
                {
                    return new StereoData(Mathf.Sin((float)phase * 2 * Mathf.PI) * amplitude);
                }
                default:
                {
                    return new StereoData(Mathf.Sin((float)phase * 2 * Mathf.PI) * amplitude);
                }
            }
        }

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
    }
}