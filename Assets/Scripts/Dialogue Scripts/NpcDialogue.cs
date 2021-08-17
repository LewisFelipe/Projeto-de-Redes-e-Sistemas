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
    private int canBuyCount;
    public static bool isShopping = false;
    public GameObject lunarStoneButton, generateWeaponButton;
    private float tweenTime = .55f;
    public LeanTweenType leanType;

    private void Start()
    {
        openShopButton.SetActive(false);
        isFinishDialogue = false;
        lunarStoneButton.SetActive(true);
        generateWeaponButton.SetActive(false);
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
            isShopping = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if(player.gameObject.tag == "Player")
        {        
            LeanTween.scale(dialoguePanel, new Vector3(0, 0, 0), tweenTime).setEaseInBack();
            dialoguePanel.transform.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo();
            //dialoguePanel.SetActive(false);
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
        shopTab.transform.LeanMoveLocalY(0f, 0.5f).setEaseOutExpo().delay = 0.1f;
        shopTab.SetActive(true);
        dialoguePanel.SetActive(false);
        shopping = true;
        isShopping = true;
    }

    public void CloseShop()
    {
        shopTab.transform.LeanMoveLocalY(1920, 0.5f).setEaseInExpo();
        StartCoroutine(UICooldown(shopTab));
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
            //canBuy = true;    
            canBuyCount++;
            lunarStoneButton.SetActive(false);
            generateWeaponButton.SetActive(true);
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
        if(canBuyCount > 0)
        {
            
            GameObject[] weapon = GameObject.FindGameObjectsWithTag("Weapon"); //Encontra a arma atual
            
            /*
            if(weapon.activeSelf) //Desativa arma atual para poder trocar
            {
                weapon.SetActive(false);
            }
            */
            foreach(GameObject usedWeapons in weapon)
            {
                usedWeapons.SetActive(false);
            }  

            GenerateRandomInt();
            if(randomIndex <= 90) //Itens normais
            {   
                int randomNumber = Random.Range(0, basicItensList.Length);
                basicItensList[randomNumber].SetActive(true);
                if(basicItensList[randomNumber].activeSelf == false)
                {
                    basicItensList[randomNumber].SetActive(true);
                }
            }
            else if(randomIndex <= 99 && randomIndex > 90) //Itens bons
            {
                Debug.Log("Item bom");
            }
            else if(randomIndex == 100) //Itens lendários
            {
                    Debug.Log("Item lendário");
            }

            canBuyCount--;
            lunarStoneButton.SetActive(true);
            generateWeaponButton.SetActive(false);
            
        }

    }

    private void GenerateRandomInt()
    {
        randomIndex = Random.Range(0, 100);
    }

    private IEnumerator UICooldown(GameObject panelUI)
    {
        yield return new WaitForSeconds(1f);
        panelUI.SetActive(false);
    }
}
