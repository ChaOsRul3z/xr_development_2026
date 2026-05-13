using Game.States;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    BaseState currentState;

    [field: SerializeField] public GameManager GameManager { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; set; } = 2.0f;
    [field: SerializeField] public float VisibilityDistance { get; private set; } = 10.0f;
    [field: SerializeField] public float VisibilityAngle { get; private set; } = 30.0f;
    [field: SerializeField] public float ShootDistance { get; private set; } = 7.0f;
    [field: SerializeField] public float NavigationSpeed { get; private set; } = 5.0f;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        
        
        currentState = new IdleState(this.gameObject, agent, animator, player);
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
