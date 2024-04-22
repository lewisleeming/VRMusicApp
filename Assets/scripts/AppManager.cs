using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set 
        //if(OVRInput.Get(OVRInput.Button.Start)){
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool IsPressed, inputThreshold);

        if(IsPressed){
            if(canvas.activeSelf){
                canvas.SetActive(false);
            }else{
                canvas.SetActive(true);
            }
        }
        
    }

    public void loadHomeScene(){
        SceneManager.LoadScene(0);
    }

    public void reloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
