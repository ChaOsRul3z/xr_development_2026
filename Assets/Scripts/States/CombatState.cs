using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public abstract class CombatState : BaseState
    {
        protected float rotationSpeed = 2.0f;
        protected AudioSource shootAudio;

        public CombatState(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player) 
            : base(_npc, _agent, _animator, _player)
        {
            shootAudio = npc.GetComponent<AudioSource>();
        }

        protected void StopAgent()
        {
            agent.isStopped = true;
        }

        protected void StartAgent(float speed = 5f)
        {
            agent.isStopped = false;
            agent.speed = speed;
        }

        protected void PlayShootAudio()
        {
            if (shootAudio != null)
            {
                shootAudio.Play();
            }
        }

        protected void StopShootAudio()
        {
            if (shootAudio != null)
            {
                shootAudio.Stop();
            }
        }

        protected void RotateTowards(Vector3 targetDirection, float speed)
        {
            Vector3 direction = targetDirection;
            direction.y = 0;

            npc.transform.rotation = Quaternion.Slerp(
                npc.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * speed
            );
        }
    }
}
