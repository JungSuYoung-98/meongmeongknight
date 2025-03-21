using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TrgatDistance = Vector3.Distance(transform.position, player.transform.position);
        Move();
        Attack();
    }


    private void Move()
    {
        if (AttackRange >= TrgatDistance)
        {
            animator.SetBool("IsMove", false);
            transform.position = transform.position;
            return;
        }

        animator.SetBool("IsMove", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Time.time - ListAttackTime >= AttackSpeed && AttackRange >= TrgatDistance)
        {
            animator.SetTrigger("IsAttack");
            ListAttackTime = Time.time;
            Debug.Log($"{this.name} : АјАн");
            player.hit(Damage());
        }
    }
}
