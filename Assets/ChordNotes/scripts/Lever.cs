using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private TextMeshPro leverText;
    [SerializeField] private GameObject ball;
    private HingeJoint lever;


    void Start() {
        lever = GetComponent<HingeJoint>();
    }

    void Update(){
        leverText.text = "Value: " + (int)((lever.angle - 50)/10);
        ball.transform.position = new Vector3(-4.48999977f,3.8599999f,((lever.angle - 50)/10)+3);
    }
}
