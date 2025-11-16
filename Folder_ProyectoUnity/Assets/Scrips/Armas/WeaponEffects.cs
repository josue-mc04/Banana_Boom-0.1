using UnityEngine;
using DG.Tweening;

public class WeaponEffects : MonoBehaviour
{
    [Header("Movimiento con Mathf ")]
    [SerializeField] private float floatAmplitude = 0.1f;
    [SerializeField] private float floatSpeed = 2f;
    private Vector3 startPos;

    [Header("DOTween  Animación al cambiar de arma")]
    [SerializeField] private Transform weaponModel;

    [Header("Animation Curve Velocidad del Proyectil")]
    [SerializeField] private AnimationCurve projectileCurve;
    [SerializeField] private float maxProjectileSpeed = 10f;
    private float projectileTime;

    private void Start()
    {
        startPos = transform.localPosition;

      
        if (weaponModel != null)
        {
            weaponModel.localScale = Vector3.one * 0.5f;
            weaponModel.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
        }
    }

    private void Update()
    {

        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.localPosition = startPos + new Vector3(0, offsetY, 0);


        projectileTime += Time.deltaTime;
    }

    public float GetProjectileSpeed()
    {
        return projectileCurve.Evaluate(projectileTime) * maxProjectileSpeed;
    }

    public void PlaySwitchTween()
    {
        if (weaponModel == null)
            return;

        weaponModel.localScale = Vector3.one * 0.5f;
        weaponModel.DOScale(1f, 0.25f).SetEase(Ease.OutBack);
    }

    public void ResetCurve()
    {
        projectileTime = 0f;
    }
}