using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class GorillaAI : MonoBehaviour
{
    [Header("Patrulla")]
    [SerializeField] private List<Transform> points;
    private NavMeshAgent agent;
    private int currentPoint = 0;

    [Header("Persecución")]
    [SerializeField] private Transform player;
    [SerializeField] private float detectRange;
    private bool chasing = false;

    [Header("Empujar")]
    [SerializeField] private float pushForce;

    [Header("Sistema de Cola")]
    [SerializeField] private List<Transform> allPlayers;
    private QueuePersecution<Transform> persecutionQueue;
    private void OnEnable()
    {
        PlayerInitialScript.transPlayer += GetPlayer;
        Debug.Log("Se suscribio");
    }
    private void OnDisable()
    {
        Debug.Log("Se desuscribio");
        PlayerInitialScript.transPlayer -= GetPlayer;
    }
    private void GetPlayer(Transform transform)
    {
        Debug.Log("ayudaaaaaaaaa");
        allPlayers.Add(transform);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        persecutionQueue = new QueuePersecution<Transform>();

        if (points.Count > 0)
        {
            agent.SetDestination(points[currentPoint].position);
        }
    }

    private void Update()
    {
        UpdateQueueSystem();

        //verificar si tenemos un jugador para perseguir
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            //persecucion
            if (distanceToPlayer < detectRange)
            {
                chasing = true;
                agent.SetDestination(player.position);
            }
            else
            {
                chasing = false;
                player = null; //el jugador se alejo demasiado
            }
        }
        else
        {
            chasing = false;
        }

        //si no estamos persiguiendo, patrullar
        if (!chasing)
        {
            Patrol();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //busca la interfaz IthrowAble en el objeto golpeado o en cualquiera de sus padres
        IthrowAble throwable = collision.gameObject.GetComponentInParent<IthrowAble>();

        if (throwable != null)
        {
            //llama al metodo Throw del player
            throwable.Throw();

            //intenta obtener el Rigidbody: primero usa collision.rigidbody (si existe),
            //si no esta, busca en los padres (porque el Rigidbody suele estar en el root)
            Rigidbody rb = collision.rigidbody ?? collision.gameObject.GetComponentInParent<Rigidbody>();

            if (rb != null && !rb.isKinematic)
            {
                // Calcula direccion de empuje horizontal más un impulso vertical
                Vector3 pushDir = (rb.transform.position - transform.position);
                pushDir.y = 0;
                pushDir = pushDir.normalized;

                Vector3 finalDir = (pushDir + Vector3.up * 2f).normalized;

                rb.AddForce(finalDir * pushForce, ForceMode.Impulse);

                Debug.Log("Gorila empujo a: " + rb.gameObject.name);
            }
            else
            {
                Debug.LogWarning("Gorila: no encontre Rigidbody valido para empujar en " + collision.gameObject.name);
            }
        }
        else
        {
            // Debug util para ver que esta chocando
            Debug.Log("Gorila colisiono con (no lanzable): " + collision.gameObject.name);
        }
    }


    private void UpdateQueueSystem()
    {
        //revisar jugadores
        for (int i = 0; i < allPlayers.Count; i++)
        {
            Transform jugador = allPlayers[i];

            if (jugador != null)
            {
                float distancia = Vector3.Distance(transform.position, jugador.position);

                //si en caso esta cerca
                if (distancia < detectRange)
                {
                    //evita duplicados
                    if (!QueueHas(jugador))
                    {
                        persecutionQueue.Enqueue(jugador);
                    }
                }
            }
        }

        //si no hay jugador actual pero si en la cola
        if (player == null && !persecutionQueue.IsEmpty())
        {
            player = persecutionQueue.Dequeue();
            chasing = true;
        }
    }

    private void Patrol()
    {
        if (points.Count == 0) return;

        //verificar si hemos llegado al punto actual
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentPoint++;

            if (currentPoint >= points.Count)
            {
                currentPoint = 0;
            }

            agent.SetDestination(points[currentPoint].position);
        }
    }

    private bool QueueHas(Transform targetPlayer)
    {
        QueuePersecution<Transform> temporaryQueue = new QueuePersecution<Transform>();
        bool playerFoundInQueue = false;

        while (!persecutionQueue.IsEmpty())
        {
            Transform currentPlayer = persecutionQueue.Dequeue();

            if (currentPlayer == targetPlayer)
            {
                playerFoundInQueue = true;
            }

            temporaryQueue.Enqueue(currentPlayer);
        }

        while (!temporaryQueue.IsEmpty())
        {
            persecutionQueue.Enqueue(temporaryQueue.Dequeue());
        }

        return playerFoundInQueue;
    }
}