using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;

    [Header("Screen Boundaries")]
    public float xBound = 8.5f;
    public float yBound = 4.5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private Weapon weapon;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize(); // Normalize to prevent faster diagonal movement

        RotateTowardMouse();

        if (Input.GetMouseButton(0) && weapon != null)
        {
            weapon.Fire();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xBound, xBound);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -yBound, yBound);
        transform.position = clampedPosition;
    }

    void RotateTowardMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

}
