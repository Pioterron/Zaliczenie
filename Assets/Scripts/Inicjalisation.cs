using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicjalisation : MonoBehaviour
{
    public static Inicjalisation instance;

    [SerializeField] private GameObject[] saveObjectsArray;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int newSceneId;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        sceneLoader.LoadNewScene(newSceneId, saveObjectsArray);
    }
}
