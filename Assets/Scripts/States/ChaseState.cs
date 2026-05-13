using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class ChaseState : BaseState
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
            // StartAgent();
            base.Enter();
        }

        public override void Update()
        {
            MoveTo(player.position);

            if (agent.hasPath)
            {
                if (CanAttack(player))
                {
                    TransitionToState(new AttackState(npc, agent, animator, player));
                }
                else if(!CanSee(player))
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
