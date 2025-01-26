using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleShooting : MonoBehaviour
{
    public GameObject chargedBulletPrefab;
    public GameObject bulletPrefab;
    GameObject bullet;
    public float shootForce = 10f;
    public float sizeChangeAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float massFactor = 500f;
    public  float currentSize = 1f;
    private BubbleMovement movement;
    private Collider2D playerCollider;
    private bool canShoot=true;
    Animator childAnimator;
    bool maxSizeReached=false;
    Canvas canvas;
    private Rigidbody2D rb;
    public float chargeSpeed = 5f;
    public float maxChargeForce = 20f;

    
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private float currentCharge = 0f;
    private bool isCharging = false;
    SpriteRenderer childSpriteRenderer;
    private bool chargedBullet=false;

    void Start()
    {
        chargedBullet=false;
        rb = GetComponent<Rigidbody2D>();
    childAnimator = gameObject.GetComponentInChildren<Animator>();
    childSpriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

    if (childSpriteRenderer == null)
    {
        Debug.LogError("SpriteRenderer non trovato nel GameObject figlio!");
    }

    childAnimator.speed = 1;
    canvas = FindObjectOfType<Canvas>();

    movement = GetComponent<BubbleMovement>();
    playerCollider = GetComponent<Collider2D>();
    currentSize = transform.localScale.x;            

    if (movement.isPlayerOne)
        childAnimator.Play("FishSwimmingYellow");
    else
        childAnimator.Play("FishSwimmingRed");


    }

    void Update()
    {
        if (!canShoot || movement.isGameEnded)
            return;

        if ((movement.isPlayerOne && Input.GetButton("Fire1")) ||
            (!movement.isPlayerOne && Input.GetButton("Fire2")))
        {
            StartCharging();
        }

        if ((movement.isPlayerOne && Input.GetButtonUp("Fire1")) ||
            (!movement.isPlayerOne && Input.GetButtonUp("Fire2")))
        {
            ShootCharged();
        }
    }

    void StartCharging()
    {
        chargedBullet=false;
        isCharging = true;
        currentCharge += chargeSpeed * Time.deltaTime;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxChargeForce);

        float chargeRatio = currentCharge / maxChargeForce;
        if(movement.isPlayerOne)
        childSpriteRenderer.color = Color.Lerp(Color.white, new Color(1f, 0.53f, 0f), chargeRatio);
        else
        childSpriteRenderer.color = Color.Lerp(Color.white, new Color(0.51f, 0.19f, 1f), chargeRatio);
        if(chargeRatio==1 && currentSize>=1){
            Debug.Log("bang");
            chargedBullet=true;
        }
    }

    void ShootCharged()
    {
        if (!isCharging) return;

        isCharging = false;
        Vector2 shootDirection = movement.GetLastDirection();
        shootDirection.y = 0;
        shootDirection.Normalize();

        Vector3 shootPosition = transform.position + (Vector3)shootDirection * (currentSize * 0.6f);
        if(chargedBullet)
        bullet = Instantiate(chargedBulletPrefab, shootPosition, Quaternion.identity);
        else
        bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * (shootForce + currentCharge);

        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }
        if(chargedBullet)
        ChangeBubbleSize(-sizeChangeAmount*5);
        else
        ChangeBubbleSize(-sizeChangeAmount);

        currentCharge = 0f;
        childSpriteRenderer.color = Color.white;
    }


    void Shoot()
    {
        Vector2 shootDirection = movement.GetLastDirection();
        shootDirection.y = 0;
        shootDirection.Normalize();

        
        Vector3 shootPosition = transform.position + (Vector3)shootDirection * (currentSize * 0.6f);

        GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        //bullet.transform.localScale=transform.localScale * currentSize/5;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * shootForce;

        AudioManager.instance.PlaySoundSFX("BubbleShoot");
        
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

        if(maxSizeReached && amount>0){
            if(movement.isPlayerOne)
            childAnimator.Play("ExplodingFishYellow");
            else
            childAnimator.Play("ExplodingFishRed");
            
            CameraShake.instance.Shake(0.5f, 0.1f);
            AudioManager.instance.PlaySoundSFX("Explosion");

            childAnimator.speed =1;
            movement.GetComponent<BubbleMovement>().isGameEnded=true;
            if(movement.isPlayerOne)
            canvas.GetComponent<CanvasManager>().endGame("Red");
            else
                canvas.GetComponent<CanvasManager>().endGame("Yellow");


        }else if(maxSizeReached && amount<0){

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
        rb.mass = currentSize*massFactor;
    }
      
}   