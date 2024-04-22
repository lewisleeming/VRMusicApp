using UnityEngine;

namespace Synthic
{
    public class SynthOut : MonoBehaviour
    {
        [SerializeField] private SynthProvider provider;

        // [SerializeField, Range(0, 1)] public float amplitude = 0.5f;
        // [SerializeField, Range(16.35f, 7902.13f)] public float frequency = 261.62f; // middle C

        private void Awake() {
            // audioSource = GetComponent<AudioSource>();
            // audioSource.mute = true;
        }


        private void OnAudioFilterRead(float[] data, int channels)
        {
            if (channels != 2)
            {
                Debug.LogError("Synthic only works with unity STEREO output mode.");
                return;
            }

            if (provider == null) return;
            provider.FillBuffer(data);
        }
    }
}

