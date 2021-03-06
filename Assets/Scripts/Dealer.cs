using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum HandState { Ante, Begin, Flop, Turn, River, Reveal}
public class Dealer : MonoBehaviour
{
    HandState state;

    [SerializeField] WinnerFinder winnerFinder;
    [SerializeField] PotTracker potTracker;

    [SerializeField] GameObject[] cardImg;
    [SerializeField] GameObject cardImgBack = null;

    [SerializeField] Transform[] playerCardPos;
    [SerializeField] Transform[] opponentCardPos;
    [SerializeField] Transform[] tableCardPos;

    private int[] deck = Enumerable.Range(0, 52).ToArray();
    private int[] playerCards = new int[2];
    private GameObject[] playerHand = new GameObject[7];
    private int[] opponentCards = new int[2];
    private GameObject[] opponentHand = new GameObject[7];
    private int[] tableCards = new int[5];

    // 2 for opponent win, 1 for player win, 0 for draw
    private int handWinner;

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

    private void FinalizeHands()
    {
        // load up player hand
        playerHand[0] = cardImg[playerCards[0]];
        playerHand[1] = cardImg[playerCards[1]];
        playerHand[2] = cardImg[tableCards[0]];
        playerHand[3] = cardImg[tableCards[1]];
        playerHand[4] = cardImg[tableCards[2]];
        playerHand[5] = cardImg[tableCards[3]];
        playerHand[6] = cardImg[tableCards[4]];

        // load up opponent hand
        opponentHand[0] = cardImg[opponentCards[0]];
        opponentHand[1] = cardImg[opponentCards[1]];
        opponentHand[2] = cardImg[tableCards[0]];
        opponentHand[3] = cardImg[tableCards[1]];
        opponentHand[4] = cardImg[tableCards[2]];
        opponentHand[5] = cardImg[tableCards[3]];
        opponentHand[6] = cardImg[tableCards[4]];

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

        // load up hands to check who winner is
        FinalizeHands();

        // check who winner is
        CheckHandWinner();
        
        // switch to Reveal state
        state = HandState.Reveal;
    }

    private void CheckHandWinner()
    {
        handWinner = winnerFinder.DetermineHandWinner(playerHand, opponentHand);
    }

    public void DishOutWinnings()
    {
        if (handWinner == 2) { potTracker.OpponentWinsPot(); }
        else if (handWinner == 1) { potTracker.PlayerWinsPot(); }
        else { potTracker.SplitPot(); }
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
