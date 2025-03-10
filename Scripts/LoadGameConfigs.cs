using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameConfigs : MonoBehaviour
{
    
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Audio", 0.5f);
    }
}
