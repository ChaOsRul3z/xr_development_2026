using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class BaseState
    {
        public enum STATE
        {
            IDLE, PATROL, CHASE, ATTACK, SLEEP
        };

        public enum EVENT
        {
        ENTER, UPDATE, EXIT   
        };

        public STATE name;
        protected EVENT stage;
        protected GameObject npc;
        protected Animator animator;
        protected Transform player;
        protected BaseState nextState;
        protected NavMeshAgent agent;

        float visibilityDistance = 10.0f;
        float visibilityAngle = 30.0f;
        float shootDistance = 7.0f;

        public BaseState(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        {
            npc = _npc;
            agent = _agent;
            animator = _animator;
            stage = EVENT.ENTER;
            player = _player;
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public BaseState Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }

        public bool CanSeePlayer()
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);

            if (direction.magnitude < visibilityDistance && angle < visibilityAngle)
            {
                return true;
            }

            return false;
        }

        public bool CanAttackPlayer()
        {
            Vector3 direction = player.position - npc.transform.position;

            if (direction.magnitude < shootDistance)
            {
                return true;
            }

            return false;
        }
    }
}
