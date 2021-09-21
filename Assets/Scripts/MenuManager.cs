using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    //Ainda falta salvar com player prefs
    public GameObject optionsPanel;
    public Image muteMusic, muteSound;
    public Sprite musicOn, musicOff, soundOn, soundOff;
    public Slider musicSlider, soundSlider;
    public AudioSource musicEmitter, soundEmitter;

    public void OptionsButton()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void OptionsButtonExit()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
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
        optionsPanel.SetActive(false);
    }
}
