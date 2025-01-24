using UnityEngine;

public class BubbleShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootForce = 10f;
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    private float currentSize = 1f;
    private BubbleMovement movement;
    private Collider2D playerCollider;

    void Start()
    {
        movement = GetComponent<BubbleMovement>();
        playerCollider = GetComponent<Collider2D>();
        currentSize = transform.localScale.x;
    }

    void Update()
    {
        if ((movement.isPlayerOne && Input.GetButtonDown("Fire1")) ||
            (!movement.isPlayerOne && Input.GetButtonDown("Fire2")))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 shootDirection = movement.GetLastDirection();
        shootDirection.y = 0;
        shootDirection.Normalize();

        Vector3 shootPosition = transform.position + (Vector3)shootDirection * (currentSize * 0.6f);

        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * shootForce;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetOwner(this);
        }

        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }

        ChangeBubbleSize(-sizeChangeAmount);
    }

    public void ChangeBubbleSize(float amount)
    {
        currentSize = Mathf.Clamp(currentSize + amount, minSize, maxSize);
        transform.localScale = Vector3.one * currentSize;
    }
}