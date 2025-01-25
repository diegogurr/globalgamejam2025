using System.Collections;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
     public GameObject bulletPrefab;
    
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    public float randomSize = 1f;
    private Collider2D playerCollider;
    private float minShootForce=1f;
    private float maxShootForce=10f;

    void Start(){
       StartCoroutine(ShootBubblesRoutine());
    }

    IEnumerator ShootBubblesRoutine()
    {
        while (true)
        {
            float randomDelay = Random.Range(1f, 5f);  
            yield return new WaitForSeconds(randomDelay);

            Shoot();
        }
    }

    void Shoot()
    {
        // Genera dimensione e forza casuali
        float randomSize = Random.Range(minSize, maxSize);
        float randomShootForce = Random.Range(minShootForce, maxShootForce);

        Vector3 shootPosition = transform.position + Vector3.up * (randomSize * 0.6f);
        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);

        bullet.transform.localScale = Vector3.one * randomSize;
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector3.up * randomShootForce;

        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
    }

}
