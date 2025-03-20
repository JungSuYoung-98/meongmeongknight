using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, ICharacter
{
    public  void Move()
    {
    }

    public  void Attack()
    {
    }

    public  float Damage()
    {
        return 0;
    }

    public void hit(float Damage)
    {
        curHp -= Damage;
    }

}
