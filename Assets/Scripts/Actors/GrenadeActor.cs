using System;
using DefaultNamespace.Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class GrenadeActor : MonoBehaviour, IPoolable
    {
        public Vector3 velocity;
        public bool Thrown;

        public Vector3 position
        {
            set => transform.position = value;
            get => transform.position;
        }

        private void FixedUpdate()
        {
            if(!Thrown) return;
            velocity += Vector3.down * (GameManager.GameProperties.gravity * Time.fixedTime);
            var step = velocity * Time.fixedTime;
            if (Physics.Raycast(new Ray(position, step), out var hit))
            {
                position = hit.point;
                Explode();
                return;
            }
            position += step;
        }

        private void Explode()
        {
            //spawn particles;
            Destroy(gameObject);
        }

        public void Reset()
        {
            Thrown = false;
            velocity = Vector3.zero;
        }
    }
}