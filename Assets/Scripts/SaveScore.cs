using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveScore : MonoBehaviour
{
    public static SaveScore instance;

    public List<float> artPiecePrice = new List<float>();
    public List<float> artPieceBuyPrice = new List<float>();
    public List<bool> isArtPieceBought = new List<bool>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(gameObject);
        }
    }
}
