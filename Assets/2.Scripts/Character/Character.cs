using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public interface ICharacter
{
    public void Move()
    {
    }

    public void Attack()
    {
    }
    public float Damage()
    {
        return 0;
    }

    public void hit(int Damage)
    {

    }

    public void Die()
    {

    }
}

[Serializable]
public class Character : MonoBehaviour, ICharacter
{
    [field: Header("Stat")]
    [field: SerializeField] public float maxHp { get; protected set; } = 100;
    [field: SerializeField] public float curHp;
    [field: SerializeField] public float Atk { get; protected set; } = 5;
    public float curAtk;
    [field: SerializeField] public float Def { get; protected set; } = 5;
    public float curDef;

    [field: SerializeField] public int Lv { get; protected set; } = 1;

    [field: SerializeField] public float AttackSpeed { get; protected set; } = 1f;
    [field: SerializeField] public float AttackRange { get; protected set; } = 2f;

    [field: SerializeField] public float MoveSpeed;

    [field: SerializeField] public int Gold;

    protected float ListAttackTime = 0;
    protected float TrgatDistance = 0;

    public bool IsDie = false;

    protected Animator animator;

    protected virtual void Awake()
    {
        curHp = maxHp;
    }

    public float Damage()
    {
        return Atk;
    }

    public virtual void hit(float Damage)
    {
        curHp -= Mathf.Clamp(Damage - Def, 0 ,maxHp);

        if (curHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDie = true;
        animator.SetBool("IsDie", true);
    }



}
