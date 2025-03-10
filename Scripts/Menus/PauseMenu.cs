using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cognitive3D;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, catalogueMenu, optionsMenu, infoMenu;
    [SerializeField] private PauseHandler pauseHandler;
    
    void OnEnable(){
        pauseMenu.SetActive(true);
        catalogueMenu.SetActive(false);
        optionsMenu.SetActive(false);
        infoMenu.SetActive(false);
    }

    public void Resume(){
        pauseHandler.DisplayWristUI();
    }

    public void Catalogue(){
        pauseMenu.SetActive(false);
        catalogueMenu.SetActive(true);
        //new Cognitive3D.CustomEvent("Entrou no menu de catalogo").Send();
    }

    public void CatalogueEntered(){
        new Cognitive3D.CustomEvent("Entrou no menu de catalogo").Send();
    }

    public void Options(){
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit(){
        Time.timeScale = 1;
        LevelToLoad.instance.level = 0;
        Cognitive3D.Cognitive3D_Manager.Instance.EndSession();
        SceneManager.LoadScene(1);
    }
}
