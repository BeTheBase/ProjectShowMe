using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private List<Text> highScoreTexts;
    [SerializeField] private List<ScoreData> AllScoreData;
    [SerializeField] private List<int> scores;
    [SerializeField] private int currentScore;

    private string jsonString;

    private void OnEnable()
    {
        EventManager<int>.AddHandler(EVENT.barCompletedEvent, AddScore);
        Globals.OnGameOverEvent += SaveScore;
        scores = JsonConverter<int>.FromJson(jsonString, "/HighScoreData.json");
    }

    public void AddScore(int score)
    {
        currentScore += score;
    }

    public void SaveScore()
    {
        scores.Add(currentScore);
        jsonString = JsonConverter<int>.SerializeToJson(scores, jsonString, "/HighScoreData.json");
    }

    public void ShowScores()
    {
        scores = JsonConverter<int>.FromJson(jsonString, "/HighScoreData.json");
        for(int index = 0; index < scores.Count; index++)
        {
            highScoreTexts[index].text = "Score: " + scores[index];
        }
        highScorePanel.SetActive(true);
    }

    private void OnDisable()
    {
        Globals.OnGameOverEvent -= SaveScore;
    }
}
