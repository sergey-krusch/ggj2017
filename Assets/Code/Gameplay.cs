using UnityEngine;

public class Gameplay: MonoBehaviour
{
    public RoundWaveSpawner WaveSpawner;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            WaveSpawner.Spawn(point);
        }
    }
}