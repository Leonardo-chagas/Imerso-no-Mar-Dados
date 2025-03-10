using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
using Unity.Mathematics;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float additionalHeight = 0.2f;
    [SerializeField] private float gravity = -9.81f;
    private float fallingSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private InputActionReference leftHandMove;
    [SerializeField] private InputActionReference rightHandMove;
    [SerializeField] private Transform trackingReference;
    [SerializeField] private CanvasGroup boundaryText;
    private XROrigin rig;
    private CharacterController characterController;
    private bool isSwimming = false;
    private Vector3 swimmDirection;
    private float swimmSpeed;
    private float swimSpeedX, swimSpeedZ;
    private bool showBoundaryText = false;
    private float currentFade;
    public bool isLeftHanded = true;

    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
        //float movementHand = PlayerPrefs.GetFloat("Movement Hand Selection", 0f);
        float movementHand = 0f;
        if(movementHand == 0){
            isLeftHanded = true;
        }
        else{
            isLeftHanded = false;
        }
    }

    private void Update(){
        if(showBoundaryText && currentFade < 1){
            currentFade = boundaryText.alpha;
            boundaryText.alpha = Mathf.MoveTowards(currentFade, 1, 1*Time.deltaTime);
        }
        else if(!showBoundaryText && currentFade > 0){
            currentFade = boundaryText.alpha;
            boundaryText.alpha = Mathf.MoveTowards(currentFade, 0, 1*Time.deltaTime);
        }
    }

    
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        //movimentação quando o jogador não está nadando
        if(!isSwimming){
            //calcula a direção do movimento de acordo com a câmera do jogador
            Vector2 input = leftHandMove.action.ReadValue<Vector2>();
            Vector3 value = new Vector3(input.x, 0, input.y);
            Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, rig.Camera.transform.eulerAngles.z);
            
            Vector3 direction = headYaw *  value;
            
            characterController.Move(direction * Time.fixedDeltaTime * speed);

            bool isGrounded = IsGrounded();

            //calcula a queda do jogador
            if(isGrounded)
                fallingSpeed = 0;
            else
                fallingSpeed += gravity * Time.fixedDeltaTime;
            
            characterController.Move(Vector3.up * fallingSpeed);
        }
        //movimentação quando o jogador está nadando
        else{
            Vector2 input;
            if(isLeftHanded)
                input = leftHandMove.action.ReadValue<Vector2>();
            else
                input = rightHandMove.action.ReadValue<Vector2>();

            //calcula a direção do movimento de acordo com a câmera do jogador
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
        
            if(input.x != 0){
                int dir = input.x > 0 ? 1 : -1;
                swimSpeedX = Mathf.MoveTowards(swimSpeedX, speed*dir, 2*Time.fixedDeltaTime);
            }
            else
                swimSpeedX = Mathf.MoveTowards(swimSpeedX, 0, 3*Time.fixedDeltaTime);
            if(input.y != 0){
                int dir = input.y > 0 ? 1 : -1;
                swimSpeedZ = Mathf.MoveTowards(swimSpeedZ, speed*dir, 2*Time.fixedDeltaTime);
            }
            else
                swimSpeedZ = Mathf.MoveTowards(swimSpeedZ, 0, 3*Time.fixedDeltaTime);

            //usar o TransformDirection para mover o jogador para onde a camera estiver olhando

            //Vector3 movement = trackingReference.TransformDirection(right*swimSpeedX + forward*swimSpeedZ);
            Vector3 movement = right*swimSpeedX + forward*swimSpeedZ;
            //print(forward);
            characterController.Move(movement*Time.fixedDeltaTime);
        }
    }

    private void CapsuleFollowHeadset(){
        characterController.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        characterController.center = new Vector3(capsuleCenter.x, characterController.height/2 + characterController.skinWidth, capsuleCenter.z);
    }

    private bool IsGrounded(){
        Vector3 rayStart = transform.TransformPoint((characterController.center));
        float rayLength = characterController.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, characterController.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    private void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("water")){
            isSwimming = true;
        }
        if(col.gameObject.CompareTag("Boundary")){
            showBoundaryText = true;
        }
    }

    private void OnTriggerExit(Collider col){
        if(col.gameObject.CompareTag("water")){
            isSwimming = false;
        }
        if(col.gameObject.CompareTag("Boundary")){
            showBoundaryText = false;
        }
    }
    
}
