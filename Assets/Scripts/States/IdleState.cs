using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class IdleState : BaseState
    {
        private static readonly int IS_IDLE = Animator.StringToHash("isIdle");

        public IdleState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_IDLE);
            base.Enter();
        }

        public override void Update()
        {
            if (CanSeePlayer())
            {
                TransitionToState(new ChaseState(npc, agent, animator, player));
            }
            else if (Random.Range(0, 100) < 10)
            {
                TransitionToState(new PatrolState(npc, agent, animator, player));
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_IDLE);
            base.Exit();
        }
    }
}
