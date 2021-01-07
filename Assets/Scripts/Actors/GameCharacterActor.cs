using System;
using DefaultNamespace.Data;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class GameCharacterActor : MonoBehaviour
    {
        private static readonly int MoveKey = Animator.StringToHash("Move");
        private static readonly int ThrowKey = Animator.StringToHash("Throw");

        [SerializeField] private Animator animator;
        [SerializeField] private float MoveSpeed = 3.5f;
        private NavMeshAgent agent;

        public Inventory Inventory = new Inventory();
        public GameCharacter character;

        public Transform transform { get; private set; }
        private void Awake()
        {
            transform = base.transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            var movement = character.Move * MoveSpeed;
            animator.SetFloat(MoveKey, movement.magnitude);
            agent.Move(movement * Time.deltaTime);
            if (character.Direction.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(character.Direction, Vector3.up);
            }
        }
    }
}