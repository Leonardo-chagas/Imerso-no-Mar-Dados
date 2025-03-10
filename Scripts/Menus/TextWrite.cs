using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWrite : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private bool degree;
    [SerializeField] private Slider slider;
    void Start()
    {
        text = GetComponent<TMP_Text>();

        if(degree){
            text.text = slider.value.ToString() + "º";
        }
        else{
            text.text = slider.value.ToString();
        }
    }

    public void TextUpdate(){
        if(degree){
            text.text = slider.value.ToString() + "º";
        }
        else{
            text.text = slider.value.ToString();
        }
    }
}
