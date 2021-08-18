using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcWizard : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject openShopButton;
    public TMP_Text dialogueText;
    public TMP_Text potionsCountText;
    public GameObject shopTab;
    public GameObject potionButton, maxLifeButton;
    public GameObject buyButton, buyMaxLifeButton;
    public string[] dialogueString;
    private bool isFinishDialogue;
    public bool isSeller;
    private bool shopping;
    private float tweenTime = .55f;
    private int textNumber = 0;
    private int canBuyCount;
    public static int potionsCount;

    
    private void Start()
    {
        openShopButton.SetActive(false);
        isFinishDialogue = false;
        buyButton.SetActive(false);
        maxLifeButton.SetActive(false);
    }

    private void Update()
    {
        UpdateDialogue();
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Player" && shopping == false)
        {
            LeanTween.cancel(dialoguePanel);
            transform.localScale = Vector3.one;
            LeanTween.scale(dialoguePanel, Vector3.one * 1, tweenTime).setEaseOutExpo();
            dialoguePanel.transform.LeanMoveLocalY(-400f, 0.5f).setEaseOutExpo().delay = 0.1f;
        }              
    }

    private void OnTriggerStay(Collider player)
    {
        if(player.gameObject.tag == "Player" && shopping == false)
        {
            dialoguePanel.SetActive(true);
            NpcDialogue.isShopping = true;
            potionsCountText.text = potionsCount.ToString();
        }        
    }  

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {        
            LeanTween.scale(dialoguePanel, new Vector3(0, 0, 0), tweenTime).setEaseInBack();
            dialoguePanel.transform.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
            openShopButton.SetActive(false);
            textNumber = 0;
            isFinishDialogue = false;
            NpcDialogue.isShopping = false;
            CloseShop();
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

    public void OpenShop()
    {
        shopTab.transform.LeanMoveLocalY(0f, 0.5f).setEaseOutExpo().delay = 0.1f;
        shopTab.SetActive(true);
        dialoguePanel.SetActive(false);
        shopping = true;
        NpcDialogue.isShopping = true;
    }

    public void CloseShop()
    {
        shopTab.transform.LeanMoveLocalY(1920, 0.5f).setEaseInExpo();
        StartCoroutine(UICooldown(shopTab));
        shopping = false;
        NpcDialogue.isShopping = false;
    }

    public void BuyPotion()
    {
        if(HealthFlower.healthFlower > 0)
        {
            HealthFlower.healthFlower--;
            canBuyCount++;
            potionButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }
    
    public void GeneratePotion()
    {
        if(canBuyCount > 0)
        {
            potionsCount++;
            potionsCountText.text = potionsCount.ToString();
            canBuyCount--;
            potionButton.SetActive(true);
            buyButton.SetActive(false);
        }
    }

    public void BuyMaxLifePotion()
    {
        if(HealthFlower.healthFlower > 5)
        {
            HealthFlower.healthFlower -= 5;
            maxLifeButton.SetActive(true);
            buyMaxLifeButton.SetActive(false); 
        }       
    }

    public void GenerateMaxLifePotion()
    {
        PlayerHealth.changeMaxLife = true;
        maxLifeButton.SetActive(false);
        buyMaxLifeButton.SetActive(true);        
    }

    private IEnumerator UICooldown(GameObject panelUI)
    {
        yield return new WaitForSeconds(1f);
        panelUI.SetActive(false);
    }    

}
