using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private GameObject lazer;

    public void startLazer(){
        //Debug.Log("laser picked up");
        lazer.SetActive(true);
    }

    
    public void stopLazer(){
        //Debug.Log("laser picked up");
        lazer.SetActive(false);
    }
}
