using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] float playerMoney = 10f;
    [SerializeField] Text playerMoneyText;
    [SerializeField] GameObject betInputField = null;

    private string betString;
    public float playerBet;

    // Start is called before the first frame update
    void Start()
    {
        playerMoneyText.text = playerMoney.ToString();
    }


    public void PayAnte()
    {
        AdjustMoney(-potTracker.ante);
        potTracker.UpdatePot(potTracker.ante);
    }

    public void AdjustMoney(float amount)
    {
        float newAmount = float.Parse(playerMoneyText.text) + amount;
        playerMoney = newAmount;
        playerMoneyText.text = newAmount.ToString();
    }

    public void StoreBet()
    {
        betString = betInputField.GetComponent<Text>().text;

        playerBet = float.Parse(betString);

        potTracker.UpdatePot(playerBet);
        AdjustMoney(-playerBet);
    }

    
}
