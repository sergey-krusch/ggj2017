using Configuration;
using UnityEngine;

public class LineWaveSpawner: MonoBehaviour
{
    public float SpawnInterval;
    public Vector2 SpawnPosition;
    public Vector2 SpawnPoisitionOffset;
    public Vector2 TerminatePosition;
    public Transform Holder;
    public GameObject Prefab;
    public float TimeRemainingToNextSpawn;

    public void Awake()
    {
    }

    public void FixedUpdate()
    {
        TimeRemainingToNextSpawn -= Time.fixedDeltaTime;
        if (TimeRemainingToNextSpawn <= 0.0f)
        {
            TimeRemainingToNextSpawn = SpawnInterval;
            Spawn();
        }
    }

    public void Spawn()
    {
        var obj = Instantiate(Prefab);
        obj.transform.SetParent(Holder, false);
        float offsetAmount = Random.Range(-1.0f, 1.0f);
        obj.transform.position = SpawnPosition + offsetAmount * SpawnPoisitionOffset;
        var lineWave = obj.GetComponent<LineWave>();
        lineWave.TerminatePosition = TerminatePosition;
    }
}