using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("Daño por cada segundo dentro de la zona")]
    [SerializeField] private float damagePerTick = 5f;

    [Header("Tiempo entre cada daño (en segundos)")]
    [SerializeField] private float tickInterval = 1f;

    // Guarda una referencia a cada jugador que está recibiendo daño
    private Dictionary<PlayerControler, Coroutine> activeCoroutines = new Dictionary<PlayerControler, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();

        if (player != null && !activeCoroutines.ContainsKey(player))
        {
            Coroutine routine = StartCoroutine(ApplyDamageOverTime(player));
            activeCoroutines.Add(player, routine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerControler player = other.GetComponent<PlayerControler>();

        if (player != null && activeCoroutines.ContainsKey(player))
        {
            StopCoroutine(activeCoroutines[player]);
            activeCoroutines.Remove(player);
        }
    }

    private IEnumerator ApplyDamageOverTime(PlayerControler player)
    {
        // Mientras el jugador esté vivo y dentro de la zona, le quita vida
        while (player != null)
        {
            player.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickInterval);

            if (player == null)
                break; // si el jugador muere y se destruye, salimos
        }
    }

    private void OnDisable()
    {
        // Limpia las corutinas activas al desactivar el objeto
        foreach (var kv in new List<Coroutine>(activeCoroutines.Values))
        {
            if (kv != null)
                StopCoroutine(kv);
        }
        activeCoroutines.Clear();
    }
}