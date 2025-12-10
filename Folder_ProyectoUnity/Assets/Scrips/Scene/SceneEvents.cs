using System;
using UnityEngine;

public class SceneEvents : MonoBehaviour
{
    public static event Action OnJugar;
    public static event Action OnVolverMenu;
    public static event Action OnPlayer1Win;
    public static event Action OnPlayer2Win;

    public static void Jugar()
    {
        OnJugar?.Invoke();
    }

    public static void VolverMenu()
    {
        OnVolverMenu?.Invoke();
    }
    public static void Player1Win()
    {
        OnPlayer1Win?.Invoke();
    }
    public static void Player2Win()
    {
        OnPlayer2Win?.Invoke();
    }
}
