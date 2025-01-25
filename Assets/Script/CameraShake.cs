using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Transform camTransform;
    private Vector3 originalPos;
    private float shakeDuration = 0f;
    private float shakeIntensity = 0.2f;
    private float shakeDecay = 1.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        camTransform = Camera.main.transform;
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeIntensity;
            shakeDuration -= Time.deltaTime * shakeDecay;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Shake(float duration, float intensity)
    {
        shakeDuration = Mathf.Max(duration, 0.1f);
        shakeIntensity = Mathf.Max(intensity, 0.05f);
    }
}