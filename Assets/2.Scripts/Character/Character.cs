using System;
using System.Collections;
using System.Collections.Generic;
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
}

[Serializable]
public class Character : MonoBehaviour
{
    [field: Header("Stat")]
    [field: SerializeField] protected float maxHp { get; private set; } = 100;
    [field: SerializeField] protected float curHp;
    [field: SerializeField] protected float maxMp { get; private set; } = 100;
    protected float curMp;
    [field: SerializeField] protected float Atk { get; private set; } = 5;
    protected float curAtk;
    [field: SerializeField] protected float Def { get; private set; } = 5;
    protected float curDef;

    [field: SerializeField] protected float Lv { get; private set; } = 0;
    protected float Exp;
    protected float curExp;

    [field: SerializeField] protected float AttackSpeed { get; private set; } = 1f;
    [field: SerializeField] protected float AttackRange { get; private set; } = 2f;


    public void Awake()
    {

    }




}
