using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class PatrolState : BaseState
    {
        private static readonly int IS_WALKING = Animator.StringToHash("isWalking");

        int currentIndex = -1;

        public PatrolState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            name = STATE.PATROL;
            agent.speed = 2;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            float lastDistance = Mathf.Infinity;
            for (int i = 0; i < GameEnvironment.Instance.Checkpoints.Count; i++)
            {
                float distance = Vector3.Distance(
                    npc.transform.position, 
                    GameEnvironment.Instance.Checkpoints[i].transform.position
                );
                
                if (distance < lastDistance)
                {
                    lastDistance = distance;
                    currentIndex = i - 1;
                }
            }

            animator.SetTrigger(IS_WALKING);
            base.Enter();
        }

        public override void Update()
        {
            if (agent.remainingDistance < 1)
            {
                if(currentIndex >= GameEnvironment.Instance.Checkpoints.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;   
                }

                agent.SetDestination(GameEnvironment.Instance.Checkpoints[currentIndex].transform.position);
            }

            if (CanSeePlayer())
            {
                nextState = new ChaseState(npc, agent, animator, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            animator.ResetTrigger(IS_WALKING);
            base.Exit();
        }
    }
}
