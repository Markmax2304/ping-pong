using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.Physics
{
    public class ReflectBounceCollisionHandler : ICollisionHandler
    {
        public virtual void SolveCollision(RaycastHit2D collision, ref Vector3 direction, ref float force)
        {
            if (collision.distance > Physics2D.defaultContactOffset)
            {
                force = collision.distance;
            }
            else
            {
                direction = Vector2.Reflect(direction, collision.normal);
            }
        }
    }
}
