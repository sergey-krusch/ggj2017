using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay: MonoBehaviour
{
    public RoundWaveSpawner WaveSpawner;

    public void Awake()
    {
        Session.Score = 0;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            WaveSpawner.Spawn(point);
        }
    }

    public void RestartClick()
    {
        SceneManager.LoadScene("GameOver");
    }
}