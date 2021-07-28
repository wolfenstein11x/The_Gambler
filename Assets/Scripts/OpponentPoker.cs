using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] float opponentMoney = 10f;
    [SerializeField] Text opponentMoneyText;
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

        Check();

    }

    public void RespondToBet()
    {
        StartCoroutine(RespondToBetCoroutine());
    }

    private IEnumerator RespondToBetCoroutine()
    {
        float thinkTime = Random.Range(thinkTimeMin, thinkTimeMax);

        yield return new WaitForSeconds(thinkTime);

        Fold();
    }

    private void Check()
    {
        betProcessor.ProcessOpponentCheck();
    }

    private void Fold()
    {
        betProcessor.ProcessOpponentFold();
    }
}
