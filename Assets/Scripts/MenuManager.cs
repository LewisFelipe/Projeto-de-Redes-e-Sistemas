using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{   
    //Ainda falta salvar com player prefs
    Resolution[] resolutions;
    public Dropdown resolutionsDropdown;
    public GameObject optionsPanel;
    LTRect opTionsPanelRectTransform = new LTRect();
    public Image muteMusic, muteSound;
    public Sprite musicOn, musicOff, soundOn, soundOff;
    public Slider musicSlider, soundSlider;
    public AudioSource musicEmitter, soundEmitter;

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
        LeanTween.move(optionsPanel, new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2), 0.5f);
    }

    public void OptionsButtonExit()
    {
        LeanTween.move(optionsPanel, new Vector2(Screen.currentResolution.width / 2, -Screen.currentResolution.height), 0.5f);
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

    private void Start()
    {
        StartResolutions();

        //opTionsPanelRectTransform = optionsPanel.GetComponent<RectTransform>();
        //optionsPanel.SetActive(false);
    }
}
