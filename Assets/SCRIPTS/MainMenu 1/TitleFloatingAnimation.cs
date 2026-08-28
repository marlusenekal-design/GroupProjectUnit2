using UnityEngine;

public class TitleFloatingAnimation : MonoBehaviour
{
    [Header("Hover Motion")]
    [SerializeField] private float hoverDistance = 15f;
    [SerializeField] private float hoverSpeed = 2f;

    [Header("Pulse Effect")]
    [SerializeField] private float pulseAmount = 0.05f;
    [SerializeField] private float pulseSpeed = 1.5f;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Start()
    {
        startPosition = transform.localPosition;
        startScale = transform.localScale;
    }

    private void Update()
    {
        // Smooth vertical floating
        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverDistance;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);

        // Subtle breathing scale
        float scaleOffset = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = startScale * scaleOffset;
    }
}