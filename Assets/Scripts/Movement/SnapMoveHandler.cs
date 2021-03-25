using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.Movement
{
    public class SnapMoveHandler : IMoveHandler
    {
        const float step = .5f;

        public void MovePosition(IMovable entity, Vector3 deltaPos)
        {
            var pos = entity.ActualPosition + deltaPos;

            entity.ActualPosition = pos;
            entity.MoveTo(CutPosition(pos, step));
        }

        public void SetPosition(IMovable entity, Vector3 pos)
        {
            entity.ActualPosition = pos;
            entity.MoveTo(CutPosition(pos, step));
        }

        private Vector3 CutPosition(Vector3 pos, float step)
        {
            for(int i = 0; i < 3; i++)
            {
                float cutValue = pos[i] % step;
                pos[i] -= cutValue;

                if (Mathf.Abs(cutValue) >= step / 2f)
                    pos[i] += step * Mathf.Sign(cutValue);
            }

            return pos;
        }
    }
}