using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

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

        //Si no estamos persiguiendo, patrullar
        if (!chasing)
        {
            Patrol();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IThrowAble>() != null)
        {
            collision.gameObject.GetComponent<IThrowAble>().Throw();

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushDir = (collision.transform.position - transform.position);

            pushDir.y = 0;
            pushDir = pushDir.normalized;

            Vector3 finalDir = (pushDir + Vector3.up * 2f).normalized;
            rb.AddForce(finalDir * pushForce, ForceMode.Impulse);
        }
    }

    private void UpdateQueueSystem()
    {
        //Revisar jugadores
        for (int i = 0; i < allPlayers.Count; i++)
        {
            Transform jugador = allPlayers[i];

            if (jugador != null)
            {
                float distancia = Vector3.Distance(transform.position, jugador.position);

                //Si en caso esta cerca
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