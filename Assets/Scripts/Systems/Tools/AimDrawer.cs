using Tools;
using UnityEngine;

namespace DefaultNamespace.Systems.Tools
{
    public class AimDrawer
    {
        private LineRenderer trajectoryRenderer;
        
        
        public AimDrawer(LineRenderer trajectoryRenderer)
        {
            this.trajectoryRenderer = trajectoryRenderer;
        }
        
        public bool Enable
        {
            set => trajectoryRenderer.gameObject.SetActive(value);
        }

        public void Draw(Vector3 p1, Vector3 p2)
        {
            Ballistics.GetInitVelocity(p1, p2, GameManager.GameProperties.gravity,  out var initialVector, out var time);
            for (var i = 1; i <= trajectoryRenderer.positionCount; i++)
            {
                trajectoryRenderer.SetPosition(i - 1,
                    Ballistics.GetPosition(p1, initialVector, GameManager.GameProperties.gravity,
                        i * time / trajectoryRenderer.positionCount));
            }

        }
    }
}