using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayer : MonoBehaviour
{
    public GameObject anteButton = null;
    public GameObject dealButton = null;
    public GameObject betButton = null;
    public GameObject checkButton = null;
    public GameObject revealButton = null;
    public GameObject betInputField = null;

    private Dealer dealer;

    // Start is called before the first frame update
    void Start()
    {
        dealer = FindObjectOfType<Dealer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private HandState GetState()
    {
        return dealer.State();
    }

    public void DisplayButton(GameObject button)
    {
        button.gameObject.SetActive(true);
    }

    public void HideButton(GameObject button)
    {
        button.gameObject.SetActive(false);
    }

    public void ShowAnteButtonOnly()
    {
        DisplayButton(anteButton);

        HideButton(checkButton);
        HideButton(betButton);
        HideButton(betInputField);
        HideButton(dealButton);
        HideButton(revealButton);
    }

    public void ShowDealButtonOnly()
    {
        DisplayButton(dealButton);
        
        HideButton(checkButton);
        HideButton(betButton);
        HideButton(betInputField);
        HideButton(anteButton);
        HideButton(revealButton);
    }

    public void ShowBetButtonsOnly()
    {
        DisplayButton(checkButton);
        DisplayButton(betButton);
        DisplayButton(betInputField);

        HideButton(dealButton);
        HideButton(anteButton);
        HideButton(revealButton);
    }

    public void ShowRevealButtonOnly()
    {
        DisplayButton(revealButton);

        HideButton(dealButton);
        HideButton(checkButton);
        HideButton(betButton);
        HideButton(betInputField);
        HideButton(anteButton);
    }
}
