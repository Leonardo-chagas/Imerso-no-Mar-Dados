using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToLoad : MonoBehaviour
{
    public int level;
    public static LevelToLoad instance;
    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
}
