using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private BTSelector root;
    public Transform target;

    public CircleCollider2D attackCollider;
    public CircleCollider2D chaseCollider;
    private EnemyInfo _enemyInfo;
    private NavMeshAgent agent;
    public float AttackRange;

    private void Awake()
    {
        _enemyInfo = GetComponent<EnemyInfo>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        attackCollider.radius = _enemyInfo.AttackRange;
        chaseCollider.radius = _enemyInfo.ChaseRange;
        AttackRange = _enemyInfo.AttackRange;

        root = new BTSelector();
        BTSequence attackSequence = new BTSequence();
        BTSequence chaseSequence = new BTSequence();
        BTSequence wanderSequence = new BTSequence();
        BTAction attackAction = new BTAction(Attack);
        BTAction chaseAction = new BTAction(Chase);
        BTAction wanderAction = new BTAction(Wander);
        BTCondition playerInRange = new BTCondition(IsPlayerInRange);
        BTCondition playerInChaseRange = new BTCondition(IsPlayerInChaseRange);

        root.AddChild(attackSequence);
        root.AddChild(chaseSequence);
        root.AddChild(wanderSequence);
        attackSequence.AddChild(playerInRange);
        attackSequence.AddChild(attackAction);
        chaseSequence.AddChild(playerInChaseRange);
        chaseSequence.AddChild(chaseAction);
        wanderSequence.AddChild(wanderAction);
    }

    private void Update()
    {
        BTNodeState result = root.Evaluate();
        Debug.Log($"Behavior Tree evaluation result: {result}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            Debug.Log("Player Entered!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            Debug.Log("Player left!");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            Debug.Log("Player Stays");
        }
    }

    private BTNodeState Attack()
    {
        Debug.Log("Attack!");
        return BTNodeState.Success;
    }

    private BTNodeState Chase()
    {
        if (target == null)
        {
            Debug.Log("Target is null. Cannot chase.");
            return BTNodeState.Failure;
        }

        Debug.Log("Chasing!");
        agent.SetDestination(target.position);
        return BTNodeState.Running;
    }

    private BTNodeState Wander()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Vector3 randomDestination = GetRandomDestination();
            agent.SetDestination(randomDestination);
        }

        return BTNodeState.Running;
    }

    private Vector3 GetRandomDestination()
    {
        float randomX = UnityEngine.Random.Range(-10f, 10f);
        float randomY = UnityEngine.Random.Range(-10f, 10f);
        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas);
        return hit.position;
    }

    private bool IsPlayerInRange()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= AttackRange;
    }

    private bool IsPlayerInChaseRange()
    {
        if (target == null)
        {
            return false;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= _enemyInfo.ChaseRange;
    }
}
