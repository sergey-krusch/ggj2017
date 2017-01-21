using Configuration;
using UnityEngine;

public class LineWaveSpawner: MonoBehaviour
{
    public float SpawnInterval;
    public Vector2 SpawnPosition;
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
        obj.transform.position = SpawnPosition;
        var lineWave = obj.GetComponent<LineWave>();
        lineWave.TerminatePosition = TerminatePosition;
    }
}