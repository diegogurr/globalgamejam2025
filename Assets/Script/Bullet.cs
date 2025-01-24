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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BubbleShooting hitBubble = other.gameObject.GetComponent<BubbleShooting>();
            if (hitBubble != null)
            {
                hitBubble.ChangeBubbleSize(owner.sizeChangeAmount);
                //owner.ChangeBubbleSize(owner.sizeChangeAmount);
            }
        }

        Destroy(gameObject);
    }
}