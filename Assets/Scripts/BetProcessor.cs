using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetProcessor : MonoBehaviour
{
    [SerializeField] PlayerPoker playerPoker;
    [SerializeField] OpponentPoker opponentPoker;
    [SerializeField] Dealer dealer;
    [SerializeField] ButtonDisplayer buttonDisplayer;
    [SerializeField] PotTracker potTracker;
    
    
    

    private void Start()
    {
        
    }

    public void ProcessAntes()
    {
        dealer.SetState(HandState.Begin);

        buttonDisplayer.ShowDealButtonOnly();
    }

    public void ProcessPlayerCheck()
    {
        buttonDisplayer.HideAllButtons();

        opponentPoker.RespondToCheck();
    }

    public void ProcessPlayerBet()
    {
        buttonDisplayer.HideAllButtons();

        opponentPoker.RespondToBet();
    }

    public void ProcessPlayerCall()
    {
        FinishTurn();
    }

    public void ProcessOpponentCheck()
    {
        FinishTurn();
    }

    public void ProcessPlayerFold()
    {
        potTracker.OpponentWinsPot();

        dealer.SetState(HandState.Reveal);

    }

    public void ProcessOpponentFold()
    {
        potTracker.PlayerWinsPot();

        dealer.SetState(HandState.Reveal);

    }

    public void ProcessOpponentCall()
    {
        FinishTurn();
    }

    public void ProcessOpponentBet(float amount)
    {
        buttonDisplayer.ShowCallFoldButtonsOnly();
    }

    public void ProcessPlayerWinMatch()
    {
        // add $10 to player's total money
        PlayerData.playerTotalMoney += 10;
        
        buttonDisplayer.ShowWinMenuOnly();
    }

    public void ProcessPlayerLoseMatch()
    {
        // subtract $10 from player's total money
        PlayerData.playerTotalMoney -= 10;

        buttonDisplayer.ShowLoseMenuOnly();
    }

    public void ProcessHandComplete()
    {
        buttonDisplayer.ShowDealButtonOnly();
    }

    private void FinishTurn()
    {
        // show the deal button if the hand isn't over
        if (GetState() == HandState.Reveal)
        {
            buttonDisplayer.ShowRevealButtonOnly();
        }

        // show the reveal button if the hand is over
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
