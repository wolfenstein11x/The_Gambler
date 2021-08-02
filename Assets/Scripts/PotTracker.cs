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

    private float epsilon = 0.001f;

  
    public void UpdatePot(float amount)
    {
        pot += amount;

        // round to 2 decimal places
        pot = Mathf.Round(pot * 100f) / 100f;

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

    public void SplitPot()
    {
        StartCoroutine(opponentPoker.OutputMessage(opponentPoker.drawMessage));

        // give half the pot to player
        playerPoker.AdjustMoney(pot / 2);

        // give half the pot to the opponent
        opponentPoker.AdjustMoney(pot / 2);

        // reset pot to zero
        pot = 0;
        potText.text = pot.ToString();
    }

    private void CheckPlayerWinsMatch()
    {
      
        if (opponentPoker.opponentMoney <= epsilon)
        {
            betProcessor.ProcessPlayerWinMatch();
        }
    }

    private void CheckOpponentWinsMatch()
    {
        
        if (playerPoker.playerMoney <= epsilon)
        {
            betProcessor.ProcessPlayerLoseMatch();
        }
    }

    
}
