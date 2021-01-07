using UnityEngine;

namespace Tools
{
    public static class Ballistics
    {
        public static Vector3 GetPosition(Vector3 p1, Vector3 v1, float gravity, float time)
        {
            var g = Vector3.down * 9.8f;
            return p1 + v1 * time + g * (time * time);
        }

        public static void GetInitVelocity(Vector3 from, Vector3 to, float gravity,
            out Vector3 initialVector, out float totalFlightTime)
        {
            var initialVelocity = new Vector3();
            var flatDelta = new Vector3(to.x, 0f, to.z) - new Vector3(from.x, 0f, from.z);
            var range = flatDelta.magnitude;
            var unitDirection = flatDelta.normalized;

            var maxYPos = Mathf.Max(flatDelta.magnitude / 2, from.y, to.y);

            var h = maxYPos - from.y;
            initialVelocity.y = 2 * Mathf.Sqrt(gravity * h);
            var timeToMax = .5f * initialVelocity.y / gravity;
            var timeToFall = Mathf.Sqrt((maxYPos - to.y) / gravity);
            totalFlightTime = timeToMax + timeToFall;
            var horizontalVelocityMagnitude = range / totalFlightTime;
            initialVelocity.x = horizontalVelocityMagnitude * unitDirection.x;
            initialVelocity.z = horizontalVelocityMagnitude * unitDirection.z;

            initialVector = initialVelocity;
        }
    }
}