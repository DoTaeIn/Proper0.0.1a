using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Default Values
    public float HP = 100f;

    public float Stamina = 100f;

    public float Speed = 5f;

    public float Run_Speed = 8f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    
    //Default Functions
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }
    
    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hostile")
        {
            OnDamaged(other.transform.position, other.gameObject.GetComponent<EnemyInfo>());
            Vector2 dir = (transform.position - other.transform.position).normalized;
            _rigidbody2D.AddForce(dir * 8, ForceMode2D.Impulse);
        }
    }
    


    //Custom Functions
    void Move()
    {	
        float h;
        float v;
        
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        
        Vector2 moveVec = new Vector2(h, v);
        _rigidbody2D.velocity = moveVec * Speed;
    }
    
    void OnDamaged(Vector2 targetPos, EnemyInfo enemy)
    {
        _spriteRenderer.color = new Color(1, 0, 0, 1f);
        GetDamage(enemy.Damage);
        
        Invoke("OffDamaged", 1);
    }

    void OffDamaged()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    public void GetDamage(float damage)
    {
        HP -= damage;
        Stamina -= damage * 0.3f;
    }
    


}
