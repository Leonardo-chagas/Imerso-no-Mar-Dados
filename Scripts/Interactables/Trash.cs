using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private TrashManager trashManager;
    private GameObject trashObject;
    [SerializeField] private GameObject[] trashPrefabs;
    private MeshRenderer trashRenderer;
    private  RecordTrashEvent trashEvent;
    private AudioSource audioSource;

    private bool randomizeRotation = true;
    private bool taken = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        trashManager = transform.parent.gameObject.GetComponent<TrashManager>();
        trashEvent = GetComponent<RecordTrashEvent>();

        int index = Random.Range(0, trashPrefabs.Length);
        trashObject = Instantiate(trashPrefabs[index], transform);
        /* trashRenderer = trashObject.GetComponent<MeshRenderer>();
        trashRenderer.material = dissolve;
        trashRenderer.material.SetTexture("Texture", trashTextures[index]); */

        if(randomizeRotation){
            trashObject.transform.rotation = Random.rotation;
        }
    }

    public void TrashSelected(){
        if(taken) return;
        audioSource.Play();
        print("lixo foi pegado");
        trashManager.TrashPickedUp();
        taken = true;
        trashEvent.RecordTrashCollected();
        StartCoroutine(Dissolve());
    }

    IEnumerator Dissolve(){
        trashRenderer = trashObject.GetComponent<MeshRenderer>();
        float t = 0;
        while(t < 1f){
            //print("dissolvendo");
            t += Time.deltaTime/2f;
            print(t);
            trashRenderer.material.SetFloat("_Dissolve", t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
