using Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay: MonoBehaviour
{
    public RoundWaveSpawner WaveSpawner;
    public Transform HumanContainer;
    public Text ScoreLabel;
    private int humansLeft;

    public void Awake()
    {
        Session.Score = 0;
        foreach (var human in FindObjectsOfType<Human>())
        {
            human.Saved += HumanSaved;
            human.Drowned += HumanDrowned;
            ++humansLeft;
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
        SceneManager.LoadScene("Gameplay");
    }

    private void HumanSaved(int multiplier)
    {
        Session.Score += multiplier * Root.Instance.BaseSavedPoints;
        ExtractHuman();
    }

    private void HumanDrowned()
    {
        Session.Score -= Root.Instance.BaseDrownedPoints;
        ExtractHuman();
    }

    private void ExtractHuman()
    {
        --humansLeft;
        if (humansLeft == 0)
            SceneManager.LoadScene("GameOver");
    }
}