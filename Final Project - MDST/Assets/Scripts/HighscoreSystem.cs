using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System. IO;

public class HighscoreSystem : MonoBehaviour
{
    [SerializeField] private ScoreSystem _scoreSystem;
    private string _fileName = "highscore.txt";
    [HideInInspector] public int highscore;

    private void Awake()
    {
        GameManager.OnGameStart += LoadHighscore;
        GameManager.OnGameOver += SaveHighscore;
    }

    private void SaveHighscore()
    {
        string path = Application.persistentDataPath + "/" + _fileName;
        
        try
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(_scoreSystem.GetScore());
            
            sw.Close();
        }
        catch(Exception e)
        {
            print("Exception: " + e.Message);
        }
    }

    private void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/" + _fileName;

        try
        {
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();

            sr.Close();
            highscore = Int32.Parse(line);
        }
        catch (Exception e)
        {
            print("Exception: " + e.Message);
            highscore = 0;
        }
        finally
        {
            _scoreSystem.UpdateHighScore();
        }
    }
    
    private void OnDestroy()
    {
        GameManager.OnGameStart -= LoadHighscore;
        GameManager.OnGameOver -= SaveHighscore;
    }
}
