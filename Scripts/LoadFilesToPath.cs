using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class LoadFilesToPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!File.Exists(Application.persistentDataPath + "/FishData.json")){
            var loadingRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "FishData.json"));
            loadingRequest.SendWebRequest();

            while(!loadingRequest.isDone){
                if(loadingRequest.error != null){
                    print(loadingRequest.error);
                    break;
                }
            }
            if(loadingRequest.error != null){
                  print(loadingRequest.error);  
            }
            else{
                //print("gravando o arquivo");
                File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "FishData.json"), loadingRequest.downloadHandler.data);
            }
        }
    }
}
