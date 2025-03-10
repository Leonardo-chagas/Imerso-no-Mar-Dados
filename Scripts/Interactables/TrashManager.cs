using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashManager : MonoBehaviour
{
    private int trashAmount;
    private int trashPickedAmount;
    //public TMP_Text trashText;
    void Start()
    {
        trashAmount = transform.childCount;
        trashPickedAmount = 0;

        //trashText.text = trashPickedAmount.ToString() + "/" + trashAmount.ToString();
    }

    public void TrashPickedUp(){
        trashPickedAmount++;

        PointsAcquired.instance.AddTrashPointsTotal(30);

        //trashText.text = trashPickedAmount.ToString() + "/" + trashAmount.ToString();
    }
}
