using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverGameObject;
    [SerializeField] private GameObject startGameObject;
    [SerializeField] private GameObject gameCanvas;

    private void Start()
    {
        PauseGame(startGameObject);
        Globals.OnGameOverEvent += EndGame;
    }

    private void PauseGame(GameObject obj)
    {
        Time.timeScale = 0;
        obj.SetActive(true);
        gameCanvas.SetActive(false);
    }

    public void PlayGame(GameObject obj)
    {
        Time.timeScale = 1;
        obj.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void EndGame()
    {
        PauseGame(gameOverGameObject);
    }

    private void OnDisable()
    {
        Globals.OnGameOverEvent -= EndGame;
    }
}
