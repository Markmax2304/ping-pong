using UnityEngine;

namespace Pong.Movement
{
    public class SmoothMoveHandler : IMoveHandler
    {
        public void MovePosition(IMovable entity, Vector3 deltaPos)
        {
            entity.MoveTo(entity.ActualPosition + deltaPos);
            entity.ActualPosition += deltaPos;
        }

        public void SetPosition(IMovable entity, Vector3 pos)
        {
            entity.MoveTo(pos);
            entity.ActualPosition = pos;
        }
    }
}