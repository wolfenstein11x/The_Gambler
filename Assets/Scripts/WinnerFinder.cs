using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
straight flush = 9
quads = 8
full house = 7
flush = 6
straight = 5
trips = 4
two pair = 3
pair = 2
high card = 1
*/



public class WinnerFinder : MonoBehaviour
{

    // all possible straight combinations 
    private char[,] straightCombos = new char[,] { {'a','2','3','4','5'}, {'2','3','4','5','6'}, {'3','4','5','6','7'},
                                          {'4','5','6','7','8' },{'5','6','7','8','9'}, {'6','7','8','9','t'},
                                          {'7','8','9','t','j' },{'8','9','t','j','q'}, {'9','t','j','q','k'},
                                          {'t','j','q','k','a' } };



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public int CheckHand(GameObject[] hand)
    {
        int pairScore = CheckForPairs(hand);
        bool flush = CheckForFlush(hand);
        bool straight = CheckAllStraights(hand);

        // full house/quads
        if (pairScore > 6) { return pairScore; }

        // flush
        else if (flush) { return 6; }

        // straight
        else if (straight) { return 5; }

        // trips, pairs, high card
        else { return pairScore; }
    }

    private int CheckForPairs(GameObject[] hand)
    {
        // initialize pair counts to zero
        int numPairs = 0;
        bool trips = false;
        bool fullHouse = false;
        bool quads = false;

        foreach (GameObject card in hand)
        {
            // count matches in letter at idx 0 (which is rank)
            int dups = CountDups(card.name[0], 0, hand);

            if (dups == 3) { quads = true; }
            else if (dups == 2) { trips = true; }
            else if (dups == 1) { numPairs++; }

        }

        // pairs are double counted, so divide by 2
        numPairs /= 2;

        // check for full house
        if (numPairs >= 1 && trips) { fullHouse = true; }

        if (quads) { return 8; }
        else if (fullHouse) { return 7; }
        else if (trips) { return 4; }
        else if (numPairs == 2) { return 3; }
        else if (numPairs == 1) { return 2; }
        else { return 1; }

    }

    private bool CheckForFlush(GameObject[] hand)
    {
        foreach (GameObject card in hand)
        {
            // count matches in letter at idx 1 (which is suit)
            int suitDups = CountDups(card.name[1], 1, hand);

            // count 5 of the same suit, exit function, you have flush
            if (suitDups >= 4) { return true; }
        }

        // no flush if never counted 5 of same suit
        return false;
    }


    
    private bool CheckAllStraights(GameObject[] hand)
    {
        // loop through all 10 straight combos
        for (int i=0; i < 10; i++)
        {
            bool straightFound = CheckStraight(straightCombos[i, 0], straightCombos[i, 1], straightCombos[i, 2],
                                               straightCombos[i, 3], straightCombos[i, 4], hand);
            
            // exit the function and return true if you find a straight
            if (straightFound) { return true; }
        }

        // if you get to this point, there is no straight
        return false;
    }


    // helper function to check for a particular straight combo
    private bool CheckStraight(char r1, char r2, char r3, char r4, char r5, GameObject[] hand)
    {
        // take input chars and put the into array
        char[] straightArr = new char[] { r1, r2, r3, r4, r5 };

        // take first letters (ranks) from player hand and put into array
        char[] handRanksArr = new char[] {hand[0].name[0], hand[1].name[0], hand[2].name[0], hand[3].name[0],
                                          hand[4].name[0], hand[5].name[0], hand[6].name[0]};

        // check if player hand contains straight combo
        if (IsSubset(handRanksArr, straightArr, 5, 7)) { return true; }
        else { return false; }
    }

    // helper function to check if a 7 card array contains a certain 5 card array
    private bool IsSubset(char[] arr1, char[] arr2, int arr2Size, int arr1Size)
    {
        int i = 0;
        int j = 0;

        for (i=0; i<arr2Size; i++)
        {
            for (j=0; j<arr1Size; j++)
            {
                if (arr2[i] == arr1[j]) { break; }
            }

            // if the above loop goes once through without breaking, then its false
            if (j == arr1Size) { return false; }
        }

        // if we make it here, then arr2 is subset of arr1
        return true;
    }

    private void HandleOverTwoPairs()
    {
        // find 2 highest pairs
    }

    private int CountDups(char letter, int idx, GameObject[] hand)
    {   
        // start dup count at -1, since first dup counted will just be the original
        int dups = -1;

        // loop through cards and count dups
        foreach(GameObject card in hand)
        {
            if (card.name[idx].Equals(letter)) { dups += 1; }
        }

        return dups;
    }

    private int GetRankInt(char rankChar)
    {
        var ranks = new Dictionary<char, int>()
        {
            {'2',2},{'3',3},{'4',4},{'5',5},{'6',6},{'7',7},{'8',8},{'9',9},{'t',10},{'j',11},{'q',12},{'k',13},{'a',14}
        };

        return ranks[rankChar];
    }

    public int DetermineHighCard(GameObject[] hand)
    {
        int currentRank = 0;
        int highestRank = 0;

        foreach (GameObject card in hand)
        {
            // take first letter of card (which is rank) and convert it to integer
            currentRank = GetRankInt(card.name[0]);

            if (currentRank > highestRank) { highestRank = currentRank; }
        }

        return highestRank;
    }

    public int DetermineHandWinner(GameObject[] playerHand, GameObject[] opponentHand)
    {
        // return 2 for opponent win, 1 for player win, zero for draw

        int playerScore = CheckHand(playerHand);
        int opponentScore = CheckHand(opponentHand);

        if (playerScore > opponentScore) { return 1; }
        else if (playerScore < opponentScore) { return 2; }
        
        // case where both have trips, or both have straight, etc.
        else
        {
            // TODO check high cards, high pairs etc.

            return 0;
        }
    }

    
}
