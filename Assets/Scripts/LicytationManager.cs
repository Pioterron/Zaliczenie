using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LicytationManager : MonoBehaviour
{
    public static LicytationManager instance;

    enum LicytationFSM
    {
        PlayerTurn,
        AiTurn,
        ChangeArtpiece,
        NextScene,
        Default
    }

    LicytationFSM licytationState = LicytationFSM.Default;


    public TMP_Text priceDisplayText;
    public float tempPrice;

    private int artPiecesArrayLenght;
    private int artPieceIndex = 0;
    private GameObject newDisplay;
    private SC_ArtPiece[] artPiecesLicytation;
    private float inputPrice;
    private int aiIndex = 0;

    private bool canChangeState = true;
    private bool aiGaveUp = false;
    private bool isNextButton = false;
    private bool playerWon = false;

    [Header("Designer Variables")]
    [Range(0, 1)] [SerializeField] private float startingPriceMultiplayer = 0.5f;
    [Range(1, 2)] [SerializeField] private float priceRangeMin = 1.05f;
    [Range(1, 2)] [SerializeField] private float priceRangeMax = 1.15f;

    [Header("Programmer Variables")]
    [SerializeField] private int sceneId;
    [SerializeField] private GameObject[] saveObjectsArray;
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private GameObject vieportContent;
    [SerializeField] private SaveScore saveScore;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TMP_InputField inputField;

    //-------------------------------------------------------------------------------------------
    private void Awake()
    {
        instance = this;
        artPiecesLicytation = FindObjectOfType<SaveDataToNextScene>().GetComponent<SaveDataToNextScene>().artPiecesLicytation;
    }
    private void Start()
    {
        artPiecesArrayLenght = artPiecesLicytation.Length;
        licytationState = LicytationFSM.ChangeArtpiece;
        tempPrice = artPiecesLicytation[artPieceIndex].Price * startingPriceMultiplayer;
        Debug.Log("saveScore.isArtPieceBought.Capacity: " + saveScore.isArtPieceBought.Capacity);
    }

    private void Update()
    {
        DisplayArtPiece();
        DisplayPrice();
        if (canChangeState == false)
        {
            return;
        }
        Debug.Log("licytationState: " + licytationState);
        Debug.Log("aiGaveUp: " + aiGaveUp);
        Debug.Log("isNextButton: " + isNextButton);
        switch (licytationState)
        {           
            case LicytationFSM.PlayerTurn:
                StartCoroutine(PlayerTurn());
                break;
            case LicytationFSM.AiTurn:
                StartCoroutine(AiTurn());
                break;
            case LicytationFSM.ChangeArtpiece:
                StartCoroutine(ChangeArtpiece());
                break;
            case LicytationFSM.NextScene:
                StartCoroutine(NextScene());
                break;
            default:
                Debug.LogError("State not recognized: " + licytationState);
                break;
        }
    }

    private IEnumerator PlayerTurn()
    {
        Debug.Log("PlayerTurn");
        canChangeState = false;
        isNextButton = false;
        yield return new WaitUntil(() => isNextButton == true);
        if (inputField.text != "")
        {
            inputPrice = float.Parse(inputField.text);
        }
        else
        {
            inputPrice = 0.0f;
        }
        Debug.Log("inputPrice AAA" + inputPrice);
        if (inputField.text != "" && inputPrice >= tempPrice)
        {
            Debug.Log("EverythingIsCorrect AAA");
            tempPrice = inputPrice;
            playerWon = true;
        }
        Debug.Log("continuation");
        yield return new WaitForSeconds(0);
        licytationState = LicytationFSM.AiTurn;
        canChangeState = true;
    }

    private IEnumerator AiTurn()
    {
        float randPrice = Random.Range(priceRangeMin, priceRangeMax);
        float randAiIndex = Random.Range(0, 2);
        Debug.Log("Random Ai Index: " + randAiIndex);
        canChangeState = false;
        switch (aiIndex)
        {
            case 0:
                tempPrice = tempPrice * randPrice;
                playerWon = false;
                if (randAiIndex == 1)
                {
                aiIndex++;        
                }
                break;
            case 1:
                aiGaveUp = true;
                aiIndex = 0;
                break;
            default:
                break;
        }
        Debug.Log("Ai index :" + aiIndex);
        licytationState = LicytationFSM.ChangeArtpiece;
        yield return new WaitForEndOfFrame();
        canChangeState = true;
    }

    private IEnumerator ChangeArtpiece()
    {
        Debug.Log("ChangeArtpiece");
        Debug.Log("artPieceIndex in ChangeArtpiece: " + artPieceIndex);
        Debug.Log("artPiecesArrayLenght: " + artPiecesArrayLenght);
        Debug.Log("FutureState: " + licytationState);
        canChangeState = false;
        if (artPieceIndex == artPiecesArrayLenght - 1 && aiGaveUp == true)
        {
            SavePrice();
            licytationState = LicytationFSM.NextScene;
            //yield break;
        }else
        {
        licytationState = LicytationFSM.PlayerTurn;
        }
        if(aiGaveUp == true && licytationState != LicytationFSM.NextScene)
        {
            SavePrice();
            artPieceIndex++;
            aiGaveUp = false;
            tempPrice = artPiecesLicytation[artPieceIndex].Price * startingPriceMultiplayer;
        }
        DisplayPrice();
        yield return new WaitForSeconds(0);
        canChangeState = true;
    }

    private IEnumerator NextScene()
    {
        Debug.Log("NextScene");
        canChangeState = false;
        yield return new WaitForEndOfFrame();
        sceneLoader.LoadNewScene(sceneId, saveObjectsArray);
    }





    private void SavePrice()
    {
        if (playerWon)
        {
            saveScore.isArtPieceBought.Add(true);
            saveScore.artPieceBuyPrice.Add(tempPrice);
            saveScore.artPiecePrice.Add(artPiecesLicytation[artPieceIndex].Price);
        }
        else
        {
            saveScore.isArtPieceBought.Add(false);
            saveScore.artPieceBuyPrice.Add(tempPrice);
            saveScore.artPiecePrice.Add(artPiecesLicytation[artPieceIndex].Price);

        }
    }
    private void DisplayPrice()
    {
        priceDisplayText.text = tempPrice.ToString();
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

    public void NextButton()
    {
        Debug.Log("pressed");
        if(licytationState == LicytationFSM.PlayerTurn)
        {
            licytationState = LicytationFSM.AiTurn;

        } else if(licytationState == LicytationFSM.AiTurn)
        {
            licytationState = LicytationFSM.ChangeArtpiece;
        }
        isNextButton = true;
    }
    //-------------------------------------------------------------------------------------------
    /*
    [SerializeField] private GameObject priceInputButton;

    private float artPiecePrice;

    private bool isDelayLicytationAi = false;



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
    }*/
}