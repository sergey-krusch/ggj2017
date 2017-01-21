using Configuration;
using UnityEngine;

public class RoundWave: MonoBehaviour
{
    private RoundWaveConfig config;
    private new SpriteRenderer renderer;
    private float radius;
    private float elapsedTime;

    public void Awake()
    {
        config = Root.Instance.RoundWave;
        renderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime >= config.Lifetime)
        {
            Destroy(gameObject);
            return;
        }
        float t = elapsedTime / config.Lifetime;
        radius = t * config.MaxRadius;
        transform.localScale = radius * Vector2.one;
        renderer.color = config.Color.Evaluate(t);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 waveCenter = transform.position;
        var otherPosition = other.attachedRigidbody.position;
        var direction = (otherPosition - waveCenter).normalized;
        other.attachedRigidbody.AddForce(config.Force * direction);
    }
}