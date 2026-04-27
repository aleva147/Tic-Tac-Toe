// This script is attached to AudioManager and is responsible for turning music and sound on/off in all scenes.

using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings : Singleton<AudioSettings>
{
    [SerializeField]
    private AudioMixer mixer;
    private bool musicOn = true;
    private bool sfxOn = true;

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        mixer.SetFloat("MusicVolume", musicOn ? 0f : -80f);
    }

    public void ToggleSFX()
    {
        sfxOn = !sfxOn;
        mixer.SetFloat("SFXVolume", sfxOn ? 0f : -80f);
    }
}