using Game.Manager;
using Game.States;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    BaseState currentState;
    bool initializationWarningLogged;

    [field: SerializeField] public GameManager GameManager { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; set; } = 2.0f;
    [field: SerializeField] public float VisibilityDistance { get; private set; } = 10.0f;
    [field: SerializeField] public float VisibilityAngle { get; private set; } = 30.0f;
    [field: SerializeField] public float ShootDistance { get; private set; } = 7.0f;
    [field: SerializeField] public float NavigationSpeed { get; private set; } = 2.0f;

    void Start()
    {
        TryInitialize();
    }

    void OnDestroy()
    {
        if (GameManager != null) GameManager.OnNavigationSpeedChanged -= OnNavigationSpeedChanged;
    }

    void OnNavigationSpeedChanged(float speed)
    {
        NavigationSpeed = speed;
        if (agent != null) agent.speed = speed;
    }

    void Update()
    {
        if (currentState == null)
        {
            TryInitialize();
            return;
        }

        currentState = currentState.Process();
    }

    bool TryInitialize()
    {
        if (currentState != null)
        {
            return true;
        }

        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        if (GameManager == null)
        {
            if (SceneExecutionWorker.HasInstance)
            {
                GameManager = SceneExecutionWorker.Instance.GameManager;
            }
        }

        if (GameManager == null)
        {
            GameManager = Resources.Load<GameManager>("GameManager");
        }

        if (GameManager == null || GameManager.Player == null)
        {
            if (!initializationWarningLogged)
            {
                Debug.LogWarning($"{nameof(AI)} on {name} is waiting for a GameManager and Player before it can start.", this);
                initializationWarningLogged = true;
            }

            return false;
        }

        currentState = new IdleState(gameObject, agent, animator, GameManager.Player.transform);

        GameManager.OnNavigationSpeedChanged += OnNavigationSpeedChanged;
        agent.speed = GameManager.NavigationSpeed;
        NavigationSpeed = GameManager.NavigationSpeed;

        return true;
    }
}
