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
    public GameObject callButton = null;
    public GameObject foldButton = null;
    public GameObject raiseButton = null;
    public GameObject betInputField = null;
    [SerializeField] GameObject winMenu = null;
    [SerializeField] GameObject loseMenu = null;
    [SerializeField] Text callAmount;
    [SerializeField] OpponentPoker opponentPoker;

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
        HideAllButtons();

        DisplayButton(anteButton);
    }

    public void ShowDealButtonOnly()
    {
        HideAllButtons();

        DisplayButton(dealButton);
    }

    public void ShowBetButtonsOnly()
    {
        HideAllButtons();

        DisplayButton(checkButton);
        DisplayButton(betButton);
        DisplayButton(betInputField);
    }

    public void ShowRevealButtonOnly()
    {
        HideAllButtons();

        DisplayButton(revealButton);
    }

    public void ShowCallFoldButtonsOnly()
    {
        HideAllButtons();

        DisplayButton(callButton);
        UpdateCallAmountText(opponentPoker.betAmount);
        
        DisplayButton(foldButton);
        DisplayButton(raiseButton);
        DisplayButton(betInputField);
    }

    private void UpdateCallAmountText(float amount)
    {
        callAmount.text = "$" + amount.ToString() + " to call...";
    }

    public void ShowWinMenuOnly()
    {
        HideAllButtons();

        DisplayButton(winMenu);
    }

    public void ShowLoseMenuOnly()
    {
        HideAllButtons();

        DisplayButton(loseMenu);
    }

    public void HideAllButtons()
    {
        HideButton(dealButton);
        HideButton(checkButton);
        HideButton(betButton);
        HideButton(betInputField);
        HideButton(anteButton);
        HideButton(revealButton);
        HideButton(foldButton);
        HideButton(callButton);
        HideButton(raiseButton);
        HideButton(winMenu);
        HideButton(loseMenu);
    }

    
    
}
