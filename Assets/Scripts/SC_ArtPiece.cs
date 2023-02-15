using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtPiece",menuName = "ArtPiece")]
public class SC_ArtPiece : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private string artPieceName;
    [SerializeField] private string artistName;
    //[SerializeField] private GameObject model3D;
    [SerializeField] private Sprite grafika2D;
    public int Price {
        get { return price; }
    }
    public string ArtPieceName
    {
        get { return artPieceName; }
    }
    public string ArtistName
    {
        get { return artistName; }
    }
   /* public GameObject Model3d
    {
        get { return model3D; }
    }*/
    public Sprite Grafika2d
    {
        get { return grafika2D; }
    }
}
