using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour, Interactables
{
    [SerializeField] GameObject dialogueBox = null;
    [SerializeField] Text dialogueText;
    [SerializeField] Sprite sprite;
    [SerializeField] string[] lines;
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

    private IEnumerator ShowDialogue()
    {
        yield return new WaitForEndOfFrame();

        image.sprite = sprite;

        if (currentLine < lines.Length)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(TypeLine(lines[currentLine]));
            currentLine++;
        }

        else
        {
            currentLine = 0;
            dialogueBox.SetActive(false);
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

        StartCoroutine(ShowDialogue());
    }
}
