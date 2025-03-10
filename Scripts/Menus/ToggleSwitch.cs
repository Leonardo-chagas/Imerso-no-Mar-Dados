using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [Header("Slider Setup")]
    [SerializeField, Range(0f, 1f)] private float sliderValue;
    public bool CurrentValue { get; private set; }
    private Slider slider;

    [Header("Animation")]
    [SerializeField, Range(0, 1f)] private float animationDuration = 0.5f;
    [SerializeField] private AnimationCurve slideEase = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private GameObject fill1, fill2;

    private Coroutine AnimationSliderCoroutine;

    [Header("Events")]
    [SerializeField] private UnityEvent onToggleOn;
    [SerializeField] private UnityEvent onToggleOff;

    private ToggleSwitchGroupManager toggleSwitchGroupManager;

    protected void OnValidate(){
        SetupToggleComponents();
        slider.value = sliderValue;
    }

    private void SetupToggleComponents(){
        if(slider != null) return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent(){
        slider = GetComponent<Slider>();

        if(slider == null){
            Debug.Log("No slider found", context:this);
            return;
        }

        if(CurrentValue){
            fill1.SetActive(false);
            fill2.SetActive(true);
        }
        else{
            fill1.SetActive(true);
            fill2.SetActive(false);
        }

        slider.interactable = false;
        var sliderColors = slider.colors;
        sliderColors.disabledColor = Color.white;
        slider.colors = sliderColors;
        slider.transition = Selectable.Transition.None;
    }

    public void SetupForManager(ToggleSwitchGroupManager manager){
        toggleSwitchGroupManager = manager;
    }

    private void Awake(){
        SetupToggleComponents();
    }

    public void OnPointerClick(PointerEventData eventData){
        Toggle();
    }

    private void Toggle(){
        if(toggleSwitchGroupManager != null)
            toggleSwitchGroupManager.ToggleGroup(this);
        else
            SetStateAndStartAnimation(!CurrentValue);
    }

    public void ToggleByGroupManager(bool valueToSetTo){
        SetStateAndStartAnimation(valueToSetTo);
    }

    private void SetStateAndStartAnimation(bool state){
        CurrentValue = state;

        if(CurrentValue)
            onToggleOn?.Invoke();
        else
            onToggleOff?.Invoke();
        
        if(AnimationSliderCoroutine != null)
            StopCoroutine(AnimationSliderCoroutine);
        AnimationSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider(){
        float startValue = slider.value;
        float endValue =CurrentValue ? 1 : 0;

        float time = 0;
        if(animationDuration > 0){
            while(time < animationDuration){
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time/animationDuration);
                slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                yield return null;
            }
        }

        slider.value = endValue;
    }
}
