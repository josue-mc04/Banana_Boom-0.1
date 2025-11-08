using System;
using UnityEngine;

public class SceneEvents : MonoBehaviour
{
    public static event Action OnJugar;
    public static event Action OnVolverMenu;

    public static void Jugar()
    {
        OnJugar?.Invoke();
    }

    public static void VolverMenu()
    {
        OnVolverMenu?.Invoke();
    }
}
