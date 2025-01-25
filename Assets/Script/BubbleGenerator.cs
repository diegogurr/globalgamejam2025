using System.Collections;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.1f;
    public float maxSize = 0.5f;
    public float minTime = 1f;
    public float maxTime = 3f;

    public float minShootForce = 1f;
    public float maxShootForce = 10f;
    public float coneAngle = 30f; // Angolo del cono di sparo in gradi

    void Start()
    {
        StartCoroutine(ShootBubblesRoutine());
    }

    IEnumerator ShootBubblesRoutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(minTime, maxTime);  
            yield return new WaitForSeconds(randomDelay);

            Shoot();
        }
    }

    void Shoot()
    {
        float randomSize = Random.Range(minSize, maxSize);
        float randomShootForce = Random.Range(minShootForce, maxShootForce);
        float randomAngle = Random.Range(-coneAngle / 2f, coneAngle / 2f);

        Vector3 shootDirection = Quaternion.Euler(0, 0, randomAngle) * Vector3.up;
        Vector3 shootPosition = transform.position + shootDirection * randomSize;
        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);

        bullet.transform.localScale = Vector3.one * randomSize;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * randomShootForce;
    }
}
