using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GrenadeActor : MonoBehaviour
    {
        public Vector3 velocity;

        public Vector3 position
        {
            set => transform.position = value;
            get => transform.position;
        }

        private void FixedUpdate()
        {
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
    }
}