using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public delegate void GameOverEvent();
    public static GameOverEvent OnGameOverEvent { get; set; }

    public static GameOverEvent OnLivesLostEvent { get; set; }
}
