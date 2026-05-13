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
            state = STATE.PATROL;
            SetNavigationSpeed(2f);
        }

        public override void Enter()
        {
            float lastDistance = Mathf.Infinity;
            for (int i = 0; i < gameManager.Checkpoints.Count; i++)
            {
                float distance = Vector3.Distance(
                    npc.transform.position, 
                    gameManager.Checkpoints[i].transform.position
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
            if (IsBehind(player) && DistanceTo(player) < 10f)
            {
                TransitionToState(new FleeState(npc, agent, animator, player));
                return;
            }
            if (agent.remainingDistance < 1)
            {
                if(currentIndex >= SceneExecutionWorker.Instance.GameCheckpoints.Count - 1)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentIndex++;   
                }

                MoveTo(SceneExecutionWorker.Instance.GameCheckpoints[currentIndex].transform.position);
            }

            if (CanSee(player))
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
