using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cognitive3D;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordPhotoEvent : MonoBehaviour
{
    private DynamicObject dynamicObject;
    void Start()
    {
        dynamicObject = GetComponent<DynamicObject>();
    }

    public void RecordPhotoTaken(string text){
        //dynamicObject.BeginEngagement(text);
        new Cognitive3D.CustomEvent(text).Send();
    }
}
