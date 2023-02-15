using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArtPieceUIDisplayManager : MonoBehaviour
{
    public static ArtPieceUIDisplayManager instance;

    [SerializeField] SC_ArtPiece[] artPieceArray;
    private SC_ArtPiece[] artPiecesInspection;
    public SC_ArtPiece[] artPiecesLicytation { get; private set; }

    [SerializeField] private int isnspectionArrayLenght;
    [SerializeField] private int licytationArrayLenght;

    [SerializeField] private GameObject vieportContent;
    [SerializeField] private GameObject displayPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        artPiecesInspection =  GetRandomizedArray(artPieceArray, isnspectionArrayLenght);
        artPiecesLicytation = GetRandomizedArray(artPiecesInspection, licytationArrayLenght);
        DisplayArray();
    }

    private SC_ArtPiece[] GetRandomizedArray(SC_ArtPiece[] donorArray, int recipientLenght)
    {
        List<SC_ArtPiece> tempList = new List<SC_ArtPiece>();
        SC_ArtPiece newElement;
        int rand;
        int i = 0;
        bool isRepeating;
        while (tempList.Count < recipientLenght)
        {
            isRepeating = false;
            rand = Random.Range(0, donorArray.Length);
            newElement = donorArray[rand];
            foreach (SC_ArtPiece artPiece in tempList)
            {
                if(artPiece.ArtPieceName == newElement.ArtPieceName)
                {
                    isRepeating = true;
                }
            }
            if(isRepeating != true)
            {
                tempList.Add(newElement);
                i++;
            }
        }
        return tempList.ToArray();
    }

    private void DisplayArray()
    {
        foreach (SC_ArtPiece artPiece in artPiecesInspection)
        {
            GameObject newDisplay = Instantiate(displayPrefab, vieportContent.transform);
            newDisplay.GetComponentInChildren<TMP_Text>().text = artPiece.ArtPieceName;
            foreach (Transform child in newDisplay.transform)
            {
                if(child.gameObject.name == "DisplayImage")
                {
                    child.GetComponent<Image>().sprite = artPiece.Grafika2d;
                }
            }
        }
    }

}
