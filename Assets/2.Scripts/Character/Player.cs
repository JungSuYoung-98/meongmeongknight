using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    private static Player instance = null;
    Vector3 NextStagePosition = new Vector3(7.3f, 0, -12.3f);
    bool AttackCnt = false;

    public float Exp = 3; 
    public float curExp = 0;
    
    public float AddHp;
    public float AddAtk;
    public float AddDef;

    Item Item;

    public static Player Instance
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
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        Item = GetComponent<Item>();
    }

    private void FixedUpdate()
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

        if (Enemy.Instance == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextStagePosition, MoveSpeed * Time.deltaTime);
            TrgatDistance = Vector3.Distance(transform.position, NextStagePosition);
            return;
        }
        else
        {
            TrgatDistance = Vector3.Distance(transform.position, Enemy.Instance.transform.position);

            if (AttackRange >= TrgatDistance)
            {
                animator.SetBool("IsMove", false);
                transform.position = transform.position;
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, Enemy.Instance.transform.position, MoveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (Enemy.Instance == null || Player.Instance.IsDie) return;

        if (Time.time - ListAttackTime >= AttackSpeed && AttackRange >= TrgatDistance && !Enemy.Instance.IsDie)
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
            Enemy.Instance.hit(Damage());
        }
    }

    public void KillEnemy(int AddExp, int AddGold,Item AddItem)
    {
        curExp += AddExp;
        Gold += AddGold;

        for(int i= 0; i< UIManager.Instance.InventoryuI.slot.Length; i++)
        {
            if (UIManager.Instance.InventoryuI.slot[i].item == null)
            {
                UIManager.Instance.InventoryuI.slot[i].AddItem(AddItem, i);
                return;
            }
        }

        if (Exp <= curExp)
        {
            Lv++;
            curExp = curExp - Exp;
            Exp = Lv * 3;
        }

    }

    protected override void Die()
    {
        base.Die();
    }

    public void Equipped()
    {
        maxHp = maxHp + Item.Data.Hp;
        Atk = Atk + Item.Data.Atk;
        Def = Def + Item.Data.Def;
    }
}
