using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetCollector : MonoBehaviour
{
    private Dealer dealer;
    private ButtonDisplayer buttonDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        dealer = FindObjectOfType<Dealer>();
        buttonDisplayer = FindObjectOfType<ButtonDisplayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveAntes()
    {
        Debug.Log("Ante received");
        dealer.SetState(HandState.Begin);

        buttonDisplayer.ShowDealButtonOnly();
    }

    public void ProcessCheck()
    {
        Debug.Log("You checked");

        if (GetState() == HandState.Reveal)
        {
            buttonDisplayer.ShowRevealButtonOnly();
        }

        else
        {
            buttonDisplayer.ShowDealButtonOnly();
        }
        
    }

    public void ReceivePlayerBet()
    {
        Debug.Log("You bet");

        if (GetState() == HandState.Reveal)
        {
            buttonDisplayer.ShowRevealButtonOnly();
        }

        else
        {
            buttonDisplayer.ShowDealButtonOnly();
        }
    }

    private HandState GetState()
    {
        return dealer.State();
    }
}
