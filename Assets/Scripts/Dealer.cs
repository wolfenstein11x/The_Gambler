using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum HandState { Ante, Begin, Flop, Turn, River, Reveal}
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
    private GameObject playerCard1, playerCard2;
    private GameObject flopCard1, flopCard2, flopCard3, turnCard, riverCard;

    private ButtonDisplayer buttonDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        buttonDisplayer = FindObjectOfType<ButtonDisplayer>();

        NewHand();

    }

    

    private void NewHand()
    {
        DestroyCards();
        state = HandState.Ante;
        Shuffle();
        buttonDisplayer.ShowAnteButtonOnly();

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
        else if (state == HandState.Reveal) { NewHand(); }

    }

    private void DealHands()
    {
        // deal player cards
        playerCards[0] = deck[0];
        playerCards[1] = deck[1];

        // show player cards
        playerCard1 = Instantiate(cardImg[playerCards[0]], playerCardPos[0].position, Quaternion.identity) as GameObject;
        playerCard2 = Instantiate(cardImg[playerCards[1]], playerCardPos[1].position, Quaternion.identity) as GameObject;

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
        flopCard1 = Instantiate(cardImg[deck[4]], tableCardPos[0].position, Quaternion.identity) as GameObject;

        tableCards[1] = deck[5];
        flopCard2 = Instantiate(cardImg[deck[5]], tableCardPos[1].position, Quaternion.identity) as GameObject;

        tableCards[2] = deck[6];
        flopCard3 = Instantiate(cardImg[deck[6]], tableCardPos[2].position, Quaternion.identity) as GameObject;

        // change states
        state = HandState.Turn;

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();
    }

    private void DealTurn()
    {
        tableCards[3] = deck[7];
        turnCard = Instantiate(cardImg[deck[7]], tableCardPos[3].position, Quaternion.identity) as GameObject;

        // change states
        state = HandState.River;

        // show bet buttons
        buttonDisplayer.ShowBetButtonsOnly();
    }

    private void DealRiver()
    {
        tableCards[4] = deck[8];
        riverCard = Instantiate(cardImg[deck[8]], tableCardPos[4].position, Quaternion.identity);

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

        // show deal button to start new hand
        buttonDisplayer.ShowDealButtonOnly();
    }

    public HandState State()
    {
        return state;
    }

    public void SetState(HandState newState)
    {
        state = newState;
    }

    private void DestroyCards()
    {
        Destroy(playerCard1);
        Destroy(playerCard2);
        Destroy(opponentCard1);
        Destroy(opponentCard2);
        Destroy(flopCard1);
        Destroy(flopCard2);
        Destroy(flopCard3);
        Destroy(turnCard);
        Destroy(riverCard);
    }
}
