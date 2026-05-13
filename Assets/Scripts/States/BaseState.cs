using UnityEngine;
using UnityEngine.AI;

namespace Game.States
{
    public class BaseState
    {
        [SerializeField] protected GameManager gameManager;

        public enum STATE
        {
            IDLE, PATROL, CHASE, ATTACK, FLEE, HIDING
        };

        public enum EVENT
        {
        ENTER, UPDATE, EXIT   
        };

        public STATE state;
        protected EVENT stage;
        protected GameObject npc;
        protected Animator animator;
        protected Transform player;
        protected BaseState nextState;
        protected NavMeshAgent agent;
        protected AI component;

        public BaseState(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        {
            npc = _npc;
            agent = _agent;
            animator = _animator;
            stage = EVENT.ENTER;
            player = _player;
            component = npc.GetComponent<AI>();

            gameManager = Resources.Load<GameManager>("GameManager");
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
                Debug.Log("Transitioning from " + state + " to " + nextState.state);
                Exit();
                return nextState;
            }
            return this;
        }

        public bool IsBehind(Transform target, float threshold = -0.25f)
        {
            Vector3 toPlayer = (target.position - npc.transform.position).normalized;
            return Vector3.Dot(npc.transform.forward, toPlayer) < threshold;
        }

        public bool CanSee(Transform target)
        {
            Vector3 direction = target.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);

            if (direction.magnitude < component.VisibilityDistance && angle < component.VisibilityAngle)
            {
                return true;
            }

            return false;
        }

        public bool CanAttack(Transform target)
        {
            Vector3 direction = target.position - npc.transform.position;

            if (direction.magnitude < component.ShootDistance)
            {
                return true;
            }

            return false;
        }

        public float DistanceTo(Transform target)
        {
            return Vector3.Distance(npc.transform.position, target.position);
        }

        protected void SetAnimatorTrigger(int animHash)
        {
            animator.SetTrigger(animHash);
        }

        protected void ResetAnimatorTrigger(int animHash)
        {
            animator.ResetTrigger(animHash);
        }

        protected void TransitionToState(BaseState newState)
        {
            nextState = newState;
            stage = EVENT.EXIT;
        }

        protected void PlayAudio(AudioSource audioSource)
        {
            if(audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        protected void StopAudio(AudioSource audioSource)
        {
            if(audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        protected void MoveTo(Vector3 target)
        {
            agent.SetDestination(target);
        }

        protected void SetNavigationSpeed(float speed)
        {
            agent.speed = speed;
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

        protected void StopAgent()
        {
            agent.isStopped = true;
        }

        protected void StartAgent(float speed = 5f)
        {
            agent.isStopped = false;
            agent.speed = speed;
        }
    }
}
