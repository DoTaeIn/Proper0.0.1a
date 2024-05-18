using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class EnemyAI : MonoBehaviour
{
   private BTSelector root;
   public Transform target;

   public CircleCollider2D attackCollider;
   public CircleCollider2D chaseCollider;
   private EnemyInfo _enemyInfo;
   public float AttackRange;

   private void Awake()
   {
      _enemyInfo = GetComponent<EnemyInfo>();
   }

   private void Start()
   {
      attackCollider.radius = _enemyInfo.AttackRange;
      chaseCollider.radius = _enemyInfo.ChaseRange;
      AttackRange = _enemyInfo.AttackRange;
      
      root = new BTSelector();
      BTSequence attackSequence = new BTSequence();
      BTSequence chaseSequence = new BTSequence();
      BTAction attackAction = new BTAction(Attack);
      BTAction chaseAction = new BTAction(Chase);
      BTCondition playerInRange = new BTCondition(IsPlayerInRange);
      
      root.AddChild(attackSequence);
      root.AddChild(chaseSequence);
      attackSequence.AddChild(playerInRange);
      attackSequence.AddChild(attackAction);
      chaseSequence.AddChild(chaseAction);


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
         Debug.Log("player Entered!");
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
      transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * 2);
      return BTNodeState.Running;
   }

   private bool IsPlayerInRange()
   {
      if (target == null)
      {
         return false;
      }

      float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), 
         new Vector2(target.position.x, target.position.y));
      return distance <= AttackRange;
   }
   
   
}
