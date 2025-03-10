using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private bool isThereTrash = false;
    private GameObject trashObject;
    
    void Start()
    {
        
    }

    public void InteractWithTrash(InputAction.CallbackContext context){
        if(context.performed && isThereTrash && trashObject != null){
            Trash trash = trashObject.GetComponent<Trash>();
            trash.TrashSelected();
            trashObject = null;
        }
    }

    private void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Trash")){
            isThereTrash = true;
            trashObject = col.gameObject;
            print("encontrou lixo");
        }
    }

    private void OnCollisionExit(Collision col){
        if(col.gameObject.CompareTag("Trash")){
            isThereTrash = false;
            trashObject = null;
            print("saiu do lixo");
        }
    }

    private void OnTriggerEnter(Collider col){
        if(col.gameObject.CompareTag("Trash")){
            isThereTrash = true;
            trashObject = col.gameObject;
            print("encontrou lixo");
        }
    }

    private void OnTriggerExit(Collider col){
        if(col.gameObject.CompareTag("Trash")){
            isThereTrash = false;
            trashObject = null;
            print("saiu do lixo");
        }
    }
}
