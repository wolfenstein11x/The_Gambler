using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotTracker : MonoBehaviour
{
    [SerializeField] PlayerPoker playerPoker;
    [SerializeField] OpponentPoker opponentPoker;
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
        StartCoroutine(opponentPoker.OutputMessage(opponentPoker.loseMessage));

        playerPoker.AdjustMoney(pot);

        pot = 0;
        potText.text = pot.ToString();

    }

    public void OpponentWinsPot()
    {
        StartCoroutine(opponentPoker.OutputMessage(opponentPoker.winMessage));

        opponentPoker.AdjustMoney(pot);

        pot = 0;
        potText.text = pot.ToString();
    }

    
}
