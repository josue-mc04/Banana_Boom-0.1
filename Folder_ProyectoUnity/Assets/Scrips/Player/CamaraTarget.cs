using System;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public event Action OnRotationCamera;

    private void LateUpdate()
    {
        // Si algún objeto se suscribe, se le notifica
        OnRotationCamera?.Invoke();
    }
}
