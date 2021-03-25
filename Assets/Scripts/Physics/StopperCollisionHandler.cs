using UnityEngine;

namespace Pong.Physics
{
    public class StopperCollisionHandler : ICollisionHandler
    {
        public void SolveCollision(RaycastHit2D collision, ref Vector3 direction, ref float force)
        {
            force = collision.distance;
        }
    }
}
