using UnityEngine;

public class DebuffEnemy : Enemy
{
    private float targetSpeed;
    //attack variables
    [SerializeField] private float attackTime = 1;
    [SerializeField] private float weaponDamage = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float attackRange;
    //[SerializeField] private Bullet bulletPrefab;


    float timer = 1;

    protected override void Start()
    {
        base.Start();
        targetSpeed = moveSpeed;
        weapon = new Weapon("DebuffEnemy Weapon", weaponDamage, bulletSpeed);
    }
    protected override void Update()
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= attackRange)
        {
            moveSpeed = 0;
            Attack(attackTime);
        }
        else
        {
            moveSpeed = targetSpeed;
        }
    }
    public override void Attack(float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
        }
    }
    public void SetupDebuffEnemy(float desiredAttackRange, float desiredAttackTime)
    {
        attackRange = desiredAttackRange;
        attackTime = desiredAttackTime;
    }
    public override void Shoot()
    {
        weapon.Shoot(bulletPrefab, this, "Player", 5);
    }
}
