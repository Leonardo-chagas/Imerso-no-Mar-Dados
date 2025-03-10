using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class FishButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    
    void Start()
    {
        
    }

    public void ReadyButton(UnityAction call, Sprite sprite, string fishName, bool isInteractable){
        button.onClick.AddListener(call);
        button.interactable = isInteractable;
        if(isInteractable)
            image.sprite = sprite;
        text.text = fishName;
    }
}
