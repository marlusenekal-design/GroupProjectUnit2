using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFXController : MonoBehaviour
{
    public static ScreenFXController Instance { get; private set; }

    [Header("UI Flash Overlays")]
    [SerializeField] private Image lowHealthImage;
    [SerializeField] private Image nukeFlashImage;

    [Header("Low Health Settings")]
    [SerializeField] private float pulseSpeed = 5f;
    [SerializeField] private float maxRedAlpha = 0.5f;
    private bool isLowHealthActive = false;

    private Coroutine nukeFlashCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Update()
    {
        if (isLowHealthActive && lowHealthImage != null)
        {
            float alpha = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f * maxRedAlpha;
            SetImageAlpha(lowHealthImage, alpha);
        }
        else if (lowHealthImage != null && lowHealthImage.color.a > 0f)
        {
            SetImageAlpha(lowHealthImage, 0f);
        }
    }

    public void SetLowHealthState(bool isLow)
    {
        isLowHealthActive = isLow;
    }

    public void TriggerNukeImpact(float shakeDuration = 0.4f, float shakeMagnitude = 0.5f)
    {
        if (CameraShake.Instance != null)
        {
            CameraShake.Instance.Shake(shakeDuration, shakeMagnitude);
        }

        if (nukeFlashImage != null)
        {
            if (nukeFlashCoroutine != null) StopCoroutine(nukeFlashCoroutine);
            nukeFlashCoroutine = StartCoroutine(NukeFlashRoutine());
        }
    }

    private IEnumerator NukeFlashRoutine()
    {
        SetImageAlpha(nukeFlashImage, 0.85f);

        float duration = 0.15f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.85f, 0f, elapsed / duration);
            SetImageAlpha(nukeFlashImage, alpha);
            yield return null;
        }

        SetImageAlpha(nukeFlashImage, 0f);
    }

    private void SetImageAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }
}