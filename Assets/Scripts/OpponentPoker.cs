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

    // character traits
    [SerializeField] float callRating = 0.5f;
    [SerializeField] float betRating = 0.5f;
    [SerializeField] float raiseRating = 0.7f;
    [SerializeField] float thinkTimeMin = 3f;
    [SerializeField] float thinkTimeMax = 6f;

    public float betAmount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        opponentMoneyText.text = opponentMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        StartCoroutine(RespondToBetCoroutine());
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
        betProcessor.ProcessOpponentCheck();
    }

    private void Fold()
    {
        Debug.Log("Opponent folds");
        betProcessor.ProcessOpponentFold();
    }

    private void Call()
    {
        Debug.Log("Opponent calls");
        PayBet(playerPoker.betAmount);
        betProcessor.ProcessOpponentCall();
    }

    private void Bet()
    {
        // random amount between ante and all-in
        betAmount = Random.Range(potTracker.ante, opponentMoney);
        
        // round to 2 decimal places
        betAmount = Mathf.Round(betAmount * 100f) / 100f;

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

        // then process the raise
        betProcessor.ProcessOpponentBet(betAmount);
    }
}
