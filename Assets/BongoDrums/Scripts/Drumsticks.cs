using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drumsticks : MonoBehaviour {

    private Vector3 lastPosition;
    public float speed;

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
	}
	
	void FixedUpdate () {
		speed = (((transform.position - lastPosition).magnitude) / Time.deltaTime);
        lastPosition = transform.position;
	}
}
