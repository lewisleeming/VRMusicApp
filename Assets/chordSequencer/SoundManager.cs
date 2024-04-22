using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public int _bankSize;
    private List<AudioSource> _soundClip;


    void Start(){
        _soundClip = new List<AudioSource>();
        for(int i = 0; i < _bankSize; i++){
            GameObject soundinstance = new GameObject("sound");
            soundinstance. AddComponent<AudioSource>();
            soundinstance.transform.parent = this.transform;
            _soundClip.Add(soundinstance.GetComponent<AudioSource>());
        }
    }

    public void PlaySound(AudioClip clip, float volume){
        for (int i = 0; i < _soundClip.Count; i++){
            if(!_soundClip[i].isPlaying){
                _soundClip[i].clip = clip;
                _soundClip[i].volume = volume;
                _soundClip[i].Play();
                return;
            }
        }
        GameObject soundinstance = new GameObject("sound");
            soundinstance. AddComponent<AudioSource>();
            soundinstance.transform.parent = this.transform;
            soundinstance.GetComponent<AudioSource>().clip = clip;
            soundinstance.GetComponent<AudioSource>().volume = volume;
            soundinstance.GetComponent<AudioSource>().Play();
            _soundClip.Add(soundinstance.GetComponent<AudioSource>());
    }
}
