using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] OpponentPoker opponentPoker;
    public float playerMoney = 10f;
    [SerializeField] Text playerMoneyText;
    [SerializeField] GameObject betInputField = null;

    private string betString;
    public float betAmount;

    // Start is called before the first frame update
    void Start()
    {
        playerMoneyText.text = playerMoney.ToString();
    }


    public void PayAnte()
    {
        // level off bet amount so not to go below zero
        if (potTracker.ante >= playerMoney)
        {
            potTracker.UpdatePot(playerMoney);
            AdjustMoney(-playerMoney);

        }
        else
        {
            AdjustMoney(-potTracker.ante);
            potTracker.UpdatePot(potTracker.ante);
        }
    }

    public void AdjustMoney(float amount)
    {
        float newAmount = float.Parse(playerMoneyText.text) + amount;
        playerMoney = newAmount;

        // level off amount so not to go below zero
        if (newAmount <= 0) { newAmount = 0; }

        playerMoneyText.text = newAmount.ToString();
    }

    public void StoreBet()
    {
        betString = betInputField.GetComponent<Text>().text;

        betAmount = float.Parse(betString);

        // level off bet amount so not to go below zero
        if (betAmount >= playerMoney) { betAmount = playerMoney; }

        // level off with opponent money if more than opponent money
        if (betAmount >= opponentPoker.opponentMoney) { betAmount = opponentPoker.opponentMoney; }

        potTracker.UpdatePot(betAmount);
        AdjustMoney(-betAmount);
    }

    public void Raise()
    {
        betString = betInputField.GetComponent<Text>().text;
        betAmount = float.Parse(betString);

        float totalAmount = betAmount + opponentPoker.betAmount;

        potTracker.UpdatePot(totalAmount);
        AdjustMoney(-totalAmount);
    }

    public void Call()
    {
        AdjustMoney(-opponentPoker.betAmount);

        potTracker.UpdatePot(opponentPoker.betAmount);

        betProcessor.ProcessPlayerCall();
    }

    public void Fold()
    {
        Debug.Log("Player fold");
        betProcessor.ProcessPlayerFold();
    }

    
}
