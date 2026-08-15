using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float damage = 10f;

    public string ownerTag;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ownerTag) || other.CompareTag("Projectile"))
        {
            return;
        }

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage((int)damage);
        }

        Destroy(gameObject);
    }
}
