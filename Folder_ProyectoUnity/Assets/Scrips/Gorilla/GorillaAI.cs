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
    [SerializeField]private Transform player;
    [SerializeField]private float detectRange;   
    private bool chasing = false;

    [Header("Empujar")]
    [SerializeField]private float pushForce;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (points.Count > 0)
        {
            agent.SetDestination(points[currentPoint].position);
        }
    }

    private void Update()
    {
        if (player == null)
        {
            return;
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
            return; //deja de patrullar
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

        if(collision.gameObject.GetComponent<IThrowAble>() != null)
        {
            collision.gameObject.GetComponent<IThrowAble>().Throw();

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();


            Vector3 pushDir = (collision.transform.position - transform.position);

            pushDir.y = 0;
            pushDir = pushDir.normalized;

            Vector3 finalDir = (pushDir + Vector3.up * 2f).normalized;

            rb.AddForce(finalDir * pushForce, ForceMode.Impulse);

        }

        /*
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody player = collision.gameObject.GetComponent<Rigidbody>();
            if (player != null)
            {
                //dirección desde el gorila hacia el jugador
                //Vector3 ThrowDir = 

                Vector3 pushDir = (collision.transform.position - transform.position);

                pushDir.y = 0;
                pushDir = pushDir.normalized;

                Vector3 finalDir = (pushDir + Vector3.up * 0.1f).normalized;

                player.AddForce(finalDir * pushForce, ForceMode.Impulse);
            }
        }*/
    }
}
