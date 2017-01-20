using UnityEngine;

public class LineWave: MonoBehaviour
{
    public Vector2 Direction;
    public float Force;

    public void FixedUpdate()
    {
        Vector2 p = transform.position;
        p += Direction * Time.fixedDeltaTime;
        transform.position = p;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var direction = Direction.normalized;
        other.attachedRigidbody.AddForce(Force * direction);
    }
}