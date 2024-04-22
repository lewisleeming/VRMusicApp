using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChorusScript : MonoBehaviour
{
    [SerializeField] private GameObject chuckSub;

    public void chorusPressed(){
        if(chuckSub.GetComponent<AudioChorusFilter>().enabled){
            chuckSub.GetComponent<AudioChorusFilter>().enabled = false;
        }else{
            chuckSub.GetComponent<AudioChorusFilter>().enabled = true;
        }
    }

    public void EchoPressed(){
        if(chuckSub.GetComponent<AudioEchoFilter>().enabled){
            chuckSub.GetComponent<AudioEchoFilter>().enabled = false;
        }else{
            chuckSub.GetComponent<AudioEchoFilter>().enabled = true;
        }
    }
}
