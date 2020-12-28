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
        [SerializeField] private float MoveSpeed = 3.5f;
        private NavMeshAgent agent;

        public Vector3 Move { get; set; }
        public Vector3 Direction { get; set; }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var movement = Move * MoveSpeed;
            animator.SetFloat(MoveKey, movement.magnitude);
            agent.Move(movement * Time.deltaTime);
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