// Logic and visuals for every board Cell. It raises an event when an empty cell is clicked 
// and it is used for visually marking cells with X and O by TicTacToeVisuals.

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    private int row, col;
    private bool isMarked;
    private Image cellImage;

    [Header("Popup Animation")]
    [SerializeField] private float initialScale = 0.2f;
    [SerializeField] private float overshootScale = 1.2f;
    [SerializeField] private float finalScale = 0.8f;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float overshootDuration = 0.1f;

    [Header("Event Channels")]
    public IntIntEventSO onCellMark; 


    void Awake()
    {
        cellImage = GetComponent<Image>();

        // Extract row and col from the GameObject name (third to last and last characters in the name):
        row = gameObject.name[^3] - '0';
        col = gameObject.name[^1] - '0';
    }


    public void ClickCell()
    {
        // Possible improvement: raise an event for unsuccessful marking -> shake animation + different sfx 
        if (isMarked) return;

        isMarked = true;
        onCellMark.Raise(row, col);
    }

    // Visually marks cell with X/O (this could be moved to CellVisuals.cs)
    public void Mark(Sprite sprite)
    {
        cellImage.sprite = sprite;
        cellImage.rectTransform.localScale = new Vector3(initialScale, initialScale, 1);

        Sequence seq = DOTween.Sequence();

        seq.Append(cellImage.rectTransform.DOScale(overshootScale, duration).SetEase(Ease.OutBack))
           .Append(cellImage.rectTransform.DOScale(finalScale, overshootDuration).SetEase(Ease.OutQuad));

        cellImage.DOFade(1f, duration);
    }

}
