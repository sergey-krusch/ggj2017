using Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay: MonoBehaviour
{
    public RoundWaveSpawner WaveSpawner;
    public Transform HumanContainer;
    public Text ScoreLabel;

    public void Awake()
    {
        Session.Score = 0;
        foreach (var human in HumanContainer.GetComponentsInChildren<Human>())
        {
            human.Saved += HumanSaved;
            human.Drowned += HumanDrowned;
        }
    }

    public void Update()
    {
        ScoreLabel.text = Session.Score.ToString();
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

    private void HumanSaved(int multiplier)
    {
        Session.Score += multiplier * Root.Instance.BaseSavedPoints;
    }

    private void HumanDrowned()
    {
        Session.Score -= Root.Instance.BaseDrownedPoints;
    }
}