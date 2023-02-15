using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataToNextScene : MonoBehaviour
{
    public static SaveDataToNextScene instance;
    [SerializeField] private ArtPieceUIDisplayManager ArtPieceUIDisplayManager;
    public SC_ArtPiece[] artPiecesLicytation { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        artPiecesLicytation = ArtPieceUIDisplayManager.artPiecesLicytation;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Destroy(gameObject);
        }
    }
}
