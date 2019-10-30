using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverGameObject;
    [SerializeField] private GameObject startGameObject;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private Image babyImage;
    [SerializeField] private Sprite babyHappy;
    [SerializeField] private Sprite babySad;
    [SerializeField] private Sprite babyNormal;
    [SerializeField] private float babySpriteTime =2;
    private void Start()
    {
        PauseGame(startGameObject);
        Globals.OnGameOverEvent += EndGame;
        EventManager<int>.AddHandler(EVENT.gameUpdateEvent, CheckOnBaby);
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

    public void RetryGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CheckOnBaby(int amount)
    {
        if(amount < 0)
        {
            babyImage.sprite = babySad;
            StartCoroutine(Timer.Start(babySpriteTime, false, () =>
            {
                babyImage.sprite = babyNormal;
            }));
        }
        else
        {
            babyImage.sprite = babyHappy;
            StartCoroutine(Timer.Start(babySpriteTime, false, () =>
            {
                babyImage.sprite = babyNormal;
            }));
        }
    }

    private void OnDisable()
    {
        Globals.OnGameOverEvent -= EndGame;
    }
}
