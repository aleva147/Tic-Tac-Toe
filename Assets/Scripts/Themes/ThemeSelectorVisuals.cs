// Visuals for Theme selection in the ThemeSelectionMenu.

using UnityEngine;

[RequireComponent(typeof(ThemeSelector))]
public class ThemeSelectorVisuals : MonoBehaviour
{
    private enum ButtonStatus { INACTIVE, ACTIVE }

    [Header("Inactive/Active Tiles and Frames")]
    [SerializeField] private Color[] tileColors;
    [SerializeField] private Color[] frameColors;
    [SerializeField] private Sprite[] frameSprites;

    [Header("ThemeButton Prefab")]
    [SerializeField] ThemeButton buttonPrefab;

    [Header("Event Channels")]
    public IntEventSO onThemeButtonClick; 

    private ThemeButton[] buttons;
    private ThemeSelector themeSelector; 
    private int activeButtonId;



    void Start()
    {
        themeSelector = GetComponent<ThemeSelector>();
        activeButtonId = themeSelector.activeThemeId;
        buttons = new ThemeButton[themeSelector.themes.Length];

        InstantiateThemeButtons();

        UpdateButton(activeButtonId, true);
    }


    private void OnEnable()
    {
        onThemeButtonClick.OnEventRaised += ChangeSelectedButton;
    }

    private void OnDisable()
    {
        onThemeButtonClick.OnEventRaised -= ChangeSelectedButton;
    }


    private void InstantiateThemeButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = Instantiate(buttonPrefab, transform);
            buttons[i].Initialize(themeSelector.themes[i]);
        }
    }


    // Change button style to Selected or Deselected:
    private void UpdateButton(int buttonId, bool setActive)
    {
        ButtonStatus buttonStatus = setActive ? ButtonStatus.ACTIVE : ButtonStatus.INACTIVE;  

        buttons[buttonId].UpdateButton(
            setActive, 
            tileColors[(int)buttonStatus],
            frameColors[(int)buttonStatus],
            frameSprites[(int)buttonStatus]
        );
    }


    // Visually deselect old button and select new button:
    public void ChangeSelectedButton(int index)
    {
        if (index < 0 || index >= buttons.Length || index == activeButtonId) return;

        UpdateButton(activeButtonId, false);
        activeButtonId = index;
        UpdateButton(activeButtonId, true);
    }
}
