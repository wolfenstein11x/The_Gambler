using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotTracker : MonoBehaviour
{
    [SerializeField] Dealer dealer;
    [SerializeField] PlayerPoker playerPoker;
    [SerializeField] OpponentPoker opponentPoker;
    [SerializeField] BetProcessor betProcessor;
    [SerializeField] Text potText;
   
    public float ante = 0.50f;
    private float pot = 0;

  
    public void UpdatePot(float amount)
    {
        pot += amount;
        potText.text = pot.ToString();
    }

    

    public void PlayerWinsPot()
    {
        // only ouput lose message if it was on a reveal (not a fold)
        if (dealer.State() == HandState.Reveal)
        {
            StartCoroutine(opponentPoker.OutputMessage(opponentPoker.loseMessage));
        }
        
        // add pot to player money
        playerPoker.AdjustMoney(pot);

        // reset pot to zero
        pot = 0;
        potText.text = pot.ToString();

        // check if player has won the match
        CheckPlayerWinsMatch();

    }

    public void OpponentWinsPot()
    {
        StartCoroutine(opponentPoker.OutputMessage(opponentPoker.winMessage));

        // add pot to opponent money
        opponentPoker.AdjustMoney(pot);

        // reset pot to zero
        pot = 0;
        potText.text = pot.ToString();
        
        // check if opponent has won match
        CheckOpponentWinsMatch();
    }

    private void CheckPlayerWinsMatch()
    {
        if (opponentPoker.opponentMoney == 0)
        {
            betProcessor.ProcessPlayerWinMatch();
        }
    }

    private void CheckOpponentWinsMatch()
    {
        if (playerPoker.playerMoney == 0)
        {
            betProcessor.ProcessPlayerLoseMatch();
        }
    }

    
}
