using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImpactVFX : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameDuration = 0.05f;


    public void Play(Vector2 anchoredPosition)
    {
        transform.localScale = Vector3.one;
        image.rectTransform.rotation = Quaternion.Euler(0,0,Random.Range(0, 360f));
        image.rectTransform.anchoredPosition = anchoredPosition;
        image.color = new Color(1, 1, 1, 1);

        gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        // Use frames during animation:
        for (int i = 0; i < frames.Length; i++)
        {
            int index = i;
            seq.AppendCallback(() => image.sprite = frames[index]);
            seq.AppendInterval(frameDuration);
        }

        seq.OnComplete(() => {
            gameObject.SetActive(false);
        });
    }
}