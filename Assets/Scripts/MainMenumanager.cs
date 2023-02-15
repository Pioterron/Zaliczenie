using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenumanager : MonoBehaviour
{
    public static MainMenumanager instance;

    [SerializeField] private GameObject[] saveObjectsArray;
    [SerializeField] private int sceneId;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject optionsMenu;
    private void Awake()
    {
        instance = this;
    }

    public void StartGame()
    {
        sceneLoader.LoadNewScene(sceneId, saveObjectsArray);
    }

    public void OpenOptionsMenu() 
    {
        optionsMenu.SetActive(true);
    }
    public void CloseOptionsMenu() 
    {
        optionsMenu.SetActive(false);
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
}
