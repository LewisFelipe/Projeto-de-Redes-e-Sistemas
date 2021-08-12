using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{

    public string[] dialogueString;
    private int textNumber = 0;
    public TMP_Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject openShopButton;
    private bool isFinishDialogue;
    public bool isSeller;

    private void Start()
    {
        openShopButton.SetActive(false);
        isFinishDialogue = false;
    }
    
    private void Update()
    {
        UpdateDialogue();
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player")
        dialoguePanel.SetActive(true);
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            dialoguePanel.SetActive(false);
            openShopButton.SetActive(false);
            textNumber = 0;
            isFinishDialogue = false;
        }
    }

    private void UpdateDialogue()
    {
        dialogueText.text = dialogueString[textNumber];
        if(isFinishDialogue == true)
        {
            if(isSeller == true)
            openShopButton.SetActive(true);
        }
    }

    public void NextText()
    {
        textNumber++;
        if(textNumber >= dialogueString.Length)
        {
            textNumber = 0;
            isFinishDialogue = true;
        }
    }
}
