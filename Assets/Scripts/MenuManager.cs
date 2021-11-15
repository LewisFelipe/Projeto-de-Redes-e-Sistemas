using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Realms;

public class MenuManager : MonoBehaviour
{   
    //Ainda falta salvar com player prefs
    public Dropdown resolutionsDropdown;
    public GameObject optionsPanel, loginPanel;
    public GameObject[] informations;
    public Image muteMusic, muteSound;
    public Sprite musicOn, musicOff, soundOn, soundOff;
    public Slider musicSlider, soundSlider;
    public AudioSource musicEmitter, soundEmitter;
    public Text nick, password;
    public static bool isLogged {get; private set;} = false;

    Realm realm;
    RankingModel rankingModel;
    Resolution[] resolutions;
    LTRect optionsPanelRectTransform = new LTRect();

    private void StartResolutions()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;
        List<string> resolutionsList = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsList.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionsDropdown.ClearOptions();
        resolutionsDropdown.AddOptions(resolutionsList);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void OptionsButton()
    {
        //optionsPanel.SetActive(!optionsPanel.activeSelf);
        LeanTween.move(optionsPanel, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.5f);
    }

    public void OptionsButtonExit()
    {
        LeanTween.move(optionsPanel, new Vector2(gameObject.transform.position.x, -2 * gameObject.transform.position.y), 0.5f);
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen); 
    }

    public void SetFullScreen(bool isOn)
    {
        Screen.fullScreen = isOn;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVSync(bool isOn)
    {
        QualitySettings.vSyncCount = Convert.ToInt32(isOn);
    }

    private void ChangeMuteUI(AudioSource source, Image UIButton, Sprite on, Sprite off)
    {
        if(source.mute || source.volume <= 0)
        {
            UIButton.sprite = off;
        }
        else
        {
            UIButton.sprite = on;
        }
    }

    public void MuteMusic()
    {
        musicEmitter.mute = !musicEmitter.mute;

        ChangeMuteUI(musicEmitter, muteMusic, musicOn, musicOff);
    }

    public void MusicSlider()
    {
        musicEmitter.volume = musicSlider.value;

        ChangeMuteUI(musicEmitter, muteMusic, musicOn, musicOff);
    }

    public void MuteSound()
    {
        soundEmitter.mute = !soundEmitter.mute;

        ChangeMuteUI(soundEmitter, muteSound, soundOn, soundOff);
    }

    public void SoundSlider()
    {
        soundEmitter.volume = soundSlider.value;

        ChangeMuteUI(soundEmitter, muteSound, soundOn, soundOff);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    IEnumerator InformationManager()
    {
        foreach (GameObject page in informations)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            page.SetActive(true);
            //AudioManager.Singleton.PlaySoundEffect(0);
        }
        foreach (GameObject page in informations)
        {
            page.SetActive(false);
        }
    }

    public void InfoButton()
    {
        StartCoroutine("InformationManager");
    }

    public void LoginButtonExit()
    {
        LeanTween.move(loginPanel, new Vector2(gameObject.transform.position.x, 4 * gameObject.transform.position.y), 0.5f);
    }

    public void StartGame()
    {
        if(!isLogged)
        {
            LeanTween.move(loginPanel, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), 0.5f);
        }
    }

    public void Login()
    {
        /*if(password.text != "" && )
        {

        }
        realm = Realm.GetInstance();
        rankingModel = realm.Find<RankingModel>(nick.text);
        if(rankingModel == null)
        {
            realm.Write(() => { rankingModel = realm.Add(new RankingModel(nick.text, password.text, -1, 0)); });
        }*/
    }

    private void Start()
    {
        StartResolutions();
    }
}