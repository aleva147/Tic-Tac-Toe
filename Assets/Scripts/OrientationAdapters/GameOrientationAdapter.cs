// This script is attached to OrientationAdapter and is responsible for 
// repositioning elements on the screen when screen orientation changes
// in the Game scene.

using UnityEngine;

public class GameOrientationAdapter : MonoBehaviour
{
    [SerializeField] private RectTransform settingsButton;
    [SerializeField] private RectTransform timer;
    [SerializeField] private RectTransform board;

    [Header("Landscape Positions")]
    [SerializeField] private Vector2 settingsLandscapePos;
    [SerializeField] private Vector2 timerLandscapePos;
    [SerializeField] private Vector2 boardLandscapePos;

    [Header("Portrait Positions")]
    [SerializeField] private Vector2 settingsPortraitPos;
    [SerializeField] private Vector2 timerPortraitPos;
    [SerializeField] private Vector2 boardPortraitPos;

    private bool isPortrait;


    void Start()
    {
        isPortrait = Screen.height > Screen.width;
        AdaptUI();
    }

    void LateUpdate()
    {
        if (!settingsButton || !timer || !board) return;

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
            timer.anchoredPosition = timerPortraitPos;
            board.anchoredPosition = boardPortraitPos;
        }
        else
        {
            settingsButton.anchoredPosition = settingsLandscapePos;
            timer.anchoredPosition = timerLandscapePos;
            board.anchoredPosition = boardLandscapePos;
        }
    }
}