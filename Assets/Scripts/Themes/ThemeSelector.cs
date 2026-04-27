// Logic for Theme selection in the ThemeSelectionMenu.

using UnityEngine;

public class ThemeSelector : MonoBehaviour
{
    public ThemeDataSO[] themes;
    public int activeThemeId { get; private set; }

    // The event channel that ThemeButtons use to trigger events when clicked:
    public IntEventSO onThemeButtonClick;


    void Awake()
    {
        activeThemeId = 0;
        TicTacToeDataService.SelectedTheme = themes[activeThemeId];
    }


    private void OnEnable()
    {
        onThemeButtonClick.OnEventRaised += SelectTheme;
    }

    private void OnDisable()
    {
        onThemeButtonClick.OnEventRaised -= SelectTheme;
    }


    public void SelectTheme(int index)
    {
        if (index < 0 || index >= themes.Length || index == activeThemeId) return;

        activeThemeId = index;
        TicTacToeDataService.SelectedTheme = themes[activeThemeId];
    }
}