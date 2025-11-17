using UnityEngine;
using DG.Tweening;

public class WeaponEffects : MonoBehaviour
{
    [Header("Switch Animation")]
    [SerializeField] private float animDuration = 0.25f;
    [SerializeField] private Vector3 hiddenOffset = new Vector3(0.5f, -0.5f, 0);

    private Vector3 originalPos;

    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    public void PlaySwitchTween()
    {
        transform.localPosition = originalPos + hiddenOffset;
        transform.DOLocalMove(originalPos, animDuration).SetEase(Ease.OutQuad);
    }
}