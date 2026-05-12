using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public abstract class NavigationState : BaseState
    {
        protected float navigationSpeed = 3.5f;

        public NavigationState(
            GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player
            ) : base(_npc, _agent, _animator, _player)
        {
            agent.isStopped = false;
            agent.speed = navigationSpeed;
        }

        protected void SetNavigationSpeed(float speed)
        {
            navigationSpeed = speed;
            agent.speed = speed;
        }

        protected void MoveToDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        protected bool HasReachedDestination()
        {
            return agent.remainingDistance < 1f && !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        protected bool HasPathPending()
        {
            return !agent.hasPath;
        }
    }
}
