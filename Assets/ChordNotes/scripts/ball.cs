using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Vector3 spawnPoint;

    void Start(){
        //Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
    }

    public void setSpawnPoint(Vector3 spawnPoint){
        this.spawnPoint = spawnPoint;
    }

//problem of ball zooming too fast 
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("floor")){
            GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            transform.position = spawnPoint;
            
        }
    }
}
