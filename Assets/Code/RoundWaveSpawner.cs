using UnityEngine;

public class RoundWaveSpawner: MonoBehaviour
{
    public float MinSpawnInterval;
    public Transform Holder;
    public GameObject Prefab;

    private float timeRemainingToNextSpawn;

    public void FixedUpdate()
    {
        timeRemainingToNextSpawn -= Time.fixedDeltaTime;
    }

    public void Spawn(Vector2 point)
    {
        if (timeRemainingToNextSpawn > 0.0f)
            return;
        timeRemainingToNextSpawn = MinSpawnInterval;
        var obj = Instantiate(Prefab);
        obj.transform.SetParent(Holder, false);
        obj.transform.position = point;
    }
}