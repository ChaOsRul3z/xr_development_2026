using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class Attack : BaseState
    {
        private static readonly int IS_SHOOTING = Animator.StringToHash("isShooting");

        float roationSpeed = 2.0f;
        AudioSource shootAudio;

        public Attack(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.ATTACK;
            shootAudio = npc.GetComponent<AudioSource>();
        }

        public override void Enter()
        {
            animator.SetTrigger(IS_SHOOTING);
            agent.isStopped = true;
            shootAudio.Play();
            base.Enter();
        }

        public override void Update()
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y = 0;

            npc.transform.rotation = Quaternion.Slerp(
                npc.transform.rotation, 
                Quaternion.LookRotation(direction), Time.deltaTime * roationSpeed
            );

            if (!CanAttackPlayer())
            {
                nextState = new Idle(npc, agent, animator, player);  
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            animator.ResetTrigger(IS_SHOOTING);
            shootAudio.Stop();
            base.Exit();
        }
    }
}
