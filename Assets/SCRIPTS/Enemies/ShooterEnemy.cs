using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class ShooterEnemy : Enemy
{
    public float stoppingDistance = 6f;
    public float retreatDistance = 3f;
    public float shootDistance = 8f;
    public int contactDamage = 5;

    private Weapon weapon;

    protected override void Start()
    {
        base.Start();
        weapon = GetComponent<Weapon>();
    }

    protected override void Update()
    {
        base.Update();
        if (playerTransform == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stoppingDistance)
        {
            MoveToward(playerTransform.position);
        }
        else if (distanceToPlayer < retreatDistance)
        {
            Vector3 retreatTarget = transform.position - (playerTransform.position - transform.position);
            MoveToward(retreatTarget);
        }

        if (weapon != null && distanceToPlayer <= shootDistance)
        {
            weapon.Fire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(contactDamage);
            }
        }
    }
}
