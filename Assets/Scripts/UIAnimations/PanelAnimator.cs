using UnityEngine;
using DG.Tweening;

public class PanelAnimator : MonoBehaviour
{
    public CanvasGroup dimmedBg;
    public Transform panelContainer;
    private Tween currentTween;

    [Header("Scale Settings")]
    public float duration = 0.25f;
    public Vector3 openScale = Vector3.one;
    public Vector3 closedScale = new Vector3(0.2f, 0.2f, 0.2f);

    [Header("Event Channels")]
    [SerializeField] private VoidEventSO onPanelAppear;   // AudioManager is subscribed to this channel.



    public void Open()
    {
        gameObject.SetActive(true);

        onPanelAppear.Raise();

        // Kill any running animation
        currentTween?.Kill();

        // Set initial state
        dimmedBg.alpha = 0;
        panelContainer.localScale = closedScale;

        // Animate
        currentTween = DOTween.Sequence()
            .Join(dimmedBg.DOFade(1, duration))
            .Join(panelContainer.DOScale(openScale, duration).SetEase(Ease.OutBack, 1.2f));
    }

    public void Close()
    {
        onPanelAppear.Raise();

        currentTween?.Kill();

        currentTween = DOTween.Sequence()
            .Join(dimmedBg.DOFade(0, duration))
            .Join(panelContainer.DOScale(closedScale, duration).SetEase(Ease.InBack))
            .OnComplete(() => gameObject.SetActive(false));
    }
}