using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class Idle : BaseState
    {
        private static readonly int IS_IDLE = Animator.StringToHash("isIdle");

        public Idle(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            animator.SetTrigger(IS_IDLE);
            base.Enter();
        }

        public override void Update()
        {
            if (CanSeePlayer())
            {
                nextState = new ChaseState(npc, agent, animator, player);
                stage = EVENT.EXIT;
            }
            else if (Random.Range(0, 100) < 10)
            {
                nextState = new PatrolState(npc, agent, animator, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            animator.ResetTrigger(IS_IDLE);
            base.Exit();
        }
    }
}
