using UnityEngine;

namespace Pong.Physics
{
    public interface ICollisionHandler
    {
        void SolveCollision(RaycastHit2D collision, ref Vector3 direction, ref float force);
    }
}
