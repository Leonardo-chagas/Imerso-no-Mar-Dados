using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cognitive3D;

public class MenuEvent : MonoBehaviour
{
    private DynamicObject dynamicObject;
    void Start()
    {
        dynamicObject = GetComponent<DynamicObject>();
    }

    public void RecordMenuEntered(string menu){
        dynamicObject.BeginEngagement(menu);
    }
}
