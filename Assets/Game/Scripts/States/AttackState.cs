using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class AttackState : BaseState
    {
        private static readonly int IS_SHOOTING = Animator.StringToHash("isShooting");

        protected AudioSource shootAudio;

        public AttackState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            state = STATE.ATTACK;
            shootAudio = npc.GetComponent<AudioSource>();
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_SHOOTING);
            StopAgent();
            PlayAudio(shootAudio);
            base.Enter();
        }

        public override void Update()
        {
            float distance = DistanceTo(player);
            bool playerBehind = IsBehind(player, -0.2f);

            if (playerBehind && distance < 6.0f)
            {
                RotateTowards(player.position - npc.transform.position, component.RotationSpeed);
                TransitionToState(new FleeState(npc, agent, animator, player));
                return;
            }

            Vector3 direction = player.position - npc.transform.position;
            RotateTowards(direction, component.RotationSpeed);

            if (!CanAttack(player))
            {
                TransitionToState(new IdleState(npc, agent, animator, player));
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_SHOOTING);
            StopAudio(shootAudio);
            base.Exit();
        }
    }
}
