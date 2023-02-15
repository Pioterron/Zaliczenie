using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LicytationManager : MonoBehaviour
{
    public static LicytationManager instance;

    [SerializeField]  private int sceneId;
    [SerializeField]  private GameObject[] saveObjectsArray;
    [SerializeField] private GameObject vieportContent;
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text priceDisplayText;
    [SerializeField] private GameObject priceInputButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private SaveScore saveScore;



    private SC_ArtPiece[] artPiecesLicytation;
    private GameObject newDisplay;
    private int artPieceIndex = 0;
    private float artPiecePrice;
    private float tempPrice;
    private int artPiecesArrayLenght;
    private int aiIndex = 0;

    private bool isDelayLicytationAi = false;

    private void Awake()
    {
        instance = this;
        artPiecesLicytation = FindObjectOfType<SaveDataToNextScene>().GetComponent<SaveDataToNextScene>().artPiecesLicytation;
    }

    private void Start()
    {
        artPiecesArrayLenght = artPiecesLicytation.Length;
        Debug.Log(artPiecesArrayLenght);
        Licytation();
    }

    private void Licytation()
    {
        if (artPieceIndex == artPiecesArrayLenght)
        {
            return;
        }
        tempPrice = artPiecesLicytation[artPieceIndex].Price * 0.7f;
        DisplayArtPiece();
        DisplayPrice();
    }

    private void DisplayArtPiece()
    {
        if (newDisplay == null)
        {
            newDisplay = Instantiate(displayPrefab, vieportContent.transform);
        }
        newDisplay.GetComponentInChildren<TMP_Text>().text = artPiecesLicytation[artPieceIndex].ArtPieceName;
        foreach (Transform child in newDisplay.transform)
        {
            if (child.gameObject.name == "DisplayImage")
            {
                child.GetComponent<Image>().sprite = artPiecesLicytation[artPieceIndex].Grafika2d;
            }
        }
    }

    private void DisplayPrice()
    {
        priceDisplayText.text = tempPrice.ToString();
    }

    private void LicytationAi()
    {
        switch (aiIndex)
        {
            case 0:
                tempPrice = tempPrice * 1.05f;
                DisplayPrice();
                aiIndex++;
                saveScore.isArtPieceBought[artPieceIndex] = false;
                break;
            case 1:
                NextArtPiece();
                break;
            default:
                break;
        }
    }

    public void InputPrice()
    {
        if(aiIndex == 0)
        {
        saveScore.artPiecePrice.Add(artPiecesLicytation[artPieceIndex].Price);
        saveScore.isArtPieceBought.Add(false);
        saveScore.artPieceBuyPrice.Add(tempPrice);
        }
        if(inputField.text == "")
        {
            return;
        }
        Debug.Log(inputField.text);
        float inputPrice;
        inputPrice = float.Parse(inputField.text);
        if(inputPrice > tempPrice)
        {
            tempPrice = inputPrice;
            saveScore.isArtPieceBought[artPieceIndex] = true;
            saveScore.artPieceBuyPrice[artPieceIndex] = tempPrice;
        }
        DisplayPrice();
        StartCoroutine(DelayLicytationAi());
        priceInputButton.SetActive(false);
        inputField.gameObject.SetActive(false);
        nextButton.SetActive(true);
    }

    public void NextButton()
    {
        isDelayLicytationAi = true;
        nextButton.SetActive(false);
    }

    private void NextArtPiece()
    {
        if(artPieceIndex == artPiecesArrayLenght - 1)
        {
            sceneLoader.LoadNewScene(sceneId, saveObjectsArray);
        }
        artPieceIndex++;
        aiIndex = 0;
        Licytation();
    }

    public IEnumerator DelayLicytationAi()
    {
        yield return new WaitUntil(() => isDelayLicytationAi == true);
        isDelayLicytationAi = false;
        LicytationAi();
        priceInputButton.gameObject.SetActive(true);
        inputField.gameObject.SetActive(true);
    }
}