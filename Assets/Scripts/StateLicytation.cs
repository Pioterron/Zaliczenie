using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateLicytation : MonoBehaviour
{
    public LicytationManager licytationManager;

    //public string stateName; 
    public virtual void Enter()
    {
        return;
    }

    public virtual void DoStuff()
    {
        return;
    }

    public virtual void IsNextState()
    {
        return;
    }

    public void DisplayPrice()
    {
        licytationManager.priceDisplayText.text = licytationManager.tempPrice.ToString(); ;
    }
}
