using UnityEngine;

public class Enemy : Character
{
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
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
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
        if (Time.time - ListAttackTime >= AttackSpeed && AttackRange >= TrgatDistance)
        {
            animator.SetTrigger("IsAttack");
            ListAttackTime = Time.time;
            Debug.Log($"{this.name} : АјАн");
            Player.Instance.hit(Damage());
        }
    }
    protected override void Die()
    {
        base.Die();
        Player.Instance.KillEnemy(Lv, Gold);
    }
}
