using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootForce = 10f;
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    public  float currentSize = 1f;
    private BubbleMovement movement;
    private Collider2D playerCollider;
    private bool canShoot=true;
    Animator childAnimator;
    bool maxSizeReached=false;
    Canvas canvas;
    
    void Start()
    {
        
        childAnimator = gameObject.GetComponentInChildren<Animator>();

            childAnimator.speed = 1;
        canvas = FindObjectOfType<Canvas>();

        movement = GetComponent<BubbleMovement>();
        playerCollider = GetComponent<Collider2D>();
        currentSize = transform.localScale.x;            
        if(movement.isPlayerOne)
        childAnimator.Play("FishSwimmingYellow");
        else
        childAnimator.Play("FishSwimmingRed");


    }

    void Update()
    {
        if ((movement.isPlayerOne && Input.GetButtonDown("Fire1")) ||
            (!movement.isPlayerOne && Input.GetButtonDown("Fire2")))
        {
            if(canShoot)
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
        bullet.transform.localScale=transform.localScale * currentSize/5;
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
                    childAnimator.speed =1;

        currentSize = Mathf.Clamp(currentSize + amount, minSize, maxSize);
        Debug.Log("currentSize "+ currentSize+ " maxSize "+ maxSize);

        if(maxSizeReached && amount>0){
            if(movement.isPlayerOne)
            childAnimator.Play("ExplodingFishYellow");
            else
            childAnimator.Play("ExplodingFishRed");
            childAnimator.speed =1;
            movement.GetComponent<BubbleMovement>().isGameEnded=true;
            if(movement.isPlayerOne)
            canvas.GetComponent<CanvasManager>().endGame("Player two won");
            else
                canvas.GetComponent<CanvasManager>().endGame("Player one won");


        }else if(maxSizeReached && amount<0){
            Debug.Log("Print Diego "+ maxSizeReached + " " + amount);
            maxSizeReached=false;
            if(movement.isPlayerOne)
            childAnimator.Play("FishSwimmingYellow");
            else
            childAnimator.Play("FishSwimmingRed");
        }
        if(currentSize==maxSize){
            maxSizeReached=true;
            childAnimator.Play("SizeLimit");

            childAnimator.speed =1;

        }
        
        canShoot = !Mathf.Approximately(currentSize, minSize);
        
        transform.localScale = Vector3.one * currentSize;
    }
      
}
/* particellare
using UnityEngine;

public class BubbleShooting : MonoBehaviour
{
    public ParticleSystem shootParticleSystem;
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
        
        if (shootParticleSystem != null)
        {
            ParticleSystem instance = Instantiate(shootParticleSystem, shootPosition, Quaternion.LookRotation(shootDirection));
            instance.Play();
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

        ChangeBubbleSize(-sizeChangeAmount);
    }

    public void ChangeBubbleSize(float amount)
    {
        currentSize = Mathf.Clamp(currentSize + amount, minSize, maxSize);
        transform.localScale = Vector3.one * currentSize;
    }
}
*/