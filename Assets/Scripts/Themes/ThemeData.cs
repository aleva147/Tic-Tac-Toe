// Themes are defined as Scriptable Objects, for easy and designer-friendly expansion.
// Currently gridColor and bgColor aren't actually being used.


using UnityEngine;

[CreateAssetMenu(menuName = "Theme", fileName = "Theme1")]
public class ThemeDataSO : ScriptableObject
{
    [Header("Theme Id")]
    public int id;
    public string themeName;

    [Header("Sprites")]
    public Sprite xSprite;
    public Sprite oSprite;

    [Header("Colors")]
    public Color gridColor;
    public Color bgColor;
}