using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class FleeState : BaseState
    {
        private static readonly int IS_RUNNING = Animator.StringToHash("isRunning");
        private static readonly string HIDING_SPOT_TAG  = "HidingSpot";
        private GameObject _hidingSpot;

        public FleeState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            state = STATE.FLEE;
            SetNavigationSpeed(5f);
        }

        public override void Enter()
        {
            SetAnimatorTrigger(IS_RUNNING);
            StartAgent();
            _hidingSpot = FindNearestHidingSpot();

            if (_hidingSpot != null)
            {
                MoveTo(_hidingSpot.transform.position);
            }

            base.Enter();
        }

        public override void Update()
        {
            if (_hidingSpot == null)
            {
                _hidingSpot = FindNearestHidingSpot();
            }

            if (_hidingSpot != null)
            {
                MoveTo(_hidingSpot.transform.position);

                if (agent.remainingDistance < 2f)
                {
                    if (DistanceTo(player) > 20f || !CanSee(player))
                    {
                        TransitionToState(new PatrolState(npc, agent, animator, player));
                    }
                }
            }
            else
            {
                Vector3 fleeDirection = (npc.transform.position - player.position).normalized;
                Vector3 fleeTarget = npc.transform.position + fleeDirection * 10f;

                MoveTo(fleeTarget);

                if (DistanceTo(player) > 15f)
                {
                    TransitionToState(new PatrolState(npc, agent, animator, player));
                }
            }
        }

        public override void Exit()
        {
            ResetAnimatorTrigger(IS_RUNNING);
            _hidingSpot = null;
            base.Exit();
        }

        private GameObject FindNearestHidingSpot()
        {
            GameObject[] spots = GameObject.FindGameObjectsWithTag(HIDING_SPOT_TAG);
            
            GameObject nearest = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject spot in spots)
            {
                float distance = DistanceTo(spot.transform);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = spot;
                }
            }

            return nearest;
        }
    }
}
