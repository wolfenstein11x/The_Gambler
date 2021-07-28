using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentPoker : MonoBehaviour
{
    [SerializeField] PotTracker potTracker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] float opponentMoney = 10f;
    [SerializeField] float thinkTimeMin = 3f;
    [SerializeField] float thinkTimeMax = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PayAnte()
    {
        opponentMoney -= potTracker.ante;

        potTracker.UpdatePot(potTracker.ante);
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
