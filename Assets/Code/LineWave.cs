using UnityEngine;

public class LineWave: MonoBehaviour
{
    public Vector2 Direction;
    public Vector2 TerminatePosition;
    public float Force;
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
        Vector2 p = transform.position;
        p += Direction * Time.fixedDeltaTime;
        transform.position = p;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Human>() == null)
            return;
        var direction = Direction.normalized;
        other.attachedRigidbody.AddForce(Force * direction);
    }

}