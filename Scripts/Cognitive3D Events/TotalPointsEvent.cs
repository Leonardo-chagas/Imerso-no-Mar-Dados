using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cognitive3D;

public class TotalPointsEvent : MonoBehaviour
{
    private DynamicObject dynamicObject;
    void Start()
    {
        dynamicObject = GetComponent<DynamicObject>();
    }

    
    public void RecordTotalPoints(int trashPoints, int fishPoints, int totalPoints){
        dynamicObject.BeginEngagement($"pontos de lixo: {trashPoints}; pontos de peixe: {fishPoints}; pontos totais: {totalPoints}");
    }
}
