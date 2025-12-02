using System;
using UnityEngine;

public class PlayerInitialScript : MonoBehaviour
{
    public Transform SpawnPoint1, SpawnPoint2;
    public GameObject Player1, Player2;

    public static event Action<Transform> transPlayer;
    private void Start()
    {
        GameObject player1 =Instantiate(Player1, SpawnPoint1.position, SpawnPoint1.rotation);
        transPlayer?.Invoke(player1.transform);
        Debug.Log("Se mando el primer Player");
        GameObject player2 = Instantiate(Player2, SpawnPoint2.position, SpawnPoint2.rotation);
        transPlayer?.Invoke(player2.transform);
        Debug.Log("Se mando el Segundo Player");
    }
}
