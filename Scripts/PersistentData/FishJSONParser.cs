using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

//script em um objeto específico para o armazenamento das informações dos peixes
//usado para ler o arquivo json
public class FishJSONParser : MonoBehaviour
{
    public void SaveToJSON(int i){
        //string json = File.ReadAllText(Application.dataPath + "/Scripts/FishData.json");
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "FishData.json"));
        FishesData data = JsonUtility.FromJson<FishesData>(json);

        print(i);
        data.fishes[i].detected = true;
        string saveJson = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Application.dataPath + "/Scripts/FishData.json", saveJson);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "FishData.json"), saveJson);
    }

    public void ResetJSON(){
        //string json = File.ReadAllText(Application.dataPath + "/Scripts/FishData.json");
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "FishData.json"));
        FishesData data = JsonUtility.FromJson<FishesData>(json);

        foreach(Fish fish in data.fishes){
            fish.detected = false;
        }

        string saveJson = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Application.dataPath + "/Scripts/FishData.json", saveJson);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "FishData.json"), saveJson);
    }

    public FishesData LoadFromJson(){
        //string json = File.ReadAllText(Application.dataPath + "/Scripts/FishData.json");
        string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, "FishData.json"));
        FishesData data = JsonUtility.FromJson<FishesData>(json);
        
        
        return data;
    }
}
