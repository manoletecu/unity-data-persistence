using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

[CreateAssetMenu(fileName ="HighscoreManager", menuName = "Scriptable Ojects")]
public class HighscoreManager : ScriptableObject
{
    static List<Tuple<int, string>> highscores;

    public static void AddHighscore(int highscore, string playerName)
    {
        if(highscores != null)
        {
            if(highscores.Count > 10)
            {
                var min = highscores.Min(t => t.Item1);
                if (min <= highscore)
                    return;
                var minItemIndex = highscores.FindIndex(t => t.Item1 == min);
                highscores[minItemIndex] = new Tuple<int, string>(highscore, playerName);
            }
            else
            {
                highscores.Add(new Tuple<int, string> (highscore, playerName));
            }
            highscores.Sort((t1, t2) => { return t2.Item1.CompareTo(t1.Item1); });
        }
        else
        {
            highscores = new List<Tuple<int, string>> { new Tuple<int, string>(highscore, playerName) };
        }
        SaveHighScores();
    }

    public static Tuple<int, string> GetHighestScore()
    {
        if (highscores == null)
        {
            LoadHighScores();
        }
        if (highscores != null && highscores.Count > 0)
        {
            return highscores[0];
        }
        return null;
    }

    public static List<Tuple<int, string>> GetHighScores()
    {
        if (highscores == null)
        {
            LoadHighScores();
        }
        return highscores;
    }

    public static void SaveHighScores()
    {
        try
        {
            if (highscores == null)
                return;

            var file = File.Open(Application.dataPath + "/Highscores/Highscores.score", FileMode.OpenOrCreate);
            var writer = new BinaryWriter(file);

            foreach(var highscore in highscores)
            {
                writer.Write(highscore.Item1);
                writer.Write(highscore.Item2);
            }

            writer.Close();

        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
    public static void LoadHighScores()
    {
        try
        {           
            var file = File.Open(Application.dataPath + "/Highscores/Highscores.score", FileMode.OpenOrCreate);
            if(file.CanRead)
            {
                var reader = new BinaryReader(file);
                highscores = new List<Tuple<int, string>>();

                while(file.Position < file.Length)
                {
                    var score = reader.ReadInt32();
                    var name = reader.ReadString();
                    highscores.Add(new Tuple<int, string>(score, name));
                }
                
                reader.Close();
            }      

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
