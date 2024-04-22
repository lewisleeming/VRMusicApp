using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.Data.ValueReferences;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    private AudioSource source;
    private float frequency = 150;//base freq - might change
    private Vector2 myAxis;
    [SerializeField] private GameObject mychuck;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}

    void Update(){

        // myAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        // if(myAxis.x < -1 && myAxis.x > -0.05){
        //     frequency+=3;
        // }else if (myAxis.x > 0.05 && myAxis.x < 1){
        //     frequency-=3;
        // }else if (myAxis.x ==0 && frequency>0) {
        //     frequency-=2;
        // }else if (myAxis.x ==0 && frequency<0) {
        //     frequency+=2;
        // }

    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     //Debug.Log("drum hit");
    //     if(other.CompareTag("Drumstick"))
    //     {
    //         Debug.Log("drum hit");
    //         source.volume = other.gameObject.GetComponent<Drumsticks>().speed;
    //         ActivateSound();
    //     }
    // }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.tag == "Drum"){
            mychuck.GetComponent<Drum>().playDrumSound(other, other.gameObject.GetComponent<DrumSounds>().getClipName());
        }
        //mychuck.GetComponent<Drum>().playDrumSound(other, other.gameObject.GetComponent<DrumSounds>().getClipName());
    }

    private void ActivateSound()
    {
        source.Play();
    }
}
