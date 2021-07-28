using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] PlayerPoker playerPoker;
    [SerializeField] float opponentMoney = 10f;
    [SerializeField] Text opponentMoneyText;

    // character traits
    [SerializeField] float callRating = 0.9f;
    [SerializeField] float betRating = 0.9f;
    [SerializeField] float thinkTimeMin = 3f;
    [SerializeField] float thinkTimeMax = 6f;

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
        AdjustMoney(-potTracker.ante);
        potTracker.UpdatePot(potTracker.ante);
    }

    public void PayBet(float amount)
    {
        potTracker.UpdatePot(amount);
        AdjustMoney(-amount);
    }

    public void AdjustMoney(float amount)
    {
        float newAmount = float.Parse(opponentMoneyText.text) + amount;
        opponentMoney = newAmount;
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

        // generate random float from 0 to 1 to determine check or bet
        float decision = Random.Range(0f, 1f);
        if (decision >= betRating) { Check(); }
        else { Bet(); }

    }

    public void RespondToBet()
    {
        StartCoroutine(RespondToBetCoroutine());
    }

    private IEnumerator RespondToBetCoroutine()
    {
        float thinkTime = Random.Range(thinkTimeMin, thinkTimeMax);

        yield return new WaitForSeconds(thinkTime);

        // generate random float from 0 to 1 to determine call or fold
        float decision = Random.Range(0f, 1f);
        if (decision >= callRating) { Fold(); }
        else { Call(); }

    }

    private void Check()
    {
        betProcessor.ProcessOpponentCheck();
    }

    private void Fold()
    {
        Debug.Log("I fold!");
        betProcessor.ProcessOpponentFold();
    }

    private void Call()
    {
        Debug.Log("I call!");
        PayBet(playerPoker.playerBet);
        betProcessor.ProcessOpponentCall();
    }

    private void Bet()
    {
        // random amount between ante and all-in
        float betAmount = Random.Range(potTracker.ante, opponentMoney);
        
        // round to 2 decimal places
        betAmount = Mathf.Round(betAmount * 100f) / 100f;

        PayBet(betAmount);

        betProcessor.ProcessOpponentBet(betAmount);
    }
}
