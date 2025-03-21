using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    Vector3 NextStagePosition = new Vector3(7.3f, 0, -12.3f);
    Enemy enemy;
    bool AttackCnt = false;

    public float Exp;
    public float curExp;


    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Attack();
    }

    private void OnEnable()
    {
        IsDie = false;
    }

    private void Move()
    {
        animator.SetBool("IsMove", true);

        if (enemy.gameObject.activeSelf == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextStagePosition, MoveSpeed * Time.deltaTime);
            TrgatDistance = Vector3.Distance(transform.position, NextStagePosition);
            return;
        }
        else
        {
            TrgatDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (AttackRange >= TrgatDistance)
            {
                animator.SetBool("IsMove", false);
                transform.position = transform.position;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, MoveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (Time.time - ListAttackTime >= AttackSpeed && AttackRange >= TrgatDistance && !enemy.IsDie)
        {
            if (AttackCnt)
            {
                animator.SetTrigger("IsAttack2");
            }
            else
            {
                animator.SetTrigger("IsAttack");

            }
            AttackCnt = !AttackCnt;
            ListAttackTime = Time.time;
            Debug.Log($"{this.name} : АјАн");
            enemy.hit(Damage());
        }
    }


}
