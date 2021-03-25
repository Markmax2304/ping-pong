using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Pong.Physics
{
    public class PongBallCollisionHandler : ReflectBounceCollisionHandler
    {
        public override void SolveCollision(RaycastHit2D collision, ref Vector3 direction, ref float force)
        {
            const float maxAngle = 70f;

            if (collision.collider.CompareTag(Player.playerTag))
            {
                Bounds collBounds = collision.collider.bounds;
                
                // NOTE: work only if racket is moving vertically
                float verticalOffset = collision.point.y - collBounds.center.y;
                float verticalHalfLength = collBounds.extents.y;
                float reflectFactor = verticalOffset / verticalHalfLength;
                reflectFactor = Mathf.Clamp(reflectFactor, -1f, 1f);

                Vector3 commonDir = Vector3.right * Mathf.Sign(-direction.x);
                float angle = maxAngle * reflectFactor;
                direction = Quaternion.Euler(0f, 0f, angle) * commonDir;
            }
            else
            {
                base.SolveCollision(collision, ref direction, ref force);
            }
        }
    }
}
