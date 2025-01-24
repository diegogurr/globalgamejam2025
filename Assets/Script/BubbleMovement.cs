using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float baseSpeed = 5f; 
    public float gravityFactor = 2f; 

    public bool isPlayerOne = true;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = isPlayerOne ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        float vertical = isPlayerOne ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2");

        Vector2 movement = new Vector2(horizontal, AdjustVerticalSpeed(vertical));
        rb.linearVelocity = movement * baseSpeed;
    }

    private float AdjustVerticalSpeed(float input)
    {
        float size = transform.localScale.x;

        if (input > 0)
        {
            return input * (size / gravityFactor);
        }
        else if (input < 0) 
        {
            return input / (size * gravityFactor);
        }

        return 0;
    }
}