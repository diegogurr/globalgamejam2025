using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    private BubbleShooting owner;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetOwner(BubbleShooting shooter)
    {
        owner = shooter;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            BubbleShooting hitBubble = other.GetComponent<BubbleShooting>();
            if (hitBubble != null)
            {
                hitBubble.ChangeBubbleSize(-owner.sizeChangeAmount);
                owner.ChangeBubbleSize(owner.sizeChangeAmount);
            }
        }

        Destroy(gameObject);
    }
}