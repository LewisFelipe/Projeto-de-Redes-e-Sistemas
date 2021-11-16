using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialTab;
    public string[] tutoString;
    public TMP_Text tutoText;
    public Sprite[] tutoSprites;
    public Image tutoImage;
    public Texture2D gameCursor;

    private int textNumber;
    private ScoreManager scoreManager;

    void Start()
    {
        tutorialTab.SetActive(true);
        NpcDialogue.isShopping = true;
        Cursor.SetCursor(gameCursor, Vector2.zero, CursorMode.ForceSoftware);
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        UpdateDialogue();

        if(Input.GetKeyDown(KeyCode.LeftArrow) && tutorialTab.activeSelf)
        {
            textNumber--;
            if(textNumber < 0)
            {
                textNumber = 0;
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && tutorialTab.activeSelf)
        {
            textNumber++;
            if(textNumber >= tutoString.Length)
            {
                textNumber = 0;
            }
        }
    }

    private void UpdateDialogue()
    {
        tutoText.text = tutoString[textNumber];
        tutoImage.sprite = tutoSprites[textNumber];
    }

    public void NextText()
    {
        textNumber++;
        if(textNumber >= tutoString.Length)
        {
            textNumber = 0;
        }
    }

    public void PreviousText()
    {
        textNumber--;
        if(textNumber < 0)
        {
            textNumber = 0;
        }
    }

    public void CloseTutorial()
    {
        tutorialTab.SetActive(false);
        NpcDialogue.isShopping = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreManager.changeHighestScore();
        ScoreManager.runTime = 0;
        ScoreManager.score = 0;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
