using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFishNotification : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject text;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateNotification(Component sender, object data){
        StartCoroutine(Notification());
    }

    IEnumerator Notification(){
        yield return new WaitForSeconds(3f);
        audioSource.Play();
        panel.SetActive(true);
        text.SetActive(true);

        yield return new WaitForSeconds(4f);
        panel.SetActive(false);
        text.SetActive(false);
    }
}
