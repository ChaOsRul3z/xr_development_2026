using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class ChaseState : NavigationState
    {
        private static readonly int IS_RUNNING = Animator.StringToHash("isRunning");

        public ChaseState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.CHASE;
            SetNavigationSpeed(5f);
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_RUNNING);
            base.Enter();
        }

        public override void Update()
        {
            MoveToDestination(player.position);

            if (agent.hasPath)
            {
                if (CanAttackPlayer())
                {
                    TransitionToState(new AttackState(npc, agent, animator, player));
                } 
                else if (!CanSeePlayer())
                {
                    TransitionToState(new PatrolState(npc, agent, animator, player));
                }   
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_RUNNING);
            base.Exit();
        }
    }
}
