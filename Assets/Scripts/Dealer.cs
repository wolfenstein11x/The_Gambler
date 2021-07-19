using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject[] cardImg;

    private int[] deck = Enumerable.Range(0, 52).ToArray();
    private int[] playerCards = new int[2];
    private int[] opponentCards = new int[2];
    private int[] tableCards = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        Deal();
        Flop();
        Turn();
        River();

        Debug.Log("playerCards: " + playerCards[0] + "," + playerCards[1]);
        Debug.Log("opponentCards: " + opponentCards[0] + "," + opponentCards[1]);
        Debug.Log("tableCards: " + tableCards[0] + "," + tableCards[1] + "," + tableCards[2] + "," + tableCards[3] + "," + tableCards[4]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shuffle()
    {
        for (int i=0; i < deck.Length-1; i++)
        {
            int rnd = Random.Range(i, deck.Length);
            int tempGO = deck[rnd];
            deck[rnd] = deck[i];
            deck[i] = tempGO;

        }
    }

    private void Deal()
    {
        // deal player cards
        playerCards[0] = deck[0];
        playerCards[1] = deck[1];

        // deal opponent cards
        opponentCards[0] = deck[2];
        opponentCards[1] = deck[3];
    }

    private void Flop()
    {
        tableCards[0] = deck[4];
        tableCards[1] = deck[5];
        tableCards[2] = deck[6];
    }

    private void Turn()
    {
        tableCards[3] = deck[7];
    }

    private void River()
    {
        tableCards[4] = deck[8];
    }
}
