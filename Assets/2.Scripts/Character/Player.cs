using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character, ICharacter
{
    Enemy enemy;

    float ListAttackTime = 0;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        if (Time.time - ListAttackTime >= AttackSpeed)
        {
            ListAttackTime = Time.time;
            
            Attack();
        }
    }

    public void Move()
    {

    }

    public void Attack()
    {
        Debug.Log("АјАн");
        enemy.hit(Damage());
    }

    public float Damage()
    {
        return Atk;
    }

    public void hit(float Damage)
    {
        curHp -= Damage;
    }

}
