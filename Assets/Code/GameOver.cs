using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public Text ScoreLabel;

    public void Awake()
    {
        ScoreLabel.text = string.Format(ScoreLabel.text, Session.Score);
    }

    public void RestartClick()
    {
        Level.Load(1);
    }
}