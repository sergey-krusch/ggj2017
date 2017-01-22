using System;
using Configuration;
using UnityEngine;

public class Human: MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new SpriteRenderer renderer;
    private HumanConfig config;
    private float angularSpeed;

    public Action<int> Saved;
    public Action Drowned;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        config = Root.Instance.Human;
        angularSpeed = 0.0f;
    }

    public void FixedUpdate()
    {
        if (rigidbody.velocity.magnitude > config.MaxVelocity)
            rigidbody.velocity = config.MaxVelocity * rigidbody.velocity.normalized;
    }

    public void Update()
    {
        var a = transform.localRotation.eulerAngles.z;
        a += angularSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, a);
    }

    public void HitByWave()
    {
        var n = UnityEngine.Random.Range(-1.0f, 1.0f);
        angularSpeed += n * config.MaxAngularVelocityChange;
        angularSpeed = Mathf.Clamp(
            angularSpeed,
            -config.MaxAngularVelocity,
            config.MaxAngularVelocity);
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