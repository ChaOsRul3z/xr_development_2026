using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class PatrolState : NavigationState
    {
        private static readonly int IS_WALKING = Animator.StringToHash("isWalking");

        int currentIndex = -1;

        public PatrolState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.PATROL;
            SetNavigationSpeed(2f);
        }

        public override void Enter()
        {
            float lastDistance = Mathf.Infinity;
            for (int i = 0; i < GameManager.Instance.Checkpoints.Count; i++)
            {
                float distance = Vector3.Distance(
                    npc.transform.position, 
                    GameManager.Instance.Checkpoints[i].transform.position
                );
                
                if (distance < lastDistance)
                {
                    lastDistance = distance;
                    currentIndex = i - 1;
                }
            }

            SetAnimatorTrigger(IS_WALKING);
            base.Enter();
        }

        public override void Update()
        {
            if (agent.remainingDistance < 1)
            {
                if(currentIndex >= GameManager.Instance.Checkpoints.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;   
                }

                MoveToDestination(GameManager.Instance.Checkpoints[currentIndex].transform.position);
            }

            if (CanSeePlayer())
            {
                TransitionToState(new ChaseState(npc, agent, animator, player));
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_WALKING);
            base.Exit();
        }
    }
}
