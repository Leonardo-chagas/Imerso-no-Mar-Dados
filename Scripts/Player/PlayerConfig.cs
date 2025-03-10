using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR.Interaction.Toolkit;

//Sceipt que recebe um evento para mudar as configurações do jogador
public class PlayerConfig : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousTurnProvider continuousTurnProvider;
    [SerializeField] private ActionBasedSnapTurnProvider snapTurnProvider;
    [SerializeField] private Volume vignetteEffect;

    [Header("Action Inputs")]
    [SerializeField] private InputActionReference continuousLeftHand, continuousRightHand;
    [SerializeField] private InputActionReference snapLeftHand, snapRightHand;

    private PlayerMovement playerMovement;
    
    void Start()
    {
        continuousTurnProvider = GetComponent<ActionBasedContinuousTurnProvider>();
        snapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
        playerMovement = GetComponent<PlayerMovement>();

        float turnType = PlayerPrefs.GetFloat("Turning Selection", 0f);
        float snapAngle = (float)PlayerPrefs.GetInt("Snap Angle", 15);
        //float movementHand = PlayerPrefs.GetFloat("Movement Hand Selection", 0f);
        float movementHand = 0f;
        float vignetteIntensity = PlayerPrefs.GetFloat("Vignette", 0f);

        continuousTurnProvider.enabled = turnType == 0 ? true : false;
        snapTurnProvider.enabled = turnType != 0 ? true : false;
        snapTurnProvider.turnAmount = snapAngle;

        if(movementHand == 0){
            print("rotação com mão direita");
            continuousTurnProvider.leftHandTurnAction = new InputActionProperty(null);
            continuousTurnProvider.rightHandTurnAction = new InputActionProperty(continuousRightHand);
            snapTurnProvider.leftHandSnapTurnAction = new InputActionProperty(null);
            snapTurnProvider.rightHandSnapTurnAction = new InputActionProperty(snapRightHand);
        }
        else{
            print("rotação com mão esquerda");
            continuousTurnProvider.leftHandTurnAction = new InputActionProperty(continuousLeftHand);
            continuousTurnProvider.rightHandTurnAction = new InputActionProperty(null);
            snapTurnProvider.leftHandSnapTurnAction = new InputActionProperty(snapLeftHand);
            snapTurnProvider.rightHandSnapTurnAction = new InputActionProperty(null);
        }
        
        Vignette vignette;
        if(vignetteEffect.profile.TryGet(out vignette)){
            vignette.intensity.overrideState = true;
            vignette.intensity.SetValue(new ClampedFloatParameter(vignetteIntensity, 0, 1f));
        }
    }

    //Função que recebe o array de valores para mudar as configurações do jogador
    public void PlayerConfigChanged(Component sender, object data){
        print("new config detected");
        if(data is List<float>){
            print("changing config");
            List<float> newData = data as List<float>;
            //altera o tipo de rotação da camera
            continuousTurnProvider.enabled = newData[0] == 0 ? true : false;
            snapTurnProvider.enabled = newData[0] != 0 ? true : false;

            //altera o angulo de rotação do snap turn
            snapTurnProvider.turnAmount = newData[1];

            //Altera a mão de rotação e de movimento
            if(newData[2] == 0){
                playerMovement.isLeftHanded = true;
                continuousTurnProvider.leftHandTurnAction = new InputActionProperty(null);
                continuousTurnProvider.rightHandTurnAction = new InputActionProperty(continuousRightHand);
                snapTurnProvider.leftHandSnapTurnAction = new InputActionProperty(null);
                snapTurnProvider.rightHandSnapTurnAction = new InputActionProperty(snapRightHand);
            }
            else{
                playerMovement.isLeftHanded = false;
                continuousTurnProvider.leftHandTurnAction = new InputActionProperty(continuousLeftHand);
                continuousTurnProvider.rightHandTurnAction = new InputActionProperty(null);
                snapTurnProvider.leftHandSnapTurnAction = new InputActionProperty(snapLeftHand);
                snapTurnProvider.rightHandSnapTurnAction = new InputActionProperty(null);
            }

            //Altera a intensidade do efeito de vignette
            Vignette vignette;
            if(vignetteEffect.profile.TryGet(out vignette)){
                vignette.intensity.overrideState = true;
                vignette.intensity.SetValue(new ClampedFloatParameter(newData[3], 0, 1f));
            }
        }
    }
}
