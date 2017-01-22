using Configuration;
using UnityEngine;

public class LineWaveSpawner: MonoBehaviour
{
    public float RewindTime;
    public float SpawnInterval;
    public Vector2 SpawnPosition;
    public Vector2 SpawnPoisitionOffset;
    public Vector2 TerminatePosition;
    public Transform Holder;
    public GameObject Prefab;
    private float timeRemainingToNextSpawn;

    public void Awake()
    {
        Rewind(RewindTime);
    }

    public void FixedUpdate()
    {
        timeRemainingToNextSpawn -= Time.fixedDeltaTime;
        if (timeRemainingToNextSpawn <= 0.0f)
        {
            timeRemainingToNextSpawn = SpawnInterval;
            Spawn();
        }
    }

    private LineWave Spawn()
    {
        var obj = Instantiate(Prefab);
        obj.transform.SetParent(Holder, false);
        float offsetAmount = Random.Range(-1.0f, 1.0f);
        obj.transform.position = SpawnPosition + offsetAmount * SpawnPoisitionOffset;
        var lineWave = obj.GetComponent<LineWave>();
        lineWave.TerminatePosition = TerminatePosition;
        return lineWave;
    }

    private void Rewind(float deltaTime)
    {
        timeRemainingToNextSpawn -= deltaTime;
        while (timeRemainingToNextSpawn <= 0.0f)
        {
            timeRemainingToNextSpawn += SpawnInterval;
            var wave = Spawn();
            wave.Rewind(deltaTime);
            deltaTime -= SpawnInterval;
        }
    }
}