using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class AttackState : CombatState
    {
        private static readonly int IS_SHOOTING = Animator.StringToHash("isShooting");

        public AttackState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.ATTACK;
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_SHOOTING);
            StopAgent();
            PlayShootAudio();
            base.Enter();
        }

        public override void Update()
        {
            Vector3 direction = player.position - npc.transform.position;
            RotateTowards(direction, rotationSpeed);

            if (!CanAttackPlayer())
            {
                TransitionToState(new IdleState(npc, agent, animator, player));
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_SHOOTING);
            StopShootAudio();
            base.Exit();
        }
    }
}
