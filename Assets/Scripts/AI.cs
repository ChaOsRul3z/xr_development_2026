using Game.States;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public Transform player;
    BaseState currentState;

    void Start()
    {
        GameManager.Instance.Init();
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        currentState = new IdleState(this.gameObject, agent, animator, player);
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
