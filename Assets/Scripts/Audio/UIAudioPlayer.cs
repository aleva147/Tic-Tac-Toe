// This script is attached to AudioManager and is responsible for played sounds by listening to events.

using UnityEngine;

public class UIAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;

    [Header("Event Channels")]
    [SerializeField] private VoidEventSO onMenuButtonClick;
    [SerializeField] private VoidEventSO onPanelButtonClick;
    [SerializeField] private VoidEventSO onPanelAppear;
    [SerializeField] private IntIntEventSO onCellMark;
    [SerializeField] private VoidEventSO onGameOver;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuButtonSFX;
    [SerializeField] private AudioClip panelButtonSFX;
    [SerializeField] private AudioClip panelAppearSFX;
    [SerializeField] private AudioClip cellMarkSFX;
    [SerializeField] private AudioClip gameOverSFX;


    private void OnEnable()
    {
        onMenuButtonClick.OnEventRaised += PlayClickMenu;
        onPanelButtonClick.OnEventRaised += PlayClickPanel;
        onPanelAppear.OnEventRaised += PlayPanelAppear;
        onCellMark.OnEventRaised += PlayCellMark;
        onGameOver.OnEventRaised += PlayGameOver;
    }

    private void OnDisable()
    {
        onMenuButtonClick.OnEventRaised -= PlayClickMenu;
        onPanelButtonClick.OnEventRaised -= PlayClickPanel;
        onPanelAppear.OnEventRaised -= PlayPanelAppear;
        onCellMark.OnEventRaised -= PlayCellMark;
        onGameOver.OnEventRaised -= PlayGameOver;
    }


    public void PlayClickMenu()
    {
        sfxSource.PlayOneShot(menuButtonSFX);
    }

    public void PlayClickPanel()
    {
        sfxSource.PlayOneShot(panelButtonSFX);
    }

    public void PlayPanelAppear()
    {
        sfxSource.PlayOneShot(panelAppearSFX);
    }

    public void PlayCellMark(int r, int c)
    {
        sfxSource.PlayOneShot(cellMarkSFX);
    }

    public void PlayGameOver()
    {
        sfxSource.PlayOneShot(gameOverSFX);
    }
}