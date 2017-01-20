using UnityEngine;

public class Human: MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new SpriteRenderer renderer;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Beach>())
        {
            ReachedBeach();
            return;
        }
        if (other.gameObject.GetComponent<Rock>())
        {
            HitRock();
            return;
        }
    }

    private void ReachedBeach()
    {
        rigidbody.isKinematic = true;
        renderer.color = Color.green;
    }

    private void HitRock()
    {
        rigidbody.isKinematic = true;
        renderer.color = Color.red;
    }
}