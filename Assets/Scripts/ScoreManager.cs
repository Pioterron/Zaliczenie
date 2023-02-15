using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TMP_Text textObject;
    [SerializeField] private string scoreText;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private int sceneId;
    [SerializeField] private GameObject[] saveObjectsArray;

    private SaveScore saveScore;
    private float score;
    private int listLenght;

    private void Awake()
    {
        instance = this;
        saveScore = FindObjectOfType<SaveScore>();
    }

    private void Start()
    {
        listLenght = saveScore.artPiecePrice.Count;
        for(int i = 0; i < listLenght; i++)
        {
            if (saveScore.isArtPieceBought[i])
            {
                score = saveScore.artPiecePrice[i] - saveScore.artPieceBuyPrice[i];
                if(score < 0)
                {
                    score = 0;
                }
            }
        }
        textObject.text = scoreText + score.ToString();
    }

    public void ReturnToMenu()
    {
        sceneLoader.LoadNewScene(sceneId, saveObjectsArray);
    }
}
