using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnimatorMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Image Fade")]
    [SerializeField] private Image targetImage;
    [SerializeField] private float fadeDuration = 0.3f;

    [Header("Button Scale")]
    [SerializeField] private RectTransform targetTransform;
    [SerializeField] private float scaleUp = 1.1f;
    [SerializeField] private float scaleDuration = 0.25f;
    

    private Tween fadeTween;
    private Tween scaleTween;
    

    void Start()
    {
        Color c = targetImage.color;
        c.a = 0f;
        targetImage.color = c;

        if (targetTransform == null)
            targetTransform = transform as RectTransform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        FadeTo(1f);
        ScaleTo(scaleUp);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FadeTo(0f);
        ScaleTo(1f);
    }

    private void FadeTo(float targetAlpha)
    {
        if (fadeTween != null && fadeTween.IsActive())
            fadeTween.Kill();

        fadeTween = targetImage
            .DOFade(targetAlpha, fadeDuration)
            .SetEase(Ease.InOutQuad);
    }

    private void ScaleTo(float targetScale)
    {
        if (scaleTween != null && scaleTween.IsActive())
            scaleTween.Kill();

        scaleTween = targetTransform
            .DOScale(targetScale, scaleDuration)
            .SetEase(Ease.OutBack);
    }
}
