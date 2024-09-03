using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public static event Action OnPlayerBitten;

    public NavMeshAgent agent;
    public float range;
    public float waitTime = 2.0f;
    public float visionRadius = 10f;
    public float biteDistance = 0.5f;
    public Transform centrePoint;

    private Animator animator;
    private Transform player;
    private bool isChasing = false;
    private bool isBiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        SphereCollider visionCollider = gameObject.AddComponent<SphereCollider>();
        visionCollider.radius = visionRadius;
        visionCollider.isTrigger = true;

        StartCoroutine(MoveToRandomPoints());
    }

    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= biteDistance)
            {
                isBiting = true;
                animator.SetTrigger("Bite");
                StartCoroutine(BitePlayer());
            }
        }
    }

    private IEnumerator BitePlayer()
    {
        yield return new WaitForSeconds(1.7f);
        if (isBiting)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.OnBittenByZombie(transform);
                OnPlayerBitten?.Invoke();
            }
            isBiting = false;
        }
    }

    IEnumerator MoveToRandomPoints()
    {
        while (true)
        {
            if (!isChasing && agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                }

                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield return null;
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
    
        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }
}
