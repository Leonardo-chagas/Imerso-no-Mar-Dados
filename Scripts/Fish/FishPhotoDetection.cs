using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPhotoDetection : MonoBehaviour
{
    public string fishName;
    void Start()
    {
        
    }

    public void PhotoDetection(Component sender, object data){
        if(data is Camera && sender is PhotoCamera){
            //-----primeiro passo da detecção de peixe------
            //verificar se o peixe está na direção de onde a camera está olhando
            PhotoCamera photoCamera = (PhotoCamera) sender;
            Camera cam = (Camera) data;
            Vector3 point = cam.WorldToViewportPoint(transform.position);

            if(point.x >= 0 && point.x <= 1 && point.y >= 0 && point.y <= 1 && point.z > 0){
                
                //------segundo passo da detecção de peixe------
                //verificar se existe algum objeto no caminho entre a camera e o peixe
                bool hit = Physics.Linecast(sender.transform.position, transform.position, 11);
                

                if(!hit){
                    print(fishName);
                    photoCamera.FishInView(gameObject, fishName);
                }
            }
        }
    }
}
