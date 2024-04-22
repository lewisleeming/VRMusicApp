using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class spawnButton : MonoBehaviour
{
    [SerializeField] private HingeJoint lever;
    [SerializeField] private GameObject ball;
    [SerializeField] private XRSlider slider;
    [SerializeField] private ball b;

    List<GameObject> allBalls = new List<GameObject>();
    private Vector3 newSpawn;
    private void Update(){
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space key was pressed");
            spawnButtonPressed();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Debug.Log("space key was pressed");
            resetButtonPressed();
        }
    }
    
    
    public void spawnButtonPressed(){
        //Debug.Log("button pressed");
        //wall goes from -1 to 3.2
        //
        //Debug.Log(lever.angle);
        //float leverAngle = lever.angle/10 - 2;
        newSpawn = new Vector3(-4.72900009f,3.3900001f,((lever.angle - 50)/10)+3);
        //b.spawnPoint = newSpawn;s
        b.setSpawnPoint(newSpawn);
        GameObject balls = Instantiate(ball, newSpawn, Quaternion.identity);
        // foreach(var item in allBalls){
        //     Physics.IgnoreCollision(ball.GetComponent<Collider>(), item.GetComponent<Collider>());
        // }
        allBalls.Add(balls);
        // b.setSpawnPoint(newSpawn);
    }

    public void resetButtonPressed(){
        //Debug.Log(allBalls.ToArray().Length);
        foreach (GameObject balls in allBalls)
        {
            Destroy(balls);
        }

    }
}
