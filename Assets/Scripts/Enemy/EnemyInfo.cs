using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    enemy,
    trap,
    weapon,
}

public class EnemyInfo : MonoBehaviour
{
    public Type type;
    public float HP = 100f;
    public float Damage = 10f;
    public int CoolTime = 3;
    public float AttackRange = 1.5f;
    public float ChaseRange = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
