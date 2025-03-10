using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsAcquired : MonoBehaviour
{
    public int trashCollected = 0;
    public int trashPointsTotal = 0;
    public int fishPointsTotal = 0;
    public static PointsAcquired instance;

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

    public void AddTrashPointsTotal(int points){
        trashCollected++;
        trashPointsTotal += points;
    }

    public void AddFishPointsTotal(int points){
        fishPointsTotal += points;
    }

    public void ResetPoints(){
        trashCollected = 0;
        trashPointsTotal = 0;
        fishPointsTotal = 0;
    }
}
