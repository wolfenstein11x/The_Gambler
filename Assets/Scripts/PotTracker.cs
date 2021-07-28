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
        playerPoker.AdjustMoney(pot);

        //AdjustOpoonentMoney

        pot = 0;
        potText.text = pot.ToString();

    }

    
}
