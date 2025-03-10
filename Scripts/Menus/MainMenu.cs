using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void Play(){
        FishDataHolder.instance.ResetFishData();
        LevelToLoad.instance.level = 2;
        SceneManager.LoadScene(1);
    }

    public void Quit(){
        Application.Quit();
    }
}
