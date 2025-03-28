using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Enemy : Character
{

    public Item[] items;
    private static Enemy instance = null;

    public static Enemy Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (null == instance) instance = this;

        Gold = Random.Range(Lv*100,Lv*1000)/10 * 10;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (null == instance) instance = this;
        transform.localPosition = Vector3.zero;
        IsDie = false;
        curHp = maxHp;
        Gold = Random.Range(Lv * 100, Lv * 1000) / 10 * 10;
    }

    private void FixedUpdate()
    {
        TrgatDistance = Vector3.Distance(transform.position, Player.Instance.transform.position);
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
        transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (Player.Instance.IsDie) return;

        if (Time.time - ListAttackTime >= AttackSpeed && AttackRange >= TrgatDistance )
        {
            animator.SetTrigger("IsAttack");
            ListAttackTime = Time.time;
            Player.Instance.hit(Damage());
        }
    }
    protected override void Die()
    {
        base.Die();
        Player.Instance.KillEnemy(Lv, Gold, items[Random.Range(0, items.Length)]); // �������
        UIManager.Instance.baseUI.BaseUIUpdate();
    }
    public void SetActiveEnd()
    {
        animator.SetBool("IsDie", false);
        instance = null;
        gameObject.SetActive(false);
    }
}
