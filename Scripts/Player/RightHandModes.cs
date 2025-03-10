using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class RightHandModes : MonoBehaviour
{
    [SerializeField] private GameObject cameraModePrefab;
    [SerializeField] private XRBaseController rightController;
    [SerializeField] private InputActionReference rightHandSelect;
    private GameObject handMode;
    private GameObject cameraMode;
    private PhotoCamera photoCamera;
    private int mode = 0;
    
    void Start()
    {
        //Cria o objeto camera para poder ser ativado
        GameObject obj = Instantiate(cameraModePrefab, rightController.transform.GetChild(0), true);
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.transform.rotation = rightController.transform.rotation;
        obj.transform.Rotate(0, 180, 0);
        cameraMode = obj;
        photoCamera = cameraMode.GetComponent<PhotoCamera>();
        cameraMode.SetActive(false);

        rightHandSelect.action.performed += _ => ChangeMode();
    }


    private void ChangeMode(){
        if(photoCamera.viewingPhoto) return;
        mode = mode == 0 ? 1 : 0;
        //muda o modo da mão direita para mão
        if(mode == 0){
        
            if(handMode == null)
                handMode = rightController.transform.GetChild(0).GetChild(1).gameObject;

            handMode.SetActive(true);
            cameraMode.SetActive(false);
            handMode.transform.rotation = rightController.transform.rotation;
            handMode.transform.Rotate(0, 0, -90);
        }
        //muda o modo da mão direita para camera
        else{
            
            if(handMode == null)
                handMode = rightController.transform.GetChild(0).GetChild(1).gameObject;
                
            handMode.SetActive(false);
            cameraMode.SetActive(true);
            cameraMode.transform.rotation = rightController.transform.rotation;
            cameraMode.transform.Rotate(0, 180, 0);
        }
        print("mode changed");
    }
}
