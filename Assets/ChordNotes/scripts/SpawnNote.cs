                using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{

    [SerializeField] private Dial dial;
    [SerializeField] private ComputerText computerText;
    [SerializeField] private AudioClip[] guitarAudioClips;
    [SerializeField] private AudioClip[] drumAudioClips;
    [SerializeField] private AudioClip[] synthAudioClips;
    private AudioSource audioSource;
    [SerializeField] private GameObject note;
    [SerializeField] private NoteScript noteScript;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnNewNote(){
        if(computerText.currentImage == 0){
            //audioSource.clip = guitarAudioClips[dial.index];
            //Debug.Log("1"+dial.index);
           // Debug.Log("notehita");
            noteScript.audioClip = guitarAudioClips[dial.index];
            Instantiate(note,new Vector3(-1.24300003f,1.23800004f,-1.75999999f),Quaternion.identity);

        }else if(computerText.currentImage == 1){
            //audioSource.clip = drumAudioClips[dial.index];
            //Debug.Log("2"+dial.index);
            //Debug.Log("notehitb");
            noteScript.audioClip = drumAudioClips[dial.index];
            Instantiate(note,new Vector3(-1.24300003f,1.23800004f,-1.75999999f),Quaternion.identity);
        }else if(computerText.currentImage == 2){
            //audioSource.clip = drumAudioClips[dial.index];
            //Debug.Log("2"+dial.index);
            //Debug.Log("notehitb");
            noteScript.audioClip = synthAudioClips[dial.index];
            Instantiate(note,new Vector3(-1.24300003f,1.23800004f,-1.75999999f),Quaternion.identity);
        }
    }
}
