using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionManager : MonoBehaviour
{
    public static InspectionManager isnstance;

    [SerializeField] private float timer;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int sceneId;
    [SerializeField] private GameObject[] saveObjectsArray;

    private void Awake()
    {
        isnstance = this;
        StartCoroutine(Countdown(timer));
    }

    private IEnumerator Countdown(float timer)
    {
        yield return new WaitForSeconds(timer);
        sceneLoader.LoadNewScene(sceneId, saveObjectsArray);
    }
}
