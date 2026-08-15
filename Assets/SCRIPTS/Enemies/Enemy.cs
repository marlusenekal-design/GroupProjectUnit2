using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    
    protected Transform playerTransform;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    protected virtual void Update()
    {
        if (playerTransform == null)
        {
            return;
        }
        RotateTowardsPlayer();
    }

    protected void RotateTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }

    protected virtual void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnEnemyKilled(this);
        }
    }
}
