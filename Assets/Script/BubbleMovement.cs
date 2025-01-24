using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    public float gravityFactor = 2f;
    public bool isPlayerOne = true;
    public Transform otherPlayer; 

    private Rigidbody2D rb;
    private float bubbleSize = 1f;
    private Vector2 lastDirection = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bubbleSize = transform.localScale.x;

        float horizontal = isPlayerOne ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        float vertical = isPlayerOne ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2");

        Vector2 movement = new Vector2(horizontal, AdjustVerticalSpeed(vertical));

        /*if (Mathf.Abs(horizontal) > 0.1f)
        {
            lastDirection = new Vector2(horizontal, 0).normalized;
        }*/

        rb.linearVelocity = movement * baseSpeed;

        RotateTowardsOtherPlayer();
    }

    private float AdjustVerticalSpeed(float input)
    {
        if (bubbleSize <= 0) bubbleSize = 0.1f;

        if (input > 0)
        {
            return input * (bubbleSize / gravityFactor);
        }
        else if (input < 0)
        {
            return input / (bubbleSize * gravityFactor);
        }

        return 0;
    }

    private void RotateTowardsOtherPlayer()
    {
        if (!otherPlayer) return;

        float directionX = otherPlayer.position.x - transform.position.x;
        float angle = directionX > 0 ? 0f : 180f;

        transform.rotation = Quaternion.Euler(0, angle, 0);
        lastDirection = directionX > 0 ? Vector2.right : Vector2.left;
    }

    public Vector2 GetLastDirection()
    {
        return lastDirection;
    }
}