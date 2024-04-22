using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSounds : MonoBehaviour
{
    [SerializeField] private string[] AudioClipName;

    public string getClipName(){
        return AudioClipName[0];
    }
}
