using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    public float explosionRadius = 3f;
    public int explosionDamage = 75;

    protected override void Update()
    {
        
        base.Update();

        
        if (playerTransform != null)
        {
            MoveToward(playerTransform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D obj in hitObjects)
        {
            if (obj.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject);
    }
}