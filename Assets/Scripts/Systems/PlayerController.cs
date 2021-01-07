﻿using Tools;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class PlayerController : IGameSystem
    {
        private GameCharacter character;
        private Camera camera;
        private LineRenderer trajectoryRenderer;
        public GameCharacter Target => character;

        private bool isAiming = false;
        private Vector3 aimPoint;

        public void Init(GameManager.Properties properties)
        {
            camera = Camera.main;
            this.trajectoryRenderer = properties.trajectoryRenderer;
        }

        public void Start()
        {
            character = GameManager.CharacterSpawn.Spawn(Vector3.zero, Vector3.forward);
            GameManager.UpdateEvent += OnUpdate;
            GameManager.GizmosEvent += DrawGizmos;
        }

        public void Destroy()
        {
            GameManager.UpdateEvent -= OnUpdate;
            GameManager.GizmosEvent -= DrawGizmos;
        }

        private void OnUpdate()
        {
            var aim = Input.GetButton("Fire2");
            if (aim)
            {
                isAiming = true;
                trajectoryRenderer.gameObject.SetActive(true);


                var layerMask = LayerMask.GetMask("Floor");
                if (InputTools.MouseToFloorPoint(Camera.main, 20, layerMask, out aimPoint))
                {
                    var p1 = character.transform.position;
                    Ballistics.GetInitVelocity(p1, aimPoint, 9.8f, out var initialVector, out var time);

                    for (var i = 1; i <= trajectoryRenderer.positionCount; i++)
                    {
                        trajectoryRenderer.SetPosition(i - 1,
                            Ballistics.GetPosition(p1, initialVector, 9.8f,
                                i * time / trajectoryRenderer.positionCount));
                    }
                }
            }
            else
            {
                if (isAiming)
                {
                    trajectoryRenderer.gameObject.SetActive(false);
                    isAiming = false;
                }

                var moveVector = new Vector3(
                    Input.GetAxis("Horizontal"),
                    0,
                    Input.GetAxis("Vertical"));
                moveVector = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * moveVector;
                character.Move = moveVector;

                if (moveVector.magnitude > 0)
                {
                    character.Direction = moveVector;
                }
            }
        }

        private void DrawGizmos()
        {
            if (isAiming)
                Gizmos.DrawWireSphere(aimPoint, 1);
        }
    }
}