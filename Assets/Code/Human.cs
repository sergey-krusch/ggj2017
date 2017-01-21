using System;
using Configuration;
using UnityEngine;

public class Human: MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new SpriteRenderer renderer;

    public Action<int> Saved;
    public Action Drowned;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > Root.Instance.MaxHumanVelocity)
            rigidbody.velocity = Root.Instance.MaxHumanVelocity * rigidbody.velocity.normalized;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (ReachedBeach(other))
            return;
        if (other.gameObject.GetComponent<Rock>())
        {
            HitRock();
            return;
        }
    }

    private bool ReachedBeach(Collider2D other)
    {
        var beach = other.gameObject.GetComponent<Beach>();
        if (beach == null)
            return false;
        rigidbody.isKinematic = true;
        renderer.color = Color.green;
        if (Saved != null)
            Saved(beach.Multiplier);
        return true;
    }

    private void HitRock()
    {
        rigidbody.isKinematic = true;
        renderer.color = Color.red;
        if (Drowned != null)
            Drowned();
    }
}