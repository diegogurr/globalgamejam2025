using UnityEngine;

public class BubbleShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootForce = 10f;
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    private float currentSize = 1f;
    private bool isPlayerOne;

    void Start()
    {
        isPlayerOne = GetComponent<BubbleMovement>().isPlayerOne;
        currentSize = transform.localScale.x;
    }

    void Update()
    {
        if (isPlayerOne)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 shootDirection = isPlayerOne ? Vector2.right : Vector2.left;
        rb.linearVelocity = shootDirection * shootForce;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript)
        {
            bulletScript.SetOwner(this);
        }

        ChangeBubbleSize(-sizeChangeAmount);
    }

    public void ChangeBubbleSize(float amount)
    {
        currentSize = Mathf.Clamp(currentSize + amount, minSize, maxSize);
        transform.localScale = Vector3.one * currentSize;
    }
}