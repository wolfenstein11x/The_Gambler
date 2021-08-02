using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] PlayerPoker playerPoker;
    public float opponentMoney = 10f;
    [SerializeField] Text opponentMoneyText;
    [SerializeField] GameObject opponentTextbox = null;
    [SerializeField] Text opponentMessage;
    [SerializeField] float messageTime = 2f;

    // character traits
    [SerializeField] float callRating = 0.5f;
    [SerializeField] float betRating = 0.5f;
    [SerializeField] float raiseRating = 0.7f;
    [SerializeField] float thinkTimeMin = 3f;
    [SerializeField] float thinkTimeMax = 6f;
    public string winMessage = "Hey Chicago, whadda ya say!";
    public string loseMessage = "Dangit!";
    public string drawMessage = "Split pot!";

    public float betAmount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        opponentTextbox.SetActive(false);

        opponentMoneyText.text = opponentMoney.ToString();
    }

    public IEnumerator OutputMessage(string message)
    {
        opponentTextbox.SetActive(true);
        opponentMessage.text = message;

        yield return new WaitForSeconds(messageTime);

        opponentMessage.text = "";
        opponentTextbox.SetActive(false);
    }

    public void PayAnte()
    {
        // level off bet amount so not to go below zero
        if (potTracker.ante >= opponentMoney) 
        {
            potTracker.UpdatePot(opponentMoney);
            AdjustMoney(-opponentMoney);
            
        }
        else
        {
            AdjustMoney(-potTracker.ante);
            potTracker.UpdatePot(potTracker.ante);
        }
    }

    public void PayBet(float amount)
    {
        // level off bet amount so not to go below zero
        if (amount >= opponentMoney) { amount = opponentMoney; }

        // level off with player money if more than player money
        if (betAmount >= playerPoker.playerMoney) { betAmount = playerPoker.playerMoney; }

        potTracker.UpdatePot(amount);
        AdjustMoney(-amount);
    }

    public void AdjustMoney(float amount)
    {
        float newAmount = float.Parse(opponentMoneyText.text) + amount;
        opponentMoney = newAmount;

        // level off amount so not to go below zero
        if (newAmount <= 0) { newAmount = 0; }

        // round to 2 decimal places
        newAmount = Mathf.Round(newAmount * 100f) / 100f;

        opponentMoneyText.text = newAmount.ToString();
    }

    public void RespondToCheck()
    {
        StartCoroutine(RespondToCheckCoroutine());
    }

    private IEnumerator RespondToCheckCoroutine()
    {
        float thinkTime = Random.Range(thinkTimeMin, thinkTimeMax);

        yield return new WaitForSeconds(thinkTime);

        // automatically check if have no money
        if (opponentMoney <= 0)
        {
            Check();
        }

        else
        {
            // generate random float from 0 to 1 to determine check or bet
            float decision = Random.Range(0f, 1f);
            
            if (decision >= betRating) { Check(); }
            else { Bet(); }
        }

        

    }

    public void RespondToBet()
    {
        // if player is weird and bets zero, treat is as a check
        if (playerPoker.betAmount <= 0) { StartCoroutine(RespondToCheckCoroutine()); }
        
        else { StartCoroutine(RespondToBetCoroutine()); }
        
    }

    private IEnumerator RespondToBetCoroutine()
    {
        float thinkTime = Random.Range(thinkTimeMin, thinkTimeMax);

        yield return new WaitForSeconds(thinkTime);

        // generate random float from 0 to 1 to determine raise or not
        float decision = Random.Range(0f, 1f);

        // don't try to raise if you don't have enough money
        if (decision <= raiseRating && (opponentMoney >= playerPoker.betAmount + potTracker.ante))
        {
            Raise();
        }

        // not raising, so either call or fold
        else
        {
            // generate random float from 0 to 1 to determine call or fold
            decision = Random.Range(0f, 1f);
            if (decision >= callRating) { Fold(); }
            else { Call(); }
        }

        

    }


    private void Check()
    {
        StartCoroutine(OutputMessage("I check!"));

        betProcessor.ProcessOpponentCheck();
    }

    private void Fold()
    {
        StartCoroutine(OutputMessage("I fold!"));

        betProcessor.ProcessOpponentFold();
    }

    private void Call()
    {
        StartCoroutine(OutputMessage("I'll call!"));

        PayBet(playerPoker.betAmount);
        betProcessor.ProcessOpponentCall();
    }

    private void Bet()
    {
        // random amount between ante and all-in
        betAmount = Random.Range(potTracker.ante, opponentMoney);
        
        // round to 2 decimal places
        betAmount = Mathf.Round(betAmount * 100f) / 100f;

        StartCoroutine(OutputMessage("I bet $" + betAmount.ToString()));

        PayBet(betAmount);

        betProcessor.ProcessOpponentBet(betAmount);
    }

    private void Raise()
    {
        // first match the player bet
        PayBet(playerPoker.betAmount);

        // random amount between ante and all-in
        betAmount = Random.Range(potTracker.ante, opponentMoney);

        // round to 2 decimal places
        betAmount = Mathf.Round(betAmount * 100f) / 100f;

        // then bet the raise
        PayBet(betAmount);

        StartCoroutine(OutputMessage("I raise " + betAmount));

        // then process the raise
        betProcessor.ProcessOpponentBet(betAmount);
    }
}
