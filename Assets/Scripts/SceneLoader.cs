using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [SerializeField] private GameObject[] saveObjecstArray;

    private void Awake()
    {
        instance = this;
    }
    public void LoadNewScene(int id, GameObject[] saveObjectsArray)
    {
        SaveObjects(saveObjectsArray);
        SceneManager.LoadScene(id);
    }

    private void SaveObjects(GameObject[] saveObjecstArray)
    {
        foreach(GameObject gameObject in saveObjecstArray)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
