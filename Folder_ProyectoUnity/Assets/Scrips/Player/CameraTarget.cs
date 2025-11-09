using System;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public event Action OnRotationCamera;

    private void LateUpdate()
    {
        RotationCamara();
    }

    public void RotationCamara()
    {
        // reset local rotation (si eso es lo que quieres)
        transform.localRotation = Quaternion.identity;

        // invoca de forma segura (null-safe)
        OnRotationCamera?.Invoke();
    }
}
