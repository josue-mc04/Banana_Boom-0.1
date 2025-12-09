using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("Damage por cada segundo dentro de la zona")]
    [SerializeField] private float damagePerTick = 5f;

    [Header("tiempo entre cada daño (en segundos)")]
    [SerializeField] private float tickInterval = 1f;

    //guarda una referencia a cada jugador que esta recibiendo damage
    private Dictionary<PlayerControler, Coroutine> activeCoroutines = new Dictionary<PlayerControler, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler player = other.GetComponentInParent<PlayerControler>();

        if (player != null && !activeCoroutines.ContainsKey(player))
        {
            Coroutine routine = StartCoroutine(ApplyDamageOverTime(player));
            activeCoroutines.Add(player, routine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControler player = other.GetComponentInParent<PlayerControler>();

        if (player != null && activeCoroutines.ContainsKey(player))
        {
            StopCoroutine(activeCoroutines[player]);
            activeCoroutines.Remove(player);
        }
    }


    private IEnumerator ApplyDamageOverTime(PlayerControler player)
    {
        while (player != null)
        {
            //damage proporcional al tiempo de frame 
            float elapsed = 0f;
            while (elapsed < tickInterval)
            {
                if (player == null)
                {
                    break;
                }
                player.TakeDamage(damagePerTick * Time.deltaTime / tickInterval);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
    private void OnDisable()
    {
        //limpia las corutinas activas al desactivar el objeto
        foreach (var kv in new List<Coroutine>(activeCoroutines.Values))
        {
            if (kv != null)
                StopCoroutine(kv);
        }
        activeCoroutines.Clear();
    }
}