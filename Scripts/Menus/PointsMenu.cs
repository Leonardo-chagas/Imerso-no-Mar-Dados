using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PointsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text trashAmountText;
    [SerializeField] private TMP_Text trashPointsText;
    [SerializeField] private Image[] fishButtons;
    [SerializeField] private TMP_Text fishPointsText;
    [SerializeField] private TMP_Text pointsTotalText;
    void Start()
    {
        trashAmountText.text = string.Format("{0:0}/10", PointsAcquired.instance.trashCollected);
        trashPointsText.text = PointsAcquired.instance.trashPointsTotal.ToString();
        fishPointsText.text = PointsAcquired.instance.fishPointsTotal.ToString();

        int pointsTotal = PointsAcquired.instance.trashPointsTotal + PointsAcquired.instance.fishPointsTotal;
        pointsTotalText.text = pointsTotal.ToString();

        FishesData fishesData = FishDataHolder.instance.fishesData;
        List<Sprite> fishImages = FishDataHolder.instance.fishImages;
        int cont = 0;

        foreach(Fish fish in fishesData){
            if(fish.detected){
                fishButtons[cont].sprite = fishImages[cont];
                fishButtons[cont].color = Color.white;
                fishButtons[cont].transform.GetChild(0).gameObject.SetActive(true);
            }
            cont++;
        }
    }

    public void BackToMainMenu(){
        PointsAcquired.instance.ResetPoints();
        LevelToLoad.instance.level = 0;
        SceneManager.LoadScene(1);
    }
}
