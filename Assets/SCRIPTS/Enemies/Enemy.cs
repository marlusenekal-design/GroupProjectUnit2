using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    public static float moveSpeed = 3f;
    private bool hasEnteredScreen = false;
    public bool clampToScreen = true;
    public float screenPadding = 0.5f;

    protected Transform playerTransform;
    protected Rigidbody2D rb;
    private Camera mainCamera;

    [Header("Drops")]
    public GameObject powerUpPrefab;

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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    protected void MoveToward(Vector3 targetPosition)
    {
        Vector2 Direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(rb.position + Direction * moveSpeed * Time.deltaTime);
    }

    protected void ClampToScreenBounds()
    {
        if (mainCamera == null)
        {
            return;
        }

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (!hasEnteredScreen)
        {
            if (viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f)
            {
                hasEnteredScreen = true;
            }
            else
            {
                return;
            }
        }

        float paddingX = screenPadding / (mainCamera.orthographicSize * mainCamera.aspect * 2f);
        float paddingY = screenPadding / (mainCamera.orthographicSize * 2f);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f + paddingX, 1f - paddingX);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0f + paddingY, 1f - paddingY);

        transform.position = mainCamera.ViewportToWorldPoint(viewportPosition);
    }

    protected bool IsOnScreen()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return false;
        }

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        return viewportPosition.x >= 0f && viewportPosition.x <= 1f && viewportPosition.y >= 0f && viewportPosition.y <= 1f;
    }


    protected virtual void OnDestroy()
    {
        
        if (!Application.isPlaying) return;

        
        UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        
        if (activeScene.name == "MainMenu" || !gameObject.scene.isLoaded)
        {
            return;
        }

        
        GameOverManager gameOverMgr = Object.FindFirstObjectByType<GameOverManager>();
        if (gameOverMgr != null && gameOverMgr.gameOverPanel.activeSelf)
        {
            
            return;
        }
        if (powerUpPrefab != null && !Weapon.IsPowerUpPresentInScene)
        {
            
            float randomRoll = Random.Range(0f, 100f);

            if (randomRoll <= 30f) 
            {
                
                Weapon.IsPowerUpPresentInScene = true;

                
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }
        }


        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayEnemyDeath();
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnEnemyKilled(this);
        }
    }

}