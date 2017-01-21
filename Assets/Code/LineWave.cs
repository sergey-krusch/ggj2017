using Configuration;
using UnityEngine;

public class LineWave: MonoBehaviour
{
    public float Speed;
    public Vector2 TerminatePosition;
    private Vector2 initialTerminateDelta;

    public void Start()
    {
        initialTerminateDelta = TerminatePosition - (Vector2)transform.position;
    }

    public void Update()
    {
        Vector2 terminateDelta = TerminatePosition - (Vector2)transform.position;
        if (Vector2.Dot(terminateDelta, initialTerminateDelta) < 0)
            Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        transform.position += transform.up * Speed * Time.fixedDeltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var human = other.GetComponent<Human>();
        if (human == null)
            return;
        var direction = transform.up.normalized;
        var force = Root.Instance.LineWave.Force;
        other.attachedRigidbody.AddForce(force * direction);
        human.HitByWave();
    }

}