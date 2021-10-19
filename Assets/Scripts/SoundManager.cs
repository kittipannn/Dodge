using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;
    public static SoundManager SoundInstance;
    public GameObject muteIcon, unMuteIcon;
    private bool muted = false;
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
        if (SoundInstance == null)
            SoundInstance = this;
        else
            Destroy(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadValueSoundControl();
        }
        else
        {
            LoadValueSoundControl();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
        Play("BGM");
    }
    private void Update()
    {
        UpdateButtonIcon();
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " Not found!");
            return;
        }
        s.source.Play();
    }

    public void OnButtonSoundControl()
    {
        if (!muted)
        {
            muted = true;
            AudioListener.pause = true;
            Debug.Log("Mute");
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            Debug.Log("UnMute");
        }
        SaveValueSoundControl();
        UpdateButtonIcon();
    }
    private void LoadValueSoundControl()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void SaveValueSoundControl()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    public void UpdateButtonIcon() //ถ้ากดปุ่มจะเปลี่ยน icon
    {
        if (!muted)
        {
            unMuteIcon.SetActive(false);
            muteIcon.SetActive(true);
        }
        else
        {
            muteIcon.SetActive(false);
            unMuteIcon.SetActive(true);
        }
    }
}
