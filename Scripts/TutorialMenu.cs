using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private GameObject controllers;
    [SerializeField] private GameObject headset;
    void OnEnable(){
        controllers.SetActive(true);
        headset.SetActive(false);
    }
}
