using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotTracker : MonoBehaviour
{
    [SerializeField] Text potText;
    [SerializeField] Text playerMoneyText;
    [SerializeField] GameObject betInputField = null;
    [SerializeField] float ante = 0.50f;
    [SerializeField] float startingMoney = 10f;

    private string betString;
    private float bet;
    private float pot = 0;

    private void Start()
    {
        playerMoneyText.text = startingMoney.ToString();
    }

    public void StoreBet()
    {
        betString = betInputField.GetComponent<Text>().text;

        bet = float.Parse(betString);

        UpdatePot(bet);
        AdjustPlayerMoney(-bet);
    }

    public void UpdatePot(float amount)
    {
        pot += amount;
        potText.text = pot.ToString();
    }

    public void CollectAntes()
    {
        UpdatePot(ante * 2);
        AdjustPlayerMoney(-ante);
        //AdjustOpponentMoney(-ante);
    }

    private void AdjustPlayerMoney(float amount)
    {
        float newAmount = float.Parse(playerMoneyText.text) + amount;

        playerMoneyText.text = newAmount.ToString();
    }

    public void PlayerWinsPot()
    {
        AdjustPlayerMoney(pot);

        //AdjustOpoonentMoney

        pot = 0;
        potText.text = pot.ToString();

    }

    
}
