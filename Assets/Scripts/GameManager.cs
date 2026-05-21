using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Systems/GameManager")]
public class GameManager : ScriptableObject
{
    public List<GameObject> Checkpoints { get; private set; } =  new List<GameObject>();
	
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    [NonSerialized] protected GameState CurrentState;
    [NonSerialized] protected int _score;

    public int Score => _score;

    public event Action<GameState> OnGameStateChanged;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnNavigationSpeedChanged;

    [SerializeField]
    float navigationSpeed = 2f;

    public float NavigationSpeed
    {
        get => navigationSpeed;
        set
        {
            if (Mathf.Approximately(navigationSpeed, value)) return;
            navigationSpeed = value;
            OnNavigationSpeedChanged?.Invoke(value);
        }
    }

    public void SetNavigationSpeed(float value) => NavigationSpeed = value;

    public void Initialize()
    {
        _score = 0;
        CurrentState = GameState.MainMenu;

        this.Checkpoints.Clear();

        this.Checkpoints.AddRange(
            GameObject.FindGameObjectsWithTag("Checkpoint")
        );

        this.Checkpoints.Sort((a, b) => a.name.CompareTo(b.name));
    }

    public void UpdateState(GameState newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case GameState.MainMenu:
                Time.timeScale = 1f;
                _score = 0;
                    break;
                case GameState.Playing:
                Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                Time.timeScale = 0f;
                    break;
                case GameState.GameOver:
                Time.timeScale = 0f;
                    break;
            }

            OnGameStateChanged?.Invoke(CurrentState);
        }
    }

    public void AddScore(int amount)
    {
        _score += amount;
        OnScoreChanged?.Invoke(_score);
    }
}