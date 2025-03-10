using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cognitive3D;
using TMPro;

public class CatalogueMenu : MonoBehaviour
{
    [SerializeField] private GameObject catalogue, catalogueMenu, infoMenu;
    [SerializeField] private GameObject fishButton;
    [SerializeField] private Image fishImage;
    [SerializeField] private TMP_Text fishName;
    [SerializeField] private TMP_Text fishConcern;
    [SerializeField] private TMP_Text fishText;
    private FishesData fishes;
    private List<Sprite> fishImages = new List<Sprite>();
    
    void Awake()
    {
        fishes = FishDataHolder.instance.fishesData;
        fishImages = FishDataHolder.instance.fishImages;

        int cont = 0;
        foreach(Fish fish in fishes.fishes){
            GameObject obj = Instantiate(fishButton, catalogueMenu.transform);
            /* Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => FishInfo(fish)); */
            FishButton button = obj.GetComponent<FishButton>();
            print(cont);

            if(fish.detected){
                /* button.interactable = true;
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = fish.name;
                button. = fishImages[cont]; */
                button.ReadyButton(() => FishInfo(fish.id), fishImages[cont], fish.name, true);
            }
            else{
                button.ReadyButton(() => FishInfo(fish.id), fishImages[cont], "?", false);
            }

            cont++;
        }
    }

    private void OnEnable(){
        /* catalogue.SetActive(true);
        infoMenu.SetActive(false); */
        //if(fishes != FishDataHolder.instance.fishesData){
            print("passou");
            fishes = FishDataHolder.instance.fishesData;
            int cont = 0;
            foreach(Fish fish in fishes.fishes){
                FishButton button = catalogueMenu.transform.GetChild(cont).GetComponent<FishButton>();

                if(fish.detected){
                    button.ReadyButton(() => FishInfo(fish.id), fishImages[cont], fish.name, true);
                }
                else{
                    button.ReadyButton(() => FishInfo(fish.id), fishImages[cont], "?", false);
                }
                cont++;
            //}
        }
    }

    public void FishInfo(int index){
        print(index);
        infoMenu.SetActive(true);
        fishImage.sprite = fishImages[index];
        fishName.text = "Nome: " + fishes.fishes[index].name;
        fishConcern.text = "Preocupação: " + fishes.fishes[index].concern;
        fishText.text = fishes.fishes[index].text.Replace("\n", "<br>");

        new Cognitive3D.CustomEvent("Vendo informações sobre: " + fishes.fishes[index].name).Send();

        catalogue.SetActive(false);
    }
}
