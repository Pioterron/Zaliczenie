using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager instance;

    [SerializeField] private GameObject[] saveObjectsArray;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int newSceneId;
    [SerializeField] private GameObject RulesBackground;
    [SerializeField] private Sprite postac1;
    [SerializeField] private Sprite postac0;
    [SerializeField] private GameObject CharacterSprite;

    private void Awake()
    {
        instance = this;
    }

    public void NextCharacterButton()
    {
        if (CharacterSprite.GetComponent<Image>().sprite == postac1)
        {
            CharacterSprite.GetComponent<Image>().sprite = postac0;
            CharacterSprite.GetComponent<Button>().enabled = false;
        }
        else
        {
            CharacterSprite.GetComponent<Image>().sprite = postac1;
            CharacterSprite.GetComponent<Button>().enabled = true;
        }
    }

    public void SelectCharacterButton()
    {
        RulesBackground.SetActive(true);
    }

    public void LoadNextScene()
    {
        sceneLoader.LoadNewScene(newSceneId, saveObjectsArray);
    }
}
