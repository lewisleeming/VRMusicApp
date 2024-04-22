using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerText : MonoBehaviour
{

    [SerializeField] private GameObject screenImage1;
    [SerializeField] private GameObject screenImage2;
    [SerializeField] private GameObject screenImage3;
    public int currentImage = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        screenImage1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void selectClicked(){
        // Debug.Log(screenText+" "+current);
        // if(current < screenTextOptions.Length-1){
        //     current += 1;
        //     screenText.text = screenTextOptions[current];
        // }else{
        //     current = 0;
        //     screenText.text = screenTextOptions[0];
        // }
        // screenImage[currentImage].SetActive(false);
        // if(currentImage < screenImage.Length-1){
        //     currentImage += 1;
        // }else{
        //     currentImage = 0;
        // }
        // screenImage[currentImage].SetActive(true);
        if(currentImage == 0){
            screenImage1.SetActive(false);
            screenImage2.SetActive(true);
            currentImage +=1;
        }else if(currentImage == 1){
            screenImage2.SetActive(false);
            screenImage3.SetActive(true);
            currentImage +=1;
        }else{
            screenImage3.SetActive(false);
            screenImage1.SetActive(true);
            currentImage = 0;
        }


        }

    }
    // public void leftClicked(){
    //     Debug.Log(current);
    //     Debug.Log("leftbuton1: "+ currentImage);
    //     if(current == 0){
    //         screenImage[currentImage].SetActive(false);
    //         if(currentImage == 0){
    //             currentImage = screenImage.Length;
    //         }else{
    //             currentImage--;
    //         }
    //         Debug.Log("leftbuton2: "+ currentImage);
    //         screenImage[currentImage].SetActive(true);
    //     }
    // }

    // public void rightClicked(){
    //     if(current == 0){
    //         screenImage[currentImage].SetActive(false);
    //         if(currentImage == screenImage.Length){
    //             currentImage = 0;
    //         }else{
    //             currentImage++;
    //         }
    //         screenImage[currentImage].SetActive(true);
    //     }
    // }