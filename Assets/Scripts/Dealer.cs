using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum HandState { Ante, Begin, Bet, Flop, Turn, River, Reveal}
public class Dealer : MonoBehaviour
{
    HandState state;

    [SerializeField] GameObject[] cardImg;
    [SerializeField] GameObject cardImgBack = null;

    [SerializeField] Transform[] playerCardPos;
    [SerializeField] Transform[] opponentCardPos;
    [SerializeField] Transform[] tableCardPos;

    private int[] deck = Enumerable.Range(0, 52).ToArray();
    private int[] playerCards = new int[2];
    private int[] opponentCards = new int[2];
    private int[] tableCards = new int[5];

    private GameObject opponentCard1, opponentCard2;

    private ButtonDisplayer buttonDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        buttonDisplayer = FindObjectOfType<ButtonDisplayer>();

        state = HandState.Ante;
        Shuffle();
        buttonDisplayer.ShowAnteButtonOnly();

        //Debug.Log("playerCards: " + playerCards[0] + "," + playerCards[1]);
        //Debug.Log("opponentCards: " + opponentCards[0] + "," + opponentCards[1]);
        //Debug.Log("tableCards: " + tableCards[0] + "," + tableCards[1] + "," + tableCards[2] + "," + tableCards[3] + "," + tableCards[4]);

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

    public void Deal()
    {
        if (state == HandState.Begin) { DealHands(); }
        else if (state == HandState.Flop) { DealFlop(); }
        else if (state == HandState.Turn) { DealTurn(); }
        else if (state == HandState.River) { DealRiver(); }

    }

    private void DealHands()
    {
        // deal player cards
        playerCards[0] = deck[0];
        playerCards[1] = deck[1];

        // show player cards
        Instantiate(cardImg[playerCards[0]], playerCardPos[0].position, Quaternion.identity);
        Instantiate(cardImg[playerCards[1]], playerCardPos[1].position, Quaternion.identity);

        // deal opponent cards
        opponentCards[0] = deck[2];
        opponentCards[1] = deck[3];

        // show back of opponent cards
        opponentCard1 = Instantiate(cardImgBack, opponentCardPos[0].position, Quaternion.identity) as GameObject;
        opponentCard2 = Instantiate(cardImgBack, opponentCardPos[1].position, Quaternion.identity) as GameObject;

        // change states
        state = HandState.Flop;

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();

    }

    private void DealFlop()
    {
        tableCards[0] = deck[4];
        Instantiate(cardImg[deck[4]], tableCardPos[0].position, Quaternion.identity);

        tableCards[1] = deck[5];
        Instantiate(cardImg[deck[5]], tableCardPos[1].position, Quaternion.identity);

        tableCards[2] = deck[6];
        Instantiate(cardImg[deck[6]], tableCardPos[2].position, Quaternion.identity);

        // change states
        state = HandState.Turn;

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();
    }

    private void DealTurn()
    {
        tableCards[3] = deck[7];
        Instantiate(cardImg[deck[7]], tableCardPos[3].position, Quaternion.identity);

        // change states
        state = HandState.River;

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();
    }

    private void DealRiver()
    {
        tableCards[4] = deck[8];
        Instantiate(cardImg[deck[8]], tableCardPos[4].position, Quaternion.identity);

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();

        // switch to Reveal state
        state = HandState.Reveal;
    }

    public void RevealCards()
    {
        // get rid of back of opponent cards
        Destroy(opponentCard1);
        Destroy(opponentCard2);

        // show opponent cards
        opponentCard1 = Instantiate(cardImg[opponentCards[0]], opponentCardPos[0].position, Quaternion.identity);
        opponentCard2 = Instantiate(cardImg[opponentCards[1]], opponentCardPos[1].position, Quaternion.identity);
    }

    public HandState State()
    {
        return state;
    }

    public void SetState(HandState newState)
    {
        state = newState;
    }
}
