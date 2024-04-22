using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CK_FLOAT = System.Double;

public class MenuDrum : MonoBehaviour
{
    [SerializeField] private int selectScene;
    private AudioSource audioSource;

    [SerializeField] private tempSoundMaker myChuck;
    public CK_FLOAT[] myMidiNotes = { 60, 65, 69, 72 };
    
    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other) {
        //audioSource.Play();
        Debug.Log("Drum hit");
        myChuck.callPlaySound();
        StartCoroutine(WaitForSound(2));
        //SceneManager.LoadScene(selectScene);
    }

    private IEnumerator WaitForSound(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(selectScene);
        }
    }

}
