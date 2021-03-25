
using UnityEngine;

namespace Pong.Movement
{
    public interface IMoveHandler
    {
        void SetPosition(IMovable entity, Vector3 pos);
        void MovePosition(IMovable entity, Vector3 deltaPos);
    }
}