using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beaner : MonoBehaviour, Interactables
{
    [SerializeField] GameObject dialogueBox = null;
    [SerializeField] Text dialogueText;
    [SerializeField] Sprite sprite;
    [SerializeField] string[] linesSet1;
    [SerializeField] string[] linesSet2;
    [SerializeField] int lettersPerSecond = 45;

    private bool isTyping = false;
    private int currentLine = 0;
    private DialoguePic dialoguePic;
    private Image image;

    private void Start()
    {
        dialoguePic = dialogueBox.GetComponentInChildren<DialoguePic>();
        image = dialoguePic.GetComponent<Image>();


    }

    private IEnumerator ShowDialogue1()
    {
        yield return new WaitForEndOfFrame();

        image.sprite = sprite;

        if (currentLine < linesSet1.Length)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(TypeLine(linesSet1[currentLine]));
            currentLine++;
        }

        else
        {
            currentLine = 0;
            dialogueBox.SetActive(false);
        }

    }

    private IEnumerator ShowDialogue2()
    {
        yield return new WaitForEndOfFrame();

        image.sprite = sprite;

        if (currentLine < linesSet2.Length)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(TypeLine(linesSet2[currentLine]));
            currentLine++;
        }

        else
        {
            currentLine = 0;
            dialogueBox.SetActive(false);

            // dialogue is complete, so go to win menu 
            RollCredits();
        }

    }


    private IEnumerator TypeLine(string line)
    {
        isTyping = true;

        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }

    public void Interact()
    {
        if (isTyping) { return; }

        if (PlayerHasTheMoney())
        {
            StartCoroutine(ShowDialogue2());
        }

        else
        {
            StartCoroutine(ShowDialogue1());
        }
        
    }

    private bool PlayerHasTheMoney()
    {
        return (PlayerData.playerTotalMoney >= 100f);
    }

    private void RollCredits()
    {
        FindObjectOfType<SceneLoader>().LoadMatchScene("WinScene");
    }
}
