using System;
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
            state = STATE.CHASE;           
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_RUNNING);
            SetNavigationSpeed(component.NavigationSpeed * 1.5f);
            base.Enter();
        }

        public override void Update()
        {           
            if (IsBehind(player))
            {
                TransitionToState(new FleeState(npc, agent, animator, player));
                return;
            }

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
            SetNavigationSpeed(component.NavigationSpeed);
            base.Exit();
        }
    }
}
