using UnityEngine;

public class LineWaveSpawner: MonoBehaviour
{
    public float SpawnInterval;
    public Vector2 SpawnPosition;
    public Vector2 TerminatePosition;
    public Transform Holder;
    public GameObject Prefab;
    private float timeRemainingToNextSpawn;

    public void FixedUpdate()
    {
        timeRemainingToNextSpawn -= Time.fixedDeltaTime;
        if (timeRemainingToNextSpawn <= 0.0f)
        {
            timeRemainingToNextSpawn = SpawnInterval;
            Spawn();
        }
    }

    public void Spawn()
    {
        var obj = Instantiate(Prefab);
        obj.transform.SetParent(Holder, false);
        obj.transform.position = SpawnPosition;
        var lineWave = obj.GetComponent<LineWave>();
        lineWave.TerminatePosition = TerminatePosition;
    }
}