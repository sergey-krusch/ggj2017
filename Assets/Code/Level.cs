using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static void Load(int idx)
    {
        SceneManager.LoadScene(string.Format("Levels/{0:D2}", idx));
    }

    public static void LoadCurrent()
    {
        Load(Session.CurrentLevel.Value);
    }

    public void Awake()
    {
        Destroy(GameObject.Find("Main Camera"));
        if (SceneManager.sceneCount == 1)
        {
            var match = Regex.Match(SceneManager.GetActiveScene().name, @"^(Levels\/|)?(\d+)$");
            string levelStr = match.Groups[2].Captures[0].Value;
            int level = Convert.ToInt32(levelStr);
            Session.CurrentLevel = level;
            SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
        }
    }
}