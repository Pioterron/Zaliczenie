using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LicytationManager : MonoBehaviour
{
    public static LicytationManager instance;

    private SC_ArtPiece[] artPiecesLicytation;
    [SerializeField] private GameObject vieportContent;
    [SerializeField] private GameObject displayPrefab;

    private void Awake()
    {
        instance = this;
        artPiecesLicytation = FindObjectOfType<SaveDataToNextScene>().GetComponent<SaveDataToNextScene>().artPiecesLicytation;
        Debug.Log(artPiecesLicytation);
        DisplayArray();
    }

    private void DisplayArray()
    {
        foreach (SC_ArtPiece artPiece in artPiecesLicytation)
        {
            GameObject newDisplay = Instantiate(displayPrefab, vieportContent.transform);
            newDisplay.GetComponentInChildren<TMP_Text>().text = artPiece.ArtPieceName;
            foreach (Transform child in newDisplay.transform)
            {
                if (child.gameObject.name == "Display3D")
                {
                    GameObject new3DModel = Instantiate(artPiece.Model3d, child.gameObject.transform);
                    new3DModel.layer = 5;
                    new3DModel.transform.localPosition = (new Vector3(0, 0, 0));
                    new3DModel.transform.localScale *= 40;
                }
            }
        }
    }
}