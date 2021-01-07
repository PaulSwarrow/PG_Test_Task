using System.Linq;
using DefaultNamespace.Data;
using DefaultNamespace.Systems.Tools;
using Tools;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace.Systems
{
    public class PlayerController : IGameSystem
    {
        private GameCharacter character;
        private Camera camera;
        private AimDrawer aimDrawer;
        public GameCharacter Target => character;

        public void Init()
        {
            camera = Camera.main;
            aimDrawer = new AimDrawer(GameManager.GameProperties.trajectoryRenderer);
        }

        public void Start()
        {
            character = GameManager.GameCharacter.Spawn(Vector3.zero, Vector3.forward);
            character.Inventory.AddGrenade(GameManager.GameProperties.grenades.First());
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
                character.Aiming = true;

                character.Move = Vector3.zero;

                var layerMask = LayerMask.GetMask("Floor");
                if (InputTools.MouseToFloorPoint(Camera.main, 20, layerMask, out character.AimPoint))
                {
                    aimDrawer.Enable = true;
                    var p1 = character.Position;
                    aimDrawer.Draw(p1, character.AimPoint);
                    var direction = character.AimPoint - p1;
                    direction.y = 0;
                    character.Direction = direction;
                }
                else
                {
                    aimDrawer.Enable = false;
                }
            }
            else
            {
                if (character.Aiming)
                {
                    aimDrawer.Enable = false;
                    character.Aiming = false;
                    character.ThrowCurrentItem();
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
            if (character.Aiming)
                Gizmos.DrawWireSphere(character.AimPoint, 1);
        }
    }
}