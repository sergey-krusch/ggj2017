using Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay: MonoBehaviour
{
    public RoundWaveSpawner WaveSpawner;
    public Text ScoreLabel;
    public Text LevelNumberLabel;
    private int humansLeft;

    public void Awake()
    {
        if (SceneManager.sceneCount == 1)
        {
            Session.CurrentLevel = 1;
            SceneManager.LoadScene("Levels/01", LoadSceneMode.Additive);
        }
        Session.Score = 0;
    }

    public void Start()
    {
        LevelNumberLabel.text = string.Format(LevelNumberLabel.text, Session.CurrentLevel);
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
        HandleDeveloperInput();
    }

    public void RestartClick()
    {
        Level.Load(1);
    }

    private void HandleDeveloperInput()
    {
        if (!Root.Instance.DeveloperMode)
            return;
        if (Input.GetKeyDown(KeyCode.N))
            NextLevel();
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
            NextLevel();
    }

    private void NextLevel()
    {
        if (Session.CurrentLevel >= Root.Instance.LevelCount)
            SceneManager.LoadScene("GameOver");
        else
        {
            ++Session.CurrentLevel;
            Level.LoadCurrent();
        }
    }
}