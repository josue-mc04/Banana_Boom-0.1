using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localRotation = Quaternion.identity; 
    }
}
