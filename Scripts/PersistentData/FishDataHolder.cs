using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

//script em um objeto específico para o armazenamento das informações dos peixes
//guarda as informações dos peixes para serem usados por outros objetos no jogo
public class FishDataHolder : MonoBehaviour
{
    public FishesData fishesData;
    public List<Sprite> fishImages = new List<Sprite>();
    private FishJSONParser json;
    public GameEvent newFishFound;
    [HideInInspector] public static FishDataHolder instance;
    void Start()
    {
        SaveFileToPath();

        json = GetComponent<FishJSONParser>();
        fishesData = json.LoadFromJson();
        /* print(fishesData.fishes.Count);
        foreach(Fish fish in fishesData.fishes){
            print(fish.name);
            if(File.Exists(Application.dataPath + "/Images/" + fish.image)){
                var fileBytes = File.ReadAllBytes(Application.dataPath + "/Images/" + fish.image);
                var texture2D = new Texture2D(2, 2);

                if (texture2D.LoadImage(fileBytes)){
                    Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                    fishImages.Add(sprite);
                }
            }
        } */
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void SaveFileToPath(){
        //if(!File.Exists(Application.persistentDataPath + "\\FishData.json")){
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
        //}
    }

    public void ResetFishData(){
        json.ResetJSON();
        fishesData = json.LoadFromJson();
    }

    public void Save(string fish){
        int i = CheckFishIndex(fish);
        CheckFishToGetPoints(fish);
        json.SaveToJSON(i);
        //fishesData = json.LoadFromJson();
        fishesData.fishes[i].detected = true;
    }

    public void CheckFishToGetPoints(string fishDetected){
        foreach(Fish fish in fishesData){
            if(fish.name == fishDetected){
                if(!fish.detected){
                    PointsAcquired.instance.AddFishPointsTotal(fish.points);
                    newFishFound.Raise(this, fish.name);
                }
            }
        }
    }

    private int  CheckFishIndex(string fishDetected){
        int cont = 0;
        foreach(Fish fish in fishesData){
            if(fish.name == fishDetected){
                break;
            }
            else{
                cont++;
            }
        }
        return cont;
    }
}
