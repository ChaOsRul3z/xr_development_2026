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
            agent.speed = 5;
            agent.isStopped = false;
        }

        public override void Enter()
        {
            animator.SetTrigger(IS_RUNNING);
            base.Enter();
        }

        public override void Update()
        {
            agent.SetDestination(player.position);

            if (agent.hasPath)
            {
             if (CanAttackPlayer())
                {
                    nextState = new Attack(npc, agent, animator, player);  
                    stage = EVENT.EXIT;
                } 
                else if(!CanSeePlayer())
                {
                    nextState = new PatrolState(npc, agent, animator, player);  
                    stage = EVENT.EXIT;
                }   
            }
        }

        public override void Exit()
        {
            animator.ResetTrigger(IS_RUNNING);
            base.Exit();
        }
    }
}
