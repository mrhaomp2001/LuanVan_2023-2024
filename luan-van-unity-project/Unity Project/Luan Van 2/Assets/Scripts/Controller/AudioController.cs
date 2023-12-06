using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
        [Range(0.3f, 3f)]
        public float pitch;

        public bool isLoop;

        [HideInInspector]
        public AudioSource audioSource;
    }

    [SerializeField] private AudioMixerGroup audioMixer;
    [SerializeField] private AudioMixer audioMixerVolume;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Sound[] sounds;

    [SerializeField] public static AudioController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        else
        {
            Instance = this;
        }

        foreach (var sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.isLoop;
            sound.audioSource.outputAudioMixerGroup = audioMixer;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            audioMixerVolume.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
            audioSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null) return;
        sound.audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioMixerVolume.SetFloat("volume", volume);
        if (volume <= -20)
        {
            audioMixerVolume.SetFloat("volume", -80f);
        }

        PlayerPrefs.SetFloat("volume", volume);
    }
}
