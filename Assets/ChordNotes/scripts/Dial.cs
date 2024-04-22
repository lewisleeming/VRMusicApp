using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dial : MonoBehaviour
{
    private HingeJoint dial;

    [SerializeField] private TextMeshPro ScreenText;

    public int index;

    void Start() {
        dial = GetComponent<HingeJoint>();
    }

    void Update(){
        //leverText.text = "Value: " + (lever.angle/100 + 0.05);
        index = (int) dial.angle/10;
        //transform.rotation = Quaternion.Euler(0, index, 0);
        ScreenText.text = " "+index;

    }
}
