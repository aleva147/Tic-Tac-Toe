// This script is attached to OrientationAdapter and is responsible for 
// repositioning elements on the screen when screen orientation changes
// in the MainMenu scene.

using UnityEngine;

public class MenuOrientationAdapter : MonoBehaviour
{
    [SerializeField] private RectTransform settingsButton;
    [SerializeField] private RectTransform playButton;

    [Header("Landscape Positions")]
    [SerializeField] private Vector2 settingsLandscapePos;
    [SerializeField] private float playButtonLandscapeHeight;

    [Header("Portrait Positions")]
    [SerializeField] private Vector2 settingsPortraitPos;
    [SerializeField] private float playButtonPortraitHeight;


    private bool isPortrait;


    void Start()
    {
        isPortrait = Screen.height > Screen.width;
        AdaptUI();
    }

    void LateUpdate()
    {
        if (!settingsButton || !playButton) return;

        bool current = Screen.height > Screen.width;

        if (current != isPortrait)
        {
            isPortrait = current;
            AdaptUI();
            Canvas.ForceUpdateCanvases();
        }
    }


    void AdaptUI()
    {
        if (isPortrait)
        {
            settingsButton.anchoredPosition = settingsPortraitPos;
            playButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, playButtonPortraitHeight);
        }
        else
        {
            settingsButton.anchoredPosition = settingsLandscapePos;
            playButton.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, playButtonLandscapeHeight);
        }
    }
}