using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool isFading = false, fadeOut = false;
    private float currentFade;
    private float fadeSpeed = 2f;
    private GameObject objectToFadeIn;
    public bool avoidOneFade = false;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(!avoidOneFade)
            canvasGroup.alpha = 0;
        else{
            avoidOneFade = false;
        }
    }

    private void OnEnable(){
        //canvasGroup.interactable = true;
        //canvasGroup.alpha = 0;
        if(avoidOneFade){
            //avoidOneFade = false;
        }
        else{
            isFading = true;
            fadeOut = false;
        }
    }

    
    void Update()
    {
        if(isFading){
            if(fadeOut){
                currentFade = canvasGroup.alpha;
                canvasGroup.alpha = Mathf.MoveTowards(currentFade, 0, fadeSpeed*Time.deltaTime);
                if(canvasGroup.alpha <= 0){
                    isFading = false;
                    gameObject.SetActive(false);
                    objectToFadeIn.SetActive(true);
                }
            }
            else{
                currentFade = canvasGroup.alpha;
                canvasGroup.alpha = Mathf.MoveTowards(currentFade, 1, fadeSpeed*Time.deltaTime);
                if(canvasGroup.alpha >= 1){
                    isFading = false;
                    canvasGroup.interactable = true;
                }
            }
        }
    }

    public void FadeOut(GameObject obj){
        objectToFadeIn = obj;
        isFading = true;
        fadeOut = true;
        canvasGroup.interactable = false;
    }
}
