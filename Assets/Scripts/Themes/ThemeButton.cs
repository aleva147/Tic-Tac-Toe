// This script is attached to buttons used for theme selection in the ThemeSelectionMenu.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThemeButton : MonoBehaviour
{
    [Header("Select/Deselect Data")]
    [SerializeField] private GameObject highlight;
    [SerializeField] private Image tileSprite;
    [SerializeField] private Image frameSprite;

    [Header("Scriptable Object Data")]
    [SerializeField] private TMP_Text themeName;
    [SerializeField] private Image xSprite;
    [SerializeField] private Image oSprite;
    private int id;

    [Header("Event Channels")]
    [SerializeField] private IntEventSO onButtonClickID;  // ThemeSelector is subscribed to this channel.
    [SerializeField] private VoidEventSO onButtonClick;   // AudioManager is subscribed to this channel.
    


    public void Initialize(ThemeDataSO themeData)
    {
        id = themeData.id;
        themeName.text = themeData.themeName;
        xSprite.sprite = themeData.xSprite;
        oSprite.sprite = themeData.oSprite;

        // The button click triggers SO events that ThemeSelector and AudioManager are subscribed to:
        GetComponent<Button>().onClick.AddListener(() => onButtonClickID.Raise(id));
        GetComponent<Button>().onClick.AddListener(() => onButtonClick.Raise());
    }

    // Change button style to Selected or Deselected:
    public void UpdateButton(bool setActive, Color tileColor, Color frameColor, Sprite frame)
    {
        highlight.SetActive(setActive);
        tileSprite.color = tileColor;
        frameSprite.color = frameColor;
        frameSprite.sprite = frame;
    }
}