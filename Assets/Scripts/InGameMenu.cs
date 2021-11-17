using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MenuManager
{
    public static bool Paused {get; private set;} = false;
    public Text score;
    public Text nick;

    public void PausedOptionsButton()
    {
        StartResolutions();
        optionsPanel.transform.position = gameObject.transform.position;
    }

    public void PausedOptionsButtonExit()
    {
        optionsPanel.transform.position = gameObject.transform.position + new Vector3(0, (-2 * gameObject.transform.position.y), 0);
    }

    private void PauseGame()
    {
        //LeanTween.move(loginPanel, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.5f);
        loginPanel.transform.position = gameObject.transform.position;
        Time.timeScale = 0f;
        Paused = true;
    }

    public void ContinueGame()
    {
        //LeanTween.move(loginPanel, new Vector2(gameObject.transform.position.x, 4 * gameObject.transform.position.y), 0.5f);
        Time.timeScale = 1f;
        PausedOptionsButtonExit();
        loginPanel.transform.position = gameObject.transform.position + new Vector3(0f, (4 * gameObject.transform.position.y), 0f);
        Paused = false;
    }

    public void BackToMenu()
    {
        if(Paused)
        {
            ContinueGame();
        }
        SceneManager.LoadScene("Menu");
    }

    private void Start()
    {
        nick.text = Login.nick;
    }

    private void Update()
    {
        switch(PlayerInputManager.pauseChanged)
        {
            case -1:
                if(Paused)
                {
                    ContinueGame();
                }
                else
                {
                    PauseGame();
                }

                PlayerInputManager.pauseChanged = 0;
                break;

            default:
                score.text = ScoreManager.score.ToString();
                break;
        }
    }
}
