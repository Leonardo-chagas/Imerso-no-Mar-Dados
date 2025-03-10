using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftHandUI, rightHandUI, leftHandBase, rightHandBase;
    public GameEvent gamePaused;
    private bool isWristUIActive = false;
    void Start()
    {
        
    }

    
    public void PauseButtonPressed(InputAction.CallbackContext context){
        if(context.performed)
            DisplayWristUI();
    }

    public void DisplayWristUI(){
        if(isWristUIActive){
            leftHandUI.SetActive(false);
            rightHandUI.SetActive(false);

            leftHandBase.SetActive(true);
            leftHandBase.transform.rotation = leftHandUI.transform.rotation;
            rightHandBase.SetActive(true);
            rightHandBase.transform.rotation = rightHandUI.transform.rotation;
            
            isWristUIActive = false;
            Time.timeScale = 1;
        }
        else{
            gamePaused.Raise(this, true);
            leftHandUI.SetActive(true);
            leftHandUI.transform.rotation = leftHandBase.transform.rotation;
            rightHandUI.SetActive(true);
            rightHandUI.transform.rotation = rightHandBase.transform.rotation;

            leftHandBase.SetActive(false);
            rightHandBase.SetActive(false);

            isWristUIActive = true;
            Time.timeScale = 0;
        }
    }
}
