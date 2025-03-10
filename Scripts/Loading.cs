using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync(){
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(LevelToLoad.instance.level);

        while(!loadOperation.isDone){
            float progressValue = Mathf.Clamp01(loadOperation.progress/0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
