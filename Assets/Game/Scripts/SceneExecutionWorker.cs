using UnityEngine;
using System.Collections.Generic;
using Game.Manager;

public class SceneExecutionWorker : Singleton<SceneExecutionWorker>
{
    [Header("Dependencies")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected CanvasGroup fadeCanvasGroup;

    // [Header("Transition Settings")]
    // [SerializeField] private float fadeDuration = 0.4f;
    protected override void Awake()
    {
        if (_instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Bootstrap data variables on startup
        if (gameManager != null) gameManager.Initialize();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public List<GameObject> GameCheckpoints => gameManager.Checkpoints;
}
