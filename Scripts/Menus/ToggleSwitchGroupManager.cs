using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitchGroupManager : MonoBehaviour
{
    [Header("Start Value")]
    [SerializeField] private ToggleSwitch initialToggleSwitch;

    [Header("Toggle Options")]
    [SerializeField] private bool allCanBeToggledOff;

    private List<ToggleSwitch> toggleSwitches = new List<ToggleSwitch>();

    private void Awake(){
        ToggleSwitch[] toggleSwitches = GetComponentsInChildren<ToggleSwitch>();
        foreach(var toggleSwitch in toggleSwitches){
            RegisterToggleButtonToGroup(toggleSwitch);
        }
    }

    private void RegisterToggleButtonToGroup(ToggleSwitch toggleSwitch){
        if(toggleSwitches.Contains(toggleSwitch)) return;

        toggleSwitches.Add(toggleSwitch);
        toggleSwitch.SetupForManager(this);
    }

    private void Start()
    {
        bool areAllToggledOff = true;
        foreach(var button in toggleSwitches){
            if(!button.CurrentValue) continue;

            areAllToggledOff = false;
            break;
        }

        if(!areAllToggledOff || allCanBeToggledOff) return;

        if(initialToggleSwitch != null)
            initialToggleSwitch.ToggleByGroupManager(true);
        else
            toggleSwitches[0].ToggleByGroupManager(true);
    }

    public void ToggleGroup(ToggleSwitch toggleSwitch){
        if(toggleSwitches.Count <= 1) return;

        if(allCanBeToggledOff && toggleSwitch.CurrentValue){
            foreach(var button in toggleSwitches){
                if(button == null) continue;

                button.ToggleByGroupManager(false);
            }
        }
        else{
            foreach(var button in toggleSwitches){
                if(button == null) continue;

                if(button == toggleSwitch)
                    button.ToggleByGroupManager(true);
                else
                    button.ToggleByGroupManager(false);
            }
        }
    }
}
