using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //Criar uma lista ara guardar as configurações e enviar com um evento para o jogador
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider turningSelection;
    [SerializeField] private Slider snapAngleSlider;
    [SerializeField] private Slider movementHandSelection;
    [SerializeField] private Slider vignetteSlider;
    [SerializeField] private GameObject audioLayout, rotationLayout, confortLayout;
    [SerializeField] private Volume vignetteEffect;

    public GameEvent onPlayerConfigChanged;

    private List<float> config;

    void Start()
    {
        config = new List<float>
        {
            turningSelection.value,
            snapAngleSlider.value,
            0f,
            vignetteSlider.value
        };
        
        audioSlider.value = PlayerPrefs.GetFloat("Audio", 0.5f);
        turningSelection.value = PlayerPrefs.GetFloat("Turning Selection", 0f);
        snapAngleSlider.value = (float)PlayerPrefs.GetInt("Snap Angle", 15);
        //movementHandSelection.value = PlayerPrefs.GetFloat("Movement Hand Selection", 0f);
        vignetteSlider.value = PlayerPrefs.GetFloat("Vignette", 0f);

        AudioListener.volume = audioSlider.value;

        print("deu start no menu de opções");

        //config = new float[]{turningSelection.value, snapAngleSlider.value, movementHandSelection.value, vignetteSlider.value};
        config = new List<float>
        {
            turningSelection.value,
            snapAngleSlider.value,
            0f,
            vignetteSlider.value
        };

        print(config);
    }

    void OnEnable(){
        if(audioLayout != null){
            audioLayout.SetActive(true);
            rotationLayout.SetActive(false);
            confortLayout.SetActive(false);
        }
    }

    public void OnAudioSliderChanged(){
        AudioListener.volume = audioSlider.value;
        PlayerPrefs.SetFloat("Audio", audioSlider.value);
    }

    public void OnTurningSelectionChanged(){
        PlayerPrefs.SetFloat("Turning Selection", turningSelection.value);

        config[0] = turningSelection.value;
        onPlayerConfigChanged.Raise(this, config);
    }

    public void OnSnapAngleSliderChanged(){
        PlayerPrefs.SetInt("Snap Angle", (int)snapAngleSlider.value);

        config[1] = snapAngleSlider.value;
        onPlayerConfigChanged.Raise(this, config);
    }

    public void OnMovementHandSelectionChanged(){
        PlayerPrefs.SetFloat("Movement Hand Selection", movementHandSelection.value);

        config[2] = movementHandSelection.value;
        onPlayerConfigChanged.Raise(this, config);
    }

    public void OnVignetteSliderValueChanged(){
        /* Vignette vignette;
        //ShadowsMidtonesHighlights shadows;
        if(vignetteEffect.profile.TryGet(out vignette)){
            vignette.intensity.overrideState = true;
            vignette.intensity.SetValue(new ClampedFloatParameter(vignetteSlider.value, 0, 1f));
        } */
        PlayerPrefs.SetFloat("Vignette", vignetteSlider.value);

        config[3] = vignetteSlider.value;
        onPlayerConfigChanged.Raise(this, config);
    }
}
