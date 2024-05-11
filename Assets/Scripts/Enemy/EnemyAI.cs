using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Target;
    
    [SerializeField]
    private float AttackDelay;

    [SerializeField] private float AttackRange;

    public float MoveSpeed;
    
    
    

    private void Update()
    {
        AttackDelay -= Time.deltaTime;

        if (AttackDelay < 0)
            AttackDelay = 0;

        float distance = Vector2.Distance(transform.position, Target.position);

        if (distance == 0 && distance <= AttackRange)
        {
            
        }
        
    }

    void MoveToTarget()
    {
        float Xdir = Target.position.x - transform.position.x;
        float Ydir = Target.position.y - transform.position.y;

        Xdir = (Xdir < 0) ? -1 : 1;
        Ydir = (Ydir < 0) ? -1 : 1;
        
        transform.Translate(new Vector2(Xdir, Ydir) * MoveSpeed * Time.deltaTime);
        
    }
}
