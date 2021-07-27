using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotTracker : MonoBehaviour
{
    [SerializeField] Text potText;
    [SerializeField] GameObject betInputField = null;
    [SerializeField] float ante = 0.50f;

    private string betString;
    private float bet;
    private float pot = 0;

    
    public void StoreBet()
    {
        betString = betInputField.GetComponent<Text>().text;

        bet = float.Parse(betString);

        UpdatePot(bet);
    }

    public void UpdatePot(float amount)
    {
        pot += amount;
        potText.text = pot.ToString();
    }

    public void CollectAntes()
    {
        UpdatePot(ante * 2);
    }
}
