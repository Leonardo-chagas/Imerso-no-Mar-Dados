using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cognitive3D;
using UnityEngine.XR.Interaction.Toolkit;

public class RecordTrashEvent : MonoBehaviour
{
    private DynamicObject dynamicObject;
    void Start()
    {
        dynamicObject = GetComponent<DynamicObject>();
    }

    public void RecordTrashCollected(){
        //dynamicObject.BeginEngagement("Lixo coletado");
        new Cognitive3D.CustomEvent("lixo coletado").Send();
    }
}
