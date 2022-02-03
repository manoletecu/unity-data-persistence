using UnityEngine;
using TMPro;
using System;

public class HighscoreLoader : MonoBehaviour
{
    public TMP_Text HighscoreText;

    // Start is called before the first frame update
    void Start()
    {
        var highscoreText = string.Empty;
        HighscoreText.text = highscoreText;
        var highscores = HighscoreManager.GetHighScores();
        if (highscores == null)
            return;

        var place = 0;
        foreach(var score in highscores)
        {
            highscoreText += string.Format("{3}-{1} - {0} {2}", score.Item1, score.Item2, Environment.NewLine, ++place);
        }
        HighscoreText.text = highscoreText;
    }
}
