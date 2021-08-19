using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public string[] dialogueString;  
    private int textNumber = 0;
    private float tweenTime = .55f;
    private bool isTalking;
    
    private void Start()
    {
        UpdateDialogue();
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            LeanTween.cancel(dialoguePanel);
            transform.localScale = Vector3.one;
            LeanTween.scale(dialoguePanel, Vector3.one * 1, tweenTime).setEaseOutExpo();
            dialoguePanel.transform.LeanMoveLocalY(-400f, 0.5f).setEaseOutExpo().delay = 0.1f;
        }
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            dialoguePanel.SetActive(true);
            NpcDialogue.isShopping = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            LeanTween.scale(dialoguePanel, new Vector3(0, 0, 0), tweenTime).setEaseInBack();
            dialoguePanel.transform.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
            textNumber = 0;
            NpcDialogue.isShopping = false;
        }
    }

    private void UpdateDialogue()
    {
        dialogueText.text = dialogueString[textNumber];
    }

    public void NextText()
    {
        textNumber++;
        if(textNumber >= dialogueString.Length)
        {
            textNumber = 0;
        }
        UpdateDialogue();
    }        
    
}
