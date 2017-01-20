using UnityEngine;

public class RoundWave: MonoBehaviour
{
    public float GrowthSpeed;
    public float Force;
    public float TerminationRadius;
    private float radius;

    public void FixedUpdate()
    {
        radius += GrowthSpeed * Time.fixedDeltaTime;
        transform.localScale = radius * Vector2.one;
        if (radius >= TerminationRadius)
            Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 waveCenter = transform.position;
        var otherPosition = other.attachedRigidbody.position;
        var direction = (otherPosition - waveCenter).normalized;
        other.attachedRigidbody.AddForce(Force * direction);
    }
}