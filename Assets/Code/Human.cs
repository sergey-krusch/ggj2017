using System;
using Configuration;
using UnityEngine;

public class Human: MonoBehaviour, IStateReporterListener
{
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private HumanConfig config;
    private float angularSpeed;
    private bool isSaving;
    private Vector2 savingDirection;

    public Action<int> Saved;
    public Action Drowned;
    public Action Destroyed;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        if (!rigidbody.isKinematic)
        {
            var a = transform.localRotation.eulerAngles.z;
            a += angularSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, a);
        }
        if (isSaving)
            transform.position += transform.up * config.SavingSpeed * Time.deltaTime;
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
        if (rigidbody.isKinematic)
            return;
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
        isSaving = true;
        savingDirection = rigidbody.velocity.normalized;
        rigidbody.isKinematic = true;
        if (Saved != null)
            Saved(beach.Multiplier);
        animator.SetTrigger("Saved");
        return true;
    }

    private void HitRock()
    {
        rigidbody.isKinematic = true;
        if (Drowned != null)
            Drowned();
        animator.SetTrigger("Drowned");
    }

    public void Enter(int nameHash)
    {
        if (nameHash == Animator.StringToHash("Done"))
        {
            if (Destroyed != null)
                Destroyed();
            Destroy(gameObject);
        }
    }

    public void Exit(int nameHash)
    {
    }
}