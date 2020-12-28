using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacter : MonoBehaviour
    {
        private static readonly int MoveKey = Animator.StringToHash("Move");
        private static readonly int ThrowKey = Animator.StringToHash("Throw");

        [SerializeField] private Animator animator;
        private NavMeshAgent agent;

        public Vector3 Move { get; set; }
        public Vector3 Direction { get; set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            animator.SetFloat(MoveKey, Move.magnitude);
            agent.Move(Move);
            if (Direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(Direction, Vector3.up);
            }
        }

        public void Throw()
        {
        }
    }
}