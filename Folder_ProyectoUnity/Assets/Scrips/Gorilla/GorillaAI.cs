using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class GorillaAI : MonoBehaviour
{
    [Header("Patrulla")]
    [SerializeField] private List<Transform> points;
    private NavMeshAgent agent;
    private int currentPoint = 0;

    [Header("Perseción")]
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
        if (player == null)
        {
            chasing = false;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        #region persecución
        if (distanceToPlayer < detectRange)
        {
            chasing = true;
        }
        else
        {
            chasing = false;
        }

        if (chasing == true)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }
        #endregion

        #region patrulla
        if (points.Count == 0)
        {
            return;
        }

        //si en caso llegue al punto
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //paso al siguiente
            currentPoint++;

            //si en caso llego al final, vuelvo al primero
            if (currentPoint >= points.Count)
            {
                currentPoint = 0;
            }

            //nuevo punto
            agent.SetDestination(points[currentPoint].position);
        }
        #endregion
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
        //revisar jugadores con el poderoso for
        for (int i = 0; i < allPlayers.Count; i++)
        {
            Transform jugador = allPlayers[i];

            if (jugador != null)
            {
                float distancia = Vector3.Distance(transform.position, jugador.position);

                //si el jugador esta cerca
                if (distancia < detectRange)
                {
                    //evitar duplicados
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

        //si el jugador actual se aleja
        if (player != null)
        {
            float distPlayer = Vector3.Distance(transform.position, player.position);

            if (distPlayer > detectRange)
            {
                player = null;
                chasing = false;
            }
        }
    }
    private void Patrol()
    {
        if (points.Count == 0) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
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
        //cola temporal donde guardaremos los jugadores mientras revisamos
        QueuePersecution<Transform> temporaryQueue = new QueuePersecution<Transform>();

        bool playerFoundInQueue = false;

        //sacar todos los elementos de la cola principal
        while (!persecutionQueue.IsEmpty())
        {
            Transform currentPlayer = persecutionQueue.Dequeue();

            //si encontramos al jugador que buscamos
            if (currentPlayer == targetPlayer)
            {
                playerFoundInQueue = true;
            }

            //guardar el jugador en la cola temporal
            temporaryQueue.Enqueue(currentPlayer);
        }

        //regresa todos los elementos a la cola original
        while (!temporaryQueue.IsEmpty())
        {
            persecutionQueue.Enqueue(temporaryQueue.Dequeue());
        }

        return playerFoundInQueue;
    }

}
