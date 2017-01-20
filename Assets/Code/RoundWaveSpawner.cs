using UnityEngine;

public class RoundWaveSpawner: MonoBehaviour
{
    public float MinSpawnInterval;
    public Transform Holder;
    public GameObject Prefab;

    public float terminationRadius;
    private float timeRemainingToNextSpawn;

    public void Start()
    {
        var h = 2 * Camera.main.orthographicSize;
        var w = h * Camera.main.aspect;
        terminationRadius = new Vector2(w, h).magnitude;
    }

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
        var wave = obj.GetComponent<RoundWave>();
        wave.TerminationRadius = terminationRadius;
    }
}