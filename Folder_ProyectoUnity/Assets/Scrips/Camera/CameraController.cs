using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public Transform playerRoot;
    public Transform rotationRoot;
    public Transform cameraPivot;
    public Transform cameraTransform;
    public Transform aimTarget;
    public Transform visualRoot;

    [Header("Settings")]
    public float sensitivity = 170f;
    public float verticalMin = -40f;
    public float verticalMax = 60f;
    public float aimDistance = 3f;

    [Header("Look Deadzone")]
    public float deadzone = 0.05f; //si el input es menor que esto, NO se procesa

    Vector2 lookInput;
    float pitch = 0f;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        Vector2 input = lookInput;

        //aplicar deadzone
        if (Mathf.Abs(input.x) < deadzone)
        {
            input.x = 0;
        }
        if (Mathf.Abs(input.y) < deadzone) 
        {
            input.y = 0;
        }

        if (playerRoot != null)
        {
            playerRoot.Rotate(Vector3.up * input.x * sensitivity * Time.deltaTime);
        }

        pitch -= input.y * sensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, verticalMin, verticalMax);

        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }

        if (aimTarget != null && cameraPivot != null)
        {
            aimTarget.position = playerRoot.position + cameraPivot.forward * aimDistance + Vector3.up * 1.6f;
        }

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.identity;
        }

        if (visualRoot != null && playerRoot != null)
        {
            Vector3 e = visualRoot.eulerAngles;
            e.y = playerRoot.eulerAngles.y;
            visualRoot.eulerAngles = e;
        }
    }
}
