using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimatorToggle : MonoBehaviour
{
    private enum ToggleState { OFF, ON }
    private ToggleState state = ToggleState.ON; 
    public Sprite[] stateSprite;
    public GameObject[] stateText;
    public GameObject crossedLineIcon;

    public void Toggle()
    {
        stateText[(int)state].SetActive(false);
        
        state = (state == ToggleState.OFF) ? ToggleState.ON : ToggleState.OFF;

        stateText[(int)state].SetActive(true);
        transform.GetComponent<Image>().sprite = stateSprite[(int)state];

        if (crossedLineIcon != null) 
            crossedLineIcon.SetActive(state == ToggleState.OFF);
    }
}
