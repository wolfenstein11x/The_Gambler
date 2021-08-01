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
    // straight flush combos
    int[,] straightFlush = new int[,] { { 0, 4, 8, 12, 16 }, { 4, 8, 12, 116, 20 }, { 8, 12, 16, 20, 24 }, {12,16,20,24,28 },
                                        {16,20,24,28,32 }
    };

    [SerializeField] GameObject[] playerHand;

    [SerializeField] int[] arr7;
    [SerializeField] int[] arr5;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(CheckForPairs(playerHand));
            Debug.Log(CheckForFlush(playerHand));
        }
    }

    private int CheckForPairs(GameObject[] hand)
    {
        // initialize pair counts to zero
        int numPairs = 0;
        bool trips = false;
        bool fullHouse = false;
        bool quads = false;

        foreach(GameObject card in hand)
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
        if (numPairs >= 1 && trips){ fullHouse = true; }

        if (quads) { return 8; }
        else if (fullHouse) { return 7; }
        else if (trips) { return 4; }
        else if (numPairs == 2) { return 3; }
        else if (numPairs == 1) { return 2; }
        else { return 1; }

    }

    private bool CheckForFlush(GameObject[] hand)
    {
        foreach(GameObject card in hand)
        {
            // count matches in letter at idx 1 (which is suit)
            int suitDups = CountDups(card.name[1], 1, hand);
            
            // count 5 of the same suit, exit function, you have flush
            if (suitDups >= 4) { return true; }
        }

        // no flush if never counted 5 of same suit
        return false;
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

   
}
