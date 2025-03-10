using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Cognitive3D;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float remainingTime;
    [SerializeField] private CanvasGroup fadeBar;
    private bool fade = false;
    private float currentFade;
    private bool countdownEnded = false;
    //private TotalPointsEvent totalPointsEvent;

    void Start(){
        //totalPointsEvent = GetComponent<TotalPointsEvent>();
    }

    void Update()
    {
        if(remainingTime > 0){
            remainingTime -= Time.deltaTime;
        }
        else{
            remainingTime = 0;
            if(!countdownEnded){
                countdownEnded = true;
                fade = true;
                StartCoroutine(FadeToPointsScreen());
            }
        }
        
        int minutes = Mathf.FloorToInt(remainingTime/60);
        int seconds = Mathf.FloorToInt(remainingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        if(fade){
            currentFade = fadeBar.alpha;
            fadeBar.alpha = Mathf.MoveTowards(currentFade, 1, 2*Time.deltaTime);
        }
    }

    IEnumerator FadeToPointsScreen(){
        yield return new WaitForSeconds(1);
        //string totalPoints = (PointsAcquired.instance.trashPointsTotal + PointsAcquired.instance.fishPointsTotal).ToString();
        int trashPoints = PointsAcquired.instance.trashPointsTotal;
        int fishPoints = PointsAcquired.instance.fishPointsTotal;
        int totalPoints = trashPoints + fishPoints;
        new Cognitive3D.CustomEvent($"pontos de lixo: {trashPoints}; pontos de peixe: {fishPoints}; pontos totais: {totalPoints}").Send();
        Cognitive3D.Cognitive3D_Manager.Instance.EndSession();
        SceneManager.LoadScene(3);
    }
}
