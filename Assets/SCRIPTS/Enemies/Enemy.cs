using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public bool clampToScreen = true;
    public float screenPadding = 0.5f;

    protected Transform playerTransform;
    protected Rigidbody2D rb;
    private Camera mainCamera;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;


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

    protected virtual void LateUpdate()
    {
        if (clampToScreen)
        {
            ClampToScreenBounds();
        }
    }

    protected void RotateTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }

    protected void MoveToward(Vector3 targetPosition)
    {
       Vector2 Direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(rb.position + Direction * moveSpeed * Time.deltaTime);
    }

    protected void ClampToScreenBounds()
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        
        float paddingX = screenPadding / (mainCamera.orthographicSize * mainCamera.aspect * 2f);
        float paddingY = screenPadding / (mainCamera.orthographicSize * 2f);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f + paddingX, 1f - paddingX);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f + paddingY, 1f - paddingY);

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }

    protected virtual void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnEnemyKilled(this);
        }
    }
}
