using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class ButtonAnimatorPanel : MonoBehaviour
{
    public Image targetImage;
    public Sprite normalSprite;
    public Sprite pressedSprite;
    public UnityEvent onClick;

    public float pressScale = 0.9f;
    public float duration = 0.08f;
    public float holdTime = 0.05f;

    [Header("Overshoot")]
    public float overshootScale = 1.08f;
    public float overshootDuration = 0.08f;
    public float settleDuration = 0.06f;

    private bool isAnimating = false;

    

    public void OnButtonPressed()
    {
        if (isAnimating) return;
        isAnimating = true;

        Sequence seq = DOTween.Sequence();

        // Press (shrink)
        seq.AppendCallback(() => targetImage.sprite = pressedSprite);
        seq.Append(transform.DOScale(pressScale, duration).SetEase(Ease.OutQuad));

        // Hold
        seq.AppendInterval(holdTime);

        // Release (grow + overshoot)
        seq.AppendCallback(() => targetImage.sprite = normalSprite);

        // Step 1: overshoot past 1
        seq.Append(transform.DOScale(overshootScale, overshootDuration).SetEase(Ease.OutQuad));

        // Step 2: settle back to 1
        seq.Append(transform.DOScale(1f, settleDuration).SetEase(Ease.InOutQuad));

        // Fire actual click
        seq.AppendCallback(() =>
        {
            onClick?.Invoke();
            isAnimating = false;
        });
    }
}