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

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            BubbleShooting hitBubble = other.GetComponent<BubbleShooting>();
            if (hitBubble != null)
            {
                hitBubble.ChangeBubbleSize(owner.sizeChangeAmount);
                //owner.ChangeBubbleSize(owner.sizeChangeAmount);
            }
        }

        Destroy(gameObject);
    }
}