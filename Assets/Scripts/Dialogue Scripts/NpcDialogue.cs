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
    private bool shopping;
    public GameObject shopTab;
    public TMP_Text stonesUsedText;
    private int stonesCount;
    [SerializeField] private GameObject[] basicItensList;
    private int randomIndex;
    private bool canBuy;
    public static bool isShopping = false;

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
        if(player.gameObject.tag == "Player" && shopping == false)
        {
            dialoguePanel.SetActive(true);
            isShopping = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            dialoguePanel.SetActive(false);
            openShopButton.SetActive(false);
            textNumber = 0;
            isFinishDialogue = false;
            isShopping = false;
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
        shopTab.SetActive(true);
        dialoguePanel.SetActive(false);
        shopping = true;
        isShopping = true;
    }

    public void CloseShop()
    {
        shopTab.SetActive(false);
        shopping = false;
        isShopping = false;
    }

    public void BuyItem()
    {
        if(LunarStone.lunarStones > 0)
        {
            LunarStone.lunarStones--;
            stonesCount++;
            stonesUsedText.text = stonesCount.ToString();
            canBuy = true;       
        }
    }

    public void GetRandomWeapon()
    {
        if(LunarStone.lunarStones > 0)
        {
            foreach(GameObject basicWeapons in basicItensList)
            {
                basicWeapons.SetActive(false);
            }      
        }
        
        //Sistema de Gacha (loja aleatória)
        if(canBuy == true)
        {
            GameObject weapon = GameObject.FindGameObjectWithTag("Weapon"); //Encontra a arma atual
            if(weapon.activeSelf) //Desativa arma atual para poder trocar
            {
                weapon.SetActive(false);
            }

            GenerateRandomInt();
            if(randomIndex <= 90) //Itens normais
            {   
                int randomNumber = Random.Range(0, basicItensList.Length);
                basicItensList[randomNumber].SetActive(true);
                canBuy = false; 
            }
            else if(randomIndex <= 99 && randomIndex > 90) //Itens bons
            {
                Debug.Log("Item bom");
            }
            else if(randomIndex == 100) //Itens lendários
            {
                Debug.Log("Item lendário");
            }
        }

    }

    private void GenerateRandomInt()
    {
        randomIndex = Random.Range(0, 100);
    }
}
