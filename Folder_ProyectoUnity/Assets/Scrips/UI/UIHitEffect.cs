using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIHitEffect : MonoBehaviour
{

    [SerializeField] private Image damagePanel;

    public void ShowDamage()
    {
        damagePanel.DOFade(0.4f, 0.1f);
        damagePanel.DOFade(0f, 0.3f).SetDelay(0.1f);
    }
}
